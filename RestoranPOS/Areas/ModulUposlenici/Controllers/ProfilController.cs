using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulUposlenici.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulUposlenici.Controllers
{
    [Autorizacija(KorisnickeUloge.Uposlenik)]
    public class ProfilController : Controller
    {
        private RestoranContext ctx = new RestoranContext();

        // Vraca ukupan broj novih(neprocitanih) obavijesti za menadzera
        public int GetAllObavijestiZaMenadzera(int korisnikId)
        {
            int menadzerObavijest = 0;
            foreach (var x in ctx.Obavijesti)
            {
                if (x.Korisnik.Uposlenik.Menadzer)
                    if (!x.Procitana)
                        menadzerObavijest++;
            }
            return menadzerObavijest;
        }

        // Vraca ukupan broj novih(neprocitanih) obavijesti za menadzera
        public int GetAllObavijestiZaUposlenika(int korisnikId)
        {
            int uposlenikObavijest = 0;
            foreach (var x in ctx.Obavijesti)
            {
                if (x.Korisnik.Uposlenik.Id == korisnikId && !x.Korisnik.Uposlenik.Admin && !x.Korisnik.Uposlenik.Menadzer)
                    if (!x.Procitana)
                        uposlenikObavijest++;
            }
            return uposlenikObavijest;
        }

        //GET : Prikazuje Pocetni sajt nakon logina (Pocetna stranica za Uposlenika)
        public ActionResult ProfilPocetna(int korisnikId)
        {
            Korisnik korsiKorisnik = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();
            TempData["korisnik"] = korsiKorisnik.Ime;
            TempData["korisnikId"] = korsiKorisnik.Id;

            //int menadzerObavijest = 0;
            //int uposlenikObavijest = 0;
            //foreach (var x in ctx.Obavijesti)
            //{
            //    if (x.Korisnik.Uposlenik.Menadzer)
            //        if (!x.Procitana)
            //            menadzerObavijest++;
            //    if (x.Korisnik.Uposlenik.Id == korisnikId && !x.Korisnik.Uposlenik.Admin && !x.Korisnik.Uposlenik.Menadzer)
            //        if (!x.Procitana)
            //            uposlenikObavijest++;
            //}
            //ViewBag.menadzerObavijest = menadzerObavijest;
            //ViewBag.uposlenikObavijest = uposlenikObavijest;
            //TempData["uposlenikObavijest"] = uposlenikObavijest;
            TempData["uposlenikObavijest"] = GetAllObavijestiZaUposlenika(korisnikId);
            TempData["menadzerObavijest"] = GetAllObavijestiZaMenadzera(korisnikId);
            return View();
        }

        
        // GET: ModulUposlenici/Profil/Prikazi
        public ActionResult Prikazi(int korisnikId)
        {
            Uposlenik uposlenik = ctx.Uposlenici.Include(u => u.Korisnik).Where(u => u.Korisnik.Id == korisnikId).FirstOrDefault();
            TempData["uposlenikObavijest"] = GetAllObavijestiZaUposlenika(korisnikId);
            TempData["menadzerObavijest"] = GetAllObavijestiZaMenadzera(korisnikId);
            return View(uposlenik);
        }

        // GET: ModulUposlenici/Profil/UrediProfil
        public ActionResult UrediProfil(int korisnikId)
        {
            TempData["uposlenikObavijest"] = GetAllObavijestiZaUposlenika(korisnikId);
            TempData["menadzerObavijest"] = GetAllObavijestiZaMenadzera(korisnikId);

            Uposlenik uposlenik =
                ctx.Uposlenici.Include(x => x.Korisnik).Where(x => x.Korisnik.Id == korisnikId).FirstOrDefault();

            var Model = new UrediProfiViewModel
            {
                    Ime =  uposlenik.Korisnik.Ime,
                    Prezime = uposlenik.Korisnik.Prezime,
                    Adresa = uposlenik.Korisnik.Adresa,
                    BrTelefona = uposlenik.Korisnik.BrTelefona,
                    EMail = uposlenik.Korisnik.EMail,
                    Jmbg = uposlenik.Jmbg,
                    BrZiroRacuna = uposlenik.BrZiroRacuna,
                    BrRadneKnjizice = uposlenik.BrRadneKnjizice,
                    Id = uposlenik.Id
                   
            };
            return View(Model);
        }

        // POST: ModulUposlenici/Profil/UrediProfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrediProfil(UrediProfiViewModel profil)
        {
            if (ModelState.IsValid)
            {
                if (profil.Id == 0)
                {
                    ViewBag.NijePronadjen = "Korisnik ne postoji.. Pokusajte ponovo!";
                    return View();
                }
                else
                {
                    Uposlenik uposlenik =
                        ctx.Uposlenici.Include(x => x.Korisnik)
                            .Where(x => x.Korisnik.Id == profil.Id)
                            .FirstOrDefault();

                    uposlenik.Korisnik.Ime = profil.Ime;
                    uposlenik.Korisnik.Prezime = profil.Prezime;
                    uposlenik.Korisnik.Adresa = profil.Adresa;
                    uposlenik.Korisnik.BrTelefona = profil.BrTelefona;
                    uposlenik.Korisnik.EMail = profil.EMail;
                    uposlenik.Jmbg = profil.Jmbg;
                    uposlenik.BrZiroRacuna = profil.BrZiroRacuna;
                    uposlenik.BrRadneKnjizice = profil.BrRadneKnjizice;

                    ctx.SaveChanges();

                    return RedirectToAction("Prikazi", new {korisnikId = profil.Id});
                }
            }
            return View();
        }

        // Admin poziva funkciju AdminUredi
        [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer)]
        public ActionResult AdminUredi(int korisnikId)
        {
            Uposlenik uposlenik =
                ctx.Uposlenici.Include(x => x.Korisnik).Where(x => x.Korisnik.Id == korisnikId).FirstOrDefault();

            var Model = new AdminUposleniciUrediViewModel
            {
                Ime = uposlenik.Korisnik.Ime,
                Prezime = uposlenik.Korisnik.Prezime,
                Adresa = uposlenik.Korisnik.Adresa,
                BrTelefona = uposlenik.Korisnik.BrTelefona,
                EMail = uposlenik.Korisnik.EMail,
                Jmbg = uposlenik.Jmbg,
                BrZiroRacuna = uposlenik.BrZiroRacuna,
                BrRadneKnjizice = uposlenik.BrRadneKnjizice,
                OpisRadnogMjesta = uposlenik.OpisRadnogMjesta,
                Plata = uposlenik.Plata,
                BrDanaGodisnjeg = uposlenik.BrDanaGodisnjeg,
                DatumZaposlenja = uposlenik.DatumZaposlenja,
                Menadzer = uposlenik.Menadzer,
                Admin = uposlenik.Admin,

                Id = uposlenik.Id

            };
            return View(Model);
        }
        [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminUredi(AdminUposleniciUrediViewModel profil, int korisnikId)
        {
            if (ModelState.IsValid)
            {
                if (korisnikId == 0)
                {
                    return View();
                }
                else
                {
                    Uposlenik uposlenik =
                        ctx.Uposlenici.Include(x => x.Korisnik)
                            .Where(x => x.Korisnik.Id == profil.Id)
                            .FirstOrDefault();

                    uposlenik.Korisnik.Ime = profil.Ime;
                    uposlenik.Korisnik.Prezime = profil.Prezime;
                    uposlenik.Korisnik.Adresa = profil.Adresa;
                    uposlenik.Korisnik.BrTelefona = profil.BrTelefona;
                    uposlenik.Korisnik.EMail = profil.EMail;
                    uposlenik.Jmbg = profil.Jmbg;
                    uposlenik.BrZiroRacuna = profil.BrZiroRacuna;
                    uposlenik.BrRadneKnjizice = profil.BrRadneKnjizice;
                    uposlenik.OpisRadnogMjesta = profil.OpisRadnogMjesta;
                    uposlenik.Plata = profil.Plata;
                    uposlenik.BrDanaGodisnjeg = profil.BrDanaGodisnjeg;
                    uposlenik.DatumZaposlenja = profil.DatumZaposlenja;
                    uposlenik.Menadzer = profil.Menadzer;
                    uposlenik.Admin = profil.Admin;

                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index","Korisnik", new { area = "ModulAdmin" });
        }
        // GET
        // Mogucnost mjenjanja trenutne lozinke..
        public ActionResult PromjeniLozinku()
        {
            return View();
        }

        // POST Metoda
        // Mjenja lozinku, u slucaju da je unesena pogresna "stara" lozinka, ponovo ucitava formu
        // te ispisuje određeno upozorenje.. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PromjeniLozinku(UposleniciPromjeniLozinkuViewModel korisnik, int korisnikId)
        {
            if (korisnikId == 0)
                return View("UrediProfil", korisnik);
            else
            {
                Uposlenik UKorisnik = ctx.Uposlenici.Where(x => x.Id == korisnikId)
                    .Include(x => x.Korisnik).FirstOrDefault();

                if (korisnik.Password != UKorisnik.Korisnik.Password)
                {
                    ViewBag.Greska = "Pogrijesili ste staru lozinku, pokusajte ponovo!";
                    return View("PromjeniLozinku", korisnik);
                }
                else
                {
                    UKorisnik.Korisnik.Password = korisnik.NovaLozinka;
                    ctx.SaveChanges();
                    ViewBag.Poruka = "Uspješno ste promjenili Lozinku!";
                    return RedirectToAction("Prikazi", new { korisnikId = UKorisnik.Id });
                }

            }
            return View();
        }
    }
}
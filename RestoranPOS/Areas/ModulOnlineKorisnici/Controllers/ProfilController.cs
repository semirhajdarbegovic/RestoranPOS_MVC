using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.DAL;
using System.Data.Entity;
using RestoranPOS.Areas.ModulOnlineKorisnici.Models;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulOnlineKorisnici.Controllers
{
    [Autorizacija(KorisnickeUloge.OnlineKorisnik)]
    public class ProfilController : Controller
    {
        private RestoranContext ctx = new RestoranContext();

        public ActionResult Profil()
        {
            return View();
        }

        // GET: ModulOnlineKorisnici/Profil
        //Sad za sad radi kako radi, doraditi kada se ubace svi podaci, te onda testirati 
        // Ubaciti [Authorized] za autorizaciju i pristup samo logiranim korisnicima svom profilu.. 
        // Takodjer doraditi u slucaju da ne nadje trazenog usera, izbaciti neku gresku.. u slucaju da je to moguce :S .. 

        // Ispisuje Podatke vezane za logiranog korisnika, uvid u dosadasnje narudzbe 
        // te uvid u rezervacije, barem bi tako trebalo da radi... 
        // Ispisuje sve na jednoj stranici, kasnije razdvojiti na 3 stranice.. 

        public ActionResult Prikazi(int? korisnikId)
        {

            if (korisnikId.HasValue)
            {

                var korisnik = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();

                var stavkaNarudzbe = ctx.StavkeNarudzbe.Where(x => x.Narudzba.KorisnikId == korisnikId);
                var rezervacije = ctx.Rezervacije.Where(x => x.KorisnikId == korisnikId);

                var Model = new PrikaziViewModel
                {
                    Id = korisnik.Id,
                    Ime = korisnik.Ime,
                    Prezime = korisnik.Prezime,
                    Username = korisnik.Username,
                    Adresa = korisnik.Adresa,
                    BrTelefona = korisnik.BrTelefona,
                    EMail = korisnik.EMail,
                    StavkeNarudzbi = stavkaNarudzbe.ToList(),
                    ListaRezervacija = rezervacije.ToList()
                };

                return View(Model);
            }
            // Ibacuje gresku, jer nije napravljen i jedino se poziva u slucaju da nije dobio Id.. tako da
            // popraviti kasnije.. 
            return View("Error");
        }

        // Ispisuje trenutne korisnicke postavke, te korisnik ima opciju da izmjeni zeljene podatke
        public ActionResult Uredi(int korisnikId)
        {
            var korisnik =
                ctx.OnlineKorisnici.Include(x => x.Korisnik).Where(x => x.Korisnik.Id == korisnikId).FirstOrDefault();
            var Model = new UrediViewModel
            {
                Ime = korisnik.Korisnik.Ime,
                Prezime = korisnik.Korisnik.Prezime,
                Adresa = korisnik.Korisnik.Adresa,
                BrTelefona = korisnik.Korisnik.BrTelefona,
                EMail = korisnik.Korisnik.EMail,
                Username = korisnik.Korisnik.Username,
                Id = korisnik.Id
            };
            return View(Model);
        }

        //  [HttpPost]
        //   [ValidateAntiForgeryToken]
        public ActionResult Snimi(UrediViewModel korisnik)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("Uredi",korisnik);
            //}
            if (korisnik.Id == 0)
                return View("Uredi", korisnik);
            else
            {
                OnlineKorisnik OlKorisnik = ctx.OnlineKorisnici.Where(x => x.Id == korisnik.Id)
                    .Include(x => x.Korisnik).FirstOrDefault();

                OlKorisnik.Korisnik.Ime = korisnik.Ime;
                OlKorisnik.Korisnik.Prezime = korisnik.Prezime;
                OlKorisnik.Korisnik.Adresa = korisnik.Adresa;
                OlKorisnik.Korisnik.BrTelefona = korisnik.BrTelefona;
                OlKorisnik.Korisnik.EMail = korisnik.EMail;
                OlKorisnik.Korisnik.Password = korisnik.Password;
                OlKorisnik.Korisnik.Username = korisnik.Username;

                ctx.SaveChanges();
            }
            return RedirectToAction("Prikazi", new {korisnikId = korisnik.Id});
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
        public ActionResult PromjeniLozinku(UrediViewModel korisnik, int korisnikId)
        {
            if (korisnikId == 0)
                return View("Uredi", korisnik);
            else
            {
                OnlineKorisnik OlKorisnik = ctx.OnlineKorisnici.Where(x => x.Id == korisnikId)
                    .Include(x => x.Korisnik).FirstOrDefault();

                if (korisnik.Password != OlKorisnik.Korisnik.Password)
                {
                    ViewBag.Greska = "Pogrijesili ste staru lozinku, pokusajte ponovo!";
                    return View("PromjeniLozinku",korisnik);
                }
                else
                {
                    OlKorisnik.Korisnik.Password = korisnik.NovaLozinka;
                    ctx.SaveChanges();
                    ViewBag.Poruka = "Uspješno ste promjenili Lozinku!";
                    return RedirectToAction("Prikazi", new {korisnikId = OlKorisnik.Id});
                }
                
            }
            return View();
        }

       
    }
}
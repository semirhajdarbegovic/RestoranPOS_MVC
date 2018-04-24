using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulAdmin.Models;
using RestoranPOS.Areas.ModulRegistracija.Models;
using RestoranPOS.Areas.ModulUposlenici.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulAdmin.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class KorisnikController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        // GET: ModulAdmin/Korisnik
        public ActionResult Index(int? tipKorisnika)
        {
            KorisnikViewModel Model = new KorisnikViewModel();
            List<KorisnikViewModel.KorisnikInfo> ListaKorisnika = new List<KorisnikViewModel.KorisnikInfo>();
            if ((tipKorisnika.HasValue && tipKorisnika == 1) || !tipKorisnika.HasValue)
            {
                Model.Korisnici = (ctx.Korisnici
                    .Where(x => !x.IsDeleted && x.NalogAktivan && x.Uposlenik == null)
                    .Include(x => x.OnlineKorisnik)
                    .Where(x => x.OnlineKorisnik.Id != null && !x.OnlineKorisnik.IsDeleted)
                    .Select(x => new KorisnikViewModel.KorisnikInfo
                    {
                        Id = x.Id,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Username = x.Username,
                        Adresa = x.Adresa,
                        BrTelefona = x.BrTelefona,
                        NalogAktivan = x.NalogAktivan,
                        EMail = x.EMail,
                        Uposlen = false
                    })).ToList();
                ListaKorisnika.AddRange(Model.Korisnici);
            }
            if ((tipKorisnika.HasValue && tipKorisnika == 2) || !tipKorisnika.HasValue)
            {
                Model.Korisnici = (ctx.Korisnici
                    .Where(x => !x.IsDeleted && x.NalogAktivan)
                    .Include(x => x.Uposlenik)
                    .Where(x => x.Uposlenik.Id != null && !x.Uposlenik.IsDeleted)
                    .Select(x => new KorisnikViewModel.KorisnikInfo
                    {
                        Id = x.Id,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Username = x.Username,
                        Adresa = x.Adresa,
                        BrTelefona = x.BrTelefona,
                        NalogAktivan = x.NalogAktivan,
                        EMail = x.EMail,
                        Uposlen = true
                    })).ToList();
                ListaKorisnika.AddRange(Model.Korisnici);
            }
            Model.Korisnici = ListaKorisnika;
            return View("Index", Model);
        }

        public ActionResult Otpusti(int korisnikId)
        {
            Uposlenik uposlenik = ctx.Uposlenici.Where(x => x.Id == korisnikId).FirstOrDefault();
            ctx.Uposlenici.Remove(uposlenik);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UposliPrazan(int korisnikId)
        {
            Uposlenik uposlenik = new Uposlenik();
            uposlenik.Korisnik = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();
            uposlenik.IsDeleted = false;
            uposlenik.DatumZaposlenja = DateTime.Now;
            ctx.Uposlenici.Add(uposlenik);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UposliPopunjen(int korisnikId)
        {
            Uposlenik uposlenik = new Uposlenik();
            uposlenik.Korisnik = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();
            uposlenik.IsDeleted = false;
            uposlenik.DatumZaposlenja = DateTime.Now;
            ctx.Uposlenici.Add(uposlenik);
            ctx.SaveChanges();
            return RedirectToAction("AdminUredi", "Profil", new { area = "ModulUposlenici", korisnikId });
        }

        public ActionResult DodajUposlenika()
        {
            Uposlenik uposlenik = new Uposlenik();
            Korisnik korisnik = new Korisnik();
            OnlineKorisnik onlineKorisnik = new OnlineKorisnik();
            korisnik.Restoran = ctx.Restorani.First();
            korisnik.NalogAktivan = true;            

            onlineKorisnik.Korisnik = korisnik;
            onlineKorisnik.DatumRegistracije = DateTime.Now;

            uposlenik.Korisnik = korisnik;
            uposlenik.IsDeleted = false;
            uposlenik.DatumZaposlenja = DateTime.Now;
            ctx.Korisnici.Add(korisnik);
            ctx.OnlineKorisnici.Add(onlineKorisnik);
            ctx.Uposlenici.Add(uposlenik);
            ctx.SaveChanges();
            return RedirectToAction("AdminUredi", "Profil", new { area = "ModulUposlenici", korisnikId = korisnik.Id });
        }

        public ActionResult DodajOnlineKorisnika()
        {
            return View();
        }

        // POST : ModulRegistracija/Registracija
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DodajOnlineKorisnika(RegistracijaViewModel korisnik)
        {
            if (ModelState.IsValid)
            {
                // provjerava da li postoji korisnik sa unesenim username ili email-adresom i ako postoji
                // upozorava korisnika da ne moze unjeti isti username ili password..
                bool nastavi = true;
                foreach (var x in ctx.Korisnici)
                {
                    if (korisnik.EMail == x.EMail)
                    {
                        ViewBag.EmailPostoji = "Email adresa je vec postoji u bazi podataka!";
                        nastavi = false;
                    }
                    if (korisnik.Username == x.Username)
                    {
                        ViewBag.UsernamePostoji = "Username vec postoji u bazi podataka!";
                    }
                }
                if (!nastavi)
                    return View();

                // Kreiramo novi objekat tipa Korisnik i OnlineKorisnik i unjeg pohranjujemo podatke koje smo preuzeli 
                // sa web forme..
                OnlineKorisnik OlKorisnik = new OnlineKorisnik();
                OlKorisnik.Korisnik = new Korisnik();

                OlKorisnik.PotvrdjenaAdresa = true;
                OlKorisnik.DatumRegistracije = DateTime.Now;
                OlKorisnik.Korisnik.Adresa = korisnik.Adresa;
                OlKorisnik.Korisnik.BrTelefona = korisnik.BrTelefona;
                OlKorisnik.Korisnik.EMail = korisnik.EMail;
                OlKorisnik.Korisnik.Ime = korisnik.Ime;
                OlKorisnik.Korisnik.NalogAktivan = true;
                OlKorisnik.Korisnik.Password = korisnik.Password;
                OlKorisnik.Korisnik.Prezime = korisnik.Prezime;
                OlKorisnik.Korisnik.Username = korisnik.Username;
                OlKorisnik.Korisnik.RestoranId = ctx.Restorani.First().Id;

                ctx.OnlineKorisnici.Add(OlKorisnik);
                ctx.SaveChanges();

            }
            return RedirectToAction("Index");
        }


        public ActionResult Izbrisi(int korisnikId)
        {
            Korisnik korisnik = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();
            if (korisnik.Uposlenik != null)
                korisnik.Uposlenik.IsDeleted = true;
            korisnik.OnlineKorisnik.IsDeleted = true;
            korisnik.IsDeleted = true;
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Aktiviraj(int korisnikId)
        {
            Korisnik korisnik = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();
            if (korisnik.Uposlenik != null)
                korisnik.Uposlenik.IsDeleted = false;
            korisnik.OnlineKorisnik.IsDeleted = false;
            korisnik.IsDeleted = false;
            ctx.SaveChanges();
            return RedirectToAction("PrikaziIzbrisane");
        }

        public ActionResult PrikaziIzbrisane(int? tipKorisnika)
        {
            KorisnikViewModel Model = new KorisnikViewModel();
            List<KorisnikViewModel.KorisnikInfo> ListaKorisnika = new List<KorisnikViewModel.KorisnikInfo>();
            if ((tipKorisnika.HasValue && tipKorisnika == 1) || !tipKorisnika.HasValue)
            {
                Model.Korisnici = (ctx.Korisnici
                    .Where(x => x.IsDeleted && x.NalogAktivan && x.Uposlenik == null)
                    .Include(x => x.OnlineKorisnik)
                    .Where(x => x.OnlineKorisnik.Id != null && x.OnlineKorisnik.IsDeleted)
                    .Select(x => new KorisnikViewModel.KorisnikInfo
                    {
                        Id = x.Id,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Username = x.Username,
                        Adresa = x.Adresa,
                        BrTelefona = x.BrTelefona,
                        NalogAktivan = x.NalogAktivan,
                        EMail = x.EMail,
                        Uposlen = false
                    })).ToList();
                ListaKorisnika.AddRange(Model.Korisnici);
            }
            if ((tipKorisnika.HasValue && tipKorisnika == 2) || !tipKorisnika.HasValue)
            {
                Model.Korisnici = (ctx.Korisnici
                    .Where(x => x.IsDeleted && x.NalogAktivan)
                    .Include(x => x.Uposlenik)
                    .Where(x => x.Uposlenik.Id != null && x.Uposlenik.IsDeleted)
                    .Select(x => new KorisnikViewModel.KorisnikInfo
                    {
                        Id = x.Id,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Username = x.Username,
                        Adresa = x.Adresa,
                        BrTelefona = x.BrTelefona,
                        NalogAktivan = x.NalogAktivan,
                        EMail = x.EMail,
                        Uposlen = true
                    })).ToList();
                ListaKorisnika.AddRange(Model.Korisnici);
            }
            Model.Korisnici = ListaKorisnika;
            return View("PrikazIzbrisanih", Model);
        }
    }
}
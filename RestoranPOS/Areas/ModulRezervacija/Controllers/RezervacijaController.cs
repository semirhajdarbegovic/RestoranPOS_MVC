using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulRezervacija.Models;
using RestoranPOS.Areas.ModulStol.Controllers;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulRezervacija.Controllers
{
    public class RezervacijaController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        // GET: ModulRezervacija/Rezervacija
        public ActionResult Index(int? stolId, DateTime? periodOd, int? brDana)
        {
            RezervacijaViewModel Model = new RezervacijaViewModel();
            if (!periodOd.HasValue)
            {
                if (stolId.HasValue)
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => !x.IsDeleted && x.StolId == stolId && x.Datum >= DateTime.Now)
                        .OrderBy(x => x.Odobrena)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            IsDeleted = x.IsDeleted,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                    Model.StolId = (int) stolId;
                }
                else
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => !x.IsDeleted && x.Datum >= DateTime.Now)
                        .OrderBy(x => x.Odobrena)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            IsDeleted = x.IsDeleted,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                }
                Model.PeriodOd = DateTime.Now;
            }
            else
            {
                   DateTime endDate = periodOd.Value;
                /*endDate = endDate.AddDays(brDana.Value);*/
                if (stolId != 0)
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => !x.IsDeleted && x.StolId == stolId && x.Datum == periodOd)
                        .OrderBy(x => x.Odobrena)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            IsDeleted = x.IsDeleted,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                    Model.StolId = (int)stolId;
                }
                else
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => !x.IsDeleted && x.Datum == periodOd)
                        .OrderBy(x => x.Odobrena)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            IsDeleted = x.IsDeleted,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                }
                Model.PeriodOd = (DateTime) periodOd;
            }
            return View(Model);
        }

        public ActionResult PrikaziProsle(int? stolId, DateTime? periodOd, int? brDana)
        {
            RezervacijaViewModel Model = new RezervacijaViewModel();
            if (!periodOd.HasValue)
            {
                if (stolId.HasValue)
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => !x.IsDeleted && x.StolId == stolId && x.Datum < DateTime.Now)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                    Model.StolId = (int)stolId;
                }
                else
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => !x.IsDeleted && x.Datum < DateTime.Now)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                }
                Model.PeriodOd = DateTime.Now;
            }
            else
            {
                DateTime endDate = periodOd.Value;
                endDate = endDate.AddDays(brDana.Value);
                if (stolId != 0)
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => !x.IsDeleted && x.StolId == stolId && x.Datum >= periodOd && x.Datum <= endDate && x.Datum < DateTime.Now)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                    Model.StolId = (int)stolId;
                }
                else
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => !x.IsDeleted && x.Datum >= periodOd && x.Datum <= endDate && x.Datum < DateTime.Now)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                }
                Model.PeriodOd = (DateTime)periodOd;
            }
            return View(Model);
        }

        public ActionResult PrikaziIzbrisane(int? stolId, DateTime? periodOd, int? brDana)
        {
            RezervacijaViewModel Model = new RezervacijaViewModel();
            if (!periodOd.HasValue)
            {
                if (stolId.HasValue)
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => x.IsDeleted && x.StolId == stolId)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                    Model.StolId = (int)stolId;
                }
                else
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => x.IsDeleted)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                }
                Model.PeriodOd = DateTime.Now;
            }
            else
            {
                DateTime endDate = periodOd.Value;
                endDate = endDate.AddDays(brDana.Value);
                if (stolId != 0)
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => x.IsDeleted && x.StolId == stolId && x.Datum >= periodOd && x.Datum <= endDate)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                    Model.StolId = (int)stolId;
                }
                else
                {
                    Model.Rezervacije = (ctx.Rezervacije
                        .Where(x => x.IsDeleted && x.Datum >= periodOd && x.Datum <= endDate)
                        .Include(x => x.Korisnik)
                        .Include(x => x.Stol)
                        .Select(x => new RezervacijaViewModel.RezervacijaInfo
                        {
                            Id = x.Id,
                            KorisnikId = x.KorisnikId,
                            KorisnikUsername = x.Korisnik.Username,
                            ImeRezervacije = x.ImeRezervacije,
                            BrojStola = x.Stol.BrojStola,
                            DatumRezervacije = x.Datum,
                            VrijemeRezervacije = x.Vrijeme,
                            BrojOsoba = x.BrOsoba,
                            Odobrena = x.Odobrena
                        })).ToList();
                }
                Model.PeriodOd = (DateTime)periodOd;
            }
            return View(Model);
        }
        public ActionResult Uredi(int rezervacijaId)
        {
            Korisnik logiraniKorisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            Rezervacija rezervacija = ctx.Rezervacije
                .Where(x => x.Id == rezervacijaId)
                .Include(x => x.Korisnik)
                .Include(x => x.Stol)
                .FirstOrDefault();
            RezervacijaUrediViewModel model = new RezervacijaUrediViewModel
            {
                Id = rezervacija.Id,
                KorisnikId = logiraniKorisnik.Id,
                KorisnikUsername = logiraniKorisnik.Username,
                ImeRezervacije = rezervacija.ImeRezervacije,
                BrojOsoba = rezervacija.BrOsoba,
                BrojStola = rezervacija.Stol.BrojStola,
                DatumRezervacije = rezervacija.Datum,
                VrijemeRezervacije = rezervacija.Vrijeme,
                Odobrena = rezervacija.Odobrena
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Uredi(RezervacijaUrediViewModel model)
            {
            if (ModelState.IsValid)
            {
                Rezervacija rezervacija;
                if (model.Id == 0)
                {
                    rezervacija = new Rezervacija();
                    ctx.Rezervacije.Add(rezervacija);
                }
                else
                {
                    rezervacija = ctx.Rezervacije
                            .Where(x => x.Id == model.Id)
                            .Include(x => x.Korisnik)
                            .Include(x => x.Stol)
                            .FirstOrDefault();
                }
                    Korisnik logiraniKorisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
                    rezervacija.Korisnik = ctx.Korisnici.Where(x => x.Id == logiraniKorisnik.Id).FirstOrDefault();
                    rezervacija.Stol = ctx.Stolovi.Where(x => x.BrojStola == model.BrojStola).FirstOrDefault();
                    rezervacija.ImeRezervacije = model.ImeRezervacije;
                    rezervacija.BrOsoba = model.BrojOsoba;
                    rezervacija.Datum = model.DatumRezervacije;
                    rezervacija.Vrijeme = model.VrijemeRezervacije;
                    rezervacija.Odobrena = model.Odobrena;

                    ctx.SaveChanges();

                if (logiraniKorisnik.Uposlenik != null)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Prikazi", "Profil",  new { Area= "ModulOnlineKorisnici", korisnikId = logiraniKorisnik.Id });
            }
            return View();
            }
        public ActionResult Dodaj()
        {
            int korisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Id;
            RezervacijaUrediViewModel Model = new RezervacijaUrediViewModel();
            Model.KorisnikId = korisnikId;
            Model.Odobrena = true;
            Model.DatumRezervacije = DateTime.Now;
            Model.VrijemeRezervacije = DateTime.Now;
            return View("Uredi", Model);
        }

        public ActionResult Kreiraj()
        {
            Stol junkTable = new Stol();
            if (ctx.Stolovi.Where(x => x.BrojStola == 2147483647).FirstOrDefault() == null)
            {
                junkTable.Id = 2147483647;
                junkTable.IsDeleted = true;
                junkTable.BrojStola = 2147483647;
                junkTable.Zauzet = false;
                ctx.Stolovi.Add(junkTable);
                ctx.SaveChanges();
            }
            else
            {
                junkTable = ctx.Stolovi.Where(x => x.BrojStola == 2147483647).FirstOrDefault();
            }
            int korisnikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Id;
            Korisnik kreator = ctx.Korisnici.Where(x => x.Id == korisnikId).FirstOrDefault();
            RezervacijaUrediViewModel Model = new RezervacijaUrediViewModel();
            Model.KorisnikId = korisnikId;
            Model.BrojStola = junkTable.BrojStola;
            Model.ImeRezervacije = kreator.Ime + " " + kreator.Prezime;
            Model.Odobrena = false;
            Model.DatumRezervacije = DateTime.Now;
            Model.VrijemeRezervacije = DateTime.Now;
            return View("Kreiraj", Model);
        }


        public ActionResult PonistiOdobrenje(int rezervacijaId)
        {
            Rezervacija rezervacija = ctx.Rezervacije.Where(x => x.Id == rezervacijaId).FirstOrDefault();
            rezervacija.Odobrena = false;
            ctx.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult Izbrisi(int rezervacijaId)
        {
            Rezervacija rezervacija = ctx.Rezervacije.Where(x => x.Id == rezervacijaId).FirstOrDefault();
            rezervacija.IsDeleted = true;
            ctx.SaveChanges();

            return RedirectToAction("PrikaziProsle");
        }
        public ActionResult Vrati(int rezervacijaId)
        {
            Rezervacija rezervacija = ctx.Rezervacije.Where(x => x.Id == rezervacijaId).FirstOrDefault();
            rezervacija.IsDeleted = false;
            ctx.SaveChanges();

            return RedirectToAction("PrikaziIzbrisane");
        }
        public ActionResult StolSelect(int rezervacijaId)
        {
            RezervacijaViewModel Model = new RezervacijaViewModel();
            StolController c = new StolController();
            Model.rezervacija = ctx.Rezervacije.Where(x => x.Id == rezervacijaId).FirstOrDefault();
            DateTime preCheckH = Model.rezervacija.Vrijeme.AddHours(-1);
            DateTime postCheckH = Model.rezervacija.Vrijeme.AddHours(1);
            List<Rezervacija> listaRezervacija = ctx.Rezervacije.Where(x => x.Datum == Model.rezervacija.Datum &&
                                                                            x.Vrijeme >= preCheckH &&
                                                                            x.Vrijeme <= postCheckH &&
                                                                            x.Odobrena)
                                                                            .ToList();
            Model.listaStolova = c.StoloviLista();
            foreach (Rezervacija r in listaRezervacija)
            {
                Model.listaStolova.Where(s => s.Id == r.StolId).FirstOrDefault().Zauzet = true;

            }
            return PartialView("StolSelect", Model);
        }

        public ActionResult SaveStol(int stolId, int rezervacijaId)
        {
            Rezervacija rezervacija = ctx.Rezervacije.Where(x => x.Id == rezervacijaId).FirstOrDefault();
            rezervacija.Stol = ctx.Stolovi.Where(s => s.Id == stolId).FirstOrDefault();
            rezervacija.Odobrena = true;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
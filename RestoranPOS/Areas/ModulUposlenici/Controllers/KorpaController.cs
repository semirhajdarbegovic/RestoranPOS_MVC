﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulOnlineKorisnici.Models;
using RestoranPOS.Areas.ModulUposlenici.Models;
using RestoranPOS.DAL;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulUposlenici.Controllers
{
    public class KorpaController : Controller
    {
        private RestoranContext ctx = new RestoranContext();
        // GET: ModulOnlineKorisnici/Korpa
        public ActionResult KorpaPrikazi(int? stolId, int? korisnikId)
        {
            /*   NarudzbaViewModel Model = new NarudzbaViewModel();

            Model.Narudzbe = (ctx.Narudzbe
                .Where(x => (!proizvodId.HasValue && x.IsDeleted == false))
                .Include(x=> x.Korisnik)
                .Include(x => x.Racun)
                .Select(x => new NarudzbaViewModel.NarudzbaInfo
                {
                    Id = x.Id,
                    DatumNarudzbe = x.DatumNarudzbe,
                    VrijemeNarudzbe = x.VrijemeNarudzbe,
                    KorisnikId = x.KorisnikId,
                    KorisnikUsername = x.Korisnik.Username,
                    OnlineNarudzba = x.OnlineNarudzba,
                    UkupnaCijena = x.UkupnaCijena,
                    Prihvacena = x.Prihvacena,
                    RacunId = x.RacunId,
                    ListaStavki = ctx.StavkeNarudzbe.Where(s => s.NarudzbaId == x.Id).ToList()
                })).ToList();

            if(proizvodId.HasValue)
                return PartialView("KorpaPrikaziPartial", Model);
            return View("KorpaPrikazi", Model);*/

            //  KorpaViewModel Model = new KorpaViewModel();

            // Izlistava sve proizvode koji se nalaze u korpi
            if (UposlenikStolNarudzba.UposlenikKorpaViewModels != null)
            {
                List<UposlenikKorpaViewModel> korpa = new List<UposlenikKorpaViewModel>();
                foreach (var x in UposlenikStolNarudzba.UposlenikKorpaViewModels)
                {
                    if (x.Korpa.StolID == stolId && x.Korpa.KorisnikId == korisnikId)
                        korpa.Add(x);
                }

                double ukupnaCijena = 0;
                for (int i = 0; i < korpa.Count; i++)
                {
                    ukupnaCijena += korpa[i].Korpa.UkupnaCijenaProizvoda;
                }
                ViewData["Ukupno"] = ukupnaCijena;
                return View("KorpaPrikazi", korpa);

            }
            else
                return View("KorpaPrikazi");

        }

        // Zavrsava narudzbu, tako sto dodaje svaku narudzbu na stavku racuna, ali trenutno ne radi
        // razmisliti o drugom pristupu ovom problemu
        public ActionResult ZakljuciNarudzbu(int? stolId, int? korisnikId)
        {
            if (UposlenikStolNarudzba.UposlenikKorpaViewModels != null)
            {
                List<UposlenikKorpaViewModel> korpa = UposlenikStolNarudzba.UposlenikKorpaViewModels.Where(x => x.Korpa.StolID == stolId.Value && x.Korpa.KorisnikId == korisnikId.Value).ToList();

                double ukupnaCijena = 0;
                for (int i = 0; i < korpa.Count; i++)
                {
                    ukupnaCijena += korpa[i].Korpa.UkupnaCijenaProizvoda;
                }

                Narudzba narudzbe = new Narudzba();

               Racun racun = new Racun();

                racun.DatumRacuna = DateTime.Now.Date;
                racun.VrijemeRacuna = DateTime.Now;
                racun.Popust = 0;
                racun.StolId = stolId.Value;
                racun.Ukupno = float.Parse(ukupnaCijena.ToString());


                narudzbe.KorisnikId = korpa[1].Korpa.KorisnikId;
                narudzbe.IsDeleted = false;
                narudzbe.OnlineNarudzba = true;
                narudzbe.Prihvacena = false;
                narudzbe.VrijemeNarudzbe = korpa[1].Korpa.VrijemeNarudzbe;
                narudzbe.DatumNarudzbe = korpa[1].Korpa.DatumNarudzbe;
                narudzbe.RacunId = racun.Id;

                ctx.Racuni.Add(racun);
                ctx.SaveChanges();

                narudzbe.RacunId = racun.Id;


                ctx.Narudzbe.Add(narudzbe);

                for (int i = 0; i < korpa.Count; i++)
                {

                    StavkaNarudzbe stavke = new StavkaNarudzbe();

                    stavke.IsDeleted = false;
                    stavke.Kolicina = korpa[i].Korpa.Kolicina;
                    stavke.ProizvodId = korpa[i].Korpa.ProizvodId;
                    stavke.NarudzbaId = narudzbe.Id;


                    ctx.StavkeNarudzbe.Add(stavke);
                    ctx.SaveChanges();
                }
                
                foreach (var x in korpa)
                {
                    UposlenikStolNarudzba.UposlenikKorpaViewModels.Remove(x);
                }
                ViewData["uspjesnaKupovina"] = "Uspjesno ste zavrsili kupovinu. Hvala Vam!";

                if (UposleniciZauzetiStolViewModel.stolId != null)
                    while (UposleniciZauzetiStolViewModel.stolId.Contains("stolID-" + stolId.Value))
                    {
                        UposleniciZauzetiStolViewModel.stolId.Remove("stolID-" + stolId.Value);
                    }

            }

            return View("KorpaZavrsi");
        }
    }
}
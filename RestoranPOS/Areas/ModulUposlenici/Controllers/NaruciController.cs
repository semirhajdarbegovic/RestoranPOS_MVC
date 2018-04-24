using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulUposlenici.Models;
using RestoranPOS.DAL;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulUposlenici.Controllers
{
    public class NaruciController : Controller
    {
        private RestoranContext ctx = new RestoranContext();
        private double ukupnaCijena;
       
        // Info malo kasnije
        public ActionResult PrikaziPonudu(int? kategorijaId, int? proizvodId, int? korisnikId, int? deleteId, int? stolID)
        {

            UposleniciPonudaViewModel Model = new UposleniciPonudaViewModel();
         /*   if (stolID != null)
            {*/

                

              /*  Model.Stolovi = ctx.Stolovi
                .Where(x => x.Id == stolID && !x.IsDeleted)
                .Select(x => new UposleniciPonudaViewModel.StolInfo()
                {
                    Id = x.Id,
                    BrojStola = x.BrojStola,
                    Zauzet = x.Zauzet
                }).ToList();*/

            if (stolID != null){
                ViewBag.stolId = stolID;
                ViewBag.brojStola = ctx.Stolovi.Where(x=>x.Id == stolID.Value).Select(x=>x.BrojStola).FirstOrDefault();
            }
         //   else
         //   {
                Model.Stolovi = ctx.Stolovi
                .Where(x => x.Zauzet == false && !x.IsDeleted)
                .Select(x => new UposleniciPonudaViewModel.StolInfo()
                {
                    Id = x.Id,
                    BrojStola = x.BrojStola,
                    Zauzet = x.Zauzet
                }).ToList();
           // }
            

            // Prikazuje aktivne ponude 
            if (stolID != null)
            {
                Model.Ponude = ctx.Ponude
                .Where(x => x.Aktivna && !x.IsDeleted)
                .Select(x => new UposleniciPonudaViewModel.PonudaInfo()
                {
                    Id = x.Id,
                    NazivPonude = x.NazivPonude,
                    VaziOd = x.VaziOd,
                    VaziDo = x.VaziDo,
                    Aktivna = x.Aktivna,
                    restoranId = x.RestoranId
                }).ToList();
            }
            else 
                Model.Ponude = new List<UposleniciPonudaViewModel.PonudaInfo>();
            

            // Po defaultu, ako kategorijaId nije prosljedjen, izlistava sve proizvode, u suprotnom, samo one koji pripadaju prosljedjenoj kategoriji
            if (kategorijaId != null && stolID != null)
            {
                ViewBag.katId = kategorijaId;
                Model.Proizvodi = ctx.Proizvodi
                    .Where(x => !x.IsDeleted && x.KategorijaId == kategorijaId)
                    .Select(x => new UposleniciPonudaViewModel.ProizvodInfo()
                    {
                        Id = x.Id,
                        SifraProizvoda = x.SifraProizvoda,
                        NazivProizvoda = x.Naziv,
                        Kolicina = x.Kolicina,
                        Cijena = x.Cijena,
                        OpisProizvoda = x.Opis,
                        KategorijaID = x.KategorijaId,
                        Slika = x.SlikaProizvoda
                    }).ToList();
            }
            else if (stolID != null)
            {
                Model.Proizvodi = ctx.Proizvodi
                    .Where(x => !x.IsDeleted)
                    .Select(x => new UposleniciPonudaViewModel.ProizvodInfo()
                    {
                        Id = x.Id,
                        SifraProizvoda = x.SifraProizvoda,
                        NazivProizvoda = x.Naziv,
                        Kolicina = x.Kolicina,
                        Cijena = x.Cijena,
                        OpisProizvoda = x.Opis,
                        KategorijaID = x.KategorijaId,
                        Slika = x.SlikaProizvoda
                    }).ToList();
            }
            else
                Model.Proizvodi = new List<UposleniciPonudaViewModel.ProizvodInfo>();


            // Sluzi da izlista kategorije pomocu kojih mozemo filtrirati proizvode
            if (stolID != null)
            {
                Model.Kategorije = ctx.Kategorije
                .Where(x => x.DostupnaOnline && !x.IsDeleted)
                .Select(x => new UposleniciPonudaViewModel.KategorijaInfo()
                {
                    Id = x.Id,
                    NazivKategorije = x.NazivKategorije,
                    DosupnaOnline = x.DostupnaOnline,
                    PonudaId = x.PonudaId
                }).ToList();
            }
            else 
                Model.Kategorije = new List<UposleniciPonudaViewModel.KategorijaInfo>();


            // Uklanja proizvod iz korpe, kada korisnik klikne na button ukloni, onda se proslijedi deleteId = 1, te se aktivira ovaj dio
            if (deleteId.HasValue && deleteId.Value == 1 && stolID != null)
            {
                List<UposlenikKorpaViewModel> korpa = UposlenikStolNarudzba.UposlenikKorpaViewModels;
                if (korpa.Exists(x => x.Korpa.ProizvodId == proizvodId.Value))
                {
                    var item = korpa.Single(x => x.Korpa.ProizvodId == proizvodId && x.Korpa.StolID == stolID);
                    korpa.Remove(item);
                    korpa.RemoveAll(x => x == null); // uklanja sve null objekte
                }
                if(!korpa.Exists(x=>x.Korpa.StolID == stolID))
                    if (UposleniciZauzetiStolViewModel.stolId != null)
                        while (UposleniciZauzetiStolViewModel.stolId.Contains("stolID-" + stolID))
                        {
                            UposleniciZauzetiStolViewModel.stolId.Remove("stolID-" + stolID);
                        }
                
            }

            // Dodaje odgovarajuci proizvod u korpu
            if (proizvodId != null && deleteId == null && stolID != null)
            {
                Model.Korpa = ctx.Proizvodi
                         .Where(x => x.Id == proizvodId)
                         .Select(x => new UposleniciPonudaViewModel.KorpaInfo()
                         {
                             Id = x.Id,
                             NazivProizvoda = x.Naziv,
                             CijenaProizvoda = x.Cijena,
                             ProizvodId = proizvodId.Value,
                             KorisnikId = korisnikId.Value,
                             VrijemeNarudzbe = DateTime.Now,
                             DatumNarudzbe = DateTime.Now,
                             Kolicina = 1,
                             UkupnaCijenaProizvoda = x.Cijena,
                             StolID = stolID.Value
                         }).First();
                // U Session["korpa"] pohranjuje listu da trenutnim proizvodima koji se nalaze u korpi, 
                // dodaje novi proizvod te ta korpa bude aktivna i na drugim prozorima
                if (UposlenikStolNarudzba.UposlenikKorpaViewModels == null)
                {
                    
                    List<UposlenikKorpaViewModel> korpa = new List<UposlenikKorpaViewModel>();
                    korpa.Add(new UposlenikKorpaViewModel(Model.Korpa, ukupnaCijena, stolID.Value));
                    UposlenikStolNarudzba.UposlenikKorpaViewModels = korpa;
                    
                    List<string>zauzetiStolovi = new List<string>();
                    UposleniciZauzetiStolViewModel.stolId = zauzetiStolovi;
                    UposleniciZauzetiStolViewModel.stolId.Add("stolID-" + stolID.Value);
                }
                else
                {
                    List<UposlenikKorpaViewModel> korpa = UposlenikStolNarudzba.UposlenikKorpaViewModels;
                    if (korpa.Exists(x => x.Korpa.ProizvodId == proizvodId.Value && x.Korpa.StolID == stolID.Value))
                    {
                        for (int i = 0; i < korpa.Count; i++)
                        {
                            if (korpa[i].Korpa.ProizvodId == proizvodId.Value && korpa[i].Korpa.StolID == stolID.Value)
                            {
                                korpa[i].Korpa.Kolicina++;
                                korpa[i].Korpa.UkupnaCijenaProizvoda += korpa[i].Korpa.CijenaProizvoda;
                            }

                        }
                        if (!UposleniciZauzetiStolViewModel.stolId.Contains("stolID-" + stolID.Value))
                            UposleniciZauzetiStolViewModel.stolId.Add("stolID-" + stolID.Value);
                    }
                    else
                    {
                        korpa.Add(new UposlenikKorpaViewModel(Model.Korpa, ukupnaCijena, stolID.Value));
                        UposlenikStolNarudzba.UposlenikKorpaViewModels = korpa;
                        if(!UposleniciZauzetiStolViewModel.stolId.Contains("stolID-" + stolID.Value))
                            UposleniciZauzetiStolViewModel.stolId.Add("stolID-" + stolID.Value);
                    }
                }
            }

            // Racuna i vraca ukupnu cijenu svih dodanih proizvoda u korpi
            if ((proizvodId.HasValue || deleteId.HasValue) && stolID != null)
            {
                List<UposlenikKorpaViewModel> korpabrojList = UposlenikStolNarudzba.UposlenikKorpaViewModels;
                for (int i = 0; i < korpabrojList.Count; i++)
                {
                    if (korpabrojList[i].Korpa.StolID == stolID.Value)
                         ukupnaCijena += korpabrojList[i].Korpa.UkupnaCijenaProizvoda;
                }
                if (!(ukupnaCijena > 0))
                {
                    ukupnaCijena = 0;
                }
                ViewData["Ukupno"] = ukupnaCijena;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("PrikaziPonudu", Model);
            }
            else
            {
                return View("PrikaziPonudu", Model);
            }

            

        }

        public ActionResult KorpaPrikazi(int? sId, int? kId)
        {
            return RedirectToAction("KorpaPrikazi", "Korpa", new { stolId = sId, korisnikId = kId });
        }
    }
}
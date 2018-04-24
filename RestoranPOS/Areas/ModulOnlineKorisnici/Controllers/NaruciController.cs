using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulAdmin.Models;
using RestoranPOS.Areas.ModulOnlineKorisnici.Models;
using RestoranPOS.Areas.ModulUposlenici;
using RestoranPOS.DAL;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulOnlineKorisnici.Controllers
{
    public class NaruciController : Controller
    {
        private RestoranContext ctx = new RestoranContext();
        private double ukupnaCijena;
        // GET: ModulOnlineKorisnici/Naruci
        // Prvo izlistati ponude restorana u posebnom div-u, pa onda ostale proizvode
        // Porsljedjeni parametri : kategorijaId - omogucava sortiranje proizvoda po kategoriji, po defaultu izlistava sve proizvode
        // proizvodId sluzi uglavnom za prikaz proizvoda koji su dodani u korpu
        public ActionResult PrikaziPonudu(int? kategorijaId, int? proizvodId, int? korisnikId, int? deleteId)
        {
            
            OKPonudaViewModel Model = new OKPonudaViewModel();
            
            // Prikazuje aktivne ponude 
            Model.Ponude = ctx.Ponude
                .Where(x => x.Aktivna && !x.IsDeleted)
                .Select(x => new OKPonudaViewModel.PonudaInfo()
                {
                    Id = x.Id,
                    NazivPonude = x.NazivPonude,
                    VaziOd = x.VaziOd,
                    VaziDo = x.VaziDo,
                    Aktivna = x.Aktivna,
                    restoranId = x.RestoranId
                }).ToList();

            // Po defaultu, ako kategorijaId nije prosljedjen, izlistava sve proizvode, u suprotnom, samo one koji pripadaju prosljedjenoj kategoriji
            if (kategorijaId != null)
            {
                ViewBag.katId = kategorijaId;
                Model.Proizvodi = ctx.Proizvodi
                    .Where(x => !x.IsDeleted && x.KategorijaId == kategorijaId)
                    .Select(x => new OKPonudaViewModel.ProizvodInfo()
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
            {
                Model.Proizvodi = ctx.Proizvodi
                    .Where(x => !x.IsDeleted)
                    .Select(x => new OKPonudaViewModel.ProizvodInfo()
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
                

            // Sluzi da izlista kategorije pomocu kojih mozemo filtrirati proizvode
            Model.Kategorije = ctx.Kategorije
                .Where(x => x.DostupnaOnline && !x.IsDeleted)
                .Select(x => new OKPonudaViewModel.KategorijaInfo()
                {
                    Id = x.Id,
                    NazivKategorije = x.NazivKategorije,
                    DosupnaOnline = x.DostupnaOnline,
                    PonudaId = x.PonudaId
                }).ToList();


            // Uklanja proizvod iz korpe, kada korisnik klikne na button ukloni, onda se proslijedi deleteId = 1, te se aktivira ovaj dio
            if (deleteId.HasValue && deleteId.Value == 1)
            {
                List<KorpaViewModel> korpa = (List<KorpaViewModel>)Session["korpa"];
                if (korpa.Exists(x => x.Korpa.ProizvodId == proizvodId.Value))
                {
                    var item = korpa.Single(x => x.Korpa.ProizvodId == proizvodId);
                    korpa.Remove(item);
                    korpa.RemoveAll(x => x == null); // uklanja sve null objekte
                }
            }

            // Dodaje odgovarajuci proizvod u korpu
            if (proizvodId != null && deleteId == null)
            {
               Model.Korpa = ctx.Proizvodi
                        .Where(x => x.Id == proizvodId)
                        .Select(x => new OKPonudaViewModel.KorpaInfo()
                        {
                            Id = x.Id,
                            NazivProizvoda = x.Naziv,
                            CijenaProizvoda = x.Cijena,
                            ProizvodId = proizvodId.Value,
                            KorisnikId = korisnikId.Value,
                            VrijemeNarudzbe = DateTime.Now,
                            DatumNarudzbe = DateTime.Now,
                            Kolicina = 1,
                            UkupnaCijenaProizvoda = x.Cijena
                        }).First();
                // U Session["korpa"] pohranjuje listu da trenutnim proizvodima koji se nalaze u korpi, 
                // dodaje novi proizvod te ta korpa bude aktivna i na drugim prozorima
                if (Session["korpa"] == null)
                {
                    List<KorpaViewModel> korpa = new List<KorpaViewModel>();
                    korpa.Add(new KorpaViewModel(Model.Korpa, ukupnaCijena));
                    Session["korpa"] = korpa;
                }
                else
                {
                    List<KorpaViewModel> korpa = (List<KorpaViewModel>) Session["korpa"];
                    if (korpa.Exists(x => x.Korpa.ProizvodId == proizvodId.Value))
                    {
                        for (int i = 0; i < korpa.Count; i++)
                        {
                            if (korpa[i].Korpa.ProizvodId == proizvodId.Value)
                            {
                                korpa[i].Korpa.Kolicina++;
                                korpa[i].Korpa.UkupnaCijenaProizvoda += korpa[i].Korpa.CijenaProizvoda;
                            }

                        }

                    }
                    else
                    {
                        korpa.Add(new KorpaViewModel(Model.Korpa, ukupnaCijena));
                        Session["korpa"] = korpa;
                    }
                }
            }

            // Racuna i vraca ukupnu cijenu svih dodanih proizvoda u korpi
            if (proizvodId.HasValue || deleteId.HasValue)
            {
                List<KorpaViewModel> korpabrojList = (List<KorpaViewModel>)Session["korpa"];
                for (int i = 0; i < korpabrojList.Count; i++)
                {
                    ukupnaCijena += korpabrojList[i].Korpa.UkupnaCijenaProizvoda;
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
      /*  public ActionResult DodajUKorpu(int proizvodId, int korisnikId)
        {
            OKPonudaViewModel Model = new OKPonudaViewModel();
            if (Session["korpa"] == null)
            {
                //KorpaViewModel korpica = new KorpaViewModel(proizvodId, korisnikId, DateTime.Now, DateTime.Now, 1);
                Model.Korpa = ctx.StavkeNarudzbe
                    .Include(x => x.Narudzba.Korisnik)
                    .Include(x => x.Proizvod)
                    .Where(x => x.ProizvodId == proizvodId && x.Narudzba.KorisnikId == korisnikId)
                    .Select(x => new OKPonudaViewModel.KorpaInfo()
                    {
                        Id = x.Id,
                        proizvod = x.Proizvod,
                        korisnik = x.Narudzba.Korisnik,
                        VrijemeNarudzbe = DateTime.Now,
                        DatumNarudzbe = DateTime.Now
                    }).ToList();
                KorpaViewModel korpica = new KorpaViewModel(Model.Korpa.FirstOrDefault(), 1);

                List<KorpaViewModel> korpa = new List<KorpaViewModel>();
                korpa.Add(korpica);
                Session["korpa"] = korpa;
            }
            else
            {
                
            }
            return PartialView("PrikaziPonudu");
        }*/

    }
}
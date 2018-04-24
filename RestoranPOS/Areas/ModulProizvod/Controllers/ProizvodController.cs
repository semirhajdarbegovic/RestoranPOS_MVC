using RestoranPOS.DAL;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Models;
using RestoranPOS.Areas;
using RestoranPOS.Areas.ModulProizvod.Models;
using RestoranPOS.Helper;


namespace RestoranPOS.Areas.ModulProizvod.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer, KorisnickeUloge.Uposlenik)]
    public class ProizvodController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        // GET: ModulProizvod/Proizvod
        public ActionResult Index()
        {
            return View();
        }

        [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer, KorisnickeUloge.Uposlenik, KorisnickeUloge.OnlineKorisnik)]
        public ActionResult Prikazi(int? kategorijaId)
        {
            PrikaziProizvodViewModel Model = new PrikaziProizvodViewModel();
            Model.Proizvodi = (ctx.Proizvodi
                .Where(x => (!kategorijaId.HasValue && x.IsDeleted == false) || (x.KategorijaId == kategorijaId && x.IsDeleted == false))
                .Include(x => x.Kategorija)
                .Select(x => new PrikaziProizvodViewModel.ProizvodInfo
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    IsDeleted = x.IsDeleted,
                    SifraProizvoda = x.SifraProizvoda,
                    Kolicina = x.Kolicina,
                    Cijena = x.Cijena,
                    Opis = x.Opis

                })).ToList();
           // Model.Proizvodi = ctx.Proizvodi.ToList();
            Model.KategorijaId = kategorijaId;

            return View("Prikazi", Model);

        }
        public ActionResult PrikaziIzbrisane(int? kategorijaId)
        {
            PrikaziProizvodViewModel Model = new PrikaziProizvodViewModel();
            Model.Proizvodi = (ctx.Proizvodi
                .Where(x => (!kategorijaId.HasValue && x.IsDeleted == true) || (x.KategorijaId == kategorijaId && x.IsDeleted == true))
                .Include(x => x.Kategorija)
                .Select(x => new PrikaziProizvodViewModel.ProizvodInfo
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    IsDeleted = x.IsDeleted,
                    SifraProizvoda = x.SifraProizvoda,
                    Kolicina = x.Kolicina,
                    Cijena = x.Cijena,
                    Opis = x.Opis

                })).ToList();
           // Model.Proizvodi = ctx.Proizvodi.ToList();
            Model.KategorijaId = kategorijaId;

            return View("PrikaziIzbrisane", Model);

        }

        public ActionResult Uredi(int? proizvodId)
        {   /*
            public ActionResult Uredi(int? kategorijaId)
        {
            KategorijaEditModel Model = new KategorijaEditModel();
            Kategorija kategorija = ctx.Kategorije.Where(x => x.Id == kategorijaId).FirstOrDefault();
            if (kategorijaId.HasValue)
            {
                Model.NazivKategorije = kategorija.NazivKategorije;
                Model.DostupnaOnline = kategorija.DostupnaOnline;
            }
            if (!kategorijaId.HasValue)
            {
                kategorija = new Kategorija();
            }
            else
            {
                Model.Id = kategorija.Id;
                Model.NazivKategorije = kategorija.NazivKategorije;
                Model.DostupnaOnline = kategorija.DostupnaOnline;
            }
                Model.Ponude = ctx.Ponude.ToList();

            return View("Uredi", Model);
        }
            */
            UrediProizvodViewModel Model = new UrediProizvodViewModel();
            Proizvod proizvod = ctx.Proizvodi.Where(x => x.Id == proizvodId).FirstOrDefault();
            if (proizvodId.HasValue)
            {
                Model.IsDeleted = proizvod.IsDeleted;
                
                Model.Naziv = proizvod.Naziv;
                Model.Opis = proizvod.Opis;
                Model.Kolicina = proizvod.Kolicina;
                Model.Cijena = proizvod.Cijena;
                Model.SifraProizvoda = proizvod.SifraProizvoda;
                Model.KategorijaId = proizvod.KategorijaId;
            }
            if (!proizvodId.HasValue)
            {
                proizvod = new Proizvod();
            }
            else
            {
                Model.Id = proizvod.Id;
                Model.IsDeleted = proizvod.IsDeleted;
                Model.Naziv = proizvod.Naziv;
                Model.Opis = proizvod.Opis;
                Model.Kolicina = proizvod.Kolicina;
                Model.Cijena = proizvod.Cijena;
                Model.SifraProizvoda = proizvod.SifraProizvoda;
                Model.KategorijaId = proizvod.KategorijaId;
            }
            Model.Kategorije = ctx.Kategorije.ToList();
            

            return View("Uredi", Model);
        }

        public ActionResult Dodaj(int? kategorijaId)
        {
            UrediProizvodViewModel Model = new UrediProizvodViewModel();
            Model.Kategorije = ctx.Kategorije.ToList();
            if (kategorijaId.HasValue)
            {
                Model.KategorijaId = (int) kategorijaId;
            }
            return View("Uredi", Model);
        }

        public ActionResult Spremi(UrediProizvodViewModel proizvod)
        {
            if (!ModelState.IsValid)
            {
                proizvod.Kategorije = ctx.Kategorije.ToList();
                return View("Uredi", proizvod);
            }

            Proizvod ProizvodDB;
            if (proizvod.Id == 0)
            {
                ProizvodDB = new Proizvod();
                ctx.Proizvodi.Add(ProizvodDB);
            }
            else
            {
                ProizvodDB = ctx.Proizvodi.Where(x => x.Id == proizvod.Id).Include(p => p.Kategorija).FirstOrDefault();
            }
            ProizvodDB.Naziv = proizvod.Naziv;
            ProizvodDB.IsDeleted = proizvod.IsDeleted;
            ProizvodDB.KategorijaId = proizvod.KategorijaId;
            ProizvodDB.Kolicina = proizvod.Kolicina;
            ProizvodDB.Opis = proizvod.Opis;
            ProizvodDB.Cijena = proizvod.Cijena;
            ProizvodDB.SifraProizvoda = proizvod.SifraProizvoda;
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
        //Radi restore proizvoda koje su "izbrisani" tj ciji IsDeleted == true
        public ActionResult Vrati(int proizvodId)
        {
            Proizvod proizvod = ctx.Proizvodi.Where(x => x.Id == proizvodId).FirstOrDefault();
            proizvod.IsDeleted = false;
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        //"brise" odredjeni proizvod tj postavlja IsDeleted == true
        public ActionResult Izbrisi(int proizvodId)
        {
            Proizvod proizvod = ctx.Proizvodi.Where(x => x.Id == proizvodId).FirstOrDefault();
            proizvod.IsDeleted = true;
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }

       
    }
}
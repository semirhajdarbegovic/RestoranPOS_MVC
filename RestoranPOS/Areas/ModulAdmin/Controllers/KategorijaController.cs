using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulAdmin.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulAdmin.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class KategorijaController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        // GET: ModulAdmin/Kategorija
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prikazi(int? ponudaId)
        {
            KategorijaViewModel Model = new KategorijaViewModel();
            Model.Kategorije = (ctx.Kategorije
                .Where(x => (!ponudaId.HasValue && x.IsDeleted == false) || (x.PonudaId == ponudaId && x.IsDeleted == false))
                .Include(x => x.Ponuda)
                .Select(x => new KategorijaViewModel.KategorijaInfo
                {
                    Id = x.Id,
                    NazivKategorije = x.NazivKategorije,
                    DostupnaOnline = x.DostupnaOnline,
                })).ToList();

            Model.Ponude = ctx.Ponude.ToList();
            Model.PonudaId = ponudaId;

            return View("Prikazi", Model);
        }

        public ActionResult PrikaziIzbrisane(int? ponudaId)
        {
            KategorijaViewModel Model = new KategorijaViewModel();
            Model.Kategorije = (ctx.Kategorije
                .Where(x => (!ponudaId.HasValue && x.IsDeleted == true && x.Ponuda.Aktivna && !x.Ponuda.IsDeleted)
                         || (x.PonudaId == ponudaId && x.IsDeleted == true && x.Ponuda.Aktivna && !x.Ponuda.IsDeleted))
                .Include(x => x.Ponuda)
                .Select(x => new KategorijaViewModel.KategorijaInfo
                {
                    Id = x.Id,
                    NazivKategorije = x.NazivKategorije,
                    DostupnaOnline = x.DostupnaOnline,
                })).ToList();

            Model.Ponude = ctx.Ponude.ToList();
            Model.PonudaId = ponudaId;

            return View("PrikaziIzbrisane", Model);
        }

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

        public ActionResult Dodaj()
        {
            KategorijaEditModel Model = new KategorijaEditModel();
            Model.Ponude = ctx.Ponude.ToList();
            return View("Uredi", Model);
        }

        public ActionResult Spremi(KategorijaEditModel kategorija)
        {
            if (!ModelState.IsValid)
            {
                kategorija.Ponude = ctx.Ponude.ToList();
                return View("Uredi", kategorija);
            }

            Kategorija KategorijaDB;
            if (kategorija.Id == 0)
            {
                KategorijaDB = new Kategorija();
                ctx.Kategorije.Add(KategorijaDB);
            }
            else
            {
                KategorijaDB = ctx.Kategorije.Where(x => x.Id == kategorija.Id).Include(p => p.Ponuda).FirstOrDefault();
            }
            KategorijaDB.NazivKategorije = kategorija.NazivKategorije;
            KategorijaDB.DostupnaOnline = kategorija.DostupnaOnline;
            KategorijaDB.PonudaId = kategorija.PonudaId;
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        //Radi restore kategorija koje su "izbrisane" tj ciji IsDeleted == true
        public ActionResult Vrati(int kategorijaId)
        {
            Kategorija kategorija = ctx.Kategorije.Where(x => x.Id == kategorijaId).FirstOrDefault();
            kategorija.IsDeleted = false;
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        //"brise" odredjenu kategoriju tj postavlja IsDeleted == true
        public ActionResult Izbrisi(int kategorijaId)
        {
            Kategorija kategorija = ctx.Kategorije.Where(x => x.Id == kategorijaId).FirstOrDefault();
            kategorija.IsDeleted = true;
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
    }
}
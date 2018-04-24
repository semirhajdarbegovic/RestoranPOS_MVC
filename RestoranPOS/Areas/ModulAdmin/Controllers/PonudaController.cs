using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulAdmin.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulAdmin.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class PonudaController : Controller
    {
        private RestoranContext ctx = new RestoranContext();
        // GET: ModulAdmin/Ponuda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prikazi()
        {
            PonudaViewModel Model = new PonudaViewModel();
            Model.Ponude = ctx.Ponude
                .Where(x => x.Aktivna && !x.IsDeleted)
                .Select(x => new PonudaViewModel.PonudaInfo()
                {
                    Id = x.Id,
                    NazivPonude = x.NazivPonude,
                    VaziOd = x.VaziOd,
                    VaziDo = x.VaziDo,
                    Aktivna = x.Aktivna,
                    restoranId = x.RestoranId
                }).ToList();

            return View("Prikazi", Model);
        }

        public ActionResult PrikaziNeaktivne()
        {
            PonudaViewModel Model = new PonudaViewModel();
            Model.Ponude = ctx.Ponude
                .Where(x => !x.Aktivna && !x.IsDeleted)
                .Select(x => new PonudaViewModel.PonudaInfo()
                {
                    Id = x.Id,
                    NazivPonude = x.NazivPonude,
                    VaziOd = x.VaziOd,
                    VaziDo = x.VaziDo,
                    Aktivna = x.Aktivna,
                    restoranId = x.RestoranId
                }).ToList();

            return View("PrikaziNeaktivne", Model);
        }
        public ActionResult PrikaziIzbrisane()
        {
            PonudaViewModel Model = new PonudaViewModel();
            Model.Ponude = ctx.Ponude
                .Where(x => x.IsDeleted)
                .Select(x => new PonudaViewModel.PonudaInfo()
                {
                    Id = x.Id,
                    NazivPonude = x.NazivPonude,
                    VaziOd = x.VaziOd,
                    VaziDo = x.VaziDo,
                    Aktivna = x.Aktivna,
                    restoranId = x.RestoranId
                }).ToList();

            return View("PrikaziIzbrisane", Model);
        }

        public ActionResult Uredi(int ponudaId)
        {
            Ponuda ponuda = ctx.Ponude.Where(x => x.Id == ponudaId).FirstOrDefault();
            PonudaEditModel Model = new PonudaEditModel();
            if (ponudaId == 0)
            {
                ponuda = new Ponuda();
            }
            else
            {
                Model.Id = ponuda.Id;
                Model.NazivPonude = ponuda.NazivPonude;
                Model.VaziOd = ponuda.VaziOd;
                Model.VaziDo = ponuda.VaziDo;
                Model.Aktivna = ponuda.Aktivna;
                Model.RestoranId = ponuda.RestoranId;
            }

            return View("Uredi", Model);
        }

        public ActionResult Dodaj()
        {
            PonudaEditModel Model = new PonudaEditModel();
            Model.RestoranId = ctx.Restorani.First().Id;
            Model.VaziOd = DateTime.Now;
            Model.VaziDo = DateTime.Now;
            return View("Uredi", Model);
        }

        public ActionResult Spremi(PonudaEditModel ponuda)
        {
            if (!ModelState.IsValid)
                return View("Uredi", ponuda);

            Ponuda ponudaDB;
            int restoranId = ctx.Restorani.First().Id;
            if (ponuda.Id == 0)
            {
                ponudaDB = new Ponuda();
                ctx.Ponude.Add(ponudaDB);
            }
            else
            {
                ponudaDB = ctx.Ponude.Where(x => x.Id == ponuda.Id).FirstOrDefault();
            }
            ponudaDB.NazivPonude = ponuda.NazivPonude;
            ponudaDB.VaziOd = ponuda.VaziOd;
            ponudaDB.VaziDo = ponuda.VaziDo;
            ponudaDB.RestoranId = restoranId;
            ponudaDB.Aktivna = ponuda.Aktivna;
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
        //Radi restore ponuda koje su "izbrisane" tj ciji IsDeleted == true
        public ActionResult Vrati(int ponudaId)
        {
            Ponuda ponuda = ctx.Ponude.Where(x => x.Id == ponudaId).FirstOrDefault();
            ponuda.IsDeleted = false;
            ctx.SaveChanges();
            return Prikazi();
        }
        //"brise" odredjenu ponudu tj postavlja IsDeleted == true
        public ActionResult Izbrisi(int ponudaId)
        {
            Ponuda ponuda = ctx.Ponude.Where(x => x.Id == ponudaId).FirstOrDefault();
            ponuda.IsDeleted = true;
            ctx.SaveChanges();
            return Prikazi();
        }
        //Aktivira neaktivnu ponudu ovo je kao QuickOption u listi neaktivnih da se ne mora ici na Uredi pa onda postaviti da je aktivna
        public ActionResult Aktiviraj(int ponudaId)
        {
            Ponuda ponuda = ctx.Ponude.Where(x => x.Id == ponudaId).FirstOrDefault();
            ponuda.Aktivna = true;
            ctx.SaveChanges();
            return Prikazi();
        }
    }
}
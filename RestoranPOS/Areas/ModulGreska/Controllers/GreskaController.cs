using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulGreska.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulGreska.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class GreskaController : Controller
    {
        private RestoranContext ctx = new RestoranContext();

        //Prikazuje opcije za rad sa modulom Greska
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: ModulGreska/Greska/PrijaviGresku
        // Sluzi za prijavljivanje greske, te samo vraca View za unos podataka
        [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer, KorisnickeUloge.Uposlenik)]
        public ActionResult PrijaviGresku()
        {
            return View();
        }


        // POST: ModulGreska/Greska/PrijaviGresku
        // Kada kliknemo button prijavi, tada poziva [HttpPost] metodu, koja u ovom slucaju dodaje novu gresku
        [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer, KorisnickeUloge.Uposlenik)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrijaviGresku(UrediGreskuViewModel greska, int korisnikId)
        {
            if (ModelState.IsValid)
            {
                Greska novaGreska = new Greska();

                novaGreska.IsDeleted = false;
                novaGreska.Ispravljena = false;
                novaGreska.KorisnikId = korisnikId;
                novaGreska.Opis = greska.Opis;
                novaGreska.Pregledana = false;
                novaGreska.DatumPrijave = DateTime.Now.Date;
                novaGreska.VrijemePrijave = DateTime.Now;

                ctx.Greske.Add(novaGreska);
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }
            else
                return View();
        }

        // GET: ModulGreska/Greska/PrikaziGresku
        // Vraca listu svih gresaka, sortirane od posljednje dodane.. 
        public ActionResult PrikaziGresku()
        {
            List<Greska> greska = new List<Greska>();

            greska = ctx.Greske.ToList();
            
            var greske = ctx.Greske.OrderByDescending(g=>g.Id);
            return View(greske.ToList());
        }

        // GET: ModulGreska/Greska/PrikaziDetaljnoGresku
        public ActionResult PrikaziDetaljnoGresku(int greskaId)
        {
            Greska greska = new Greska();
            greska = ctx.Greske.Find(greskaId);
            ctx.Greske.Find(greskaId).Pregledana = true;
            ctx.SaveChanges();

            return View(greska);

        }

        // POST: ModulGreska/Greska/PrikaziDetaljnoGresku
        // Ako je greska ispravljena, nakon klika na dugme Ispravi, poziva ovu akciju...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrikaziDetaljnoGresku(int greskaId, bool Ispravljena)
        {
            Greska greska = new Greska();
            greska = ctx.Greske.Find(greskaId);
           
            ctx.Greske.Find(greskaId).Ispravljena = Ispravljena;
            ctx.SaveChanges();

            return RedirectToAction("PrikaziGresku");

        }
    }
}
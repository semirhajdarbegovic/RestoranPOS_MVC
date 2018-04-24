using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.DAL;
using RestoranPOS.Models;

namespace RestoranPOS.Controllers
{
    public class HomeController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        // GET: Home
        public ActionResult Prikazi()
        {
            List<Korisnik> Korisnici = ctx.Korisnici.ToList();
            ViewData["Korisnici"] = Korisnici;
            return View();
        }

        public ActionResult Pocetna()
        {
            return View();
        }
        public ActionResult Index()
        {
            List<Proizvod> proizvodi = ctx.Proizvodi.Where(x=>x.SlikaProizvoda != null).ToList();
            return View(proizvodi);
        }

        public ActionResult Onama()
        {
            return View();
        }
    }
}
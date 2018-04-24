using RestoranPOS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Models;
using RestoranPOS.Areas.ModulLogin.Models;
using RestoranPOS.Helper;

namespace RestoranPOS.Areas.ModulLogin.Controllers
{
    public class LoginController : Controller
    {
        private RestoranContext ctx = new RestoranContext();
        // GET: ModulLogin/Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Provjera(string username, string password)
        {
           // string hashPass = MD5Hash.GetMD5Hash(password);
            Korisnik k = ctx.Korisnici
                //.Include(x => x.OnlineKorisnik)
               // .Include(x => x.Uposlenik)
                .SingleOrDefault(x => x.Username == username && (x.Password == password));

            if (k == null)
            {
                return Redirect("/ModulLogin/Login/Login");
            }

            Autentifikacija.PokreniNovuSesiju(k, HttpContext);
            if (k.Uposlenik == null)
                return RedirectToAction("Prikazi", "Profil", new {Area = "ModulOnlineKorisnici", korisnikId = k.Id});
            else if (!k.Uposlenik.Admin)
                return RedirectToAction("PrikaziPonudu", "Naruci", new { Area = "ModulUposlenici", korisnikId = k.Id });
            else
                return RedirectToAction("Prikazi", "Restoran", new { Area = "ModulAdmin" });    
        }

        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext);
            return RedirectToAction("Index");
        }

    }
}
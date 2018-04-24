using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestoranPOS.Models;

namespace RestoranPOS.Helper
{
    public class Autorizacija : FilterAttribute, IAuthorizationFilter
    {
        private readonly KorisnickeUloge[] _dozvoljeneUloge;

        public Autorizacija(params KorisnickeUloge[] dozvoljeneUloge)
        {
            _dozvoljeneUloge = dozvoljeneUloge;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(filterContext.HttpContext);

            if (k == null)
            {
                filterContext.HttpContext.Response.Redirect("/ModulLogin/Login/Login");
                return;
            }

            //provjera
            foreach (KorisnickeUloge x in _dozvoljeneUloge)
            {
                if (x == KorisnickeUloge.Administrator && k.Uposlenik.Admin != false)
                    return;
                if (x == KorisnickeUloge.Mendazer && k.Uposlenik.Menadzer != false)
                    return;
                if (x == KorisnickeUloge.Uposlenik && k.Uposlenik != null)
                    return;
                if (x == KorisnickeUloge.OnlineKorisnik && k.OnlineKorisnik != null)
                    return;
            }
            //ako funkcija nije prekinuta pomoću "return", onda korisnik nema pravo pistupa pa će se vršiti redirekcija na "/Home/Index"
            filterContext.HttpContext.Response.Redirect("/");
        }
    }
    public enum KorisnickeUloge
    {
        OnlineKorisnik,
        Uposlenik,
        Mendazer,
        Administrator
    }
}

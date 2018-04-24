using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Models;

namespace RestoranPOS.Helper
{
    public class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void PokreniNovuSesiju(Korisnik korisnik, HttpContextBase context)
        {
            context.Session.Add(LogiraniKorisnik, korisnik);
        }

        public static Korisnik GetLogiraniKorisnik(HttpContextBase context)
        {
            return (Korisnik)context.Session[LogiraniKorisnik];
        }
    }
}
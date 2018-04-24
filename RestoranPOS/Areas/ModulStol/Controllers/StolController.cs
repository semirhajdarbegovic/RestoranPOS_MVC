using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulStol.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer, KorisnickeUloge.Uposlenik, KorisnickeUloge.OnlineKorisnik)]
    public class StolController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        // GET: ModulStol/Stol
        public ActionResult Prikazi()
        {
            int brojStolova = ctx.Restorani.First().BrStolova;
            //ako se smanji broj stolova u postavci restorana treba smanjiti broj stolova sa isDeleted == true;
            if (ctx.Stolovi.Where(x => x.BrojStola != 2147483647).Count() > brojStolova)
            {
                if (ctx.Stolovi.Where(x => !x.IsDeleted && x.BrojStola != 2147483647).Count() > brojStolova)
                {
                    List<Stol> visakList = ctx.Stolovi.OrderBy(x => x.BrojStola).ToList();
                    foreach (Stol s in visakList)
                    {
                        if (s.BrojStola > brojStolova)
                            s.IsDeleted = true;
                    }
                    ctx.SaveChanges();
                }
                //ako je povecan broj stolova u postavci restorana prvo se gleda postoje li vec kreirani stolovi da se samo aktiviraju
                // tako da ne treba dodavati nove stolove
                else
                {
                    List<Stol> aktivacijaList = ctx.Stolovi.Where(x => x.BrojStola != 2147483647).OrderBy(x => x.BrojStola).Take(brojStolova).ToList();
                    foreach (Stol s in aktivacijaList)
                    {
                        s.IsDeleted = false;
                    }
                    ctx.SaveChanges();
                }
            }

            if (ctx.Stolovi.Where(x => x.BrojStola != 2147483647).Count() < brojStolova)
            {
                //ako u bazi ima manje stolova tj ukupno zapisa o stolovima treba kreirati dodatne stolove
                List<Stol> aktivacijaList = ctx.Stolovi.Where(x => x.BrojStola != 2147483647).OrderBy(x => x.BrojStola).Take(brojStolova).ToList();
                foreach (Stol s in aktivacijaList)
                {
                    s.IsDeleted = false;
                }
                ctx.SaveChanges();

                int trenutnoStolova = ctx.Stolovi.Where(x => !x.IsDeleted && x.BrojStola != 2147483647).Count();
                while (trenutnoStolova < brojStolova)
                {
                    Stol temp = new Stol();
                    temp.IsDeleted = false;
                    temp.Zauzet = false;
                    temp.BrojStola = trenutnoStolova + 1;
                    ctx.Stolovi.Add(temp);
                    trenutnoStolova++;
                }
                ctx.SaveChanges();
            }
            List<Stol> Stolovi = ctx.Stolovi.Where(x => !x.IsDeleted && x.BrojStola != 2147483647).ToList();
            return View(Stolovi);
        }

        public List<Stol> StoloviLista()
        {
            int brojStolova = ctx.Restorani.First().BrStolova;
            //ako se smanji broj stolova u postavci restorana treba smanjiti broj stolova sa isDeleted == true;
            if (ctx.Stolovi.Where(x => x.BrojStola != 2147483647).Count() > brojStolova)
            {
                if (ctx.Stolovi.Where(x => !x.IsDeleted && x.BrojStola != 2147483647).Count() > brojStolova)
                {
                    List<Stol> visakList = ctx.Stolovi.OrderBy(x => x.BrojStola).ToList();
                    foreach (Stol s in visakList)
                    {
                        if (s.BrojStola > brojStolova)
                            s.IsDeleted = true;
                    }
                    ctx.SaveChanges();
                }
                //ako je povecan broj stolova u postavci restorana prvo se gleda postoje li vec kreirani stolovi da se samo aktiviraju
                // tako da ne treba dodavati nove stolove
                else
                {
                    List<Stol> aktivacijaList = ctx.Stolovi.Where(x => x.BrojStola != 2147483647).OrderBy(x => x.BrojStola).Take(brojStolova).ToList();
                    foreach (Stol s in aktivacijaList)
                    {
                        s.IsDeleted = false;
                    }
                    ctx.SaveChanges();
                }
            }

            if (ctx.Stolovi.Where(x => x.BrojStola != 2147483647).Count() < brojStolova)
            {
                //ako u bazi ima manje stolova tj ukupno zapisa o stolovima treba kreirati dodatne stolove
                List<Stol> aktivacijaList = ctx.Stolovi.Where(x => x.BrojStola != 2147483647).OrderBy(x => x.BrojStola).Take(brojStolova).ToList();
                foreach (Stol s in aktivacijaList)
                {
                    s.IsDeleted = false;
                }
                ctx.SaveChanges();

                int trenutnoStolova = ctx.Stolovi.Where(x => !x.IsDeleted && x.BrojStola != 2147483647).Count();
                while (trenutnoStolova < brojStolova)
                {
                    Stol temp = new Stol();
                    temp.IsDeleted = false;
                    temp.Zauzet = false;
                    temp.BrojStola = trenutnoStolova + 1;
                    ctx.Stolovi.Add(temp);
                    trenutnoStolova++;
                }
                ctx.SaveChanges();
            }
            List<Stol> Stolovi = ctx.Stolovi.Where(x => !x.IsDeleted && x.BrojStola != 2147483647).ToList();
            return Stolovi;
        }
    }
}
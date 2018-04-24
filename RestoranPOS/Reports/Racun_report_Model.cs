using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RestoranPOS.Areas.ModulNarudzba.Models;
using RestoranPOS.DAL;
using RestoranPOS.Models;

namespace RestoranPOS.Reports
{
    public class Racun_report_Model
    {
        public class Header
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string ImePrezime
            {
                set { this.ImePrezime = value; }
                get { return Ime + " " + Prezime; }
            }
            public int RacunId { get; set; }
            public DateTime VrijemeNarudzbe { get; set; }
            public DateTime DatumNarudzbe { get; set; }
            public float UkupnaCijena { get; set; }
        }
        public static List<Header> GetHeader(int racunId)
        {
            RestoranContext ctx = new RestoranContext();
            return (ctx.Narudzbe
                .Where(x => ( x.IsDeleted == false) || (x.RacunId == racunId && x.IsDeleted == false))
                .Include(x => x.Korisnik)
                .Include(x => x.Racun)
                .Select(x => new Header
                {
                    Id = x.Id,
                    DatumNarudzbe = x.DatumNarudzbe,
                    VrijemeNarudzbe = x.VrijemeNarudzbe,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    UkupnaCijena = x.UkupnaCijena,
                    RacunId = x.RacunId,
                })).ToList();
        }

        public class Body
        {
            public string NazivProizvoda { get; set; }
            public int Kolicina { get; set; }
            public float Cijena { get; set; }
            public float Ukupno { get; set; }
        }

        public static List<Body> GetBody(int narudzbaId)
        {
            RestoranContext ctx = new RestoranContext();
            
            return (ctx.StavkeNarudzbe
                .Where(x=>x.NarudzbaId == narudzbaId)
                .Select(x => new Body
                {
                    NazivProizvoda = x.Proizvod.Naziv,
                    Kolicina = x.Kolicina,
                    Cijena = x.Proizvod.Cijena,
                    Ukupno = x.Narudzba.Racun.Ukupno
                })).ToList();
        }
    }
}
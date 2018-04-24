using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Narudzba:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime VrijemeNarudzbe { get; set; }
        public DateTime DatumNarudzbe { get; set; }
        public bool OnlineNarudzba { get; set; }
        public float UkupnaCijena { get; set; }
        public bool Prihvacena { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public virtual Racun Racun { get; set; }
        public int RacunId { get; set; }
    }
}
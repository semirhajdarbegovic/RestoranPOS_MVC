using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Rezervacija:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Datum { get; set; }
        public DateTime Vrijeme { get; set; }
        public int BrOsoba { get; set; }
        public string ImeRezervacije { get; set; }
        public bool Odobrena { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public virtual Stol Stol { get; set; }
        public int StolId { get; set; }
    }
}
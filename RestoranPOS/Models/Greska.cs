using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Greska:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Opis { get; set; }
        public bool Pregledana { get; set; }
        public bool Ispravljena { get; set; }
        public DateTime DatumPrijave { get; set; }
        public DateTime VrijemePrijave { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
    }
}
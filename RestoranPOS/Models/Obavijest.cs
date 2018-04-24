using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Obavijest:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool Procitana { get; set; }
        public string TekstObavijesti { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime VrijemeKreiranja { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        
    }
}
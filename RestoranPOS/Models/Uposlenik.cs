using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Uposlenik:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int Jmbg { get; set; }
        public string BrRadneKnjizice { get; set; }
        public int BrZiroRacuna { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public float Plata { get; set; }
        public int BrDanaGodisnjeg { get; set; }
        public string OpisRadnogMjesta { get; set; }
        public bool Admin { get; set; }
        public bool Menadzer { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
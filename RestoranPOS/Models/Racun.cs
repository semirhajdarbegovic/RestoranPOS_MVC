using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Racun:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DatumRacuna { get; set; }
        public DateTime VrijemeRacuna { get; set; }
        public float Ukupno { get; set; }
        public float Popust { get; set; }
        public virtual Stol Stol { get; set; }
        public int StolId { get; set; }
    }
}
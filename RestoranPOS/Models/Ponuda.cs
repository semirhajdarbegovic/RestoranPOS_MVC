using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Ponuda:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string NazivPonude { get; set; }
        public DateTime VaziOd { get; set; }
        public DateTime VaziDo { get; set; }
        public bool Aktivna { get; set; }
        public virtual Restoran Restoran { get; set; }
        public int RestoranId { get; set; }
    }
}
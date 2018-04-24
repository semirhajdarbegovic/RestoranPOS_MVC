using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Novost:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public string Slika { get; set; }
        public int VisinaSlike { get; set; }
        public int SirinaSlike { get; set; }
        public virtual Restoran Restoran { get; set; }
        public int RestoranId { get; set; }
    }
}
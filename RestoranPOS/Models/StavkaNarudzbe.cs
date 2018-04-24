using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class StavkaNarudzbe:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int Kolicina { get; set; }
        public virtual Narudzba Narudzba { get; set; }
        public int NarudzbaId { get; set; }
        public virtual Proizvod Proizvod { get; set; }
        public int ProizvodId { get; set; }
        
    }
}
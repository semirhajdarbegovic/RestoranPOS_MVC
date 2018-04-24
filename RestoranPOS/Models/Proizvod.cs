using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Proizvod:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string SifraProizvoda { get; set; }
        public string Naziv { get; set; }
        public int Kolicina { get; set; }
        public float Cijena { get; set; }
        public string Opis { get; set; }
        public string SlikaProizvoda { get; set; }
        public virtual Kategorija Kategorija { get; set; }
        public int KategorijaId { get; set; }
    }
}
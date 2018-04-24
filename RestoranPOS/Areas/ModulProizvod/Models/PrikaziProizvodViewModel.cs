using RestoranPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulProizvod;
namespace RestoranPOS.Areas.ModulProizvod.Models
{
    public class PrikaziProizvodViewModel
    {
        public class ProizvodInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public bool IsDeleted { get; set; }
            public string SifraProizvoda { get; set; }
            public int Kolicina { get; set; }
            public float Cijena { get; set; }
            public string Opis { get; set; }
        }
        public List<ProizvodInfo> Proizvodi;
        

        public int? KategorijaId { get; set; }
    }
}
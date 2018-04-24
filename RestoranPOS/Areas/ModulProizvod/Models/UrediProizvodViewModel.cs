using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Models;
using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulProizvod.Models
{
    public class UrediProizvodViewModel
    {

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string SifraProizvoda { get; set; }
        public string Naziv { get; set; }
        public int Kolicina { get; set; }
        public float Cijena { get; set; }
        public string Opis { get; set; }

        public int KategorijaId { get; set; }
        public List<Kategorija> Kategorije { private get; set; }
        public List<SelectListItem> KategorijeOptions
        {
            get { return Kategorije.Where(x =>  x.IsDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.NazivKategorije }).ToList(); }
        }
    }
}
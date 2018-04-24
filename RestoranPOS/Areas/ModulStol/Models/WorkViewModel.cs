using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulStol.Models
{
    public class WorkViewModel
    {
        public int StolId { get; set; }
        public Stol Stol { get; set; }
        public int RacunId { get; set; }
        public Racun Racun { get; set; }

        public List<Narudzba> listaNarudzbi; 
        public List<Kategorija> listaKategorija;
        public List<Proizvod> listaProizvoda;
    }
}
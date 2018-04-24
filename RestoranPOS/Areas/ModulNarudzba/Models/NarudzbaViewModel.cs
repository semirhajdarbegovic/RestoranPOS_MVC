using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulNarudzba.Models
{
    public class NarudzbaViewModel
    {
        public class NarudzbaInfo
        {
            //id i objekti vezani za narudzbu
            public int Id { get; set; }
            //postavljeno korisnikId i racunId da se postavi buttone prikazi korisnika/racun
            public int KorisnikId { get; set; }
            public string KorisnikUsername { get; set; }
            public int RacunId { get; set; }
            public DateTime VrijemeNarudzbe { get; set; }
            public DateTime DatumNarudzbe { get; set; }
            public bool OnlineNarudzba { get; set; }
            public float UkupnaCijena { get; set; }
            public bool Prihvacena { get; set; }

            public List<StavkaNarudzbe> ListaStavki { get; set; }
        }

        public List<NarudzbaInfo> Narudzbe;
    }
}
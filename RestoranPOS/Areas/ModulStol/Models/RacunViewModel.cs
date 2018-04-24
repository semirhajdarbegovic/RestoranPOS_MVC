using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulStol.Models
{
    public class RacunViewModel
    {
        public class RacunInfo
        {
            //id i objekti vezani za narudzbu
            public int Id { get; set; }
            //postavljeno korisnikId i racunId da se postavi buttone prikazi korisnika/racun
            public Stol Stol { get; set; }
            public DateTime DatumRacuna { get; set; }
            public DateTime VrijemeRacuna { get; set; }
            public float Ukupno { get; set; }
            public float Popust { get; set; }
        }

        public List<RacunInfo> Racuni;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulOnlineKorisnici.Models
{
    public class PrikaziViewModel
    {
        public int Id { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }
        public string Adresa { get; set; }
        [Display(Name = "Broj telefona")]
        public string BrTelefona { get; set; }
        [Display(Name = "Email")]
        public string EMail { get; set; }

        public List<StavkaNarudzbe> StavkeNarudzbi { get; set; }
        public List<Rezervacija> ListaRezervacija { get; set; } 

    }
}

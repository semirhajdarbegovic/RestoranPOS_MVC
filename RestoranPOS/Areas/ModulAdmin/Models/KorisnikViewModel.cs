using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulAdmin.Models
{
    public class KorisnikViewModel
    {
        public class KorisnikInfo
        {
            public int Id { get; set; }

            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Username { get; set; }
            public string Adresa { get; set; }
            public string BrTelefona { get; set; }
            public string EMail { get; set; }
            public bool NalogAktivan { get; set; }

            public bool? Uposlen { get; set; }
        }

        public List<KorisnikInfo> Korisnici;
        public List<SelectListItem> KorisniciOptions
        {
            get
            {
                List<SelectListItem> data = new List<SelectListItem>();
                data.Add(new SelectListItem() { Value = null, Text = "(Svi Korisnici)" });
                data.Add(new SelectListItem() { Value = "1", Text = "Online Korisnici" });
                data.Add(new SelectListItem() { Value = "2", Text = "Uposlenici" });
                return data;
            }
        }
        public int tipKorisnika { get; set; }
    }
}
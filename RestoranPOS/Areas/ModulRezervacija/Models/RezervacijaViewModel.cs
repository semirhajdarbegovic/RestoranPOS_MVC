using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RestoranPOS.Models;
using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulRezervacija.Models
{
    public class RezervacijaViewModel
    {
        public class RezervacijaInfo
        {
            public int Id { get; set; }
            public bool IsDeleted { get; set; }
            public int KorisnikId { get; set; }
            public string KorisnikUsername { get; set; }
            public string ImeRezervacije { get; set; }
            public int BrojStola { get; set; }
            public DateTime DatumRezervacije { get; set; }
            public DateTime VrijemeRezervacije { get; set; }
            public int BrojOsoba { get; set; }
            public bool Odobrena { get; set; }

        }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PeriodOd { get; set; }
        public int BrDana { get; set; }
        public int StolId { get; set; }
        public List<SelectListItem> DaniOptions
        {
            get
            {
                List<SelectListItem> data = new List<SelectListItem>();
                data.Add(new SelectListItem() { Value = 0.ToString(), Text = "Odabrani Datum" });
                data.Add(new SelectListItem() { Value = 1.ToString(), Text = "+1 Dan" });
                data.Add(new SelectListItem() { Value = 2.ToString(), Text = "+2 Dana" });
                data.Add(new SelectListItem() { Value = 3.ToString(), Text = "+3 Dana" });
                data.Add(new SelectListItem() { Value = 4.ToString(), Text = "+4 Dana" });
                data.Add(new SelectListItem() { Value = 5.ToString(), Text = "+5 Dana" });
                data.Add(new SelectListItem() { Value = 6.ToString(), Text = "+6 Dana" });
                data.Add(new SelectListItem() { Value = 7.ToString(), Text = "+7 Dana" });
                return data;
            }
        }
        public List<RezervacijaInfo> Rezervacije;

        //-----------------------Stol Picker-----------------------------------------------------
        public Rezervacija rezervacija { get; set; }
        public List<Stol> listaStolova { get; set; } 
        //---------------------------------------------------------------------------------------
    }
}
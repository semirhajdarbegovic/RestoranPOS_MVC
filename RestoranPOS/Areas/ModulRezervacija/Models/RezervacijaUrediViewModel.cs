using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestoranPOS.Areas.ModulRezervacija.Models
{
    public class RezervacijaUrediViewModel
    {
        public int Id { get; set; }
        public int KorisnikId { get; set; }
        public string KorisnikUsername { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Morate unijeti na koga glasi rezervacija. Minimalno 3 karaktera")]
        public string ImeRezervacije { get; set; }
        public int BrojStola { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumRezervacije { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime VrijemeRezervacije { get; set; }
        public int BrojOsoba { get; set; }
        public bool Odobrena { get; set; }
    }
}
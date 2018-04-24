using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranPOS.Areas.ModulUposlenici.Models
{
    public class AdminUposleniciUrediViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "JMBG polje je obavezno za unos")]
        //[RegularExpression("([0-9])", ErrorMessage = "JMBG polje sadrzi samo brojeve!!")]
        [Display(Name = "JMBG")]
        public int Jmbg { get; set; }

        [Required(ErrorMessage = "Broj radne knjizice polje je obavezno za unos")]
        [Display(Name = "Broj radne knjizice")]
        public string BrRadneKnjizice { get; set; }

        [Required(ErrorMessage = "Broj ziro racuna polje je obavezno za unos")]
        [Display(Name = "Broj ziro racuna")]
        public int BrZiroRacuna { get; set; }

        [Required(ErrorMessage = "Ime polje je obavezno za unos")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime polje je obavezno za unos")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Adresa polje je obavezno za unos")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Broj telefona polje je obavezno za unos")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Ispravan format unosa bi trebao biti '000-000-000'")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Broj telefona")]
        public string BrTelefona { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za email polje!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Niste unjeli ispravn oblik email adrese!")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Datum zaposlenja polje je obavezno za unos")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Datum zaposlenja")]
        public DateTime DatumZaposlenja { get; set; }

        [Required(ErrorMessage = "Plata polje je obavezno za unos")]
        public float Plata { get; set; }

        [Required(ErrorMessage = "Broj dana godisnjeg polje je obavezno za unos")]
        [Display(Name = "Broj dana godisnjeg")]
        public int BrDanaGodisnjeg { get; set; }

        [Required(ErrorMessage = "Broj dana godisnjeg polje je obavezno za unos")]
        [Display(Name = "Opis radnog mjesta")]
        public string OpisRadnogMjesta { get; set; }

        public bool Admin { get; set; }
        public bool Menadzer { get; set; }
    }
}

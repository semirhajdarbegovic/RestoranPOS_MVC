using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranPOS.Areas.ModulUposlenici.Models
{
    public class UrediProfiViewModel
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

        
    }
}

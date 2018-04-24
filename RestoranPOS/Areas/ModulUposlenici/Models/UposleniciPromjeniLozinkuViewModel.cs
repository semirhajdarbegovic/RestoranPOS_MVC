using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranPOS.Areas.ModulUposlenici.Models
{
    public class UposleniciPromjeniLozinkuViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za lozinka polje!")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za ponovi-lozinka polje!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Neispravna lozinka")]
        public string PotvrdiPassword { get; set; }

        // Za promjenu Lozinke
        [Required(ErrorMessage = "Unos je obavezan za lozinka polje!")]
        [DataType(DataType.Password)]
        public string NovaLozinka { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za ponovi-lozinka polje!")]
        [DataType(DataType.Password)]
        [Compare("NovaLozinka", ErrorMessage = "Neispravna lozinka")]
        public string PonoviLozinku { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranPOS.Areas.ModulOnlineKorisnici.Models
{
    public class UrediViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za ime polje!")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za prezime polje!")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za adresa polje!")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za telefon polje!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Ispravan format unosa bi trebao biti '000-000-000'")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Broj telefona")]
        public string BrTelefona { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Username polje!")]
        [MinLength(3, ErrorMessage = "Username se treba sastojati od minimalno 3 karaktera")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za lozinka polje!")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za ponovi-lozinka polje!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Neispravna lozinka")]
        [Display(Name = "Potvrdi Lozinku")]
        public string PotvrdiPassword { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za email polje!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Niste unjeli ispravn oblik email adrese!")]
        [Display(Name = "E-mail")]
        public string EMail { get; set; }

        // Za promjenu Lozinke
        [Required(ErrorMessage = "Unos je obavezan za lozinka polje!")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova lozinka")]
        public string NovaLozinka { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za ponovi-lozinka polje!")]
        [DataType(DataType.Password)]
        [Compare("NovaLozinka", ErrorMessage = "Neispravna lozinka")]
        [Display(Name = "Ponovi lozinku")]
        public string PonoviLozinku { get; set; }

    }
}

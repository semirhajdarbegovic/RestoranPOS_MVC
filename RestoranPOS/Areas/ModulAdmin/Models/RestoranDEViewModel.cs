using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestoranPOS.Areas.ModulAdmin.Models
{
    public class RestoranDEViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Naziv polje!")]
        [MinLength(3, ErrorMessage = "Naziv se treba sastojati od minimalno 3 karaktera")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Adresa polje!")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Broj Telefona polje!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Ispravan format unosa bi trebao biti '000-000-000'")]
        [DataType(DataType.PhoneNumber)]
        public string BrTelefona { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za PDV Broj polje!")]
        [MinLength(12, ErrorMessage = "PDV Identifikacijski broj se sastoji od 12 znamenki")]
        [MaxLength(12, ErrorMessage = "PDV Identifikacijski broj se sastoji od 12 znamenki")]
        public string BrPDV { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Vrijednost PDV polje!")]
        public float VrijednostPDV { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Broj stolova polje!")]
        public int BrStolova { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Max Rezervacija polje!")]
        public int MaxRezervacija { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za VrijemeOtvaranja polje!")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime VrijemeOtvaranja { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za VrijemeZatvaranja polje!")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime VrijemeZatvaranja { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za VrijemeOtvaranjaKuhinje polje!")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime VrijemeOtvaranjaKuhinje { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za VrijemeZatvaranjaKuhinje polje!")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime VrijemeZatvaranjaKuhinje { get; set; }

        public bool ImaDostavu { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za InfoEmail polje!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Niste unjeli ispravn oblik email adrese!")]
        public string InfoEmail { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za BannerSlika polje!")]
        public string BannerSlika { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za OpisRestorana polje!")]
        public string OpisRestorana { get; set; }
    }
}
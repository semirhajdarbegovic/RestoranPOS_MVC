using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestoranPOS.Areas.ModulAdmin.Models
{
    public class PonudaEditModel
    {
        public int Id { get; set; }
        public int RestoranId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Naziv se treba sastojati od minimalno 5 karaktera")]
        public string NazivPonude { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VaziOd { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VaziDo { get; set; }
        [Required]
        public bool Aktivna { get; set; }
    }
}
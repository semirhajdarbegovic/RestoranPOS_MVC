using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulAdmin.Models
{
    public class KategorijaEditModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Naziv Kategorije se treba sastojati od minimalno 5 karaktera")]
        public string NazivKategorije { get; set; }
        public bool DostupnaOnline { get; set; }
        [Required]
        public int PonudaId { get; set; }

        public List<Ponuda> Ponude { private get; set; }
        public List<SelectListItem> PonudeOptions
        {
            get { return Ponude.Where(x => x.Aktivna == true && x.IsDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.NazivPonude }).ToList(); }
        }
    }
}
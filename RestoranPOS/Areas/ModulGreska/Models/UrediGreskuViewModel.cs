using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranPOS.Areas.ModulGreska.Models
{
    public class UrediGreskuViewModel
    {
        public int Id { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Polje Opis je obavezno za unos")]
        [DataType(DataType.Text)]
        public string Opis { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumPrijave { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime VrijemePrijave { get; set; }
    }
}

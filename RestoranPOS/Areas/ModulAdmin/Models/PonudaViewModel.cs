using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulAdmin.Models
{
    public class PonudaViewModel
    {
        public class PonudaInfo
        {
            public int Id { get; set; }
            public string NazivPonude { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public DateTime VaziOd { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public DateTime VaziDo { get; set; }
            public bool Aktivna { get; set; }
            public int restoranId { get; set; }
        }

        public List<PonudaInfo> Ponude;
    }
}
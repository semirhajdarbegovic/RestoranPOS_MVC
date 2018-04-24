using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulAdmin.Models
{
    public class KategorijaViewModel
    {
        public class KategorijaInfo
        {
            public int Id { get; set; }
            public string NazivKategorije { get; set; }
            public bool DostupnaOnline { get; set; }
        }

        public List<KategorijaInfo> Kategorije;
        public List<Ponuda> Ponude { private get; set; }
        public List<SelectListItem> PonudeOptions
        {
            get
            {
                List<SelectListItem> data = new List<SelectListItem>();
                data.Add(new SelectListItem() { Value = null, Text = "(Sve Ponude)" });
                data.AddRange(Ponude.Where(x => x.Aktivna == true && x.IsDeleted == false).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.NazivPonude }).ToList());
                return data;
            }
        }

        public int? PonudaId { get; set; }
    }
}
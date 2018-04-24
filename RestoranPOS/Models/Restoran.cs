using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Restoran:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string BrTelefona { get; set; }
        public string BrPdv { get; set; }
        public int BrStolova { get; set; }
        public int MaxRezervacija { get; set; }
        public bool ImaDostavu { get; set; }
        public DateTime VrijemeOtvaranja { get; set; }
        public DateTime VrijemeZatvaranja { get; set; }
        public DateTime VrijemeOtvaranjaKuhinje { get; set; }
        public DateTime VrijemeZatvaranjaKuhinje { get; set; }
        public string InfoEmail { get; set; }
        public string BannerSlika { get; set; }
        public string OpisRestorana { get; set; }
        public float VrijednostPdv { get; set; }
    }
}
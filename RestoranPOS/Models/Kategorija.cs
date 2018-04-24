using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Kategorija:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string NazivKategorije { get; set; }
        public bool DostupnaOnline { get; set; }
        public virtual Ponuda Ponuda { get; set; }
        public int PonudaId { get; set; }
    }
}
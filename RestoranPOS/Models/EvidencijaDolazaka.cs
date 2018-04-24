using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class EvidencijaDolazaka:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Datum { get; set; }
        public DateTime VrijemeDolaska { get; set; }
        public DateTime VrijemeOdlaska { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }
        public int UposlenikId { get; set; }
        public virtual ObracunskiMjesec ObracunskiMjesec { get; set; }
        public int ObracunskiMjesecId { get; set; }
    }
}
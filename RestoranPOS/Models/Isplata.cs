using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Isplata:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DatumIsplate { get; set; }
        public float Bonus { get; set; }
        public int BrojSatiRada { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }
        public int UposlenikId { get; set; }
        public virtual ObracunskiMjesec ObracunskiMjesec { get; set; }
        public int ObracunskiMjesecId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class ObracunskiMjesec:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        private DateTime Godina { get; set; }
        private DateTime Mjesec { get; set; }
    }
}
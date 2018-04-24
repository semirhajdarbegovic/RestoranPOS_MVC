using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Stol:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int BrojStola { get; set; }
        public bool Zauzet { get; set; }
    }
}
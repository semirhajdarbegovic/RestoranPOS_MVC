using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class UlazRobe:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        private string Naziv { get; set; }
        private DateTime DatumNabavke { get; set; }
        private float Cijena { get; set; }
        private string Opis { get; set; }
    }
}
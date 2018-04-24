using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class OnlineKorisnik:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public bool PotvrdjenaAdresa { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
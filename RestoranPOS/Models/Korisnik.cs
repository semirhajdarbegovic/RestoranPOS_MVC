using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoranPOS.Helper;

namespace RestoranPOS.Models
{
    public class Korisnik:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string BrTelefona { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public bool NalogAktivan { get; set; }
        public virtual Restoran Restoran { get; set; }
        public int RestoranId { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }
        public virtual OnlineKorisnik OnlineKorisnik { get; set; }
    }
}
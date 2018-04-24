using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulUposlenici.Models
{
    public class UposleniciPonudaViewModel
    {
        public class PonudaInfo
        {
            public int Id { get; set; }
            public string NazivPonude { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public DateTime VaziOd { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public DateTime VaziDo { get; set; }
            public bool Aktivna { get; set; }
            public int restoranId { get; set; }
        }

        public class ProizvodInfo
        {
            public int Id { get; set; }
            public string SifraProizvoda { get; set; }
            public string NazivProizvoda { get; set; }
            public int Kolicina { get; set; }
            public double Cijena { get; set; }
            public string OpisProizvoda { get; set; }
            public int KategorijaID { get; set; }
            public string Slika { get; set; }
        }
        public class KategorijaInfo
        {
            public int Id { get; set; }
            public string NazivKategorije { get; set; }
            public bool DosupnaOnline { get; set; }
            public int PonudaId { get; set; }
        }
        public class KorpaInfo
        {
            public int Id { get; set; }
            //  public Korisnik korisnik { get; set; }
            //  public Proizvod proizvod { get; set; }
            [DefaultValue("Proizvod")]
            public string NazivProizvoda { get; set; }
            public double CijenaProizvoda { get; set; }
            public double UkupnaCijenaProizvoda { get; set; }
            public int ProizvodId { get; set; }
            public int KorisnikId { get; set; }
            public DateTime DatumNarudzbe { get; set; }
            public DateTime VrijemeNarudzbe { get; set; }
            public int Kolicina { get; set; }
            public int StolID { get; set; }
        }

        public class StolInfo
        {
            public int Id { get; set; }
            public int BrojStola { get; set; }
            public bool Zauzet { get; set; }
        }

        public KorpaInfo Korpa;
        public List<PonudaInfo> Ponude;
        public List<ProizvodInfo> Proizvodi;
        public List<KategorijaInfo> Kategorije;
        public List<StolInfo> Stolovi;

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranPOS.Areas.ModulOnlineKorisnici.Models
{
    public class KorpaViewModel
    {
        private OKPonudaViewModel.KorpaInfo korpa = new OKPonudaViewModel.KorpaInfo();

        public OKPonudaViewModel.KorpaInfo Korpa { get { return korpa; } set { korpa = value; } }
      //  public int Kolicina { get; set; }
        private double ukupno { get; set; }
        
        public KorpaViewModel()
        { }

        public double Ukupno { get { return ukupno; } set { ukupno = value; } }
        public KorpaViewModel(OKPonudaViewModel.KorpaInfo korpaa, double cijenaUkupno)
        {
            
            this.korpa = korpaa;
            this.ukupno = cijenaUkupno;
        }


    }
}

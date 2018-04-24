using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranPOS.Areas.ModulUposlenici.Models
{
    public class UposlenikKorpaViewModel
    {
        public UposleniciPonudaViewModel.KorpaInfo Korpa { get; set; }

        private double ukupno { get; set; }
        private int stolId { get; set; }

        public UposlenikKorpaViewModel()
        { }

        public double Ukupno { get { return ukupno; } set { ukupno = value; } }
        public UposlenikKorpaViewModel(UposleniciPonudaViewModel.KorpaInfo korpaa, double cijenaUkupno, int stolId)
        {
            this.stolId = stolId;
            this.Korpa = korpaa;
            this.ukupno = cijenaUkupno;
        }
    }
}


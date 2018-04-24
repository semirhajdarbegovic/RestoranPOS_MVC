using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulAdmin.Models
{
    public class RestoranPrikaziViewModel
    {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string Adresa { get; set; }
            public string BrTelefona { get; set; }
            public string BrPDV { get; set; }
            public float VrijednostPDV { get; set; }
            public int BrStolova { get; set; }
            public int MaxRezervacija { get; set; }
            [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime VrijemeOtvaranja { get; set; }
            [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime VrijemeZatvaranja { get; set; }
            [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime VrijemeOtvaranjaKuhinje { get; set; }
            [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime VrijemeZatvaranjaKuhinje { get; set; }
            public bool ImaDostavu { get; set; }
            public string InfoEmail { get; set; }
            public string BannerSlika { get; set; }
            public string OpisRestorana { get; set; }
        }
}

/* Dio Skripte za enable picker za date ili za time
        <script type="text/javascript">
            $(function () {
                $('#VrijemeOtvaranja').datetimepicker({
                    pickDate: false,
                    use24hours: true,
                    format: "HH:mm"
                });
            });
    </script>

    Github source : https://github.com/Eonasdan/bootstrap-datetimepicker
*/

using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulOnlineKorisnici
{
    public class ModulOnlineKorisniciAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulOnlineKorisnici";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulOnlineKorisnici_default",
                "ModulOnlineKorisnici/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
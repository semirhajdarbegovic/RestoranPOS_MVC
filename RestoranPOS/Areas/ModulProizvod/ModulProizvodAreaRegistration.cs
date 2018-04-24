using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulProizvod
{
    public class ModulProizvodAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulProizvod";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulProizvod_default",
                "ModulProizvod/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulRegistracija
{
    public class ModulRegistracijaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulRegistracija";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulRegistracija_default",
                "ModulRegistracija/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
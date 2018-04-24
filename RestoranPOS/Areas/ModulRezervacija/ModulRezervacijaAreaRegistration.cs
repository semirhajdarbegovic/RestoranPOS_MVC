using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulRezervacija
{
    public class ModulRezervacijaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulRezervacija";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulRezervacija_default",
                "ModulRezervacija/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
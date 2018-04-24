using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulGreska
{
    public class ModulGreskaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulGreska";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulGreska_default",
                "ModulGreska/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
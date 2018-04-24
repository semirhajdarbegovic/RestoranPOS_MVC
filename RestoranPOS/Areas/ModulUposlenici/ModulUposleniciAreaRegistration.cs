using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulUposlenici
{
    public class ModulUposleniciAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulUposlenici";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulUposlenici_default",
                "ModulUposlenici/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
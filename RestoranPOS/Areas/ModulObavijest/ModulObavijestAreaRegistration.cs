using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulObavijest
{
    public class ModulObavijestAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulObavijest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulObavijest_default",
                "ModulObavijest/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
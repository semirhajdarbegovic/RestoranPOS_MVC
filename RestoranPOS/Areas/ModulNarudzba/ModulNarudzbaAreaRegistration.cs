using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulNarudzba
{
    public class ModulNarudzbaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulNarudzba";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulNarudzba_default",
                "ModulNarudzba/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
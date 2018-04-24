using System.Web.Mvc;

namespace RestoranPOS.Areas.ModulPonuda
{
    public class ModulPonudaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulPonuda";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulPonuda_default",
                "ModulPonuda/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
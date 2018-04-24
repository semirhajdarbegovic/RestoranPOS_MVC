using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulNarudzba.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulNarudzba.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer, KorisnickeUloge.Uposlenik)]
    public class NarudzbaController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        // GET: ModulNarudzba/Narudzba
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prikazi(int? racunId)
        {
            NarudzbaViewModel Model = new NarudzbaViewModel();
            Model.Narudzbe = (ctx.Narudzbe
                .Where(x => (!racunId.HasValue && x.IsDeleted == false) || (x.RacunId == racunId && x.IsDeleted == false))
                .Include(x => x.Korisnik)
                .Include(x => x.Racun)
                .Select(x => new NarudzbaViewModel.NarudzbaInfo
                {
                    Id = x.Id,
                    DatumNarudzbe = x.DatumNarudzbe,
                    VrijemeNarudzbe = x.VrijemeNarudzbe,
                    KorisnikId = x.KorisnikId,
                    KorisnikUsername = x.Korisnik.Username,
                    OnlineNarudzba = x.OnlineNarudzba,
                    UkupnaCijena = x.UkupnaCijena,
                    Prihvacena = x.Prihvacena,
                    RacunId = x.RacunId,
                    ListaStavki = ctx.StavkeNarudzbe.Where(s => s.NarudzbaId == x.Id).ToList()
                })).ToList();
            if(racunId.HasValue)
                return PartialView("PrikaziPartial", Model);
            return View("Prikazi", Model);
        }
    }
}
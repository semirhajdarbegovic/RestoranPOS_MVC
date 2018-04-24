using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulStol.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulStol.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Mendazer, KorisnickeUloge.Uposlenik)]
    public class RacunController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        // GET: ModulStol/Racun
        public ActionResult Index()
        {
            RacunViewModel Model = new RacunViewModel();
            Model.Racuni = (ctx.Racuni
                .Where(x => !x.IsDeleted)
                .Include(x => x.Stol)
                .Select(x => new RacunViewModel.RacunInfo
                {
                    Id = x.Id,
                    DatumRacuna = x.DatumRacuna,
                    VrijemeRacuna = x.VrijemeRacuna,
                    Stol = x.Stol,
                    Popust = x.Popust,
                    Ukupno = x.Ukupno
                })).ToList();
            /*Model.Narudzbe = (ctx.Narudzbe
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
                })).ToList();*/
            return View(Model);
        }
    }
}
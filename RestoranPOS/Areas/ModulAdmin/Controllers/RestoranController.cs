using System.EnterpriseServices;
using System.Linq;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulAdmin.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Migrations;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulAdmin.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class RestoranController : Controller
    {
        RestoranContext ctx = new RestoranContext();
        public ActionResult Prikazi()
        {

            //projera ima li ijedan zapis u bazi ako ima edituje se postojeci i uvijek se uzima prvi i edituje nema potrebe imati vise zapisa za postavku
            if (ctx.Restorani.Any(o => o.Id != null))
            {
                var x = ctx.Restorani.First();
                RestoranPrikaziViewModel Model = new RestoranPrikaziViewModel()
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Adresa = x.Adresa,
                    BrTelefona = x.BrTelefona,
                    BannerSlika = x.BannerSlika,
                    BrPDV = x.BrPdv,
                    BrStolova = x.BrStolova,
                    InfoEmail = x.InfoEmail,
                    MaxRezervacija = x.MaxRezervacija,
                    OpisRestorana = x.OpisRestorana,
                    VrijemeOtvaranja = x.VrijemeOtvaranja,
                    VrijemeZatvaranja = x.VrijemeZatvaranja,
                    VrijemeOtvaranjaKuhinje = x.VrijemeOtvaranjaKuhinje,
                    VrijemeZatvaranjaKuhinje = x.VrijemeZatvaranjaKuhinje,
                    VrijednostPDV = x.VrijednostPdv,
                    ImaDostavu = x.ImaDostavu
                };
                return View("Prikazi", Model);
            }
            else
            {
                RestoranDEViewModel Model = new RestoranDEViewModel();
                return View("Dodaj", Model);
            }
        }

        public ActionResult Uredi(int restoranId)
        {
            Restoran restoran = ctx.Restorani.Where(x => x.Id == restoranId).FirstOrDefault();
            RestoranDEViewModel Model = new RestoranDEViewModel();
            //ako je restoranId == 0 znaci da nema prethodno dodavanih postavki restorana pa je potrebno kreirati novi element jer dolazi do null exc...
            if (restoranId == 0)
            {
                restoran = new Restoran();
            }
            else
            {
            Model.Id = restoran.Id;
            Model.Naziv = restoran.Naziv;
            Model.Adresa = restoran.Adresa;
            Model.BrTelefona = restoran.BrTelefona;
            Model.BannerSlika = restoran.BannerSlika;
            Model.BrPDV = restoran.BrPdv;
            Model.BrStolova = restoran.BrStolova;
            Model.InfoEmail = restoran.InfoEmail;
            Model.MaxRezervacija = restoran.MaxRezervacija;
            Model.OpisRestorana = restoran.OpisRestorana;
            Model.VrijemeOtvaranja = restoran.VrijemeOtvaranja;
            Model.VrijemeZatvaranja = restoran.VrijemeZatvaranja;
            Model.VrijemeOtvaranjaKuhinje = restoran.VrijemeOtvaranjaKuhinje;
            Model.VrijemeZatvaranjaKuhinje = restoran.VrijemeZatvaranjaKuhinje;
            Model.VrijednostPDV = restoran.VrijednostPdv;
            Model.ImaDostavu = restoran.ImaDostavu;
            }

            return View("Uredi", Model);
        }
        //dodaj je ustvari edit
        public ActionResult Dodaj()
        {
            RestoranDEViewModel Model = new RestoranDEViewModel();
            return View("Dodaj", Model);
        }
        //nema potrebe za kreiranjem funkcije obrisi jer se nemoze obrisati postavka restorana o kojoj sve ostalo zavisi jer postoji samo jedna
        /*public ActionResult Obrisi(int restoranId)
        {
            Restoran x = ctx.Restorani.Find(restoranId);
            ctx.Restorani.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }*/
        public ActionResult Spremi(RestoranDEViewModel restoran)
        {
            if (!ModelState.IsValid)
                return View("Uredi", restoran);

            Restoran restoranDB;
            if (restoran.Id == 0)
            {
                restoranDB = new Restoran();
                ctx.Restorani.Add(restoranDB);
            }
            else
            {
                restoranDB = ctx.Restorani.Where(x => x.Id == restoran.Id).FirstOrDefault();
            }
            restoranDB.Id = restoran.Id;
            restoranDB.Naziv = restoran.Naziv;
            restoranDB.Adresa = restoran.Adresa;
            restoranDB.BrTelefona = restoran.BrTelefona;
            restoranDB.BannerSlika = restoran.BannerSlika;
            restoranDB.BrPdv= restoran.BrPDV;
            restoranDB.BrStolova = restoran.BrStolova;
            restoranDB.InfoEmail = restoran.InfoEmail;
            restoranDB.MaxRezervacija = restoran.MaxRezervacija;
            restoranDB.OpisRestorana = restoran.OpisRestorana;
            restoranDB.VrijemeOtvaranja = restoran.VrijemeOtvaranja;
            restoranDB.VrijemeZatvaranja = restoran.VrijemeZatvaranja;
            restoranDB.VrijemeOtvaranjaKuhinje = restoran.VrijemeOtvaranjaKuhinje;
            restoranDB.VrijemeZatvaranjaKuhinje = restoran.VrijemeZatvaranjaKuhinje;
            restoranDB.VrijednostPdv = restoran.VrijednostPDV;
            restoranDB.ImaDostavu = restoran.ImaDostavu;

            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoranPOS.Areas.ModulRegistracija.Models;
using RestoranPOS.DAL;
using RestoranPOS.Helper;
using RestoranPOS.Models;

namespace RestoranPOS.Areas.ModulRegistracija.Controllers
{
    public class RegistracijaController : Controller
    {
        // Komunikacija sa bazom
        private RestoranContext ctx = new RestoranContext();

        // GET: ModulRegistracija/Registracija
        //Vraća izgled forme za ragistraciju
        public ActionResult Registracija()
        {
            return View();
        }

        // POST : ModulRegistracija/Registracija
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registracija(RegistracijaViewModel korisnik)
        {
            if (ModelState.IsValid)
            {
                // provjerava da li postoji korisnik sa unesenim username ili email-adresom i ako postoji
                // upozorava korisnika da ne moze unjeti isti username ili password..
                bool nastavi = true; 
                foreach (var x in ctx.Korisnici)
                {
                    if (korisnik.EMail == x.EMail)
                    {
                        ViewBag.EmailPostoji = "Email adresa je vec postoji u bazi podataka!";
                        nastavi = false;
                    }
                    if (korisnik.Username == x.Username)
                    {
                        ViewBag.UsernamePostoji = "Username vec postoji u bazi podataka!";
                    }
                }
                if (!nastavi)
                    return View();

                // Kreiramo novi objekat tipa Korisnik i OnlineKorisnik i unjeg pohranjujemo podatke koje smo preuzeli 
                // sa web forme..
                OnlineKorisnik OlKorisnik = new OnlineKorisnik();
                OlKorisnik.Korisnik = new Korisnik();

                OlKorisnik.PotvrdjenaAdresa = false;
                OlKorisnik.DatumRegistracije = DateTime.Now;
                OlKorisnik.Korisnik.Adresa = korisnik.Adresa;
                OlKorisnik.Korisnik.BrTelefona = korisnik.BrTelefona;
                OlKorisnik.Korisnik.EMail = korisnik.EMail;
                OlKorisnik.Korisnik.Ime = korisnik.Ime;
                OlKorisnik.Korisnik.NalogAktivan = false;
                //OlKorisnik.Korisnik.Password = MD5Hash.GetMD5Hash(korisnik.Password);
                OlKorisnik.Korisnik.Password = korisnik.Password;
                OlKorisnik.Korisnik.Prezime = korisnik.Prezime;
                OlKorisnik.Korisnik.Username = korisnik.Username;
                OlKorisnik.Korisnik.RestoranId = ctx.Restorani.First().Id;

                ctx.OnlineKorisnici.Add(OlKorisnik);
                ctx.SaveChanges();

                // Služi za slanje aktivacijskog linka korisnuku na email adresu koju unese 
                // adresu sa koje se salje e-mail smo unjeli u postavke rucno.. 
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress("test.restoran@outlook.com", "RestoranPOS aktivacija"),
                        new System.Net.Mail.MailAddress(korisnik.EMail));
                m.Subject = "RestoranPOS aktivacija";
                m.Body = string.Format("Poštovani korisniče," +
                                       "  {0}<BR/>Hvala Vam na registraciji, molimo Vas da potvrdite i aktivirate Vaš nalog tako što" +
                                       " ćete kliknuti na sljedeći link :  <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>",
                                       korisnik.Ime + " " + korisnik.Prezime, Url.Action("PotvrdiEmail", "Registracija", 
                                       new { Token = OlKorisnik.Korisnik.Id, Email = korisnik.EMail }, Request.Url.Scheme));
                m.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.live.com");
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("test.restoran@outlook.com", "Aa@336699556611");
                smtp.EnableSsl = true;
                smtp.Send(m);
                return RedirectToAction("Potvrdi", "Registracija", new { Email = korisnik.EMail });
            }
            return View();
        }

        //Otvara se nakon sto korisnik unese ispravne podatke za kreiranje akaunta
        public ActionResult Potvrdi(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }

        // GET: /Account/ConfirmEmail
        // Nakon što korisnik klikne na link koji je dobio putem e-maila, poziva ovu akciju..
        public ActionResult PotvrdiEmail(string Token, string Email)
        {
            Korisnik korisnik = ctx.Korisnici.Find(Int32.Parse(Token));
            if (korisnik != null)
            {
                if (korisnik.EMail == Email)
                {
                    korisnik.NalogAktivan = true;
                    ctx.Korisnici.Find(Int32.Parse(Token)).NalogAktivan = true;
                    ctx.SaveChanges();
                    return View();
                }
                else
                {
                    return RedirectToAction("Potvrdi", "Registracija", new { Email = korisnik.EMail });
                }
            }
            else
            {
                return RedirectToAction("Potvrdi", "Registracija", new { Email = "" });
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using RestoranPOS.Models;

namespace RestoranPOS.DAL
{
    public class RestoranContext:DbContext
    {
        public RestoranContext()
            : base("Name=RestoranDBString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //one-to-(zero or one)
            modelBuilder.Entity<Korisnik>().HasOptional(x => x.OnlineKorisnik).WithRequired(x => x.Korisnik);
            modelBuilder.Entity<Korisnik>().HasOptional(x => x.Uposlenik).WithRequired(x => x.Korisnik);

            //many-to-one
            //modelBuilder.Entity<Smjer>().HasRequired(x => x.Fakultet).WithMany().HasForeignKey(x=>x.FakultetId);
            //modelBuilder.Entity<UpisGodine>().HasRequired(x => x.Student).WithMany().HasForeignKey(x=>x.StudentId);
            //modelBuilder.Entity<UpisGodine>().HasRequired(x => x.AkademskaGodina).WithMany().HasForeignKey(x=>x.AkademskaGodinaId);
            //http://blogs.msdn.com/b/adonet/archive/2010/12/14/ef-feature-ctp5-fluent-api-samples.aspx
        }

        public DbSet<EvidencijaDolazaka> Dolasci { set; get; }
        public DbSet<Greska> Greske { set; get; }
        public DbSet<Isplata> Isplate { set; get; }
        public DbSet<Kategorija> Kategorije { set; get; }
        public DbSet<Korisnik> Korisnici { set; get; }
        public DbSet<Narudzba> Narudzbe { set; get; }
        public DbSet<Novost> Novosti { set; get; }
        public DbSet<Obavijest> Obavijesti { set; get; }
        public DbSet<ObracunskiMjesec> ObracunskiMjeseci { set; get; }
        public DbSet<OnlineKorisnik> OnlineKorisnici { set; get; }
        public DbSet<Ponuda> Ponude { set; get; }
        public DbSet<Proizvod> Proizvodi { set; get; }
        public DbSet<Racun> Racuni { set; get; }
        public DbSet<Restoran> Restorani { set; get; }
        public DbSet<Rezervacija> Rezervacije { set; get; }
        public DbSet<StavkaNarudzbe> StavkeNarudzbe { set; get; }
        public DbSet<StavkaUlaza> StavkeUlaza { set; get; }
        public DbSet<Stol> Stolovi { set; get; }
        public DbSet<UlazRobe> UlaziRobe { set; get; }
        public DbSet<Uposlenik> Uposlenici { set; get; }

        public System.Data.Entity.DbSet<RestoranPOS.Areas.ModulOnlineKorisnici.Models.PrikaziViewModel> PrikaziViewModels { get; set; }

        public System.Data.Entity.DbSet<RestoranPOS.Areas.ModulAdmin.Models.RestoranPrikaziViewModel> RestoranPrikaziViewModels { get; set; }

        public System.Data.Entity.DbSet<RestoranPOS.Areas.ModulAdmin.Models.RestoranDEViewModel> RestoranDEViewModels { get; set; }
    }
}
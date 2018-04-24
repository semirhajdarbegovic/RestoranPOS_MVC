namespace RestoranPOS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestoranPOS.DAL.RestoranContext>
    {
        public Configuration()
        {
           // AutomaticMigrationsEnabled = true;
           // AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RestoranPOS.DAL.RestoranContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

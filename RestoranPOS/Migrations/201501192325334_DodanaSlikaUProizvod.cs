namespace RestoranPOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanaSlikaUProizvod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proizvods", "SlikaProizvoda", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Proizvods", "SlikaProizvoda");
        }
    }
}

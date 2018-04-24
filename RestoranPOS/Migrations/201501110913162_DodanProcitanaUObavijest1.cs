namespace RestoranPOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanProcitanaUObavijest1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Obavijests", "Procitana", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Obavijests", "Procitana");
        }
    }
}

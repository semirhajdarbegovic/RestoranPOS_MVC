namespace RestoranPOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _41AdminRestoranAddEdit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestoranDEViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false),
                        Adresa = c.String(nullable: false),
                        BrTelefona = c.String(nullable: false),
                        BrPDV = c.String(nullable: false, maxLength: 12),
                        VrijednostPDV = c.Single(nullable: false),
                        BrStolova = c.Int(nullable: false),
                        MaxRezervacija = c.Int(nullable: false),
                        VrijemeOtvaranja = c.DateTime(nullable: false),
                        VrijemeZatvaranja = c.DateTime(nullable: false),
                        VrijemeOtvaranjaKuhinje = c.DateTime(nullable: false),
                        VrijemeZatvaranjaKuhinje = c.DateTime(nullable: false),
                        ImaDostavu = c.Boolean(nullable: false),
                        InfoEmail = c.String(nullable: false),
                        BannerSlika = c.String(nullable: false),
                        OpisRestorana = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RestoranDEViewModels");
        }
    }
}

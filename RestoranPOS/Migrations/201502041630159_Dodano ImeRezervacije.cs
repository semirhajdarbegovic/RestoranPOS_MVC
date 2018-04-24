namespace RestoranPOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanoImeRezervacije : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rezervacijas", "ImeRezervacije", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rezervacijas", "ImeRezervacije");
        }
    }
}

namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrentFotoError : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fotoes", "City_CountryId", "dbo.Countries");
            DropIndex("dbo.Fotoes", new[] { "City_CountryId" });
            DropColumn("dbo.Fotoes", "City_CountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fotoes", "City_CountryId", c => c.Int());
            CreateIndex("dbo.Fotoes", "City_CountryId");
            AddForeignKey("dbo.Fotoes", "City_CountryId", "dbo.Countries", "CountryId");
        }
    }
}

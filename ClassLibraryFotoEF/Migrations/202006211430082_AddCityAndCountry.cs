namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityAndCountry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            AddColumn("dbo.Fotoes", "CityId", c => c.Int());
            AddColumn("dbo.Fotoes", "City_CountryId", c => c.Int());
            CreateIndex("dbo.Fotoes", "CityId");
            CreateIndex("dbo.Fotoes", "City_CountryId");
            AddForeignKey("dbo.Fotoes", "CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.Fotoes", "City_CountryId", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fotoes", "City_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Fotoes", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Fotoes", new[] { "City_CountryId" });
            DropIndex("dbo.Fotoes", new[] { "CityId" });
            DropColumn("dbo.Fotoes", "City_CountryId");
            DropColumn("dbo.Fotoes", "CityId");
            DropTable("dbo.Cities");
            DropTable("dbo.Countries");
        }
    }
}

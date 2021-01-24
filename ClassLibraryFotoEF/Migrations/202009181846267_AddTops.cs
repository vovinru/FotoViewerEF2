namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTops : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FotoTops",
                c => new
                    {
                        FotoTopId = c.Int(nullable: false, identity: true),
                        OrderPlace = c.Int(nullable: false),
                        Raiting = c.Int(nullable: false),
                        CountGame = c.Int(nullable: false),
                        CountWin = c.Int(nullable: false),
                        TopId = c.Int(),
                        FotoId = c.Int(),
                    })
                .PrimaryKey(t => t.FotoTopId)
                .ForeignKey("dbo.Fotoes", t => t.FotoId)
                .ForeignKey("dbo.Tops", t => t.TopId)
                .Index(t => t.TopId)
                .Index(t => t.FotoId);
            
            CreateTable(
                "dbo.Tops",
                c => new
                    {
                        TopId = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TopId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FotoTops", "TopId", "dbo.Tops");
            DropForeignKey("dbo.FotoTops", "FotoId", "dbo.Fotoes");
            DropIndex("dbo.FotoTops", new[] { "FotoId" });
            DropIndex("dbo.FotoTops", new[] { "TopId" });
            DropTable("dbo.Tops");
            DropTable("dbo.FotoTops");
        }
    }
}

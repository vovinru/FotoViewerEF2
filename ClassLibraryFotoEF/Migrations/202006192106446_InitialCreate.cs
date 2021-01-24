namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fotoes",
                c => new
                    {
                        FotoId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        CountWin = c.Int(nullable: false),
                        CountLose = c.Int(nullable: false),
                        CountPenalty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FotoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fotoes");
        }
    }
}

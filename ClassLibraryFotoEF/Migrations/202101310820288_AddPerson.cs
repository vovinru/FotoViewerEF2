namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPerson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.PersonFotoes",
                c => new
                    {
                        Person_PersonId = c.Int(nullable: false),
                        Foto_FotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_PersonId, t.Foto_FotoId })
                .ForeignKey("dbo.People", t => t.Person_PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Fotoes", t => t.Foto_FotoId, cascadeDelete: true)
                .Index(t => t.Person_PersonId)
                .Index(t => t.Foto_FotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonFotoes", "Foto_FotoId", "dbo.Fotoes");
            DropForeignKey("dbo.PersonFotoes", "Person_PersonId", "dbo.People");
            DropIndex("dbo.PersonFotoes", new[] { "Foto_FotoId" });
            DropIndex("dbo.PersonFotoes", new[] { "Person_PersonId" });
            DropTable("dbo.PersonFotoes");
            DropTable("dbo.People");
        }
    }
}

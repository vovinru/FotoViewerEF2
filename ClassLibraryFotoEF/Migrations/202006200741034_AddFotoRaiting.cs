namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFotoRaiting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotoes", "Raiting", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fotoes", "Raiting");
        }
    }
}

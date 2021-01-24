namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFotoRotate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotoes", "Rotate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fotoes", "Rotate");
        }
    }
}

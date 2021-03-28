namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotPersonToFoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotoes", "NotPersons", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fotoes", "NotPersons");
        }
    }
}

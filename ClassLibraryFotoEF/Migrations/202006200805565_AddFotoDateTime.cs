namespace ClassLibraryFotoEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFotoDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotoes", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fotoes", "Date");
        }
    }
}

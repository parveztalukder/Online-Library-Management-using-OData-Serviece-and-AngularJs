namespace MyLibraryApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class der1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Subscribers", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscribers", "Total", c => c.Int(nullable: false));
        }
    }
}

namespace DataAccessLayer.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Room", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Room", "Price", c => c.Single(nullable: false));
        }
    }
}

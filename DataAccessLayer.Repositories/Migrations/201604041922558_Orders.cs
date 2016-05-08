namespace DataAccessLayer.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CountOfDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "CountOfDays");
        }
    }
}

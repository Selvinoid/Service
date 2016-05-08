namespace DataAccessLayer.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Condition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Adress = c.String(),
                        Stars = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        PersonCount = c.Int(nullable: false),
                        Description = c.String(),
                        Price = c.Single(nullable: false),
                        IdHotel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotel", t => t.IdHotel, cascadeDelete: true)
                .Index(t => t.IdHotel);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        IdHotel = c.Int(nullable: false),
                        IdRoom = c.Int(nullable: false),
                        ArrivalDate = c.DateTime(nullable: false),
                        LeaveDate = c.DateTime(nullable: false),
                        TotalPrice = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.IdRoom, cascadeDelete: true)
                .ForeignKey("Security.Users", t => t.IdUser, cascadeDelete: true)
                .ForeignKey("dbo.Hotel", t => t.IdHotel)
                .Index(t => t.IdUser)
                .Index(t => t.IdHotel)
                .Index(t => t.IdRoom);
            
            CreateTable(
                "Security.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Security.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.UserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Security.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("Security.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Security.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotel_Condition",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        ConditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HotelId, t.ConditionId })
                .ForeignKey("dbo.Hotel", t => t.HotelId, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.ConditionId, cascadeDelete: true)
                .Index(t => t.HotelId)
                .Index(t => t.ConditionId);
            
            CreateTable(
                "dbo.Room_Image",
                c => new
                    {
                        RoomId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoomId, t.ImageId })
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Image", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Hotel_Image",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HotelId, t.ImageId })
                .ForeignKey("dbo.Hotel", t => t.HotelId, cascadeDelete: true)
                .ForeignKey("dbo.Image", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.HotelId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Condition_Image",
                c => new
                    {
                        ConditionId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConditionId, t.ImageId })
                .ForeignKey("dbo.Condition", t => t.ConditionId, cascadeDelete: true)
                .ForeignKey("dbo.Image", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.ConditionId)
                .Index(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.UserRoles", "RoleId", "Security.Roles");
            DropForeignKey("dbo.Condition_Image", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Condition_Image", "ConditionId", "dbo.Condition");
            DropForeignKey("dbo.Room", "IdHotel", "dbo.Hotel");
            DropForeignKey("dbo.Order", "IdHotel", "dbo.Hotel");
            DropForeignKey("dbo.Hotel_Image", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Hotel_Image", "HotelId", "dbo.Hotel");
            DropForeignKey("dbo.Order", "IdUser", "Security.Users");
            DropForeignKey("Security.UserRoles", "UserId", "Security.Users");
            DropForeignKey("Security.UserLogins", "UserId", "Security.Users");
            DropForeignKey("Security.UserClaims", "UserId", "Security.Users");
            DropForeignKey("dbo.Order", "IdRoom", "dbo.Room");
            DropForeignKey("dbo.Room_Image", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Room_Image", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Hotel_Condition", "ConditionId", "dbo.Condition");
            DropForeignKey("dbo.Hotel_Condition", "HotelId", "dbo.Hotel");
            DropIndex("dbo.Condition_Image", new[] { "ImageId" });
            DropIndex("dbo.Condition_Image", new[] { "ConditionId" });
            DropIndex("dbo.Hotel_Image", new[] { "ImageId" });
            DropIndex("dbo.Hotel_Image", new[] { "HotelId" });
            DropIndex("dbo.Room_Image", new[] { "ImageId" });
            DropIndex("dbo.Room_Image", new[] { "RoomId" });
            DropIndex("dbo.Hotel_Condition", new[] { "ConditionId" });
            DropIndex("dbo.Hotel_Condition", new[] { "HotelId" });
            DropIndex("Security.UserRoles", new[] { "RoleId" });
            DropIndex("Security.UserRoles", new[] { "UserId" });
            DropIndex("Security.UserLogins", new[] { "UserId" });
            DropIndex("Security.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Order", new[] { "IdRoom" });
            DropIndex("dbo.Order", new[] { "IdHotel" });
            DropIndex("dbo.Order", new[] { "IdUser" });
            DropIndex("dbo.Room", new[] { "IdHotel" });
            DropTable("dbo.Condition_Image");
            DropTable("dbo.Hotel_Image");
            DropTable("dbo.Room_Image");
            DropTable("dbo.Hotel_Condition");
            DropTable("Security.Roles");
            DropTable("Security.UserRoles");
            DropTable("Security.UserLogins");
            DropTable("Security.UserClaims");
            DropTable("Security.Users");
            DropTable("dbo.Order");
            DropTable("dbo.Room");
            DropTable("dbo.Image");
            DropTable("dbo.Hotel");
            DropTable("dbo.Condition");
        }
    }
}

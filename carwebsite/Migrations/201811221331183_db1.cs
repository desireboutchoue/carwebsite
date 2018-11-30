namespace carwebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingDetails",
                c => new
                    {
                        BookingDetailId = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BookingDetailId)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.BookingId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 160),
                        LastName = c.String(nullable: false, maxLength: 160),
                        Address = c.String(nullable: false, maxLength: 70),
                        Phone = c.String(nullable: false, maxLength: 24),
                        Status = c.String(),
                        Email = c.String(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BookingId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        CarTypesId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 160),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarUrl = c.String(maxLength: 1024),
                        CarUrl2 = c.String(maxLength: 1024),
                        CarUrl3 = c.String(maxLength: 1024),
                        Class = c.String(nullable: false),
                        Fuel = c.String(nullable: false),
                        Doors = c.Int(nullable: false),
                        GearBox = c.String(nullable: false),
                        Type = c.String(),
                        Description = c.String(),
                        PointurListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.CarTypes", t => t.CarTypesId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.CarTypesId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.CarTypes",
                c => new
                    {
                        CarTypesId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.CarTypesId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        CarId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "CarTypesId", "dbo.CarTypes");
            DropForeignKey("dbo.Cars", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.BookingDetails", "CarId", "dbo.Cars");
            DropForeignKey("dbo.BookingDetails", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Carts", new[] { "CarId" });
            DropIndex("dbo.Cars", new[] { "CarTypesId" });
            DropIndex("dbo.Cars", new[] { "BrandId" });
            DropIndex("dbo.BookingDetails", new[] { "CarId" });
            DropIndex("dbo.BookingDetails", new[] { "BookingId" });
            DropTable("dbo.Carts");
            DropTable("dbo.CarTypes");
            DropTable("dbo.Brands");
            DropTable("dbo.Cars");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookingDetails");
        }
    }
}

namespace MyLibraryApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.SBooks", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ShippedDate = c.DateTime(),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        ShippingAddress = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        TransactionId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.SBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false, maxLength: 500),
                        Category = c.String(nullable: false, maxLength: 30),
                        PictureFile = c.String(),
                        Picture = c.String(),
                        Stocklevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockIns",
                c => new
                    {
                        StockInId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockInId)
                .ForeignKey("dbo.SBooks", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.SBooks");
            DropForeignKey("dbo.StockIns", "ProductId", "dbo.SBooks");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.StockIns", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropTable("dbo.StockIns");
            DropTable("dbo.SBooks");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}

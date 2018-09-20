namespace MyLibraryApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.StockIns", name: "ProductId", newName: "SBookId");
            RenameColumn(table: "dbo.OrderDetails", name: "ProductId", newName: "SBookId");
            RenameIndex(table: "dbo.OrderDetails", name: "IX_ProductId", newName: "IX_SBookId");
            RenameIndex(table: "dbo.StockIns", name: "IX_ProductId", newName: "IX_SBookId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.StockIns", name: "IX_SBookId", newName: "IX_ProductId");
            RenameIndex(table: "dbo.OrderDetails", name: "IX_SBookId", newName: "IX_ProductId");
            RenameColumn(table: "dbo.OrderDetails", name: "SBookId", newName: "ProductId");
            RenameColumn(table: "dbo.StockIns", name: "SBookId", newName: "ProductId");
        }
    }
}

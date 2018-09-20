namespace MyLibraryApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class der : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        BookAuth = c.String(),
                        AvilableBook = c.Int(nullable: false),
                        IssueBook = c.Int(nullable: false),
                        RackId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        BookStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Racks", t => t.RackId, cascadeDelete: true)
                .Index(t => t.RackId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Racks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RackName = c.String(),
                        RackSatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Finehis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        BookId = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        BookId = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IssueBookHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        BookId = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IssueBookReqs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FatherName = c.String(),
                        MotehrName = c.String(),
                        Age = c.String(),
                        Gender = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        ParmanentAddress = c.String(),
                        PresentAddress = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IssueBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        IssueDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurId = c.Int(nullable: false, identity: true),
                        PurDate = c.DateTime(nullable: false),
                        PurFrom = c.String(),
                        PurNo = c.Int(nullable: false),
                        PurchaseBookName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.PurId);
            
            CreateTable(
                "dbo.RequestBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        url = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "img", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssueBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.IssueBookReqs", "MemberId", "dbo.Members");
            DropForeignKey("dbo.IssueBookReqs", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "RackId", "dbo.Racks");
            DropForeignKey("dbo.Books", "CategoryId", "dbo.Categories");
            DropIndex("dbo.IssueBooks", new[] { "BookId" });
            DropIndex("dbo.IssueBookReqs", new[] { "BookId" });
            DropIndex("dbo.IssueBookReqs", new[] { "MemberId" });
            DropIndex("dbo.Books", new[] { "CategoryId" });
            DropIndex("dbo.Books", new[] { "RackId" });
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "img");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.RequestBooks");
            DropTable("dbo.Purchases");
            DropTable("dbo.IssueBooks");
            DropTable("dbo.Members");
            DropTable("dbo.IssueBookReqs");
            DropTable("dbo.IssueBookHistories");
            DropTable("dbo.Fines");
            DropTable("dbo.Finehis");
            DropTable("dbo.Racks");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
        }
    }
}

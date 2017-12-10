namespace AMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsBranchToMerchant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountState",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        State = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusinessHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MerchantId = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        OpenTime = c.String(nullable: false, unicode: false),
                        CloseTime = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchant", t => t.MerchantId, cascadeDelete: true)
                .Index(t => t.MerchantId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameEN = c.String(nullable: false, unicode: false),
                        NameAR = c.String(nullable: false, unicode: false),
                        CountryId = c.Int(nullable: false),
                        ImageUrl = c.String(nullable: false, unicode: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        NameAR = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Mobile = c.String(nullable: false, unicode: false),
                        MerchantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Mobile = c.String(nullable: false, unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        Address = c.String(nullable: false, unicode: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Barcode = c.String(unicode: false),
                        NameEN = c.String(unicode: false),
                        NameAR = c.String(unicode: false),
                        SubCategoryId = c.Int(nullable: false),
                        SubCategoryName = c.String(unicode: false),
                        Volume = c.String(unicode: false),
                        UnitId = c.Int(nullable: false),
                        UnitName = c.String(unicode: false),
                        ImageUrl = c.String(unicode: false),
                        CreationDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameEN = c.String(nullable: false, unicode: false),
                        NameAR = c.String(nullable: false, unicode: false),
                        SubCategoryId = c.Int(nullable: false),
                        Volume = c.String(nullable: false, unicode: false),
                        UnitId = c.Int(nullable: false),
                        ImageUrl = c.String(unicode: false),
                        Barcode = c.String(nullable: false, unicode: false),
                        CreationDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemUnit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, unicode: false),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        FullName = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MerchantCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        MerchantId = c.Int(nullable: false),
                        CreationDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MerchantProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MerchantId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SubCategoryId = c.Int(),
                        CreationDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Merchant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameEN = c.String(nullable: false, unicode: false),
                        NameAR = c.String(nullable: false, unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        SmsCode = c.String(nullable: false, unicode: false),
                        Phone = c.String(nullable: false, unicode: false),
                        CountryId = c.Int(nullable: false),
                        Notes = c.String(unicode: false),
                        AccountStateId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        LangId = c.Int(nullable: false),
                        LogoUrl = c.String(unicode: false),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        DeliveryTime = c.Int(nullable: false),
                        ServiceFees = c.Double(nullable: false),
                        Tax = c.Double(nullable: false),
                        MinOrder = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
                        IsBranch = c.Int(nullable: false),
                        CreationDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MerchantServingArea",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                        MerchantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchant", t => t.MerchantId, cascadeDelete: true)
                .Index(t => t.MerchantId);
            
            CreateTable(
                "dbo.MerchantSubCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        MerchantId = c.Int(nullable: false),
                        CreationDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MerchantSupervisor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Mobile = c.String(nullable: false, unicode: false),
                        MerchantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(unicode: false),
                        UUID = c.String(unicode: false),
                        UserId = c.Int(nullable: false),
                        MerchantId = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        CourierName = c.String(unicode: false),
                        Total = c.Double(nullable: false),
                        GrandTotal = c.Double(nullable: false),
                        Tip = c.Double(nullable: false),
                        Notes = c.String(unicode: false),
                        IsSchedule = c.Boolean(nullable: false),
                        StartDateTime = c.DateTime(nullable: false, precision: 0),
                        ScheduleDateTime = c.DateTime(nullable: false, precision: 0),
                        PaymentMethod = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.ReportingIssues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        merchantId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Text = c.String(unicode: false),
                        StatusId = c.Int(nullable: false),
                        CreationDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameEN = c.String(nullable: false, unicode: false),
                        NameAR = c.String(nullable: false, unicode: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SuggestProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ProductName = c.String(unicode: false),
                        MerchantId = c.Int(nullable: false),
                        CreationDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UUID = c.String(unicode: false),
                        DeviceToken = c.String(unicode: false),
                        CountryId = c.Int(nullable: false),
                        Mobile = c.String(unicode: false),
                        SmsCode = c.String(unicode: false),
                        Verified = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.MerchantServingArea", "MerchantId", "dbo.Merchant");
            DropForeignKey("dbo.BusinessHours", "MerchantId", "dbo.Merchant");
            DropIndex("dbo.OrderProducts", new[] { "Order_Id" });
            DropIndex("dbo.MerchantServingArea", new[] { "MerchantId" });
            DropIndex("dbo.BusinessHours", new[] { "MerchantId" });
            DropTable("dbo.User");
            DropTable("dbo.SuggestProduct");
            DropTable("dbo.SubCategory");
            DropTable("dbo.Role");
            DropTable("dbo.ReportingIssues");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Order");
            DropTable("dbo.MerchantSupervisor");
            DropTable("dbo.MerchantSubCategory");
            DropTable("dbo.MerchantServingArea");
            DropTable("dbo.Merchant");
            DropTable("dbo.MerchantProduct");
            DropTable("dbo.MerchantCategories");
            DropTable("dbo.Language");
            DropTable("dbo.ItemUnit");
            DropTable("dbo.Item");
            DropTable("dbo.ItemDetails");
            DropTable("dbo.Employee");
            DropTable("dbo.EmployeeRole");
            DropTable("dbo.Currency");
            DropTable("dbo.Courier");
            DropTable("dbo.Country");
            DropTable("dbo.Category");
            DropTable("dbo.BusinessHours");
            DropTable("dbo.AccountState");
        }
    }
}

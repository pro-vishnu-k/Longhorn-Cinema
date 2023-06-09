namespace LonghornCinemaFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardID = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(maxLength: 16),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CreditCardID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        PopcornPointsBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        MovieReview_MovieReviewID = c.Int(),
                        MovieReview_MovieReviewID1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieReviews", t => t.MovieReview_MovieReviewID)
                .ForeignKey("dbo.MovieReviews", t => t.MovieReview_MovieReviewID1)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.MovieReview_MovieReviewID)
                .Index(t => t.MovieReview_MovieReviewID1);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MovieReviews",
                c => new
                    {
                        MovieReviewID = c.Int(nullable: false, identity: true),
                        ReviewText = c.String(maxLength: 100),
                        NumStars = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApprovalStatus = c.Int(nullable: false),
                        Votes = c.Int(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                        Movie_MovieID = c.Int(),
                        AppUser_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MovieReviewID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id1)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Movie_MovieID)
                .Index(t => t.AppUser_Id1);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Overview = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Revenue = c.Long(nullable: false),
                        Runtime = c.Int(nullable: false),
                        Tagline = c.String(),
                        Actors = c.String(),
                        MPAARating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Showings",
                c => new
                    {
                        ShowingID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        SpecialEventStatus = c.Int(nullable: false),
                        TheatreNum = c.Int(nullable: false),
                        Movie_MovieID = c.Int(),
                    })
                .PrimaryKey(t => t.ShowingID)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .Index(t => t.Movie_MovieID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        Seat = c.String(),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SeniorCitizenDiscount = c.String(),
                        EarlyDiscount = c.String(),
                        Order_OrderID = c.Int(),
                        Showing_ShowingID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.Showings", t => t.Showing_ShowingID)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Showing_ShowingID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        ConfirmationCode = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                        CreditCard_CreditCardID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.CreditCards", t => t.CreditCard_CreditCardID)
                .Index(t => t.AppUser_Id)
                .Index(t => t.CreditCard_CreditCardID);
            
            CreateTable(
                "dbo.MoviePrices",
                c => new
                    {
                        MoviePriceID = c.Int(nullable: false, identity: true),
                        decMatineePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        decTuesdayPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        decWeekendPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        decWeekPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MoviePriceID);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportID = c.Int(nullable: false, identity: true),
                        DisplaySeats = c.Boolean(nullable: false),
                        DisplayRevenue = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        RatingFilter = c.Int(nullable: false),
                        CustomerFilter_Id = c.String(maxLength: 128),
                        MovieFilter_MovieID = c.Int(),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerFilter_Id)
                .ForeignKey("dbo.Movies", t => t.MovieFilter_MovieID)
                .Index(t => t.CustomerFilter_Id)
                .Index(t => t.MovieFilter_MovieID);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatID = c.Int(nullable: false, identity: true),
                        SeatName = c.String(),
                    })
                .PrimaryKey(t => t.SeatID);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Movie_MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Movie_MovieID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Movie_MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reports", "MovieFilter_MovieID", "dbo.Movies");
            DropForeignKey("dbo.Reports", "CustomerFilter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MovieReviews", "AppUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "MovieReview_MovieReviewID1", "dbo.MovieReviews");
            DropForeignKey("dbo.AspNetUsers", "MovieReview_MovieReviewID", "dbo.MovieReviews");
            DropForeignKey("dbo.Tickets", "Showing_ShowingID", "dbo.Showings");
            DropForeignKey("dbo.Tickets", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CreditCard_CreditCardID", "dbo.CreditCards");
            DropForeignKey("dbo.Orders", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Showings", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.MovieReviews", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.MovieReviews", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CreditCards", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieID" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_GenreID" });
            DropIndex("dbo.Reports", new[] { "MovieFilter_MovieID" });
            DropIndex("dbo.Reports", new[] { "CustomerFilter_Id" });
            DropIndex("dbo.Orders", new[] { "CreditCard_CreditCardID" });
            DropIndex("dbo.Orders", new[] { "AppUser_Id" });
            DropIndex("dbo.Tickets", new[] { "Showing_ShowingID" });
            DropIndex("dbo.Tickets", new[] { "Order_OrderID" });
            DropIndex("dbo.Showings", new[] { "Movie_MovieID" });
            DropIndex("dbo.MovieReviews", new[] { "AppUser_Id1" });
            DropIndex("dbo.MovieReviews", new[] { "Movie_MovieID" });
            DropIndex("dbo.MovieReviews", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "MovieReview_MovieReviewID1" });
            DropIndex("dbo.AspNetUsers", new[] { "MovieReview_MovieReviewID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CreditCards", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.GenreMovies");
            DropTable("dbo.Seats");
            DropTable("dbo.Reports");
            DropTable("dbo.MoviePrices");
            DropTable("dbo.Orders");
            DropTable("dbo.Tickets");
            DropTable("dbo.Showings");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
            DropTable("dbo.MovieReviews");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CreditCards");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}

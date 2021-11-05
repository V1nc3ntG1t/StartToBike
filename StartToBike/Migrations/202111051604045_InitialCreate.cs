namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 256),
                        Name = c.String(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Picture = c.Binary(storeType: "image"),
                        RoleId = c.Int(nullable: false),
                        TrainingLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.AccountCatalog", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AccountCatalog",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        AccountRole = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.AccountTour",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                        PerformanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountId, t.TourId, t.PerformanceId })
                .ForeignKey("dbo.Performance", t => t.PerformanceId)
                .ForeignKey("dbo.Tour", t => t.PerformanceId)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.AccountId)
                .Index(t => t.PerformanceId);
            
            CreateTable(
                "dbo.Performance",
                c => new
                    {
                        PerformanceId = c.Int(nullable: false, identity: true),
                        Distance = c.Int(nullable: false),
                        Altitude = c.Int(nullable: false),
                        StartTime = c.String(nullable: false),
                        EndTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PerformanceId);
            
            CreateTable(
                "dbo.Tour",
                c => new
                    {
                        TourId = c.Int(nullable: false, identity: true),
                        TourName = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                        Location = c.String(nullable: false),
                        StartDate = c.String(nullable: false),
                        Distance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourId);
            
            CreateTable(
                "dbo.Challenge",
                c => new
                    {
                        ChallengeId = c.Int(nullable: false, identity: true),
                        ChallengeName = c.String(maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        Reward = c.String(),
                        Task = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ChallengeId);
            
            CreateTable(
                "dbo.Quest",
                c => new
                    {
                        QuestId = c.Int(nullable: false, identity: true),
                        QuestLevel = c.Int(nullable: false),
                        QuestTask = c.String(),
                    })
                .PrimaryKey(t => t.QuestId);
            
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        Friend1Id = c.Int(nullable: false),
                        Friend2Id = c.Int(nullable: false),
                        StartDate = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Friend1Id, t.Friend2Id, t.StartDate })
                .ForeignKey("dbo.Account", t => t.Friend1Id)
                .ForeignKey("dbo.Account", t => t.Friend2Id)
                .Index(t => t.Friend1Id)
                .Index(t => t.Friend2Id);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        TrainingId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingId);
            
            CreateTable(
                "dbo.ChallengeTour",
                c => new
                    {
                        ChallengeId = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChallengeId, t.TourId })
                .ForeignKey("dbo.Challenge", t => t.ChallengeId, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.TourId, cascadeDelete: true)
                .Index(t => t.ChallengeId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.QuestTour",
                c => new
                    {
                        QuestId = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestId, t.TourId })
                .ForeignKey("dbo.Quest", t => t.QuestId, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.TourId, cascadeDelete: true)
                .Index(t => t.QuestId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.AccountChallenge",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        ChallengeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountId, t.ChallengeId })
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Challenge", t => t.ChallengeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.ChallengeId);
            
            CreateTable(
                "dbo.AccountQuest",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        QuestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountId, t.QuestId })
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Quest", t => t.QuestId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.QuestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountQuest", "QuestId", "dbo.Quest");
            DropForeignKey("dbo.AccountQuest", "AccountId", "dbo.Account");
            DropForeignKey("dbo.Friend", "Friend2Id", "dbo.Account");
            DropForeignKey("dbo.Friend", "Friend1Id", "dbo.Account");
            DropForeignKey("dbo.AccountChallenge", "ChallengeId", "dbo.Challenge");
            DropForeignKey("dbo.AccountChallenge", "AccountId", "dbo.Account");
            DropForeignKey("dbo.AccountTour", "AccountId", "dbo.Account");
            DropForeignKey("dbo.QuestTour", "TourId", "dbo.Tour");
            DropForeignKey("dbo.QuestTour", "QuestId", "dbo.Quest");
            DropForeignKey("dbo.ChallengeTour", "TourId", "dbo.Tour");
            DropForeignKey("dbo.ChallengeTour", "ChallengeId", "dbo.Challenge");
            DropForeignKey("dbo.AccountTour", "PerformanceId", "dbo.Tour");
            DropForeignKey("dbo.AccountTour", "PerformanceId", "dbo.Performance");
            DropForeignKey("dbo.Account", "RoleId", "dbo.AccountCatalog");
            DropIndex("dbo.AccountQuest", new[] { "QuestId" });
            DropIndex("dbo.AccountQuest", new[] { "AccountId" });
            DropIndex("dbo.AccountChallenge", new[] { "ChallengeId" });
            DropIndex("dbo.AccountChallenge", new[] { "AccountId" });
            DropIndex("dbo.QuestTour", new[] { "TourId" });
            DropIndex("dbo.QuestTour", new[] { "QuestId" });
            DropIndex("dbo.ChallengeTour", new[] { "TourId" });
            DropIndex("dbo.ChallengeTour", new[] { "ChallengeId" });
            DropIndex("dbo.Friend", new[] { "Friend2Id" });
            DropIndex("dbo.Friend", new[] { "Friend1Id" });
            DropIndex("dbo.AccountTour", new[] { "PerformanceId" });
            DropIndex("dbo.AccountTour", new[] { "AccountId" });
            DropIndex("dbo.Account", new[] { "RoleId" });
            DropTable("dbo.AccountQuest");
            DropTable("dbo.AccountChallenge");
            DropTable("dbo.QuestTour");
            DropTable("dbo.ChallengeTour");
            DropTable("dbo.Trainings");
            DropTable("dbo.Friend");
            DropTable("dbo.Quest");
            DropTable("dbo.Challenge");
            DropTable("dbo.Tour");
            DropTable("dbo.Performance");
            DropTable("dbo.AccountTour");
            DropTable("dbo.AccountCatalog");
            DropTable("dbo.Account");
        }
    }
}

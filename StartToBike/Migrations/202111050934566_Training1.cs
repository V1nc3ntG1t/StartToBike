namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Training1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainingAccount",
                c => new
                    {
                        TrainingId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainingId, t.AccountId })
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.TrainingId, cascadeDelete: true)
                .Index(t => t.TrainingId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingAccount", "TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.TrainingAccount", "AccountId", "dbo.Account");
            DropIndex("dbo.TrainingAccount", new[] { "AccountId" });
            DropIndex("dbo.TrainingAccount", new[] { "TrainingId" });
            DropTable("dbo.TrainingAccount");
        }
    }
}

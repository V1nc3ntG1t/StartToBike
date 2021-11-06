namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTraining",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        TrainingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountId, t.TrainingId })
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.TrainingId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.TrainingId);
            
            AddColumn("dbo.Account", "Training_TrainingId", c => c.Int());
            CreateIndex("dbo.Account", "Training_TrainingId");
            AddForeignKey("dbo.Account", "Training_TrainingId", "dbo.Trainings", "TrainingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "Training_TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.AccountTraining", "TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.AccountTraining", "AccountId", "dbo.Account");
            DropIndex("dbo.AccountTraining", new[] { "TrainingId" });
            DropIndex("dbo.AccountTraining", new[] { "AccountId" });
            DropIndex("dbo.Account", new[] { "Training_TrainingId" });
            DropColumn("dbo.Account", "Training_TrainingId");
            DropTable("dbo.AccountTraining");
        }
    }
}

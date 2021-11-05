namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Account", new[] { "Training_TrainingId" });
            DropIndex("dbo.AccountTraining", new[] { "TrainingId" });
            CreateIndex("dbo.Account", "Training_TrainingId");
            CreateIndex("dbo.AccountTraining", "TrainingId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AccountTraining", new[] { "TrainingId" });
            DropIndex("dbo.Account", new[] { "Training_TrainingId" });
            CreateIndex("dbo.AccountTraining", "TrainingId");
            CreateIndex("dbo.Account", "Training_TrainingId");
        }
    }
}

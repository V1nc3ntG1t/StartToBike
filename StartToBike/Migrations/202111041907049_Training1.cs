namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Training1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "Training_TrainingId", c => c.Int());
            CreateIndex("dbo.Account", "Training_TrainingId");
            AddForeignKey("dbo.Account", "Training_TrainingId", "dbo.Trainings", "TrainingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "Training_TrainingId", "dbo.Trainings");
            DropIndex("dbo.Account", new[] { "Training_TrainingId" });
            DropColumn("dbo.Account", "Training_TrainingId");
        }
    }
}

namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Training1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "Training_TrainingID", c => c.Int());
            CreateIndex("dbo.Account", "Training_TrainingID");
            AddForeignKey("dbo.Account", "Training_TrainingID", "dbo.Trainings", "TrainingID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "Training_TrainingID", "dbo.Trainings");
            DropIndex("dbo.Account", new[] { "Training_TrainingID" });
            DropColumn("dbo.Account", "Training_TrainingID");
        }
    }
}

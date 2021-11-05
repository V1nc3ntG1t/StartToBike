namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AccountTraining", new[] { "TrainingId" });
            CreateIndex("dbo.AccountTraining", "TrainingId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AccountTraining", new[] { "TrainingId" });
            CreateIndex("dbo.AccountTraining", "TrainingId");
        }
    }
}

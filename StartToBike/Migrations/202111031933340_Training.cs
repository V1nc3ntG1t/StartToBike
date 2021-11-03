namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Training : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        TrainingID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trainings");
        }
    }
}

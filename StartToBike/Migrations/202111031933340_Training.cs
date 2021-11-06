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
                        TrainingId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TrainingLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trainings");
        }
    }
}

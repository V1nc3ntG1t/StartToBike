namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "TrainingLevelString", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "TrainingLevelString");
        }
    }
}

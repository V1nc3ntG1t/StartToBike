namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Account", "UserName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Account", "TrainingLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Challenge", "Status", c => c.String());
            AlterColumn("dbo.Account", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Account", "Picture", c => c.Binary(storeType: "image"));
            DropColumn("dbo.Account", "TrainingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "TrainingId", c => c.Int());
            DropForeignKey("dbo.Friend", "Friend2Id", "dbo.Account");
            DropForeignKey("dbo.Friend", "Friend1Id", "dbo.Account");
            DropIndex("dbo.Friend", new[] { "Friend2Id" });
            DropIndex("dbo.Friend", new[] { "Friend1Id" });
            AlterColumn("dbo.Account", "Picture", c => c.Binary(nullable: false, storeType: "image"));
            AlterColumn("dbo.Account", "BirthDate", c => c.String(nullable: false));
            DropColumn("dbo.Challenge", "Status");
            DropColumn("dbo.Account", "TrainingLevel");
            DropColumn("dbo.Account", "UserName");
            DropTable("dbo.Friend");
        }
    }
}

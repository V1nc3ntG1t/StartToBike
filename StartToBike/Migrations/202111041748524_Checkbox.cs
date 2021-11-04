namespace StartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checkbox : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friend", "Friend1Id", "dbo.Account");
            DropForeignKey("dbo.Friend", "Friend2Id", "dbo.Account");
            DropIndex("dbo.Friend", new[] { "Friend1Id" });
            DropIndex("dbo.Friend", new[] { "Friend2Id" });
            AddColumn("dbo.Account", "TrainingId", c => c.Int());
            AlterColumn("dbo.Account", "BirthDate", c => c.String(nullable: false));
            AlterColumn("dbo.Account", "Picture", c => c.Binary(nullable: false, storeType: "image"));
            DropColumn("dbo.Account", "UserName");
            DropColumn("dbo.Account", "TrainingLevel");
            DropTable("dbo.Friend");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        Friend1Id = c.Int(nullable: false),
                        Friend2Id = c.Int(nullable: false),
                        StartDate = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Friend1Id, t.Friend2Id, t.StartDate });
            
            AddColumn("dbo.Account", "TrainingLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Account", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Account", "Picture", c => c.Binary(storeType: "image"));
            AlterColumn("dbo.Account", "BirthDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Account", "TrainingId");
            CreateIndex("dbo.Friend", "Friend2Id");
            CreateIndex("dbo.Friend", "Friend1Id");
            AddForeignKey("dbo.Friend", "Friend2Id", "dbo.Account", "AccountId");
            AddForeignKey("dbo.Friend", "Friend1Id", "dbo.Account", "AccountId");
        }
    }
}

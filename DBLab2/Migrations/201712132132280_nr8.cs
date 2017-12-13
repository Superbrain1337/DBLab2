namespace DBLab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nr8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelId = c.Int(nullable: false, identity: true),
                        NumbOfBirds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LevelId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        Level_LevelId = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Levels", t => t.Level_LevelId)
                .Index(t => t.Level_LevelId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        Moves = c.Int(nullable: false),
                        Level_LevelId = c.Int(nullable: false),
                        Player_PlayerId = c.Int(),
                    })
                .PrimaryKey(t => t.ScoreId)
                .ForeignKey("dbo.Levels", t => t.Level_LevelId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId)
                .Index(t => t.Level_LevelId)
                .Index(t => t.Player_PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "Level_LevelId", "dbo.Levels");
            DropForeignKey("dbo.Scores", "Player_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Scores", "Level_LevelId", "dbo.Levels");
            DropIndex("dbo.Scores", new[] { "Player_PlayerId" });
            DropIndex("dbo.Scores", new[] { "Level_LevelId" });
            DropIndex("dbo.Players", new[] { "Level_LevelId" });
            DropTable("dbo.Scores");
            DropTable("dbo.Players");
            DropTable("dbo.Levels");
        }
    }
}

namespace DBLab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Values_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelId = c.Int(nullable: false),
                        NumbOfBirds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LevelId)
                .ForeignKey("dbo.Scores", t => t.LevelId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ScoreId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Scores", t => t.PlayerId)
                .Index(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Levels", "LevelId", "dbo.Scores");
            DropForeignKey("dbo.Players", "PlayerId", "dbo.Scores");
            DropIndex("dbo.Players", new[] { "PlayerId" });
            DropIndex("dbo.Levels", new[] { "LevelId" });
            DropTable("dbo.Players");
            DropTable("dbo.Scores");
            DropTable("dbo.Levels");
        }
    }
}

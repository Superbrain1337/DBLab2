namespace DBLab2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DBLab2.ListGenerator>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DBLab2.ListGenerator";
        }

        protected override void Seed(DBLab2.ListGenerator context)
        {
            for(int i = 0; i < 5; i++)
            {
                
                /*Player P = new Player
                {
                    Name = "Samuel",
                    PlayerId = i
                };
                context.Players.AddOrUpdate(P);*/
            }
            context.Players.AddOrUpdate(x => x.PlayerId,
                    new Player() { PlayerId = 1, Name = "Samuel" });

            context.Levels.AddOrUpdate(x => x.LevelId,
                    new Level() { LevelId = 1, NumbOfBirds = 5 });

            for (int i = 0; i < 10; i++)
            {
                
                /*Level L = new Level();
                L.LevelId = i;
                L.NumbOfBirds = 5;
                context.Levels.AddOrUpdate(L);*/
            }

            //context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

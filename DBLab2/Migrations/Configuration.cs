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
        }

        protected override void Seed(DBLab2.ListGenerator context)
        {
            Player P = new Player() { PlayerId = 2, Name = "David" };
            Level L = new Level() { LevelId = 2, NumbOfBirds = 5 };


            context.Players.AddOrUpdate(P);
            context.Levels.AddOrUpdate(L);
            context.SaveChanges();
        
        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data.
    }
    }
}

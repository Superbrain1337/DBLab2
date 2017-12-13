using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity.Infrastructure;

namespace DBLab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public const string SqlConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBLab2Database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public string SqlQuery { get; set; }
        public SqlDataReader Reader;
        public List<string> SelectedObjects = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            ListGenerator Lg = new ListGenerator();
            //AddStuff(Lg);
        }

        public void ExcecuteCommand(string sqlQuery, bool isSelectQuery)
        {
            SqlConnection MainConnection = new SqlConnection(SqlConnectionString);
            SqlCommand command = new SqlCommand(sqlQuery, MainConnection);
            try
            {
                MainConnection.Open();
                if (isSelectQuery)
                {
                    Reader = command.ExecuteReader();
                    while (Reader.Read())
                    {
                        for (int i = 0; i < Reader.FieldCount; i++)
                        {
                            SelectedObjects.Add(Reader[i].ToString());
                        }
                    }
                }
                else
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Label_Error.Content = ex.Message;
            }
            finally
            {
                MainConnection.Close();
            }
        }

        /*public void AddStuff(ListGenerator Lg)
        {
            Level L = new Level();
            Score S = new Score();
            Player P = new Player();
            L.LevelId = 1;
            L.NumbOfBirds = 5;
            L.Score = S;
            P.Name = "Samuel";
            P.PlayerId = 1;
            P.Score = S;
            S.Player = new List<Player> { P };
            S.Level = new List<Level> { L };
            Lg.Levels.Add(L);
            Lg.Players.Add(P);
            Lg.Scores.Add(S);
            Lg.SaveChanges();
        }*/
    }


    public class ListGenerator : DbContext
    {
        private const string SqlConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBLab2Database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public ListGenerator() : base (SqlConnectionString) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Score> Scores { get; set; }
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var playerEntityConfig = modelBuilder.Entity<Player>().HasKey(p => p.PlayerId);
            var levelEntityConfig = modelBuilder.Entity<Level>().HasKey(l => l.LevelId);
            var scoreEntityConfig = modelBuilder.Entity<Score>().HasRequired(s => s.Player).WithMany(p => p.LevelScore);
            var scoreEntityConfig
            base.OnModelCreating(modelBuilder);
        }*/
    }
    /*
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
    }*/
}

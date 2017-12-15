using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            FillPlayerGrid();
            FillLevelGrid();
            FillScoreGrid();
            FillComboboxes();
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
                    SelectedObjects.Clear();
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
                Label_Error.Content = ex.Message;
            }
            finally
            {
                MainConnection.Close();
            }
        }

        public void FillPlayerGrid()
        {
            string CmdString = "";
            using (SqlConnection con = new SqlConnection(SqlConnectionString))
            {
                CmdString = @"SELECT PlayerId, Name FROM dbo.Players";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Players");
                sda.Fill(dt);
                CmdString = @"SELECT Moves FROM dbo.Scores";
                cmd = new SqlCommand(CmdString, con);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                DataGrid_Players.ItemsSource = dt.DefaultView;
            }
        }

        public void FillLevelGrid()
        {
            string CmdString = "";
            using (SqlConnection con = new SqlConnection(SqlConnectionString))
            {
                CmdString = @"SELECT LevelId, NumbOfBirds FROM dbo.Levels";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Levels");
                sda.Fill(dt);
                DataGrid_Levels.ItemsSource = dt.DefaultView;
            }
        }

        public void FillScoreGrid()
        {
            string CmdString = "";
            using (SqlConnection con = new SqlConnection(SqlConnectionString))
            {
                CmdString = @"SELECT ScoreId, Moves FROM dbo.Scores";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Scores");
                sda.Fill(dt);
                DataGrid_Scores.ItemsSource = dt.DefaultView;
            }
        }

        public void FillComboboxes()
        {
            if (ComboBox_ScoresName.Items.IsEmpty)
            {
                SqlQuery = "SELECT Name FROM dbo.Players";
                ExcecuteCommand(SqlQuery, true);
                for (int i = 0; i < SelectedObjects.Count; i++)
                {
                    ComboBox_ScoresName.Items.Add(SelectedObjects.ElementAt(i));
                }
            }
            if (ComboBox_ScoreLevel.Items.IsEmpty)
            {
                SqlQuery = "SELECT LevelId FROM dbo.Levels";
                ExcecuteCommand(SqlQuery, true);
                for (int i = 0; i < SelectedObjects.Count; i++)
                {
                    ComboBox_ScoreLevel.Items.Add(SelectedObjects.ElementAt(i));
                }
            }
            if(ComboBox_ScoreLevel.SelectedIndex != -1)
            {
                SqlQuery = "SELECT NumbOfBirds FROM dbo.Levels";
                ExcecuteCommand(SqlQuery, true);
                ComboBox_ScoreScore.Items.Clear();
                for(int i = 0; i < int.Parse(SelectedObjects.ElementAt(ComboBox_ScoreLevel.SelectedIndex)); i++)
                {
                    ComboBox_ScoreScore.Items.Add(i + 1);
                }
            }
        }

        private void Button_ManagePlayer_Click(object sender, RoutedEventArgs e)
        {
            if(TextBox_Player.Text != null && TextBox_Player.Text != "")
            {
                SqlQuery = $@"INSERT INTO dbo.Players (Name) VALUES ('{TextBox_Player.Text}')";
                ExcecuteCommand(SqlQuery, false);
            }
            FillPlayerGrid();
        }

        private void Button_ManageLevel_Click(object sender, RoutedEventArgs e)
        {
            if(TextBox_NumbOfBirds.Text != null && TextBox_NumbOfBirds.Text != "")
            {
                SqlQuery = $@"INSERT INTO dbo.Levels (NumbOfBirds) VALUES ('{TextBox_NumbOfBirds.Text}')";
                ExcecuteCommand(SqlQuery, false);
            }
            FillLevelGrid();
        }

        private void DataGrid_Players_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_ScoreLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboboxes();
        }

        private void Button_AddScore_Click(object sender, RoutedEventArgs e)
        {
            SqlQuery = $@"SELECT PlayerId FROM dbo.Players WHERE Name = '{ComboBox_ScoresName.Text}'";
            ExcecuteCommand(SqlQuery, true);
            int playerId = int.Parse(SelectedObjects.First());

            if (ComboBox_ScoresName.SelectedIndex != -1 && ComboBox_ScoreLevel.SelectedIndex != -1 && ComboBox_ScoreScore.SelectedIndex != -1)
            {
                SqlQuery = $@"INSERT INTO dbo.Scores (Moves, Player_PlayerId, Level_LevelId) VALUES ({ComboBox_ScoreScore.Text}, {playerId}, {ComboBox_ScoreLevel.Text})";
                ExcecuteCommand(SqlQuery, false);
            }
            FillScoreGrid();
            FillComboboxes();
        }
    }


    public class ListGenerator : DbContext
    {
        private const string SqlConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBLab2Database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public ListGenerator() : base (SqlConnectionString) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}

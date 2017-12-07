using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

namespace DBLab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string SqlConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBLab2Database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public string SqlQuery { get; set; }
        public SqlCommand command;
        public SqlDataReader Reader;
        public SqlConnection MainConnection = new SqlConnection(SqlConnectionString);
        public List<string> SelectedObjects = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ExcecuteCommand(string sqlQuery, bool isSelectQuery)
        {
            command = new SqlCommand(sqlQuery, MainConnection);
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
    }

    public class ListGenerator : DbContext
    {
        public ListGenerator(string cs) : base (cs) { }
        DbSet<Player> Players { get; set; }
        DbSet<Level> Levels { get; set; }
        DbSet<Score> Scores { get; set; }
    }
}

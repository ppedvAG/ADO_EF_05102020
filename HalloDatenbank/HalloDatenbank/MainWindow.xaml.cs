using System.Data.SqlClient;
using System.Windows;

namespace HalloDatenbank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string conString = @"Server=(localdb)\mssqllocaldb;Database=Northwnd;Trusted_Connection=true";

        private void Laden(object sender, RoutedEventArgs e)
        {
            var con = new SqlConnection(conString);
            con.Open();


            var cmdQuery = new SqlCommand("SELECT * FROM Employees", con);
            var reader = cmdQuery.ExecuteReader();
            tb1.Clear();
            while (reader.Read())
            {
                var vorname = reader.GetString(1);
                var nachname = reader["FirstName"];

                tb1.Text += $"{vorname} {nachname} \n";
            }

        }

        private void CountEmployees(object sender, RoutedEventArgs e)
        {
            var con = new SqlConnection(conString);
            con.Open();

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT COUNT(*) FROM Employees";

            var count = cmd.ExecuteScalar();

            MessageBox.Show($"{count} Employees in DB");
        }
    }
}

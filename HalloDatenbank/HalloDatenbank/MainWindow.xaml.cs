using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Documents;

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

        private void Load(object sender, RoutedEventArgs e)
        {
            using (var con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();

                    var query = "SELECT * FROM Employees";

                    if (!string.IsNullOrEmpty(FilterTb.Text))
                    {
                        //';CREATE DATABASE HACKED;--
                        //BÖSE!!! SQL INJECTION   query = $"SELECT * FROM Employees WHERE FirstName LIKE '{FilterTb.Text}' + '%'"; 

                        query = $"SELECT * FROM Employees WHERE FirstName LIKE @suche + '%'";
                    }

                    using (var cmdQuery = new SqlCommand(query, con))
                    {
                        cmdQuery.Parameters.AddWithValue("@suche", FilterTb.Text);
                        using (var reader = cmdQuery.ExecuteReader())
                        {
                            tb1.Clear();
                            List<Mitarbeiter> mitarbeiterListe = new List<Mitarbeiter>();
                            while (reader.Read())
                            {
                                var vorname = reader.GetString(1);
                                var adr = reader.GetString(7);
                                var geburtstag = reader.GetDateTime(5);

                                var nachname = reader["LastName"];
                                DateTime einstellDatum = (DateTime)reader["HireDate"];

                                var stadt = reader.GetString(reader.GetOrdinal("City"));
                                tb1.Text += $"{vorname} {nachname} aus {stadt} \n";

                                var m = new Mitarbeiter()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                                    Vorname = reader.GetString(reader.GetOrdinal("FirstName")),
                                    Nachname = reader.GetString(reader.GetOrdinal("LastName")),
                                    EinstellDatum = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                                    GebDatum = reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                                    Stadt = reader.GetString(reader.GetOrdinal("City")),
                                };
                                mitarbeiterListe.Add(m);
                            }
                            myGrid.ItemsSource = mitarbeiterListe;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler: {ex.Message}");
                }

            } //--> //con.Dispose();  --> // con.Close();
        }



        private void CountEmployees(object sender, RoutedEventArgs e)
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT COUNT(*) FROM Employees";

                    var count = cmd.ExecuteScalar();

                    MessageBox.Show($"{count} Employees in DB");
                }
            }
        }

        private void MakeAllYounger(object sender, RoutedEventArgs e)
        {
            if (myGrid.ItemsSource == null)
            {
                MessageBox.Show("Mitarbeiter vorher laden!");
                return;
            }


            using (var con = new SqlConnection(conString))
            {
                con.Open();
                using (var trans = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Mitarbeiter item in myGrid.ItemsSource)
                        {
                            item.GebDatum = item.GebDatum.AddYears(1);

                            var cmd = con.CreateCommand();
                            cmd.Transaction = trans;
                            cmd.CommandText = "UPDATE Employees SET BirthDate = @newDate WHERE EmployeeID = @id";
                            cmd.Parameters.AddWithValue("@newDate", item.GebDatum);
                            cmd.Parameters.AddWithValue("@id", item.Id);

                            if (FilterTb.Text.Contains("error"))
                                throw new ExecutionEngineException();

                            cmd.ExecuteNonQuery();
                        }//foreach
                        trans.Commit();
                        MessageBox.Show("OK");
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        MessageBox.Show("Fehler: Keine änderungen an der Datenbank wurde übernommen!");
                    }
                }//trans
            }//con


            Load(null, null);
        }
    }
}

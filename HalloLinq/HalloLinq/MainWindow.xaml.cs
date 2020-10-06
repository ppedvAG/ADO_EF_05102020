using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;

namespace HalloLinq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Mitarbeiter> mitarbeiter = new List<Mitarbeiter>();

        public MainWindow()
        {

            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de");
            InitializeComponent();

            var abt1 = new Abteilung() { Id = 1, Bezeichnung = "Steine" };
            var abt2 = new Abteilung() { Id = 2, Bezeichnung = "Holz" };

            for (int i = 0; i < 100; i++)
            {
                var m = new Mitarbeiter()
                {
                    Id = i,
                    Name = $"Fred #{i:000}",
                    GebDatum = DateTime.Now.AddYears(-50).AddDays(i * 17)
                };

                if (i % 2 == 0)
                    m.Abteilungen.Add(abt1);
                if (i % 3 == 0)
                    m.Abteilungen.Add(abt2);

                mitarbeiter.Add(m);
            }
        }

        private void ShowAll(object sender, RoutedEventArgs e)
        {
            myGrid.ItemsSource = mitarbeiter;
        }

        private void FilterBirthDay(object sender, RoutedEventArgs e)
        {
            var query = from m in mitarbeiter
                        where m.GebDatum.Month == DateTime.Now.Month
                        orderby m.GebDatum.Day, m.Name descending
                        select m;


            myGrid.ItemsSource = query.ToList();
        }

        private void FilterBirthDayLAMBDA(object sender, RoutedEventArgs e)
        {
            http://linq101.nilzorblog.com/linq101-lambda.php
            myGrid.ItemsSource = mitarbeiter.Where(m => m.GebDatum.Month == DateTime.Now.Month)
                                            .OrderBy(x => x.GebDatum.Day)
                                            .ThenByDescending(x => x.Name)
                                            .ThenByDescending(x => x.GebDatum.Year)
                                            .ToList();
        }

        private void CountThisMonthBirthDay(object sender, RoutedEventArgs e)
        {
            //int result = mitarbeiter.Count(x => x.GebDatum.Month == DateTime.Now.Month);
            var result = mitarbeiter.Average(x => x.GebDatum.Month);
            MessageBox.Show($"Mitarbeiter die diesen Montag GebTag haben: {result}");
        }

        private void NextBirthDay(object sender, RoutedEventArgs e)
        {
            var now = new DateTime(2020, 12, 1);
            var result = mitarbeiter.OrderBy(x => x.GebDatum)
                                    .FirstOrDefault(x => x.GebDatum.Month >= now.Month && x.GebDatum.Day > now.Day);

            if (result != null)
                MessageBox.Show($"{result.Name}");
            else
                MessageBox.Show("Nix gefunden");
        }

        private void LinqSelect(object sender, RoutedEventArgs e)
        {
            //myGrid.ItemsSource = mitarbeiter.SelectMany(x => x.Abteilungen).Distinct();

            myGrid.ItemsSource = mitarbeiter.Select(x => new { DerName = x.Name, Jahr = x.GebDatum.Year });

            //alle Abt. Holz
            myGrid.ItemsSource = mitarbeiter.Where(x => x.Abteilungen.Any(y => y.Bezeichnung == "Holz"));

        }

        private void FillTree(object sender, RoutedEventArgs e)
        {
            var groups = mitarbeiter.GroupBy(x => x.GebDatum.DayOfWeek);

            tv1.Items.Clear();

            foreach (var g in groups)
            {
                var tvi = new TreeViewItem() { Header = g.Key.ToString() };
                foreach (var m in g)
                {
                    tvi.Items.Add(m.Name);
                }
                tv1.Items.Add(tvi);
            }
        }

    }
}

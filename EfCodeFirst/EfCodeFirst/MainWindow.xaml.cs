using EfCodeFirst.Data;
using EfCodeFirst.Model;
using System;
using System.Linq;
using System.Windows;

namespace EfCodeFirst
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
        EfContext context = new EfContext();
        private void Load(object sender, RoutedEventArgs e)
        {
            myGrid.ItemsSource = context.Mitarbeiter.ToList();
        }

        private void CreateDemoData(object sender, RoutedEventArgs e)
        {
            var abt1 = new Abteilung() { Bezeichnung = "Steine" };
            var abt2 = new Abteilung() { Bezeichnung = "Holz" };

            for (int i = 0; i < 100; i++)
            {
                var m = new Mitarbeiter()
                {
                    Name = $"Fred #{i:000}",
                    GebDatum = DateTime.Now.AddYears(-50).AddDays(i * 17),
                    Job = "Macht dinge!!!"
                };

                if (i % 2 == 0)
                    m.Abteilungen.Add(abt1);
                if (i % 3 == 0)
                    m.Abteilungen.Add(abt2);

                context.Mitarbeiter.Add(m);
            }

            context.SaveChanges();
        }
    }
}

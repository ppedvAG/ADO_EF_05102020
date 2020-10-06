using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EfModelFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model1Container context = new Model1Container();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void LoadAll(object sender, RoutedEventArgs e)
        {
            context = new Model1Container(); //for LazyLoading cache reset

            //   myGrid.ItemsSource = context.PersonSet.OfType<Mitarbeiter>().Where(x => x.Name.StartsWith("F")).ToList();
            //myGrid.ItemsSource = context.PersonSet.OfType<Mitarbeiter>().Where(x => x.Name.Contains("F")).ToList();
            myGrid.ItemsSource = context.PersonSet.OfType<Mitarbeiter>()
                                        .Include(x => x.Abteilungen) //EagerLoading
                                        .Include(x => x.Kunden)
                                        .Where(x => x.Abteilungen.Any(y => y.Mitarbeiter.Count > 12))
                                        .ToList();
        }

        private void CreateDemoData(object sender, RoutedEventArgs e)
        {

            var abt1 = new Abteilung() { Id = 1, Bezeichnung = "Steine" };
            var abt2 = new Abteilung() { Id = 2, Bezeichnung = "Holz" };

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

                context.PersonSet.Add(m);
            }

            context.SaveChanges();
        }

        private void ShowMitarbeiter(object sender, MouseButtonEventArgs e)
        {
            if (myGrid.SelectedItem is Mitarbeiter m)
            {
                //löscht 
                //context.Entry(m).State = EntityState.Deleted;
                //context.PersonSet.Remove(m);

                //context.Entry(m).State = EntityState.Modified;

                //list von alle die geupdated werden
                var r = context.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

                MessageBox.Show($"[{context.Entry(m).State}] {m.Name} {string.Join(", ", m.Abteilungen.Select(x => x.Bezeichnung))}");
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }
    }
}

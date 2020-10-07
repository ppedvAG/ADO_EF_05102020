using System;
using System.Collections.Generic;
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

namespace EfDBFirst
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

        NORTHWNDEntities context = new NORTHWNDEntities();

        private void Load(object sender, RoutedEventArgs e)
        {
            //myGrid.ItemsSource = context.Products.ToList();
            myGrid.ItemsSource = context.Product_Sales_for_1997.ToList();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }

        private void GetTop10(object sender, RoutedEventArgs e)
        {
            IEnumerable<GetTenMostExpensiveProducts_Result> results = context.GetTenMostExpensiveProducts();

            myGrid.ItemsSource = results;
        }
    }
}

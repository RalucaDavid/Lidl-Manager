using LidlManager.Model;
using LidlManager.ViewModel;
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

namespace LidlManager.View
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage(object dContext)
        {
            InitializeComponent();
            DataContext = dContext;
        }

        private void GoToAdminMenu(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.RemoveBackEntry();
                navigationService.Navigate(new AdminMenu(DataContext));
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string barcode = barcodeTextBox.Text;
            Category selectedCategory = cmbCategory.SelectedItem as Category;
            Producer selectedProducer = cmbProducer.SelectedItem as Producer;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.AddProduct(name, barcode, selectedCategory, selectedProducer);
            }
        }

        private void UpdateProduct(object sender, RoutedEventArgs e)
        {
          
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
           
        }
    }
}

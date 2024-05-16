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
    /// Interaction logic for StocksPage.xaml
    /// </summary>
    public partial class StocksPage : Page
    {
        public StocksPage(object dContext)
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

        private void AddStock(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = cmbProduct.SelectedItem as Product;
            string amount = amountTextBox.Text;
            string unit = unitTextBox.Text;
            DateTime? selectedExpirationDate = expirationDate.SelectedDate;
            DateTime? selectedSupplyDate = supplyDate.SelectedDate;
            string purchase = purchaseTextBox.Text;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.AddStock(selectedProduct, amount, unit, selectedExpirationDate, selectedSupplyDate, purchase);
            }
        }

        private void UpdateStock(object sender, RoutedEventArgs e)
        {
            if (stocksList.SelectedItem is Stock selectedStock)
            {
                string selling = sellingTextBox.Text;
                if (DataContext is MenuCommands menuCommands)
                {
                    menuCommands.UpdateStock(sender, e, selectedStock.Id, selling);
                }
            }
        }

        private void DeleteStock(object sender, RoutedEventArgs e)
        {
            if (stocksList.SelectedItem is Stock selectedStock)
            {
                if (DataContext is MenuCommands menuCommands)
                {
                    menuCommands.DeleteStock(sender, e, selectedStock.Id);
                }
            }
        }
    }
}

using LidlManager.Model;
using LidlManager.ViewModel;
using Microsoft.Identity.Client.NativeInterop;
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
using Microsoft.VisualBasic;

namespace LidlManager.View
{
    /// <summary>
    /// Interaction logic for CashierMenu.xaml
    /// </summary>
    public partial class CashierMenu : Page
    {
        public CashierMenu(object dContext)
        {
            InitializeComponent();
            DataContext = dContext;
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string barcode = barcodeTextBox.Text;
            Category selectedCategory = categoryComboBox.SelectedItem as Category;
            Producer selectedProducer = producersComboBox.SelectedItem as Producer;
            DateTime? selectedExpirationDate = expirationDate.SelectedDate;
            int? categoryId = selectedCategory != null ? selectedCategory.Id : (int?)null;
            int? producerId = selectedProducer != null ? selectedProducer.Id : (int?)null;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.SearchProduct(name, barcode, categoryId, producerId, selectedExpirationDate);
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            if (suggestionsList.SelectedItem is Stock selectedStock)
            { 
                string input = Interaction.InputBox("Enter the quantity you want to buy:", "Enter Quantity", "1");
                if ((int.TryParse(input, out int quantity)) && (quantity > 0) && (quantity <= selectedStock.Amount))
                {
                    //(selectedStock, quantity);
                }
                else
                {
                    MessageBox.Show("Please enter a valid quantity.");
                }
            }
            else
            {
                MessageBox.Show("Please select a product from the list first.");
            }
        }
    }
}

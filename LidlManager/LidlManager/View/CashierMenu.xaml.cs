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
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.SearchProduct(name, barcode, selectedCategory.Id, selectedProducer.Id, selectedExpirationDate);
            }
        }
    }
}

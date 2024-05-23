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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LidlManager.View
{
    /// <summary>
    /// Interaction logic for View2Page.xaml
    /// </summary>
    public partial class View2Page : Page
    {
        public View2Page(object dContext)
        {
            InitializeComponent();
            DataContext = dContext;
        }

        private void ClickSearch2(object sender, RoutedEventArgs e)
        {
            DateTime? date1 = date.SelectedDate;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.SearchBiggestReceipt(date1);
            }
        }

        private void ClickSearch1(object sender, RoutedEventArgs e)
        {
            User selectedUser = cmbUser.SelectedItem as User;
            var selectedComboBoxItem = cmbMonth.SelectedItem as ComboBoxItem;
            if (selectedComboBoxItem != null)
            {
                string tagValue = selectedComboBoxItem.Tag as string;
                if (int.TryParse(tagValue, out int selectedMonth))
                { 
                    if (DataContext is MenuCommands menuCommands)
                    {
                        menuCommands.SearchDailyEarnings(selectedUser, selectedMonth);
                    }
                }
            }
            else
                MessageBox.Show("Choose a month!");
        }
    }
}

using LidlManager.Model;
using LidlManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LidlManager.View
{
    /// <summary>
    /// Interaction logic for ProducersPage.xaml
    /// </summary>
    public partial class ProducersPage : Page
    {
        public ProducersPage(object dContext)
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

        private void AddProducer(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string country = countryTextBox.Text;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.AddProducer(sender, e, name, country);
            }
        }

        private void UpdateProducer(object sender, RoutedEventArgs e)
        {
            if (producersList.SelectedItem is Producer selectedProducer)
            {
                string name = nameTextBox.Text;
                string country = countryTextBox.Text;
                if (DataContext is MenuCommands menuCommands)
                {
                    menuCommands.UpdateProducer(sender, e, selectedProducer.Id, name, country);
                }
            }
        }

        private void DeleteProducer(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string country = countryTextBox.Text;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.DeleteProducer(sender, e, name, country);
            }
        }
    }
}

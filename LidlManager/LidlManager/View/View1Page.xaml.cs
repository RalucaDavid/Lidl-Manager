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
    /// Interaction logic for View1Page.xaml
    /// </summary>
    public partial class View1Page : Page
    {
        public View1Page(object dContext)
        {
            InitializeComponent();
            DataContext = dContext;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.AddCategoriesSums();
            }
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

        private void SearchProducersProducts(object sender, RoutedEventArgs e)
        {
            Producer selectedProducer = cmbProducer.SelectedItem as Producer;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.SearchProductsByProducer(sender, e, selectedProducer.Id);
            }
        }
    }
}

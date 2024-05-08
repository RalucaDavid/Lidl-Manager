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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(object dContext)
        {
            InitializeComponent();
            DataContext = dContext;
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            if (DataContext is MenuCommands menuCommands)
            {
                string username = usernameTextBox.Text;
                string password = passwordBox.Password;
                string location = menuCommands.Login(sender, e, username, password);
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (location == "Admin")
                {
                    if (navigationService != null)
                    {
                        navigationService.RemoveBackEntry();
                        navigationService.Navigate(new AdminMenu(DataContext));
                    }
                }
                if (location == "Cashier")
                {
                    if (navigationService != null)
                    {
                        navigationService.RemoveBackEntry();
                        navigationService.Navigate(new CashierMenu(DataContext));
                    }
                }
            }
        }
    }
}

using LidlManager.Model;
using LidlManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Design;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MenuCommands = LidlManager.ViewModel.MenuCommands;

namespace LidlManager.View
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage(object dContext)
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

        private void AddUser(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string role = cmbRole.Text;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.AddUser(sender, e, username, password, role);
            }
        }

        private void UpdateUser(object sender, RoutedEventArgs e)
        {
            if (usersList.SelectedItem is User selectedUser)
            {
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;
                string role = cmbRole.Text;
                if (DataContext is MenuCommands menuCommands)
                {
                    menuCommands.UpdateUser(sender, e,selectedUser.Id, username, password, role);
                }
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string role = cmbRole.Text;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.DeleteUser(sender, e, username, password, role);
            }
        }
    }
}

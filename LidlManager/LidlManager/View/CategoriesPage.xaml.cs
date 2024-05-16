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
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        public CategoriesPage(object dContext)
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

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.AddCategory(sender, e, name);
            }
        }

        private void UpdateCategory(object sender, RoutedEventArgs e)
        {
            if (categoriesList.SelectedItem is Category selectedCategory)
            {
                string name = nameTextBox.Text;
                if (DataContext is MenuCommands menuCommands)
                {
                    menuCommands.UpdateCategory(sender, e, selectedCategory.Id, name);
                }
            }
        }

        private void DeleteCategory(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            if (DataContext is MenuCommands menuCommands)
            {
                menuCommands.DeleteCategory(sender, e, name);
            }
        }
    }
}

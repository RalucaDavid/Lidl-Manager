using LidlManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LidlManager.View;
using System.Windows.Input;
using System.Collections.ObjectModel;
using LidlManager.Model.BussinessLogicLayer;

namespace LidlManager.ViewModel
{
    class MenuCommands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<User> users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }
        private UserBLL userBLL;
        public UserBLL UserBLL
        {
            get { return userBLL; }
            set
            {
                if (userBLL != value)
                {
                    userBLL = value;
                    OnPropertyChanged(nameof(UserBLL));
                }
            }
        }

        public MenuCommands()
        {
            userBLL = new UserBLL();
            Users = userBLL.GetAllUsers();
        }

        public string Login(object sender, RoutedEventArgs e, string username, string password)
        {
            User user = userBLL.Login(username, password);
            if (user != null)
            {
                if (user.Type == "Admin")
                {
                    return "Admin";
                }
                else if (user.Type == "Cashier")
                {
                    return "Cashier";
                }
            }
            MessageBox.Show("Incorrect username or password!");
            return "None";
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

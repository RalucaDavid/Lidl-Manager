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

namespace LidlManager.ViewModel
{
    class MenuCommands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private LidlManagerContext lidlManager =  new LidlManagerContext();
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public LidlManagerContext LidlManager
        {
            get { return lidlManager; }
            set
            {
                lidlManager = value;
                OnPropertyChanged(nameof(LidlManager));
            }
        }
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(LidlManager));
            }
        }

        public MenuCommands()
        {
            InitializeUsers();
        }
        private void InitializeUsers()
        {
            var usersFromDb = lidlManager.Users.ToList();
            foreach (var user in usersFromDb)
            {
                if((bool)user.IsActive)
                  users.Add(user);
            }
        }

        public string Login(object sender, RoutedEventArgs e, string username, string password)
        {
            User user = lidlManager.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
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

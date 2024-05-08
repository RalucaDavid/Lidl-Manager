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

namespace LidlManager.ViewModel
{
    class MenuCommands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        LidlManagerContext lidlManager =  new LidlManagerContext();

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

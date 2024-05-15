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
using Azure.Identity;
using System.Diagnostics;

namespace LidlManager.ViewModel
{
    class MenuCommands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MenuCommands()
        {
            userBLL = new UserBLL();
            Users = userBLL.GetAllUsers();

            producerBLL = new ProducerBLL();
            Producers = producerBLL.GetAllProducers();
        }

        #region Users

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

        public void AddUser(object sender, RoutedEventArgs e, string username, string password, string role)
        {
            try
            {
                if ((userBLL != null) && (!string.IsNullOrEmpty(username)) && (!string.IsNullOrEmpty(password)) && (!string.IsNullOrEmpty(role)))
                {
                    User user = new User();
                    user.Name = username;
                    user.Password = password;
                    user.Type = role;
                    userBLL.AddMethod(user);
                    Users = userBLL.GetAllUsers();
                }
                else
                    MessageBox.Show("Incorrect username or password!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void UpdateUser(object sender, RoutedEventArgs e,int id, string username, string password, string role)
        {
            try
            {
                if ((userBLL != null) && (!string.IsNullOrEmpty(username)) && (!string.IsNullOrEmpty(password)) && (!string.IsNullOrEmpty(role)))
                {
                    userBLL.UpdateMethod(id,username, password, role);
                    Users = userBLL.GetAllUsers();
                }
                else
                    MessageBox.Show("Incorrect username or password!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void DeleteUser(object sender, RoutedEventArgs e, string username, string password, string role)
        {
            try
            {
                if ((userBLL != null) && (!string.IsNullOrEmpty(username)) && (!string.IsNullOrEmpty(password)) && (!string.IsNullOrEmpty(role)))
                {
                    userBLL.DeleteMethod(username, password, role);
                    Users = userBLL.GetAllUsers();
                }
                else
                    MessageBox.Show("Incorrect username or password!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        #endregion

        #region Producers

        private ObservableCollection<Producer> producers = new ObservableCollection<Producer>();
        public ObservableCollection<Producer> Producers
        {
            get { return producers; }
            set
            {
                if (producers != value)
                {
                    producers = value;
                    OnPropertyChanged(nameof(Producers));
                }
            }
        }
        private ProducerBLL producerBLL;
        public ProducerBLL ProducerBLL
        {
            get { return producerBLL; }
            set
            {
                if (producerBLL != value)
                {
                    producerBLL = value;
                    OnPropertyChanged(nameof(ProducerBLL));
                }
            }
        }

        public void AddProducer(object sender, RoutedEventArgs e, string name, string country)
        {
            try
            {
                if ((producerBLL != null) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(country)))
                {
                    Producer producer = new Producer();
                    producer.Name = name;
                    producer.CountryOfOrigin = country;
                    producerBLL.AddMethod(producer);
                    Producers = producerBLL.GetAllProducers();
                }
                else
                    MessageBox.Show("Incorrect name or country!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void UpdateProducer(object sender, RoutedEventArgs e, int id, string name, string country)
        {
            try
            {
                if ((producerBLL != null) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(country)))
                {
                    producerBLL.UpdateMethod(id, name, country);
                    Producers = producerBLL.GetAllProducers();
                }
                else
                    MessageBox.Show("Incorrect name or country!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void DeleteProducer(object sender, RoutedEventArgs e, string name, string country)
        {
            try
            {
                if ((producerBLL != null) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(country)))
                {
                    producerBLL.DeleteMethod(name, country);
                    Producers = producerBLL.GetAllProducers();
                }
                else
                    MessageBox.Show("Incorrect name or country!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using LidlManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using LidlManager.View;
using System.Windows.Input;
using System.Collections.ObjectModel;
using LidlManager.Model.BussinessLogicLayer;
using Azure.Identity;
using System.Diagnostics;
using System.Diagnostics.Metrics;

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

            categoryBLL = new CategoryBLL();
            Categories = categoryBLL.GetAllCategories();

            productBLL = new ProductBLL();
            Products = productBLL.GetAllProducts();
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

        #region Categories

        private ObservableCollection<Category> categories = new ObservableCollection<Category>();
        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                if (categories != value)
                {
                    categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }
        private CategoryBLL categoryBLL;
        public CategoryBLL CategoryBLL
        {
            get { return categoryBLL; }
            set
            {
                if (categoryBLL != value)
                {
                    categoryBLL = value;
                    OnPropertyChanged(nameof(CategoryBLL));
                }
            }
        }

        public void AddCategory(object sender, RoutedEventArgs e, string name)
        {
            try
            {
                if ((producerBLL != null) && (!string.IsNullOrEmpty(name)))
                {
                    Category category = new Category();
                    category.Name = name;
                    categoryBLL.AddMethod(category);
                    Categories = categoryBLL.GetAllCategories();
                }
                else
                    MessageBox.Show("Incorrect name!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void UpdateCategory(object sender, RoutedEventArgs e, int id, string name)
        {
            try
            {
                if ((categoryBLL != null) && (!string.IsNullOrEmpty(name)))
                {
                    categoryBLL.UpdateMethod(id, name);
                    Categories = categoryBLL.GetAllCategories();
                }
                else
                    MessageBox.Show("Incorrect name!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void DeleteCategory(object sender, RoutedEventArgs e, string name)
        {
            try
            {
                if ((categoryBLL != null) && (!string.IsNullOrEmpty(name)))
                {
                    categoryBLL.DeleteMethod(name);
                    Categories = categoryBLL.GetAllCategories();
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

        #region Products

        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                if (products != value)
                {
                    products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }
        private ProductBLL productBLL;
        public ProductBLL ProductBLL
        {
            get { return productBLL; }
            set
            {
                if (productBLL != value)
                {
                    productBLL = value;
                    OnPropertyChanged(nameof(ProductBLL));
                }
            }
        }

        public void AddProduct(string name, string barcode, Category category,Producer producer)
        {
            try
            {
                string pattern = @"^\d{1,10}$";
                Regex regex = new Regex(pattern);
                if ((productBLL != null) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(barcode)) && (category!=null) && (producer!=null) && (regex.IsMatch(barcode)))
                {
                    Product product = new Product();
                    product.Name = name;
                    product.Barcode = barcode;
                    product.IdCategory = category.Id;
                    product.IdCategoryNavigation = category;
                    product.IdProducer = producer.Id; 
                    product.IdProducerNavigation = producer;
                    productBLL.AddMethod(product);
                    Products = productBLL.GetAllProducts();
                }
                else
                    MessageBox.Show("Incorrect information!");
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

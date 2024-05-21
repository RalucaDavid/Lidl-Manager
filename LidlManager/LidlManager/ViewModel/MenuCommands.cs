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
using Microsoft.Identity.Client.NativeInterop;

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

            stockBLL = new StockBLL();
            Stocks = stockBLL.GetAllStocks();
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

        public void UpdateProduct(object sender, RoutedEventArgs e, int id, string name, string barcode, Category category, Producer producer)
        {
            try
            {
                string pattern = @"^\d{1,10}$";
                Regex regex = new Regex(pattern);
                if ((productBLL != null) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(barcode)) && (category != null) && (producer != null) && (regex.IsMatch(barcode)))
                {
                    productBLL.UpdateMethod(id, name, barcode, category, producer);
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

        public void DeleteProduct(object sender, RoutedEventArgs e, string name, string barcode, Category category, Producer producer)
        {
            try
            {
                string pattern = @"^\d{1,10}$";
                Regex regex = new Regex(pattern);
                if ((productBLL != null) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(barcode)) && (category != null) && (producer != null) && (regex.IsMatch(barcode)))
                {
                    productBLL.DeleteMethod(name, barcode, category, producer);
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

        #region Stocks

        private ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();
        public ObservableCollection<Stock> Stocks
        {
            get { return stocks; }
            set
            {
                if (stocks != value)
                {
                    stocks = value;
                    OnPropertyChanged(nameof(Stocks));
                }
            }
        }
        private StockBLL stockBLL;
        public StockBLL StockBLL
        {
            get { return stockBLL; }
            set
            {
                if (stockBLL != value)
                {
                    stockBLL = value;
                    OnPropertyChanged(nameof(StockBLL));
                }
            }
        }

        public void AddStock(Product product,string amount, string unit, DateTime? expirationDate, DateTime? supplyDate, string purchase)
        {
            try
            {
                const double profitMarginPercentage = 20.0; 
                const double taxesAndExpenses = 15.0; 
                double amountDouble, purchaseDouble;
                if ((stockBLL != null) && (!string.IsNullOrEmpty(unit)) && (!string.IsNullOrEmpty(amount)) && (!string.IsNullOrEmpty(purchase)) && ( product!= null) 
                          && (double.TryParse(amount, out amountDouble))&&(double.TryParse(purchase, out purchaseDouble))
                          && (supplyDate != null) && ((expirationDate == null)||(expirationDate.Value > supplyDate.Value)))
                {
                    Stock stock = new Stock();
                    stock.Amount = amountDouble;
                    stock.Unit = unit;
                    stock.IdProduct = product.Id;
                    stock.IdProductNavigation = product;
                    stock.SupplyDate = DateOnly.FromDateTime(supplyDate.Value);
                    stock.PuchasePrice = purchaseDouble;
                    stock.SellingPrice = purchaseDouble + purchaseDouble * (profitMarginPercentage / 100) + taxesAndExpenses;
                    if (expirationDate != null)
                    {
                        stock.ExpirationDate = DateOnly.FromDateTime(expirationDate.Value);
                    }
                    stockBLL.AddMethod(stock);
                    Stocks = stockBLL.GetAllStocks();
                }
                else
                    MessageBox.Show("Incorrect information!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void UpdateStock(object sender, RoutedEventArgs e, int id, string selling)
        {
            try
            {
                double sellingDouble;
                if ((producerBLL != null) && (!string.IsNullOrEmpty(selling)) && (double.TryParse(selling, out sellingDouble)))
                {
                    stockBLL.UpdateMethod(id, sellingDouble);
                    Stocks = stockBLL.GetAllStocks();
                }
                else
                    MessageBox.Show("Incorrect information!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void DeleteStock(object sender, RoutedEventArgs e, int id)
        {
            try
            {
                stockBLL.DeleteMethod(id);
                Stocks = stockBLL.GetAllStocks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        #endregion

        #region View1

        private ObservableCollection<Product> producersProducts = new ObservableCollection<Product>();
        public ObservableCollection<Product> ProducersProducts
        {
            get { return producersProducts; }
            set
            {
                if (producersProducts != value)
                {
                    producersProducts = value;
                    OnPropertyChanged(nameof(ProducersProducts));
                }
            }
        }

        private ObservableCollection<CategoryTotalPrice> categoriesSums = new ObservableCollection<CategoryTotalPrice>();
        public ObservableCollection<CategoryTotalPrice> CategoriesSums
        {
            get { return categoriesSums; }
            set
            {
                if (categoriesSums != value)
                {
                    categoriesSums = value;
                    OnPropertyChanged(nameof(CategoriesSums));
                }
            }
        }

        public void SearchProductsByProducer(object sender, RoutedEventArgs e, int id)
        {
            try
            {
                ProducersProducts = productBLL.GetProductsByProducer(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void AddCategoriesSums()
        {
            try
            {
                CategoriesSums = categoryBLL.GetAllCategoriesSums();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        #endregion

        #region Cashier

        private ObservableCollection<Stock> productsSuggestions = new ObservableCollection<Stock>();
        public ObservableCollection<Stock> ProductsSuggestions
        {
            get { return productsSuggestions; }
            set
            {
                if (productsSuggestions != value)
                {
                    productsSuggestions = value;
                    OnPropertyChanged(nameof(ProductsSuggestions));
                }
            }
        }

        public void SearchProduct(string name, string barcode, int? selectedCategory, int? selectedProducer, DateTime? selectedExpirationDate)
        {
            try
            {
                string pattern = @"^\d{1,10}$";
                Regex regex = new Regex(pattern);
                if ((productBLL != null) && ((regex.IsMatch(barcode) || string.IsNullOrEmpty(barcode))))
                {
                    ProductsSuggestions = stockBLL.SearchProduct(name, barcode, selectedCategory, selectedProducer, selectedExpirationDate);
                }
                else
                {
                    MessageBox.Show("Incorrect information!The barcode must have only numbers.");
                }
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

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.BussinessLogicLayer
{
    class ProductBLL
    {
        private LidlManagerContext lidlManager = new LidlManagerContext();
        public void AddMethod(object obj)
        {
            if (obj is Product newProduct)
            {
                var existingProduct = lidlManager.Products.FirstOrDefault(p => p.Barcode == newProduct.Barcode);
                if (existingProduct != null)
                {
                    throw new InvalidOperationException("A product with the same barcode already exists.");
                }

                var existingProducer = lidlManager.Producers.FirstOrDefault(p => p.Id == newProduct.IdProducer);
                if (existingProducer == null)
                {
                    throw new InvalidOperationException("The producer doesn't exist.");
                }

                var existingCategory = lidlManager.Categories.FirstOrDefault(p => p.Id == newProduct.IdCategory);
                if (existingCategory == null)
                {
                    throw new InvalidOperationException("The category doesn't exist.");
                }

                existingProduct = lidlManager.Products.FirstOrDefault(p => p.Name == newProduct.Name && p.IdProducer == newProduct.IdProducer);
                if (existingProduct != null)
                {
                    throw new InvalidOperationException("A product with the same name and the same producer already exists.");
                }
                newProduct.IdProducerNavigation = existingProducer;
                newProduct.IdCategoryNavigation = existingCategory;
                newProduct.IsActive = true;
                lidlManager.Products.Add(newProduct);
                lidlManager.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Object is not a Product");
            }
        }

        public void UpdateMethod(int id, string name, string barcode, Category category, Producer producer)
        {
            var existingProduct = lidlManager.Products.FirstOrDefault(p => p.Barcode == barcode && p.Id != id);
            if (existingProduct != null)
            {
                throw new InvalidOperationException("A product with the same barcode already exists.");
            }

            existingProduct = lidlManager.Products.FirstOrDefault(p => p.IdProducer == producer.Id && p.Name == name && p.Id != id);
            if (existingProduct != null)
            {
                throw new InvalidOperationException("A product with the same name and the same producer already exists.");
            }

            existingProduct = lidlManager.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("The product does not exist.");
            }

            var existingProducer = lidlManager.Producers.FirstOrDefault(p => p.Id == producer.Id);
            if (existingProducer == null)
            {
                throw new InvalidOperationException("The producer doesn't exist.");
            }

            var existingCategory = lidlManager.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null)
            {
                throw new InvalidOperationException("The category doesn't exist.");
            }
            existingProduct.Name = name;
            existingProduct.Barcode = barcode;
            existingProduct.IdProducer = producer.Id;
            existingProduct.IdCategory = category.Id;
            lidlManager.SaveChanges();
        }

        public void DeleteMethod(string name, string barcode, Category category, Producer producer)
        {
            var existingProduct = lidlManager.Products.FirstOrDefault(p => p.Name == name && p.Barcode == barcode && 
                                                                           p.IdProducer == producer.Id && p.IdCategory == category.Id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("There is no product with this information.");
            }
            else
            {
                var activeStocks = lidlManager.Stocks.Any(s => s.IdProduct == existingProduct.Id && s.IsActive);

                if (activeStocks)
                {
                    throw new InvalidOperationException("Product cannot be deleted because it has stocks.");
                }

                existingProduct.IsActive = false;
                lidlManager.SaveChanges();
            }
        }

        public ObservableCollection<Product> GetAllProducts()
        {
            ObservableCollection<Product> result = new ObservableCollection<Product>();
            var productsFromDb = lidlManager.Products.Include(p => p.IdProducerNavigation)
                                                     .Include(p => p.IdCategoryNavigation).ToList();
            foreach (var product in productsFromDb)
            {
                if ((bool)product.IsActive)
                    result.Add(product);
            }
            return result;
        }

        //public ObservableCollection<Product> SearchProduct()
        //{

        //}
    }
}

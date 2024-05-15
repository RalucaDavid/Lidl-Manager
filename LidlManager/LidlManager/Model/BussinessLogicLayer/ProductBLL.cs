using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

                existingProduct = lidlManager.Products.FirstOrDefault(p => p.Name == newProduct.Name && p.IdProducer == newProduct.IdProducer);
                if (existingProduct != null)
                {
                    throw new InvalidOperationException("A product with the same name and the same producer already exists.");
                }
                newProduct.IdProducerNavigation = existingProducer;
                newProduct.IsActive = true;
                lidlManager.Products.Add(newProduct);
                lidlManager.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Object is not a Product");
            }
        }

        public void UpdateMethod(int id, string name, string country)
        {
            
        }

        public void DeleteMethod(string name, string country)
        {
           
        }

        public ObservableCollection<Product> GetAllProducts()
        {
            ObservableCollection<Product> result = new ObservableCollection<Product>();
            var productsFromDb = lidlManager.Products.Include(p => p.IdProducerNavigation).ToList();
            foreach (var product in productsFromDb)
            {
                if ((bool)product.IsActive)
                    result.Add(product);
            }
            return result;
        }
    }
}

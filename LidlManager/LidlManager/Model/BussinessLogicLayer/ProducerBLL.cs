using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LidlManager.Model.BussinessLogicLayer
{
    class ProducerBLL
    {
        private LidlManagerContext lidlManager = new LidlManagerContext();
        public void AddMethod(object obj)
        {
            if (obj is Producer newProducer)
            {
                var existingProducer = lidlManager.Producers.FirstOrDefault(p => p.Name == newProducer.Name && p.CountryOfOrigin == newProducer.CountryOfOrigin);
                if (existingProducer != null)
                {
                    throw new InvalidOperationException("A producer with the same name and the same country already exists.");
                }
                newProducer.IsActive = true;
                lidlManager.Producers.Add(newProducer);
                lidlManager.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Object is not a Producer");
            }
        }

        public void UpdateMethod(int id, string name, string country)
        {
            var existingProducer = lidlManager.Producers.FirstOrDefault(p => p.Name == name && p.CountryOfOrigin == country && p.Id != id);
            if (existingProducer != null)
            {
                throw new InvalidOperationException("A producer with the same information already exists.");
            }
            else
            {
                existingProducer = lidlManager.Producers.FirstOrDefault(p => p.Id == id);
                if (existingProducer == null)
                {
                    throw new InvalidOperationException("Something went wrong.");
                }
                existingProducer.Name = name;
                existingProducer.CountryOfOrigin = country;
                lidlManager.SaveChanges();
            }
        }

        public void DeleteMethod(string name, string country)
        {
            var existingProducer = lidlManager.Producers.FirstOrDefault(p => p.Name == name && p.CountryOfOrigin == country);
            if (existingProducer == null)
            {
                throw new InvalidOperationException("There is no producer with this information.");
            }
            else
            {
                existingProducer.IsActive = false;
                lidlManager.SaveChanges();
            }
        }

        public ObservableCollection<Producer> GetAllProducers()
        {
            ObservableCollection<Producer> result = new ObservableCollection<Producer>();
            var producersFromDb = lidlManager.Producers.ToList();
            foreach (var producer in producersFromDb)
            {
                if ((bool)producer.IsActive)
                    result.Add(producer);
            }
            return result;
        }

        public ObservableCollection<Product>  GetProductsByProducer(int id)
        {
            var products = lidlManager.Products
            .Where(p => p.IdProducer == id && p.IsActive)
            .Include(p => p.IdCategoryNavigation)
            .ToList();
            return new ObservableCollection<Product>(products);
        }
    }
}

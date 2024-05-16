using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.BussinessLogicLayer
{
    class StockBLL
    {
        private LidlManagerContext lidlManager = new LidlManagerContext();
        public void AddMethod(object obj)
        {
            if (obj is Stock newStock)
            {
                var existingProduct = lidlManager.Products.FirstOrDefault(p => p.Id == newStock.IdProduct);
                if (existingProduct == null)
                {
                    throw new InvalidOperationException("The product doesn't exist.");
                }

                newStock.IdProductNavigation = existingProduct;
                newStock.IsActive = true;
                lidlManager.Stocks.Add(newStock);
                lidlManager.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Object is not a Product");
            }
        }

        public void UpdateMethod(int id, string name, string barcode, Category category, Producer producer)
        {
           
        }

        public void DeleteMethod(string name, string barcode, Category category, Producer producer)
        {
           
        }

        public ObservableCollection<Stock> GetAllStocks()
        {
            ObservableCollection<Stock> result = new ObservableCollection<Stock>();
            var stocksFromDb = lidlManager.Stocks.Include(s => s.IdProductNavigation);
            foreach (var stock in stocksFromDb)
            {
                if ((bool)stock.IsActive)
                    result.Add(stock);
            }
            return result;
        }
    }
}

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
                throw new ArgumentException("Object is not a Stock");
            }
        }

        public void UpdateMethod(int id, double sellingPrice)
        {
            var existingStock = lidlManager.Stocks.FirstOrDefault(s => s.Id == id);
            if (existingStock == null)
            {
                throw new InvalidOperationException("The stock doesn't exist.");
            }
            else
            {
                if (existingStock.PuchasePrice > sellingPrice)
                {
                    throw new InvalidOperationException("The sale price is lower than the purchase price.");
                }
                existingStock.SellingPrice = sellingPrice;
                lidlManager.SaveChanges();
            }
        }

        public void DeleteMethod(int id)
        {
            var existingStock = lidlManager.Stocks.FirstOrDefault(s => s.Id == id);
            if (existingStock == null)
            {
                throw new InvalidOperationException("The stock doesn't exist.");
            }
            existingStock.IsActive = false;
            lidlManager.SaveChanges();
        }

        public ObservableCollection<Stock> GetAllStocks()
        {
            ObservableCollection<Stock> result = new ObservableCollection<Stock>();
            var stocksFromDb = lidlManager.Stocks.Include(s => s.IdProductNavigation);
            foreach (var stock in stocksFromDb)
            {
                if (stock.ExpirationDate.HasValue && stock.ExpirationDate.Value.ToDateTime(TimeOnly.MinValue) < DateTime.Now)
                    stock.IsActive = false;
                if ((bool)stock.IsActive)
                    result.Add(stock);
            }
            return result;
        }
    }
}

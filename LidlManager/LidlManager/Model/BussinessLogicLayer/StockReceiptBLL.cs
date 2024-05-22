using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.BussinessLogicLayer
{
    class StockReceiptBLL
    {
        private LidlManagerContext lidlManager = new LidlManagerContext();

        public void AddMethod(object obj, int quantity)
        {
            if (obj is StockReceipt newStockReceipt)
            {
                var existingStock = lidlManager.Stocks
                    .Include(s => s.IdProductNavigation)
                    .FirstOrDefault(s => s.Id == newStockReceipt.IdStock);
                if (existingStock== null)
                {
                    throw new InvalidOperationException("The stock doesn't exist.");
                }
                var existingReceipt = lidlManager.Receipts.FirstOrDefault(r => r.Id == newStockReceipt.IdReceipt);
                if (existingReceipt == null)
                {
                    throw new InvalidOperationException("The receipt doesn't exist.");
                }
                newStockReceipt.IdReceiptNavigation = existingReceipt;
                newStockReceipt.IdStockNavigation = existingStock;
                newStockReceipt.Subtotal = quantity * existingStock.SellingPrice;
                newStockReceipt.Amount = quantity;
                existingStock.Amount= existingStock.Amount - quantity;
                if(existingStock.Amount==0)
                    existingStock.IsActive = false;
                lidlManager.StockReceipts.Add(newStockReceipt);
                lidlManager.SaveChanges();
            }
            else
            {
                throw new ArgumentException("The provided object is not a StockReceipt.");
            }
        }

        public ObservableCollection<StockReceipt> GetAllStockReceipts()
        {
            ObservableCollection<StockReceipt> result = new ObservableCollection<StockReceipt>();
            var stockReceiptFromDb = lidlManager.StockReceipts
                    .Include(sr => sr.IdReceiptNavigation)
                    .Include(sr => sr.IdStockNavigation)
                    .ThenInclude(sn => sn.IdProductNavigation)
                    .ToList();
            foreach (var stockReceipt in stockReceiptFromDb)
            {
                if ((bool)stockReceipt.IdReceiptNavigation.IsActive)
                    result.Add(stockReceipt);
            }
            return result;
        }
    }
}

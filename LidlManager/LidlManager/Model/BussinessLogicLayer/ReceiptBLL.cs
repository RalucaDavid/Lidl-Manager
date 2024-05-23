using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidlManager.Model.BussinessLogicLayer
{
    class ReceiptBLL
    {
        private LidlManagerContext lidlManager = new LidlManagerContext();

        public void AddMethod(object obj, User user)
        {
            if (obj is Receipt newReceipt)
            {
                var existingUser = lidlManager.Users.FirstOrDefault(u => u.Name == user.Name);
                if (existingUser == null)
                {
                    throw new InvalidOperationException("The user doesn't exist.");
                }
                newReceipt.IdUser = user.Id;
                newReceipt.IdUserNavigation = existingUser;
                newReceipt.IsActive = true;
                newReceipt.ReleaseDate = DateOnly.FromDateTime(DateTime.Now.Date);
                lidlManager.Add(newReceipt);
                lidlManager.SaveChanges();
            }
            else
            {
                throw new ArgumentException("The provided object is not a Receipt.");
            }
        }

        public void DeleteMethod(int id)
        {
            var existingReceipt = lidlManager.Receipts.FirstOrDefault(r => r.Id == id);
            if (existingReceipt == null)
            {
                throw new InvalidOperationException("The receipt doesn't exist.");
            }
            existingReceipt.IsActive = false;
            lidlManager.SaveChanges();
        }

        public ObservableCollection<StockReceipt> GetTheBiggestReceipt(DateTime date)
        {
            var biggestReceipt = lidlManager.Receipts
                                    .Where(r => r.ReleaseDate == DateOnly.FromDateTime(date.Date) && r.IsActive)
                                    .OrderByDescending(r => r.FloatTotalSum)
                                    .FirstOrDefault();
            if (biggestReceipt == null)
            {
                return new ObservableCollection<StockReceipt>();
            }
            var stockReceipts = lidlManager.StockReceipts
                                           .Where(sr => sr.IdReceipt == biggestReceipt.Id)
                                           .ToList();
            return new ObservableCollection<StockReceipt>(stockReceipts);
        }

        public ObservableCollection<DailyEarnings> GetDailyEarnings(int idUser, int month)
        {
            var receipts = lidlManager.Receipts
                                      .Where(r => r.IdUser == idUser &&
                                                  r.ReleaseDate.Month == month &&
                                                  r.IsActive)
                                      .ToList();
            var dailyEarnings = receipts.GroupBy(r => r.ReleaseDate)
                                        .Select(g => new DailyEarnings
                                        {
                                            Date = g.Key.ToDateTime(TimeOnly.MinValue),
                                            TotalAmount = (decimal)g.Sum(r => r.FloatTotalSum)
                                        })
                                        .OrderBy(e => e.Date)
                                        .ToList();
            return new ObservableCollection<DailyEarnings>(dailyEarnings);
        }
    }
}

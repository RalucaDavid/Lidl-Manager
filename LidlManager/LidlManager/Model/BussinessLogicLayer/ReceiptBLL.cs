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

        }
    }
}

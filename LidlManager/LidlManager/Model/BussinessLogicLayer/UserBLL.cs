using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LidlManager.Model.BussinessLogicLayer
{
    class UserBLL
    {
        private LidlManagerContext lidlManager = new LidlManagerContext();

        public void AddMethod(object obj)
        {
            if (obj is User newUser)
            {
                var existingUser = lidlManager.Users.FirstOrDefault(u => u.Name == newUser.Name);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("A user with the same username already exists.");
                }
                newUser.IsActive = true;
                lidlManager.Users.Add(newUser);
                lidlManager.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Object is not a User");
            }
        }

        //public void UpdateMethod(object obj)
        //{

        //}

        //public void DeleteMethod(object obj)
        //{

        //}

        public ObservableCollection<User> GetAllUsers()
        {
            ObservableCollection<User> result = new ObservableCollection<User>();
            var usersFromDb = lidlManager.Users.ToList();
            foreach (var user in usersFromDb)
            {
                if ((bool)user.IsActive)
                    result.Add(user);
            }
            return result;
        }

        public User? Login(string username, string password)
        {
            return lidlManager.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
        }
    }
}

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
    class CategoryBLL
    {
        private LidlManagerContext lidlManager = new LidlManagerContext();
        public void AddMethod(object obj)
        {
            if (obj is Category newCategory)
            {
                var existingCategory = lidlManager.Categories.FirstOrDefault(c => c.Name == newCategory.Name);
                if (existingCategory != null)
                {
                    throw new InvalidOperationException("A category with the same name already exists.");
                }
                newCategory.IsActive = true;
                lidlManager.Categories.Add(newCategory);
                lidlManager.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Object is not a Category");
            }
        }

        public void UpdateMethod(int id, string name)
        {
            var existingCategory = lidlManager.Categories.FirstOrDefault(c => c.Name == name && c.Id != id);
            if (existingCategory != null)
            {
                throw new InvalidOperationException("A category with the same information already exists.");
            }
            else
            {
                existingCategory = lidlManager.Categories.FirstOrDefault(c => c.Id == id);
                if (existingCategory == null)
                {
                    throw new InvalidOperationException("Something went wrong.");
                }
                existingCategory.Name = name;
                lidlManager.SaveChanges();
            }
        }

        public void DeleteMethod(string name)
        {
            var existingCategory = lidlManager.Categories.FirstOrDefault(c => c.Name == name);
            if (existingCategory == null)
            {
                throw new InvalidOperationException("There is no category with this information.");
            }
            else
            {
                existingCategory.IsActive = false;
                lidlManager.SaveChanges();
            }
        }

        public ObservableCollection<Category> GetAllCategories()
        {
            ObservableCollection<Category> result = new ObservableCollection<Category>();
            var categoriesFromDb = lidlManager.Categories.ToList();
            foreach (var category in categoriesFromDb)
            {
                if ((bool)category.IsActive)
                    result.Add(category);
            }
            return result;
        }
    }
}

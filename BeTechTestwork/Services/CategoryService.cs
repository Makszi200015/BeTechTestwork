using BeTechTestwork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTechTestwork.Services
{
    public class CategoryService : InventoryControlInterface<Category>
    {
        private InventoryControlContext db;

        public CategoryService(InventoryControlContext _db)
        {
            db = _db;
        }
        public void Create(Category item)
        {
            db.Category.Add(item);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            Category category = db.Category.Find(id);
            db.Category.Remove(category);
            db.SaveChanges();
        }

        public IEnumerable<Category> GetList()
        {
            IEnumerable<Category> categories = db.Category.ToList();
            return categories;
        }

        public void Update(Category item)
        {
            db.Category.Update(item);
            db.SaveChanges();
        }

        public Category Get(string Name)
        {
            return db.Category.FirstOrDefault(x => x.ProdCategory == Name);
        }
    }
}

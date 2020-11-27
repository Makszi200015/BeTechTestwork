using BeTechTestwork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTechTestwork.Services
{
    public class WarehouseService : InventoryControlInterface<Warehouse>
    {
        private InventoryControlContext db;
        public WarehouseService(InventoryControlContext _db)
        {
            db = _db;
        }
        public void Create(Warehouse item)
        {
            db.Warehouses.Add(item);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            Warehouse warehouse = db.Warehouses.Find(id);
            db.Warehouses.Remove(warehouse);
            db.SaveChanges();
        }

        public IEnumerable<Warehouse> GetList()
        {
            IEnumerable<Warehouse> warehouse = db.Warehouses.ToList();
            return warehouse;
        }

        public void Update(Warehouse item)
        {
            db.Warehouses.Update(item);
            db.SaveChanges();
        }

        public Warehouse Get(string id) 
        {
            return db.Warehouses.FirstOrDefault(x => x.WarehouseName == id );
        }
    }
}

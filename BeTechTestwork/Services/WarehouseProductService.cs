using BeTechTestwork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTechTestwork.Services
{
    public class WarehouseProductService : InventoryControlInterface<WarehouseProduct>
    {
        private InventoryControlContext db;
        public WarehouseProductService(InventoryControlContext _db)
        {
            db = _db;
        }
        public void Create(WarehouseProduct item)
        {
            db.WarehouseProducts.Add(item);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            WarehouseProduct warehouseProduct = db.WarehouseProducts.Find(id);
            db.WarehouseProducts.Remove(warehouseProduct);
            db.SaveChanges();
        }

        public IEnumerable<WarehouseProduct> GetList()
        {
            IEnumerable<WarehouseProduct> warehouseProducts = db.WarehouseProducts.ToList();
            return warehouseProducts;
        }

        public void Update(WarehouseProduct item)
        {
            db.WarehouseProducts.Update(item);
            db.SaveChanges();
        }

        public bool IsWarehouseProductsExist(string Name, string propertyName)
        {
            bool result = false;
            switch (propertyName)
            {
                case "ProdBarcodeNumber":
                    result = db.WarehouseProducts.Any(x => x.ProdBarcodeNumber == Name);
                    break;
                case "WarehouseName":
                    result = db.WarehouseProducts.Any(x => x.WarehouseName == Name);
                    break;
                case "id":
                    result = db.WarehouseProducts.Any(x => x.id == Name);
                    break;
            }
            return result;
        }

        public WarehouseProduct Get(string Name, string propertyName)
        {
            WarehouseProduct warehouseProduct = new WarehouseProduct();
            switch (propertyName)
            {
                case "ProdBarcodeNumber":
                    warehouseProduct = db.WarehouseProducts.FirstOrDefault(x => x.ProdBarcodeNumber == Name);
                    break;
                case "WarehouseName":
                    warehouseProduct = db.WarehouseProducts.FirstOrDefault(x => x.WarehouseName == Name);
                    break;
                case "id":
                    warehouseProduct = db.WarehouseProducts.FirstOrDefault(x => x.id == Name);
                    break;
            }
            return warehouseProduct;
        }
    }
}

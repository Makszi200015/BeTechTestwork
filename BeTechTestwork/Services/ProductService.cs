using BeTechTestwork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BeTechTestwork.Services
{
    public class ProductService : InventoryControlInterface<Product>
    {
        private InventoryControlContext db;
        public ProductService(InventoryControlContext _db)
        {
            db = _db;
        }
        public static string CreateUniqueBarcodeNumber()
        {
            var bytes = new byte[4];
            var randomNumber = RandomNumberGenerator.Create();
            randomNumber.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            string uniqueBarcodeNumber = String.Format("{0:D8}", random);
            return uniqueBarcodeNumber;
        }
        public void Create(Product item)
        {
            IEnumerable<Currency> currencies = db.Currency.ToList();
            db.Product.Add(CreateCurrencyPriceInNewModel(item, currencies));
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
        }

        public IEnumerable<Product> GetList()
        {
            IEnumerable<Product> products = db.Product.ToList();
            return products;
        }

        public void Update(Product item)
        {
            IEnumerable<Currency> currencies = db.Currency.ToList();
            db.Update(CreateCurrencyPriceInNewModel(item, currencies));
            db.SaveChanges();
        }

        public static Product CreateCurrencyPriceInNewModel(Product item, IEnumerable<Currency> currencies)
        {
            //Product product = new Product { BarcodeNumber = CreateUniqueBarcodeNumber(), BaseCurrencyPrice = item.BaseCurrencyPrice, Currency = item.Currency, Name = item.Name, PriceEur = item.PriceEur, PriceUah = item.PriceUah, ProdCategory = item.ProdCategory };
            foreach (var currencyitem in currencies)
            {
                if (currencyitem.Code == "EUR")
                {
                    item.PriceEur = item.BaseCurrencyPrice * currencyitem.Course;
                }
                if (currencyitem.Code == "UAH")
                {
                    item.PriceUah = item.BaseCurrencyPrice * currencyitem.Course;
                }
            }
            if(item.BarcodeNumber == null) 
            {
                item.BarcodeNumber = CreateUniqueBarcodeNumber();
            }
            return item;
        }

        public bool IsProductExist(string Name, string propertyName)
        {
            bool result = false;
            switch (propertyName)
            {
                case "Name":
                    result = db.Product.Any(x => x.Name == Name);
                    break;
                case "BarcodeNumber":
                    result = db.Product.Any(x => x.BarcodeNumber == Name);
                    break;
                case "Category":
                    result = db.Product.Any(x => x.ProdCategory == Name);
                    break;
            }
            return result;
        }

        public Product Get(string Name, string propertyName)
        {
            Product product = new Product();
            switch (propertyName)
            {
                case "Name":
                    product = db.Product.FirstOrDefault(x => x.Name == Name);
                    break;
                case "BarcodeNumber":
                    product = db.Product.FirstOrDefault(x => x.BarcodeNumber == Name);
                    break;
                case "Category":
                    product = db.Product.FirstOrDefault(x => x.ProdCategory == Name);
                    break;
            }
            return product;
        }
    }
}

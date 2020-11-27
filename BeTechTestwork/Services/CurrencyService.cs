using BeTechTestwork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTechTestwork.Services
{
    public class CurrencyService : InventoryControlInterface<Currency>
    {
        private InventoryControlContext db;
        public CurrencyService(InventoryControlContext _db)
        {
            db = _db;
        }
        public void Create(Currency item)
        {

            if (item.Code != "EUR")
            {
                db.Currency.Add(item);
            }
            else if (item.Code == "EUR")
            {
                float UAHCourse = db.Currency.FirstOrDefault(x => x.Code == "UAH").Course;
                db.Currency.Add(CalculationEuroCourse(item, UAHCourse));
            }

            db.SaveChanges();
        }

        public void Delete(string id)
        {
            Currency currency = db.Currency.Find(id);
            db.Remove(currency);
            db.SaveChanges();
        }

        public IEnumerable<Currency> GetList()
        { 
            return db.Currency.ToList(); 
        }

        public bool IsCurrencyExist(string CurrencyName)
        {
            return db.Currency.Any(x => x.CurrencyName == CurrencyName);
        }

        public void Update(Currency item)
        {
            if (item.Code != "EUR")
            {
                db.Currency.Update(item);
            }
            else 
            {
                float UAHCourse = db.Currency.FirstOrDefault(x => x.Code == "UAH").Course;
                db.Currency.Update(CalculationEuroCourse(item, UAHCourse));
            }

            db.SaveChanges();
        }
        public static Currency CalculationEuroCourse(Currency item, float UAHCourse)
        {
            Currency currency = new Currency { Code = item.Code, Course = UAHCourse / item.Course, CurrencyName = item.CurrencyName, UpdateDate = item.UpdateDate };
            return currency;
        }
        public Currency GetCourse(string id)
        {
            return db.Currency.FirstOrDefault(x => x.CurrencyName == id);
        }
    }
}

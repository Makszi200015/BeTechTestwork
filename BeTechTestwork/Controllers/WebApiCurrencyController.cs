using BeTechTestwork.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTechTestwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiCurrencyController : ControllerBase
    {
        CurrencyService service;
        ProductService productService;
        public WebApiCurrencyController(CurrencyService _service, ProductService _productService)
        {
            service = _service;
            productService = _productService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Create([FromBody] Currency currency)
        {
            if (currency != null)
            {
                if (!service.IsCurrencyExist(currency.CurrencyName))
                {
                    service.Create(currency);
                }
                else
                {
                    service.Update(currency);
                    IEnumerable<Product> products = productService.GetList();
                    foreach (var item in products) 
                    {
                        productService.Update(item);
                    }
                    
                }
                return Ok();
            }
            return BadRequest();
        }
        //[HttpPost]
        //[Route("/Currency/Update")]
        //public IActionResult Update([FromBody]Currency currencyUAH,[FromBody] Currency currencyEUR)
        //{
        //    if (currencyUAH != null && currencyEUR != null)
        //    {
        //        service.Update(currencyUAH);
        //        service.Update(currencyEUR);
        //        IEnumerable<Product> products = productService.GetList();
        //        foreach (var item in products)
        //        {
        //            Product product = new Product { BarcodeNumber = item.BarcodeNumber, BaseCurrencyPrice = item.BaseCurrencyPrice, Currency = item.Currency, Name = item.Name, PriceEur = item.PriceEur, PriceUah = item.PriceUah, ProdCategory = item.ProdCategory };
        //            int? productPriceInEurAtTheCourse = currencyEUR.Course * item.BaseCurrencyPrice;
        //            int? productPriceInUahAtTheCourse = currencyUAH.Course * item.BaseCurrencyPrice;
        //            if (item.PriceEur != productPriceInEurAtTheCourse && item.PriceUah != productPriceInUahAtTheCourse)
        //            {
        //                product.PriceEur = productPriceInEurAtTheCourse;
        //                product.PriceUah = productPriceInUahAtTheCourse;
        //                productService.Update(product);
        //            }
        //            if (item.PriceEur != productPriceInEurAtTheCourse)
        //            {
        //                product.PriceEur = productPriceInEurAtTheCourse;
        //                productService.Update(product);
        //            }
        //            if (item.PriceUah != productPriceInUahAtTheCourse)
        //            {
        //                product.PriceUah = productPriceInUahAtTheCourse;
        //                productService.Update(product);
        //            }
        //        }
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        //[HttpDelete]
       // [Route("/Currency/Delete")]
        //public IActionResult Delete(string id)
        //{
           // if (id != null)
            //{
            //    Currency currency = service.GetCourse(id);
            //    service.Delete(id);
            //    IEnumerable<Product> products = productService.GetList();
            //    foreach (var item in products)
            //    {
            //        Product product = new Product { BarcodeNumber = item.BarcodeNumber, BaseCurrencyPrice = item.BaseCurrencyPrice, Currency = item.Currency, Name = item.Name, PriceEur = item.PriceEur, PriceUah = item.PriceUah, ProdCategory = null };
            //        float? productPriceRemotedCurrency = currency.Course * item.BaseCurrencyPrice;
            //        if (item.BaseCurrencyPrice == productPriceRemotedCurrency)
            //        {
            //            item.BaseCurrencyPrice = 0;
            //            productService.Update(product);
            //        }
            //        else if (item.PriceEur == productPriceRemotedCurrency)
            //        {
            //            item.PriceEur = 0;
            //            productService.Update(product);
            //        }
            //        else if (item.PriceUah == productPriceRemotedCurrency)
            //        {
            //            item.PriceUah = 0;
            //            productService.Update(product);
            //        }
            //    }
            //    return Ok();
            //}
         //   return BadRequest();
       // }

    }
}

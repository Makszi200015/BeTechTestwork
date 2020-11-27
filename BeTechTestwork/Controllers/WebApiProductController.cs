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
    public class WebApiProductController : ControllerBase
    {
        ProductService service;
        CurrencyService currencyservice;
        WarehouseProductService warehouseProductService;
        CategoryService categoryService;
        public WebApiProductController(ProductService _service, CurrencyService _currencyservice, WarehouseProductService _warehouseProductService, CategoryService _categoryService)
        {
            service = _service;
            currencyservice = _currencyservice;
            warehouseProductService = _warehouseProductService;
            categoryService = _categoryService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Product product, string warehouseNameFromwWarehouseProduct, int countProductFromwWarehouseProduct, string propertyName)
        {
            if (product != null)
            {
                if (!service.IsProductExist(product.Name, propertyName))
                {

                    service.Create(product);
                    WarehouseProduct warehouseProduct = new WarehouseProduct { id = Guid.NewGuid().ToString(), WarehouseName = warehouseNameFromwWarehouseProduct, ProdBarcodeNumber = product.BarcodeNumber, Count = countProductFromwWarehouseProduct };
                    warehouseProductService.Create(warehouseProduct);
                }
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/Product/GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = service.GetList();
            if (products != null)
            {
                return Ok(products);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get(string id, string propertyName)
        {
            if (id != null && propertyName != null)
            {
                string warehouse;
                Product products = service.Get(id, propertyName);
                WarehouseProduct warehouseProduct = warehouseProductService.Get(id, "Prod" + propertyName);
                if (products != null)
                {
                    warehouse = warehouseProduct.WarehouseName;
                    return Ok(new { products, warehouse });
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string id, string propertyName)
        {
            if (id != null)
            {
                if (warehouseProductService.IsWarehouseProductsExist(id, propertyName))
                {
                    WarehouseProduct warehouseProduct = warehouseProductService.Get(id, propertyName);
                    warehouseProductService.Delete(warehouseProduct.id);
                }
                service.Delete(id);
                return Ok();
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Product product, string Warehouse, int Count)
        {
            if (product != null)
            {
                IEnumerable<WarehouseProduct> warehouseProducts = warehouseProductService.GetList();
                foreach (var item in warehouseProducts)
                {
                    if (item.ProdBarcodeNumber == product.BarcodeNumber)
                    {
                        if(item.WarehouseName != Warehouse)
                        {
                            WarehouseProduct warehouseProduct = new WarehouseProduct { id = Guid.NewGuid().ToString(), Count = Count, ProdBarcodeNumber = product.BarcodeNumber, WarehouseName = Warehouse };
                            warehouseProductService.Create(warehouseProduct);
                        }
                    }
                }
                service.Update(product);
                return Ok();
            }
            return BadRequest();
        }
    }
}

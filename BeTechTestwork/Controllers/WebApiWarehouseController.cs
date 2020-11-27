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
    public class WebApiWarehouseController : ControllerBase
    {
        WarehouseService service;
        WarehouseProductService warehouseProductService;
        ProductService productService;
        public WebApiWarehouseController(WarehouseService _service, WarehouseProductService _warehouseProductService, ProductService _productService)
        {
            service = _service;
            warehouseProductService = _warehouseProductService;
            productService = _productService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Warehouse warehouse)
        {
            if (warehouse != null)
            {
                service.Create(warehouse);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateProductInWarehouse")]
        public IActionResult CreateProductInWarehouse(WarehouseProduct warehouseProduct, string productName)
        {
            if (warehouseProduct != null)
            {
                string propertyName = "Name";
                Product product = productService.Get(productName, propertyName);
                IEnumerable<WarehouseProduct> warehouseProducts = warehouseProductService.GetList();
                foreach (var item in warehouseProducts)
                {
                    if (item.WarehouseName != warehouseProduct.WarehouseName)
                    {
                        if (item.ProdBarcodeNumber != product.BarcodeNumber)
                        {
                            warehouseProduct.id = Guid.NewGuid().ToString();
                            warehouseProduct.ProdBarcodeNumber = product.BarcodeNumber;
                            warehouseProductService.Create(warehouseProduct);
                            return Ok();
                        }
                    }
                }

            }
            return BadRequest("This product already exiting");
        }
        [HttpPost]
        [Route("UpdateProductInWarehouse")]
        public IActionResult UpdateProductInWarehouse(WarehouseProduct warehouseProduct)
        {
            if (warehouseProduct != null)
            {
                warehouseProductService.Update(warehouseProduct);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get(string id)
        {
            if (id != null)
            {
                Warehouse warehouse = service.Get(id);
                if (warehouse != null)
                {
                    return Ok(warehouse);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetProductInWarehouse")]
        public IActionResult GetProductInWarehouse(string id)
        {
            if (id != null )
            {
                WarehouseProduct warehouseProduct = warehouseProductService.Get(id, "id");
                if (warehouseProduct != null)
                {
                    return Ok(warehouseProduct);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Warehouse warehouse)
        {
            if (warehouse != null)
            {
                service.Update(warehouse);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/Warehouse/GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Warehouse> warehouses = service.GetList();
            if (warehouses != null)
            {
                return Ok(warehouses);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/Warehouse/GetAllProductsInWarehouse")]
        public IActionResult GetAllProductsInWarehouse()
        {
            IEnumerable<WarehouseProduct> warehouses = warehouseProductService.GetList();
            if (warehouses != null)
            {
                return Ok(warehouses);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("DeleteProductInWarehouse")]
        public IActionResult DeleteProductInWarehouse(string id)
        {
            if (id != null)
            {
                warehouseProductService.Delete(id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string id, string propertyName)
        {
            if (id != null && propertyName != null)
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

    }
}

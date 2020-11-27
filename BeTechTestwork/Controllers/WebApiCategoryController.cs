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
    public class WebApiCategoryController : ControllerBase
    {
        CategoryService service;
        ProductService productService;
        public WebApiCategoryController(CategoryService _service, ProductService _productService)
        {
            service = _service;
            productService = _productService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Category category)
        {
            if (category != null)
            {
                service.Create(category);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Category category, string categoryNameBeforeChange)
        {
            if (category != null)
            {
                Product product = productService.Get(categoryNameBeforeChange, "Category");
                if (product != null && product.ProdCategory == categoryNameBeforeChange)
                {
                    product.ProdCategory = null;
                    productService.Update(product);
                    service.Delete(categoryNameBeforeChange);
                    service.Create(category);
                    product.ProdCategory = category.ProdCategory;
                    productService.Update(product);
                }
                else 
                {
                    service.Delete(categoryNameBeforeChange);
                    service.Create(category);
                }              
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("/Category/GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Category> categories = service.GetList();
            if (categories != null)
            {
                return Ok(categories);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string id, string propertyName)
        {
            if (id != null)
            {
                Product product = productService.Get(id, propertyName);
                if (product.ProdCategory == id)
                {
                    product.ProdCategory = null;
                    productService.Update(product);
                }
                service.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
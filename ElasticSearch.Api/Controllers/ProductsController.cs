using ElasticSearch.Api.DTOs;
using ElasticSearch.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {

        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto reqest)
        {

            return CreateActionResult(await _productService.SaveAsync(reqest)); 

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return CreateActionResult(await _productService.GetAll());

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(string id) 
        {

            return CreateActionResult(await _productService.GetById(id));

        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto updateDto)
        {

            return CreateActionResult(await _productService.Update(updateDto));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            return CreateActionResult(await _productService.Delete(id));

        }
    }
}

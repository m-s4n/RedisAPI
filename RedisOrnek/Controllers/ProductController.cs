
using Microsoft.AspNetCore.Mvc;
using RedisOrnek.Models;
using RedisOrnek.Repositories;
using RedisOrnek.Services;

namespace RedisOrnek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
           
        }

        [HttpGet("[action]")]
        public async Task<List<Product>> GetAll()
        {
            return await _productService.GetAsync();
        }

        [HttpGet("[action]/{id}")]
        public async Task<Product> GetById(int id)
        {
            return await _productService.GetByIdAsync(id);
        }

        [HttpPost("[action]")]
        public async Task<Product> Create(Product product)
        {
            return await _productService.CreateAsync(product);
        }
    }
}

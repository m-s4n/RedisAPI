
using RedisOrnek.Models;
using RedisOrnek.Repositories;

namespace RedisOrnek.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            return await _productRepository.CreateAsync(product);
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _productRepository.GetAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            
            // mapper DTO

            return product;
        }
    }
}

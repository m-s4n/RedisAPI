
using RedisOrnek.Models;
using Redis.Cache.Extensions;
using StackExchange.Redis;
using Redis.Cache.CacheKeys;
using System.Text.Json;

namespace RedisOrnek.Repositories
{
    public class ProductRepositoryWithCache : IProductRepository
    {
        private const string productCacheKey = "product";
        // db ile igili işlemde yapacağımız için IProductRepository türündeki nesnesine ihtiyacıız var.
        private readonly IProductRepository _productRepository;
        // aynı zamanda cache işlemide yapacağımız için IDatabase türündeki nesneyede ihtiyacımız var.
        private readonly IDatabase _redisDatabase;
        public ProductRepositoryWithCache(IProductRepository productRepository, IDatabase redisDatabase)
        {
            _productRepository = productRepository;
            _redisDatabase = redisDatabase;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            // db ve cache eklenir.
            var addedProduct = await _productRepository.CreateAsync(product);

            await _redisDatabase
                .HashSetAsync(
                ProductKey.GetProductAllKey(),
                addedProduct.Id,
                JsonSerializer.Serialize(addedProduct));

            return addedProduct;
        }

        public async Task<List<Product>> GetAsync()
        {
            // from db
            if(!await _redisDatabase.KeyExistsAsync(ProductKey.GetProductAllKey()))
            {
                return await LoadToCacheFromDbAsync();
            }

            // from cache
            List<Product> products = new();
            
            foreach (var item in (await _redisDatabase.HashGetAllAsync(ProductKey.GetProductAllKey())).ToList())
            {
                var product = JsonSerializer.Deserialize<Product>(item.Value);
                products.Add(product);
            }

            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            // from cache
            if (await _redisDatabase.KeyExistsAsync(ProductKey.GetProductAllKey()))
            {
                var product = await _redisDatabase.HashGetAsync(ProductKey.GetProductAllKey(), id);
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }

            // from db

            var products = await LoadToCacheFromDbAsync();

            return products.FirstOrDefault(x => x.Id == id);
        }

        // Db den cache yükle sonra datayı dön.
        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var products = await _productRepository.GetAsync();
            // objeler hash veri türü ile saklanır.
            products.ForEach(async product =>
            {
                await _redisDatabase
                .HashSetAsync(
                    ProductKey.GetProductAllKey(), 
                    product.Id, 
                    JsonSerializer.Serialize(product));
            });

            return products;

            
        }
    }
}

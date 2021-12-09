using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Linq;

namespace Avensia.Storefront.Developertest
{
    public class ProductRepository : IProductRepository
    {
        private const string connectionPath = "C:\\backend\\products.json";
        private const string CacheKey = "availableProducts";
        public IEnumerable<IProductDto> GetProducts()
        {
            ObjectCache cache = MemoryCache.Default;

            //check first if it finds in cache else read from file
            if (cache.Contains(CacheKey))
                return (IEnumerable<IProductDto>)cache.Get(CacheKey);
            else
            {
                IEnumerable<IProductDto> availableProducts = ReadProductsFromFile();

                // Store data in the cache    
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, availableProducts, cacheItemPolicy);

                return availableProducts;
            }
        }

        private IEnumerable<IProductDto> ReadProductsFromFile()
        {
            List<ProductDto> items = null;
            using (StreamReader r = new StreamReader(connectionPath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<ProductDto>>(json);
            }

            return items;
        }

        public IEnumerable<IProductDto> GetProducts(int start, int pageSize)
        {
           
            return GetProducts()?.Skip(start*pageSize).Take(pageSize);
        }

        IProductDto IProductRepository.ProductGroupedByPriceSegment()
        {
            throw new NotImplementedException();
        }
    }
}
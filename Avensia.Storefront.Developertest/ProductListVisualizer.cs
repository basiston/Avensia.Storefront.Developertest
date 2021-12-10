using System;
using System.Linq;

namespace Avensia.Storefront.Developertest
{
    public class ProductListVisualizer
    {
        private readonly IProductRepository _productRepository;

        public ProductListVisualizer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OutputAllProduct(string currency)
        {
            var products = _productRepository.GetProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}\t{product.ProductName}\t{ConvertRate(product.Price, currency)}");
            }
        }

        public void OutputPaginatedProducts(int start, int size, string currency)
        {

            var products = _productRepository.GetProducts(start, size);

            if (products == null || products?.Count() == 0) Console.WriteLine("No product found");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}\t{product.ProductName}\t{ConvertRate(product.Price, currency)}");
            }
        }

        /// <summary>
        ///  list products in chunk of 5
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="currency"></param>

        public void ListProductInChunk(int skip, string currency)
        {
            var products = _productRepository.GetProducts().Skip(skip).Take(5);
            if (products == null || products?.Count() == 0) Console.WriteLine("No product found");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}\t{product.ProductName}\t{ConvertRate(product.Price, currency)}");
            }
        }

        /// <summary>
        /// Convert rate based on USD
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        private double ConvertRate(double value, string currency)
        {
            switch (currency?.ToUpper())
            {
                case "USD":
                    return value;
                case "SEK":
                    return value * 8.38;
                case "GBP":
                    return value * 0.71;
                case "DKK":
                    return value * 6.06;
                default:
                    return value;

            }


        }

        public void OutputProductGroupedByPriceSegment(string currency)
        {
            var products = _productRepository.GetProducts().ToList();
            var queryPrice =
            from IProductDto in products
            group IProductDto by IProductDto.Price into newGroup
            orderby newGroup.Key
            select newGroup;
            foreach (var pGroup in queryPrice)
            {
                Console.WriteLine($"Price: {pGroup.Key}");
                foreach (var product in pGroup)
                {
                    Console.WriteLine($"{product.Id}\t{product.ProductName}\t{ConvertRate(product.Price, currency)}");
                }
            }
 
        }
    }
}
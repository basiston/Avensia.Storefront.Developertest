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

        public void OutputPaginatedProducts(int start, int size)
        {

            var products = _productRepository.GetProducts(start, size);

            if (products == null || products?.Count() == 0) Console.WriteLine("No product found");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}\t{product.ProductName}\t{product.Price}");
            }
        }

        /// <summary>
        ///  list products in chunk of 5
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="currency"></param>

        public void ListProductInChunk(int skip)
        {
            var products = _productRepository.GetProducts().Skip(skip).Take(5);
            if (products == null || products?.Count() == 0) Console.WriteLine("No product found");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}\t{product.ProductName}\t{product.Price}");
            }
        }

     

        public void OutputProductGroupedByPriceSegment()
        {
            var products = _productRepository.GetProducts().ToList();
            var ranges = new[] {100, 200, 300, 400, 500,600,700,800,900,1000,1100,1200,1300,1400,1500 };
            var grp = products.GroupBy(x => ranges.FirstOrDefault(r => r > x.Price)).OrderBy(p=>p.Key);
            
            foreach (var pGroup in grp)
            {
                
                Console.WriteLine($"{pGroup.Key-100}-{pGroup.Key} kr");
                    foreach (var product in pGroup)
                    {
                        Console.WriteLine($"{product.Id}\t{product.ProductName}\t{product.Price}");
                    }
                   
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


    }

        
    }

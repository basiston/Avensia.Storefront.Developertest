namespace Avensia.Storefront.Developertest
{
    public class ProductDto : IProductDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
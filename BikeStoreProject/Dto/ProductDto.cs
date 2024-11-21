namespace BikeStoreProject.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }
    }

    // DTO for Product Details with Category and Brand names
    public class ProductDetailsDto
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
    }

    // DTO for Product Create and Update operations
    public class ProductCreateUpdateDto
    {
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }
    }

    // DTO for searching by Category, Brand, or Model Year
    public class ProductSearchDto
    {
        public string SearchTerm { get; set; }
    }


    public class StoreProductDto
    {
        public string StoreName { get; set; }
        public int NumberOfProductsSold { get; set; }
    }

}



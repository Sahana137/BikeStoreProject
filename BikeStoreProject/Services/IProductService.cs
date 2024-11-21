using BikeStoreProject.Dto;

namespace BikeStoreProject.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<string> AddProductAsync(ProductCreateUpdateDto productDto);
        Task<bool> UpdateProductAsync(int productId, ProductCreateUpdateDto productDto);
        Task<bool> PatchProductAsync(int productId, ProductCreateUpdateDto productDto);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string categoryName);
        Task<IEnumerable<ProductDto>> GetProductsByBrandAsync(string brandName);
        Task<IEnumerable<ProductDto>> GetProductsByModelYearAsync(short modelYear);
        Task<IEnumerable<ProductDto>> GetProductsPurchasedByCustomerAsync(int customerId);
        Task<ProductDto> GetProductPurchasedByMaxCustomerAsync();
        Task<IEnumerable<ProductDto>> GetProductsWithMinimumStockAsync();
        Task<IEnumerable<ProductDetailsDto>> GetProductDetailsAsync();
        Task<IEnumerable<StoreProductDto>> GetNumberOfProductsSoldByEachStoreAsync();
    }
}


using AutoMapper;
using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreProject.Services
{
    public class ProductService : IProductService
    {
        private readonly BikeStoreContext _context;
        private readonly IMapper _mapper;

        public ProductService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }



        public async Task<bool> UpdateProductAsync(int productId, ProductCreateUpdateDto productDto)
        {

            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return false; // Product not found

            // Map updated fields from DTO to the existing product
            _mapper.Map(productDto, product);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true; // Update successful
        }

        public async Task<bool> PatchProductAsync(int productId, ProductCreateUpdateDto productDto)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            if (!string.IsNullOrEmpty(productDto.ProductName)) product.ProductName = productDto.ProductName;
            if (productDto.BrandId != 0) product.BrandId = productDto.BrandId;
            if (productDto.CategoryId != 0) product.CategoryId = productDto.CategoryId;
            if (productDto.ModelYear != 0) product.ModelYear = productDto.ModelYear;
            if (productDto.ListPrice != 0) product.ListPrice = productDto.ListPrice;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string categoryName)
        {
            var products = await _context.Products
                .Where(p => p.Category.CategoryName == categoryName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByBrandAsync(string brandName)
        {
            var products = await _context.Products
                .Where(p => p.Brand.BrandName == brandName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByModelYearAsync(short modelYear)
        {
            var products = await _context.Products
                .Where(p => p.ModelYear == modelYear)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsPurchasedByCustomerAsync(int customerId)
        {
            var products = await _context.OrderItems
                .Where(oi => oi.Order.CustomerId == customerId)
                .Select(oi => oi.Product)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductPurchasedByMaxCustomerAsync()
        {

            var productId = await _context.OrderItems
    .GroupBy(oi => oi.ProductId)
    .OrderByDescending(g => g.Count())
    .Select(g => g.Key)
    .FirstOrDefaultAsync();

            var product = await _context.Products
                .Where(p => p.ProductId == productId)
                .FirstOrDefaultAsync();

            if (product == null) return null;

            // Map the Product entity to ProductDto
            return _mapper.Map<ProductDto>(product);

        }

        public async Task<IEnumerable<StoreProductDto>> GetNumberOfProductsSoldByEachStoreAsync()
        {
            var data = await _context.Stores
                .Include(s => s.Orders)
                .ThenInclude(o => o.OrderItems)
                .Select(s => new StoreProductDto
                {
                    StoreName = s.StoreName,
                    NumberOfProductsSold = s.Orders
                        .SelectMany(o => o.OrderItems)
                        .Count()
                })
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetProductDetailsAsync()
        {

            return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Select(p => new ProductDetailsDto
            {
                // Use null-coalescing operator instead of null-conditional
                CategoryName = p.Category != null ? p.Category.CategoryName : "No Category",
                ProductName = p.ProductName,
                BrandName = p.Brand != null ? p.Brand.BrandName : "No Brand"
            })
            .ToListAsync();

        }

        public async Task<IEnumerable<ProductDto>> GetProductsWithMinimumStockAsync()
        {

            var minStock = await _context.Stocks
                     .GroupBy(s => s.ProductId)  // Group by ProductId
                     .Select(g => new
                     {
                         ProductId = g.Key,
                         MinStock = g.Min(s => s.Quantity)  // Get the minimum stock for each product
                     })
                     .OrderBy(g => g.MinStock)  // Sort by minimum stock level
                     .Select(g => g.MinStock)  // Get the minimum stock value
                     .FirstOrDefaultAsync();

            if (minStock == 1)
                return Enumerable.Empty<ProductDto>(); // If no stock or all stock is 0


            var products = await _context.Products
                .Where(p => p.Stocks.Min(s => s.Quantity) == minStock) // Fetch products with the minimum stock
                .ToListAsync();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }

        public async Task<string> AddProductAsync(ProductCreateUpdateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            // Add the product to the database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Return a success message with the Product ID
            return $"Record Added Successfully! Product ID: {product.ProductId}";
        }
    }
}


using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using BikeStoreProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductCreateUpdateDto productDto)
        {
            try
            {
                if (productDto == null)
                    return BadRequest(new { message = "Product data is required" });

                // Call the service to add the product and get the success message
                var result = await _productService.AddProductAsync(productDto);

                // Return a 201 Created response with the success message
                return StatusCode(StatusCodes.Status201Created, new { message = result });
            }
            catch (Exception ex)
            {
                // Handle exceptions globally if you have exception middleware or locally here
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while adding the product", error = ex.Message });
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching products", error = ex.Message });
            }
        }


        [HttpPut("edit/{productId}")]
        public async Task<IActionResult> UpdateProductAsync(int productId, [FromBody] ProductCreateUpdateDto productDto)
        {
            try
            {
                if (productDto == null)
                    return BadRequest(new { message = "Product data is required" });

                // Call the service to update the product
                var result = await _productService.UpdateProductAsync(productId, productDto);

                if (!result)
                    return NotFound(new { message = "Product not found to update" });

                // Return 204 No Content if the update is successful
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while updating the product", error = ex.Message });
            }

        }

        [HttpPatch("edit/{productId}")]
        public async Task<IActionResult> PatchProductAsync(int productId, [FromBody] ProductCreateUpdateDto productDto)
        {
            try
            {
                if (productDto == null)
                    return BadRequest(new { message = "Product data is required" });

                var result = await _productService.PatchProductAsync(productId, productDto);
                if (!result)
                    return NotFound(new { message = "Product not found to update" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the product", error = ex.Message });
            }
        }

        [HttpGet("bycategoryname/{categoryName}")]
        public async Task<IActionResult> GetProductsByCategoryAsync(string categoryName)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(categoryName);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching products by category", error = ex.Message });
            }
        }

        [HttpGet("bybrandname/{brandName}")]
        public async Task<IActionResult> GetProductsByBrandAsync(string brandName)
        {
            try
            {
                var products = await _productService.GetProductsByBrandAsync(brandName);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching products by brand", error = ex.Message });
            }
        }

        [HttpGet("bymodelyear/{modelYear}")]
        public async Task<IActionResult> GetProductsByModelYearAsync(short modelYear)
        {
            try
            {
                var products = await _productService.GetProductsByModelYearAsync(modelYear);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching products by model year", error = ex.Message });
            }
        }

        [HttpGet("purchasedbycustomer/{customerId}")]
        public async Task<IActionResult> GetProductsPurchasedByCustomerAsync(int customerId)
        {
            try
            {
                var products = await _productService.GetProductsPurchasedByCustomerAsync(customerId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching products purchased by customer", error = ex.Message });
            }
        }

        [HttpGet("productpurchasedbymaxcustomer")]
        public async Task<IActionResult> GetProductPurchasedByMaxCustomerAsync()
        {
            try
            {
                var product = await _productService.GetProductPurchasedByMaxCustomerAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching the product purchased by the maximum customer", error = ex.Message });
            }
        }

        [HttpGet("numberofproductssoldbyeachstore")]
        public async Task<IActionResult> GetNumberOfProductsSoldByEachStoreAsync()
        {
            try
            {
                var data = await _productService.GetNumberOfProductsSoldByEachStoreAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching the number of products sold by each store", error = ex.Message });
            }
        }

        [HttpGet("ProductDetails")]
        public async Task<IActionResult> GetProductDetailsAsync()
        {
            try
            {
                var products = await _productService.GetProductDetailsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching product details", error = ex.Message });
            }
        }

        [HttpGet("getproductwithminiumstock")]
        public async Task<IActionResult> GetProductsWithMinimumStockAsync()
        {
            try
            {
                var products = await _productService.GetProductsWithMinimumStockAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching products with minimum stock", error = ex.Message });
            }
        }
    }
}
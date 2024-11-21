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
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly BikeStoreContext _context;

        public StoreController(IStoreService storeService, BikeStoreContext context)
        {
            _storeService = storeService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddStore([FromBody] CreateStoreDto storeDto)
        {
            var storeId = await _storeService.AddStoreAsync(storeDto);
            return CreatedAtAction(nameof(GetAllStores), new { id = storeId }, $"Record Created Successfully with ID: {storeId}");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }

        [HttpPut("edit/{storeId}")]
        public async Task<IActionResult> UpdateStore(int storeId, [FromBody] UpdateStoreDto storeDto)
        {
            await _storeService.UpdateStoreAsync(storeId, storeDto);
            return NoContent();
        }


        [HttpPatch("edit/{storeId}")]
        public async Task<IActionResult> PatchStoreDetails(int storeId, [FromBody] UpdateStoreDto storeDto)
        {
            // Validate the incoming DTO
            if (storeDto == null)
            {
                return BadRequest("Invalid data.");
            }

            // Call the service to apply the patch
            var result = await _storeService.PatchStoreDetailsAsync(storeId, storeDto);

            if (result == null)
            {
                return NotFound(new { message = "Store not found." });
            }

            // Return NoContent response if successful
            return NoContent();
        }
        //[HttpPatch("edit/{storeId}")]
        //public async Task<IActionResult> PatchStoreDetails(int storeId, [FromBody] UpdateStoreDto storeDto)
        //{
        //    await _storeService.PatchStoreDetailsAsync(storeId, storeDto);
        //    return NoContent();
        //}
        //fdfo

        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetStoresByCity(string city)
        {
            var stores = await _storeService.GetStoresByCityAsync(city);
            return Ok(stores);
        }

        [HttpGet("totalstoreineachstate")]
        public async Task<IActionResult> GetTotalStoresByState()
        {
            var stores = await _storeService.GetTotalStoresByStateAsync();
            return Ok(stores);
        }

        [HttpGet("maxiumcustomers")]
        public async Task<IActionResult> GetStoreWithMaximumCustomersAsync()
        {
            var storeWithMaximumCustomers = await _context.Orders // Use Orders to relate Customers and Stores
                .GroupBy(o => o.StoreId) // Group by StoreId from Orders
                .Select(g => new
                {
                    StoreId = g.Key,
                    CustomerCount = g.Select(o => o.CustomerId).Distinct().Count() // Count distinct customers for each store
                })
                .OrderByDescending(s => s.CustomerCount)
                .FirstOrDefaultAsync();

            if (storeWithMaximumCustomers == null)
            {
                return NotFound(new { message = "No customers found." });
            }

            // Find the store details using the StoreId
            var store = await _context.Stores.FindAsync(storeWithMaximumCustomers.StoreId);

            if (store == null)
            {
                return NotFound(new { message = "Store not found." });
            }

            // Create and populate the StoreDto
            var storeDto = new StoreDto
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName,
                Phone = store.Phone,
                Email = store.Email,
                Street = store.Street,
                City = store.City,
                State = store.State,
                ZipCode = store.ZipCode
            };

            return Ok(storeDto);
        }

        [HttpGet("highestsale")]
        //[HttpGet("highest-sale")]
        public async Task<ActionResult<StoreDto>> GetStoreWithHighestSale()
        {
            var storeWithHighestSale = await _context.Orders
                .GroupBy(o => o.StoreId)
                .Select(g => new
                {
                    StoreId = g.Key,
                    TotalSales = g.Sum(o => o.OrderItems.Sum(oi => oi.ListPrice * oi.Quantity)) // Calculate total sales
                })
                .OrderByDescending(s => s.TotalSales)
                .FirstOrDefaultAsync();

            if (storeWithHighestSale == null)
            {
                return NotFound(new { message = "No sales data found." });
            }

            var store = await _context.Stores.FindAsync(storeWithHighestSale.StoreId);

            if (store == null)
            {
                return NotFound(new { message = "Store not found." });
            }

            // Create and populate the StoreDto
            var storeDto = new StoreDto
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName,
                Phone = store.Phone,
                Email = store.Email,
                Street = store.Street,
                City = store.City,
                State = store.State,
                ZipCode = store.ZipCode
            };

            return Ok(storeDto); // Return the StoreDto
        }
    }
}

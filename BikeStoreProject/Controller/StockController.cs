using BikeStoreProject.Dto;
using BikeStoreProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockService.GetAllStocks();
            return Ok(stocks);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateStock(CreateStockDto stockDto)
        //{
        //    var createdStock = await _stockService.CreateStock(stockDto);
        //    return Created($"api/stock/{createdStock.StoreId}/{createdStock.ProductId}", createdStock);
        //}


        [HttpPost]
        public async Task<IActionResult> CreateStock(CreateStockDto stockDto)
        {
            try
            {
                var createdStock = await _stockService.CreateStock(stockDto);
                return Created($"api/stock/{createdStock.StoreId}/{createdStock.ProductId}", createdStock);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPut("{storeId}/{productId}")]
        public async Task<IActionResult> UpdateStock(int storeId, int productId, UpdateStockDto stockDto)
        {
            var updatedStock = await _stockService.UpdateStock(storeId, productId, stockDto);
            if (updatedStock == null) return NotFound();
            return Ok(updatedStock);
        }
    }
}

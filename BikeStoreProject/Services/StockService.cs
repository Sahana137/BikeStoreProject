using AutoMapper;
using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreProject.Services
{
    public class StockService : IStockService
    {
        private readonly BikeStoreContext _context;
        private readonly IMapper _mapper;

        public StockService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResponseStockDto>> GetAllStocks()
        {
            var stocks = await _context.Stocks
                .Include(s => s.Store)
                .Include(s => s.Product)
                .ToListAsync();
            return _mapper.Map<List<ResponseStockDto>>(stocks);
        }



        //public async Task<ResponseStockDto> CreateStock(CreateStockDto stockDto)
        //{
        //    var stock = _mapper.Map<Stock>(stockDto);
        //    _context.Stocks.Add(stock);
        //    await _context.SaveChangesAsync();
        //    return _mapper.Map<ResponseStockDto>(stock);
        //}


        public async Task<ResponseStockDto> CreateStock(CreateStockDto stockDto)
        {
            // Check if the store exists
            var storeExists = await _context.Stores.AnyAsync(s => s.StoreId == stockDto.StoreId);
            if (!storeExists)
            {
                throw new InvalidOperationException("The specified store does not exist.");
            }

            // Check if the product exists (optional, if you also have a product FK)
            var productExists = await _context.Products.AnyAsync(p => p.ProductId == stockDto.ProductId);
            if (!productExists)
            {
                throw new InvalidOperationException("The specified product does not exist.");
            }

            // Create the stock record
            var stock = _mapper.Map<Stock>(stockDto);
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return _mapper.Map<ResponseStockDto>(stock);
        }


        public async Task<ResponseStockDto?> UpdateStock(int storeId, int productId, UpdateStockDto stockDto)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.ProductId == productId);

            if (stock == null) return null;

            stock.Quantity = stockDto.Quantity;
            await _context.SaveChangesAsync();

            return _mapper.Map<ResponseStockDto>(stock);
        }

      
    }
}

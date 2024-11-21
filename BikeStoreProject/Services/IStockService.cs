using BikeStoreProject.Dto;

namespace BikeStoreProject.Services
{
    public interface IStockService
    {
        Task<List<ResponseStockDto>> GetAllStocks();
      
        Task<ResponseStockDto> CreateStock(CreateStockDto stockDto);
        Task<ResponseStockDto?> UpdateStock(int storeId, int productId, UpdateStockDto stockDto);
       
    }
}

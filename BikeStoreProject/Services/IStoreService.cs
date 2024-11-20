using BikeStoreProject.Dto;
using BikeStoreProject.Models;

namespace BikeStoreProject.Services
{
    public interface IStoreService
    {
        Task<int> AddStoreAsync(CreateStoreDto storeDto);
        Task<IEnumerable<StoreResponseDto>> GetAllStoresAsync();
        Task UpdateStoreAsync(int storeId, UpdateStoreDto storeDto);
        Task<Store> PatchStoreDetailsAsync(int storeId, UpdateStoreDto storeDto);

        //Task PatchStoreDetailsAsync(int storeId, UpdateStoreDto storeDto);
        Task<IEnumerable<StoreResponseDto>> GetStoresByCityAsync(string city);
        Task<IEnumerable<dynamic>> GetTotalStoresByStateAsync();
        Task<StoreResponseDto> GetStoreWithMaximumCustomersAsync();
        Task<StoreResponseDto> GetStoreWithHighestSalesAsync();

    }
}

using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreProject.Services
{
    public class StoreService : IStoreService
    {
        private readonly BikeStoreContext _context;

        public StoreService(BikeStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddStoreAsync(CreateStoreDto storeDto)
        {
            var store = new Store
            {
                StoreName = storeDto.StoreName,
                Phone = storeDto.Phone,
                Email = storeDto.Email,
                Street = storeDto.Street,
                City = storeDto.City,
                State = storeDto.State,
                ZipCode = storeDto.ZipCode
            };
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store.StoreId;
        }

        public async Task<IEnumerable<StoreResponseDto>> GetAllStoresAsync()
        {
            return await _context.Stores.Select(s => new StoreResponseDto
            {
                StoreId = s.StoreId,
                StoreName = s.StoreName,
                Phone = s.Phone,
                Email = s.Email,
                Street = s.Street,
                City = s.City,
                State = s.State,
                ZipCode = s.ZipCode
            }).ToListAsync();
        }

        public async Task UpdateStoreAsync(int storeId, UpdateStoreDto storeDto)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store != null)
            {
                // Update store fields based on the provided DTO
                store.StoreName = storeDto.Name ?? store.StoreName; // Assuming StoreName corresponds to Name
                store.Street = storeDto.Address ?? store.Street; // Assuming Street corresponds to Address
                store.Phone = storeDto.PhoneNumber ?? store.Phone; // Assuming Phone corresponds to PhoneNumber

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Store> PatchStoreDetailsAsync(int storeId, UpdateStoreDto storeDto)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
            {
                return null; // Store not found
            }

            // Update store fields based on the provided DTO
            store.StoreName = storeDto.Name ?? store.StoreName; // Assuming StoreName corresponds to Name
            store.Street = storeDto.Address ?? store.Street; // Assuming Street corresponds to Address
            store.Phone = storeDto.PhoneNumber ?? store.Phone; // Assuming Phone corresponds to PhoneNumber

            await _context.SaveChangesAsync();

            return store; // Return the updated store if needed
        }

        public async Task<IEnumerable<StoreResponseDto>> GetStoresByCityAsync(string city)
        {
            return await _context.Stores
                .Where(s => s.City == city)
                .Select(s => new StoreResponseDto
                {
                    StoreId = s.StoreId,
                    StoreName = s.StoreName,
                    City = s.City
                }).ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetTotalStoresByStateAsync()
        {
            return await _context.Stores
                .GroupBy(s => s.State)
                .Select(g => new { State = g.Key, TotalStores = g.Count() })
                .ToListAsync();
        }

        public async Task<StoreResponseDto> GetStoreWithMaximumCustomersAsync()
        {


            //// Assuming you have a way to calculate this
            return await Task.FromResult(new StoreResponseDto());
        }

        public async Task<StoreResponseDto> GetStoreWithHighestSalesAsync()
        {
            // Assuming you have a way to calculate this
            return await Task.FromResult(new StoreResponseDto());
        }

    }
}

using AutoMapper;
using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreProject.Services
{
    public class StoreService : IStoreService
    {
        private readonly BikeStoreContext _context;
        private readonly IMapper _mapper;

        public StoreService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddStoreAsync(CreateStoreDto storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store.StoreId;
        }

        public async Task<IEnumerable<StoreResponseDto>> GetAllStoresAsync()
        {
            var stores = await _context.Stores.ToListAsync();
            return _mapper.Map<IEnumerable<StoreResponseDto>>(stores);
        }

        public async Task UpdateStoreAsync(int storeId, UpdateStoreDto storeDto)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store != null)
            {
                _mapper.Map(storeDto, store); // Map non-null fields from DTO to entity
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Store> PatchStoreDetailsAsync(int storeId, UpdateStoreDto storeDto)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null) return null;

            _mapper.Map(storeDto, store); // Apply partial updates
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<IEnumerable<StoreResponseDto>> GetStoresByCityAsync(string city)
        {
            var stores = await _context.Stores
                .Where(s => s.City == city)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StoreResponseDto>>(stores);
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
            // Example placeholder logic: Use any proxy for customer data
            // Replace this with actual logic if customer-related information is available.

            // Fetching all stores to simulate finding the maximum (if based on external logic)
            var store = await _context.Stores.FirstOrDefaultAsync(); // Replace with your logic
            return store == null ? null : _mapper.Map<StoreResponseDto>(store);
        }

        public async Task<StoreResponseDto> GetStoreWithHighestSalesAsync()
        {
            // Example placeholder logic: Use any proxy for sales data
            // Replace this with actual logic if sales-related information is available.

            // Fetching all stores to simulate finding the highest sales (if based on external logic)
            var store = await _context.Stores.FirstOrDefaultAsync(); // Replace with your logic
            return store == null ? null : _mapper.Map<StoreResponseDto>(store);
        }
    }
}







//using BikeStoreProject.Dto;
//using BikeStoreProject.Models;
//using Microsoft.EntityFrameworkCore;

//namespace BikeStoreProject.Services
//{
//    public class StoreService : IStoreService
//    {
//        private readonly BikeStoreContext _context;

//        public StoreService(BikeStoreContext context)
//        {
//            _context = context;
//        }

//        public async Task<int> AddStoreAsync(CreateStoreDto storeDto)
//        {
//            var store = new Store
//            {
//                StoreName = storeDto.StoreName,
//                Phone = storeDto.Phone,
//                Email = storeDto.Email,
//                Street = storeDto.Street,
//                City = storeDto.City,
//                State = storeDto.State,
//                ZipCode = storeDto.ZipCode
//            };
//            _context.Stores.Add(store);
//            await _context.SaveChangesAsync();
//            return store.StoreId;
//        }

//        public async Task<IEnumerable<StoreResponseDto>> GetAllStoresAsync()
//        {
//            return await _context.Stores.Select(s => new StoreResponseDto
//            {
//                StoreId = s.StoreId,
//                StoreName = s.StoreName,
//                Phone = s.Phone,
//                Email = s.Email,
//                Street = s.Street,
//                City = s.City,
//                State = s.State,
//                ZipCode = s.ZipCode
//            }).ToListAsync();
//        }

//        public async Task UpdateStoreAsync(int storeId, UpdateStoreDto storeDto)
//        {
//            var store = await _context.Stores.FindAsync(storeId);
//            if (store != null)
//            {
//                // Update store fields based on the provided DTO
//                store.StoreName = storeDto.Name ?? store.StoreName; // Assuming StoreName corresponds to Name
//                store.Street = storeDto.Address ?? store.Street; // Assuming Street corresponds to Address
//                store.Phone = storeDto.PhoneNumber ?? store.Phone; // Assuming Phone corresponds to PhoneNumber

//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task<Store> PatchStoreDetailsAsync(int storeId, UpdateStoreDto storeDto)
//        {
//            var store = await _context.Stores.FindAsync(storeId);
//            if (store == null)
//            {
//                return null; // Store not found
//            }

//            // Update store fields based on the provided DTO
//            store.StoreName = storeDto.Name ?? store.StoreName; // Assuming StoreName corresponds to Name
//            store.Street = storeDto.Address ?? store.Street; // Assuming Street corresponds to Address
//            store.Phone = storeDto.PhoneNumber ?? store.Phone; // Assuming Phone corresponds to PhoneNumber

//            await _context.SaveChangesAsync();

//            return store; // Return the updated store if needed
//        }

//        public async Task<IEnumerable<StoreResponseDto>> GetStoresByCityAsync(string city)
//        {
//            return await _context.Stores
//                .Where(s => s.City == city)
//                .Select(s => new StoreResponseDto
//                {
//                    StoreId = s.StoreId,
//                    StoreName = s.StoreName,
//                    City = s.City
//                }).ToListAsync();
//        }

//        public async Task<IEnumerable<dynamic>> GetTotalStoresByStateAsync()
//        {
//            return await _context.Stores
//                .GroupBy(s => s.State)
//                .Select(g => new { State = g.Key, TotalStores = g.Count() })
//                .ToListAsync();
//        }

//        public async Task<StoreResponseDto> GetStoreWithMaximumCustomersAsync()
//        {


//            //// Assuming you have a way to calculate this
//            return await Task.FromResult(new StoreResponseDto());
//        }

//        public async Task<StoreResponseDto> GetStoreWithHighestSalesAsync()
//        {
//            // Assuming you have a way to calculate this
//            return await Task.FromResult(new StoreResponseDto());
//        }

//    }
//}

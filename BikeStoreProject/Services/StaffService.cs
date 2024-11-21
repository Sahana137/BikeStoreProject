
using AutoMapper;
using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreProject.Services
{
    public class StaffService : IStaffService
    {
        private readonly BikeStoreContext _context;
        private readonly IMapper _mapper;

        public StaffService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddStaffAsync(CreateStaffDto staffDto)
        {
            var staff = _mapper.Map<Staff>(staffDto);
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
            return staff.StaffId;
        }

        public async Task<IEnumerable<StaffResponseDto>> GetAllStaffAsync()
        {
            var staffList = await _context.Staffs.ToListAsync();
            return _mapper.Map<IEnumerable<StaffResponseDto>>(staffList);
        }

        public async Task UpdateStaffAsync(int staffId, UpdateStaffDto staffDto)
        {
            var staff = await _context.Staffs.FindAsync(staffId);
            if (staff != null)
            {
                _mapper.Map(staffDto, staff); // Apply changes only for provided fields
                await _context.SaveChangesAsync();
            }
        }

        public async Task PatchStaffDetailsAsync(int staffId, UpdateStaffDto staffDto)
        {
            await UpdateStaffAsync(staffId, staffDto);
        }

        public async Task<IEnumerable<StaffResponseDto>> GetStaffByStoreNameAsync(string storeName)
        {
            var staffList = await _context.Staffs
                .Where(s => s.Store.StoreName == storeName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StaffResponseDto>>(staffList);
        }








        public async Task<IEnumerable<dynamic>> GetSalesMadeByStaffAsync(int staffId)
        {
            return await _context.Orders
        .Where(order => order.StaffId == staffId) // Ensure this matches your database schema
        .ToListAsync();
            // Assuming you have a logic to calculate this
            //return await Task.FromResult(new List<dynamic>());
        }

        public async Task<StaffResponseDto> GetManagerDetailsAsync(int staffId)
        {
            var staff = await _context.Staffs
                .Include(s => s.Manager)
                .FirstOrDefaultAsync(s => s.StaffId == staffId);

            return staff?.Manager == null ? null : _mapper.Map<StaffResponseDto>(staff.Manager);
        }
    }
}






//using BikeStoreProject.Dto;
//using BikeStoreProject.Models;
//using Microsoft.EntityFrameworkCore;

//namespace BikeStoreProject.Services
//{
//    public class StaffService:IStaffService
//    {
//        private readonly BikeStoreContext _context;

//        public StaffService(BikeStoreContext context)
//        {
//            _context = context;
//        }

//        public async Task<int> AddStaffAsync(CreateStaffDto staffDto)
//        {
//            var staff = new Staff
//            {
//                FirstName = staffDto.FirstName,
//                LastName = staffDto.LastName,
//                Email = staffDto.Email,
//                Phone = staffDto.Phone,
//                Active = staffDto.Active,
//                StoreId = staffDto.StoreId,
//                ManagerId = staffDto.ManagerId
//            };
//            _context.Staffs.Add(staff);
//            await _context.SaveChangesAsync();
//            return staff.StaffId;
//        }

//        public async Task<IEnumerable<StaffResponseDto>> GetAllStaffAsync()
//        {
//            return await _context.Staffs.Select(s => new StaffResponseDto
//            {
//                StaffId = s.StaffId,
//                FirstName = s.FirstName,
//                LastName = s.LastName,
//                Email = s.Email,
//                Phone = s.Phone,
//                Active = s.Active,
//                StoreId = s.StoreId,
//                ManagerId = s.ManagerId
//            }).ToListAsync();
//        }

//        public async Task UpdateStaffAsync(int staffId, UpdateStaffDto staffDto)
//        {
//            var staff = await _context.Staffs.FindAsync(staffId);
//            if (staff != null)
//            {
//                staff.FirstName = staffDto.FirstName ?? staff.FirstName;
//                staff.LastName = staffDto.LastName ?? staff.LastName;
//                staff.Email = staffDto.Email ?? staff.Email;
//                staff.Phone = staffDto.Phone ?? staff.Phone;
//                staff.Active = staffDto.Active ?? staff.Active;
//                staff.StoreId = staffDto.StoreId ?? staff.StoreId;
//                staff.ManagerId = staffDto.ManagerId ?? staff.ManagerId;

//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task PatchStaffDetailsAsync(int staffId, UpdateStaffDto staffDto)
//        {
//            // Similar to UpdateStaffAsync but only apply partial updates
//            await UpdateStaffAsync(staffId, staffDto);
//        }

//        public async Task<IEnumerable<StaffResponseDto>> GetStaffByStoreNameAsync(string storeName)
//        {
//            return await _context.Staffs
//                .Where(s => s.Store.StoreName == storeName)
//                .Select(s => new StaffResponseDto
//                {
//                    StaffId = s.StaffId,
//                    FirstName = s.FirstName,
//                    LastName = s.LastName,
//                    Email = s.Email
//                }).ToListAsync();
//        }

//        public async Task<IEnumerable<dynamic>> GetSalesMadeByStaffAsync(int staffId)
//        {

//            // Assuming you have a way to calculate this
//            return await Task.FromResult(new List<dynamic>());
//        }

//        public async Task<StaffResponseDto> GetManagerDetailsAsync(int staffId)
//        {
//            var staff = await _context.Staffs.Include(s => s.Manager)
//                .FirstOrDefaultAsync(s => s.StaffId == staffId);

//            if (staff?.Manager == null) return null;

//            return new StaffResponseDto
//            {
//                StaffId = staff.Manager.StaffId,
//                FirstName = staff.Manager.FirstName,
//                LastName = staff.Manager.LastName,
//                Email = staff.Manager.Email
//            };
//        }
//    }
//}

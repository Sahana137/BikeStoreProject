using BikeStoreProject.Dto;

namespace BikeStoreProject.Services
{
    public interface IStaffService
    {
        Task<int> AddStaffAsync(CreateStaffDto staffDto);
        Task<IEnumerable<StaffResponseDto>> GetAllStaffAsync();
        Task UpdateStaffAsync(int staffId, UpdateStaffDto staffDto);
        Task PatchStaffDetailsAsync(int staffId, UpdateStaffDto staffDto);
        Task<IEnumerable<StaffResponseDto>> GetStaffByStoreNameAsync(string storeName);
        Task<IEnumerable<dynamic>> GetSalesMadeByStaffAsync(int staffId);
        Task<StaffResponseDto> GetManagerDetailsAsync(int staffId);
    }
}

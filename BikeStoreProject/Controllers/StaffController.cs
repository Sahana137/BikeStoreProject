using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using BikeStoreProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly BikeStoreContext _context;
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService, BikeStoreContext context)
        {
            _staffService = staffService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff([FromBody] CreateStaffDto staffDto)
        {
            var staffId = await _staffService.AddStaffAsync(staffDto);
            return CreatedAtAction(nameof(GetAllStaff), new { id = staffId }, $"Record Created Successfully with ID: {staffId}");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var staff = await _staffService.GetAllStaffAsync();
            return Ok(staff);
        }

        [HttpPut("edit/{staffId}")]
        public async Task<IActionResult> UpdateStaff(int staffId, [FromBody] UpdateStaffDto staffDto)
        {
            await _staffService.UpdateStaffAsync(staffId, staffDto);
            return NoContent();
        }

        [HttpPatch("{staffId}")]
        public async Task<IActionResult> PatchStaffDetailsAsync(int staffId, [FromBody] UpdateStaffDto staffDto)
        {
            // Check if the staff member exists
            var staff = await _context.Staffs.FindAsync(staffId);
            if (staff == null)
            {
                return NotFound(new { message = "Staff member not found." });
            }

            // Update staff properties if provided
            if (!string.IsNullOrEmpty(staffDto.FirstName))
            {
                staff.FirstName = staffDto.FirstName;
            }
            if (!string.IsNullOrEmpty(staffDto.LastName))
            {
                staff.LastName = staffDto.LastName;
            }
            if (!string.IsNullOrEmpty(staffDto.Email))
            {
                staff.Email = staffDto.Email;
            }
            if (!string.IsNullOrEmpty(staffDto.Phone))
            {
                staff.Phone = staffDto.Phone;
            }
            if (staffDto.Active.HasValue)
            {
                staff.Active = staffDto.Active.Value;
            }
            if (staffDto.StoreId.HasValue)
            {
                staff.StoreId = staffDto.StoreId.Value;
            }
            if (staffDto.ManagerId.HasValue)
            {
                staff.ManagerId = staffDto.ManagerId.Value;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on successful update
        }

        //[HttpPatch("edit/{staffId}")]
        //public async Task<IActionResult> PatchStaffDetails(int staffId, [FromBody] UpdateStaffDto staffDto)
        //{
        //    await _staffService.PatchStaffDetailsAsync(staffId, staffDto);
        //    return NoContent();
        //}

        //[HttpGet("storename/{storeName}")]
        //public async Task<IActionResult> GetStaffByStoreName(string storeName)
        //{
        //    var staff = await _staffService.GetStaffByStoreNameAsync(storeName);
        //    return Ok(staff);
        //}
        [HttpGet("storename/{storeName}")]
        public async Task<IActionResult> GetStaffByStoreName(string storeName)
        {
            // Call the service to get staff by store name
            var staff = await _staffService.GetStaffByStoreNameAsync(storeName);

            // Check if staff is null or empty
            if (staff == null || !staff.Any())
            {
                return NotFound(new { message = "No staff found for the specified store name." });
            }

            // Return the staff list with a 200 OK response
            return Ok(staff);
        }

        //[HttpGet("salesmadebystaff/{staffId}")]
        //public async Task<IActionResult> GetSalesMadeByStaff(int staffId)
        //{
        //    var sales = await _staffService.GetSalesMadeByStaffAsync(staffId);
        //    return Ok(sales);
        //}
        [HttpGet("salesmadebystaff/{staffId}")]
        public async Task<IActionResult> GetSalesMadeByStaff(int staffId)
        {
            // Call the service to get sales made by the staff
            var sales = await _staffService.GetSalesMadeByStaffAsync(staffId);

            // Check if sales data is null or empty
            if (sales == null || !sales.Any())
            {
                return NotFound(new { message = "No sales found for the specified staff ID." });
            }

            // Return the sales data with a 200 OK response
            return Ok(sales);
        }

        //[HttpGet("managerdetails/{staffId}")]
        //public async Task<IActionResult> GetManagerDetails(int staffId)
        //{
        //    var manager = await _staffService.GetManagerDetailsAsync(staffId);
        //    return Ok(manager);
        //}
        [HttpGet("managerdetails/{staffId}")]
        public async Task<IActionResult> GetManagerDetails(int staffId)
        {
            // Call the service to get manager details for the specified staff ID
            var manager = await _staffService.GetManagerDetailsAsync(staffId);

            // Check if manager data is null
            if (manager == null)
            {
                return NotFound(new { message = "Manager not found for the specified staff ID." });
            }

            // Return the manager details with a 200 OK response
            return Ok(manager);
        }
    }
}

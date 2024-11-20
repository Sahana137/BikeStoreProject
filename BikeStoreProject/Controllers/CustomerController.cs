using BikeStoreApp.DTOs;
using BikeStoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using Student_13WebApiProject.Data;

namespace BikeStoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var customer = await _customerService.CreateCustomer(createCustomerDto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customerService.UpdateCustomer(id, updateCustomerDto);
            return customer == null ? NotFound() : Ok(customer);
        }

        

        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetCustomersByCity(string city)
        {
            var customers = await _customerService.GetCustomersByCity(city);
            return Ok(customers);
        }

        [HttpGet("orderdate/{orderDate}")]
        public async Task<IActionResult> GetCustomersByOrderDate(DateOnly orderDate)
        {
            var customers = await _customerService.GetCustomersByOrderDate(orderDate);
            return Ok(customers);
        }

        [HttpPut("{id}/street")]
        public async Task<IActionResult> UpdateCustomerStreet(int id, [FromBody] string street)
        {
            var customer = await _customerService.UpdateCustomerStreet(id, street);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpGet("zipcode/{zipCode}")]
        public async Task<IActionResult> GetCustomersByZipCode(string zipCode)
        {
            var customers = await _customerService.GetCustomersByZipCode(zipCode);
            return Ok(customers);
        }

        [HttpGet("highest-orders")]
        public async Task<IActionResult> GetCustomerWithHighestOrders()
        {
            var customer = await _customerService.GetCustomerWithHighestOrders();
            return customer == null ? NotFound() : Ok(customer);
        }
    }
}

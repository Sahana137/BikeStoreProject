using AutoMapper;
using BikeStoreApp.DTOs;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;
using Student_13WebApiProject.Data;

namespace BikeStoreApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BikeStoreContext _context;
        private readonly IMapper _mapper;

        public CustomerService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Core CRUD Operations
        public async Task<IEnumerable<ResponseCustomerDto>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<ResponseCustomerDto>>(customers);
        }

        public async Task<ResponseCustomerDto?> GetCustomerById(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            return customer == null ? null : _mapper.Map<ResponseCustomerDto>(customer);
        }

        public async Task<ResponseCustomerDto> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<ResponseCustomerDto>(customer);
        }

        public async Task<ResponseCustomerDto?> UpdateCustomer(int customerId, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return null;

            _mapper.Map(updateCustomerDto, customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<ResponseCustomerDto>(customer);
        }



        // Advanced Query Operations
        public async Task<IEnumerable<ResponseCustomerDto>> GetCustomersByCity(string city)
        {
            var customers = await _context.Customers.Where(c => c.City == city).ToListAsync();
            return _mapper.Map<IEnumerable<ResponseCustomerDto>>(customers);
        }

       

        public async Task<ResponseCustomerDto?> UpdateCustomerStreet(int customerId, string street)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return null;

            customer.Street = street;
            await _context.SaveChangesAsync();
            return _mapper.Map<ResponseCustomerDto>(customer);
        }

        public async Task<IEnumerable<ResponseCustomerDto>> GetCustomersByZipCode(string zipCode)
        {
            var customers = await _context.Customers.Where(c => c.ZipCode == zipCode).ToListAsync();
            return _mapper.Map<IEnumerable<ResponseCustomerDto>>(customers);
        }

        public async Task<ResponseCustomerDto?> GetCustomerWithHighestOrders()
        {
            var customer = await _context.Customers
                .Include(c => c.Orders)
                .OrderByDescending(c => c.Orders.Count)
                .FirstOrDefaultAsync();

            return customer == null ? null : _mapper.Map<ResponseCustomerDto>(customer);
        }

        public async Task<IEnumerable<ResponseCustomerDto>> GetCustomersByOrderDate(DateOnly orderDate)
        {
            
                var customers = await _context.Customers
                    .Where(c => c.Orders.Any(o => o.OrderDate == orderDate))
                    .ToListAsync();
                return _mapper.Map<IEnumerable<ResponseCustomerDto>>(customers);
            
        }
    }
}

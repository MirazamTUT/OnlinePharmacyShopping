using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PharmacyDbContext _pharmacyDbContext;

        public CustomerRepository(PharmacyDbContext pharmacyDbContext)
        {
            _pharmacyDbContext = pharmacyDbContext;
        }
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            try
            {
                _pharmacyDbContext.Customers.Add(customer);
                await _pharmacyDbContext.SaveChangesAsync();
                return customer.CustomerId;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it saved changes");
            }
        }

        public async Task<int> DeleteCustomerAsync(Customer customer)
        {
            try
            {
                _pharmacyDbContext.Customers.Remove(customer);
                await _pharmacyDbContext.SaveChangesAsync();
                return customer.CustomerId;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it saved changes");
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _pharmacyDbContext.Customers
                    .Include(x => x.CustomerId)
                    .Include(x => x.CustomerFirstName)
                    .Include(x => x.CustomerLastName)
                    .Include(x => x.CustomerEmail)
                    .Include(x => x.CustomerPassword)
                    .Include(x => x.Gender)
                    .Include(x => x.PhoneNumber)
                    .Include(x => x.BirthDate)
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed wnet it was given the info");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it saved changes");
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _pharmacyDbContext.Customers
                   .Include(x => x.CustomerId)
                   .Include(x => x.CustomerFirstName)
                   .Include(x => x.CustomerLastName)
                   .Include(x => x.CustomerEmail)
                   .Include(x => x.CustomerPassword)
                   .Include(x => x.Gender)
                   .Include(x => x.PhoneNumber)
                   .Include(x => x.BirthDate)
                   .FirstOrDefaultAsync(x => x.CustomerId == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed wnet it was given the info");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it saved changes");
            }
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                var customerForUpdate = await GetCustomerByIdAsync(customer.CustomerId);
                customerForUpdate.CustomerFirstName = customer.CustomerFirstName;
                customerForUpdate.CustomerLastName = customer.CustomerLastName;
                customerForUpdate.PhoneNumber = customer.PhoneNumber;
                customerForUpdate.CustomerEmail = customer.CustomerEmail;
                customerForUpdate.CustomerPassword = customer.CustomerPassword;
                await _pharmacyDbContext.SaveChangesAsync();
                return customer.CustomerId;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it saved changes");
            }
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

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
                throw new Exception("Operation was failed when it was adding changes");
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
                throw new Exception("Operation was failed when it was deleting changes");
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _pharmacyDbContext.Customers
                    .Include(x => x.Purchases)
                    .Include(x => x.Sales)
                    .Include(x => x.Reports)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving Customers information");
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _pharmacyDbContext.Customers
                   .Include(x => x.Purchases)
                   .Include(x => x.Sales)
                   .Include(x => x.Reports)
                   .AsSplitQuery()
                   .FirstOrDefaultAsync(x => x.CustomerId == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving CustomerById information");
            }
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                _pharmacyDbContext.Customers.Update(customer);
                await _pharmacyDbContext.SaveChangesAsync();
                return customer.CustomerId;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was updating changes");
            }
        }
    }
}
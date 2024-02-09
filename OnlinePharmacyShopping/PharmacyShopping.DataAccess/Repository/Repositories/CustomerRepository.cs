using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PharmacyDbContext _pharmacyDbContext;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(PharmacyDbContext pharmacyDbContext, ILogger<CustomerRepository> logger)
        {
            _pharmacyDbContext = pharmacyDbContext;
            _logger = logger;
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            try
            {
                _pharmacyDbContext.Customers.Add(customer);
                await _pharmacyDbContext.SaveChangesAsync();
                _logger.LogInformation("Customer was successfully added.");
                return customer.CustomerId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Customer to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Customer to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task<int> DeleteCustomerAsync(Customer customer)
        {
            try
            {
                _pharmacyDbContext.Customers.Remove(customer);
                await _pharmacyDbContext.SaveChangesAsync();
                _logger.LogInformation("Customer was successfully deleted.");
                return customer.CustomerId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Customer to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Customer to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync(string? searchWord)
        {
            try
            {
                var allCustomer = await _pharmacyDbContext.Customers
                    .Include(x => x.Purchases)
                    .Include(x => x.Sales)
                    .Include(x => x.Reports)
                    .AsSplitQuery()
                    .ToListAsync();
                if (!string.IsNullOrEmpty(searchWord))
                {
                    allCustomer = allCustomer.Where(n => n.CustomerFirstName.Contains(searchWord) || n.CustomerLastName.Contains(searchWord)).ToList();
                }
                _logger.LogInformation("All Customers were found successfully.");
                return allCustomer;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Customers in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Customers from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving Customers information.");
            }
        }

        public async Task<Customer> GetCustomerByFirstAndLastNamesAsync(string customerFirstName, string customerLastName)
        {
            try
            {
                _logger.LogInformation("nma diyishniham bilmiman.");
                return await GetCustomerByFullName(customerFirstName, customerLastName);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving CustomerById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving CustomerById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving CustomerById information.");
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("CustomerById was found successfully.");
                return await _pharmacyDbContext.Customers
                   .Include(x => x.Purchases)
                   .Include(x => x.Sales)
                   .Include(x => x.Reports)
                   .AsSplitQuery()
                   .FirstOrDefaultAsync(x => x.CustomerId == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving CustomerById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving CustomerById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving CustomerById information.");
            }
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                _pharmacyDbContext.Customers.Update(customer);
                await _pharmacyDbContext.SaveChangesAsync();
                _logger.LogInformation("Customer was successfully updated.");
                return customer.CustomerId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Customer {customer.CustomerId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Customer {customer.CustomerId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }


        //Private methods bro!!

        private void GetCustomerByFullName(string customerFirstName, string customerLastName)
        {
            // Connection string to your Npgsql database
            string connString = "Host=myserver;Username=myuser;Password=mypass;Database=mydatabase";

            // SQL query to select the user by first name and last name
            string sql = "SELECT * FROM customers WHERE customer_first_name = @CustomerFirstName AND customer_last_name = @CustomerLastName";

            // Create a new NpgsqlConnection using the connection string
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create a new NpgsqlCommand with the SQL query and connection
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@CustomerFirstName", customerFirstName);
                        cmd.Parameters.AddWithValue("@CustomerLastName", customerLastName);

                        // Execute the query and retrieve the results
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Iterate through the rows
                                while (reader.Read())
                                {
                                    // Access the values of the returned columns
                                    int userId = reader.GetInt32(reader.GetOrdinal("customer_id"));
                                    string userFirstName = reader.GetString(reader.GetOrdinal("customer_first_name"));
                                    string userLastName = reader.GetString(reader.GetOrdinal("customer_last_name"));
                                }
                            }
                            else
                            {
                                Console.WriteLine("No user found with the provided first name and last name.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
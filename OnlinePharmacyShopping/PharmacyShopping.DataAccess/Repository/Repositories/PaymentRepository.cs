using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ILogger<PaymentRepository> _logger;

        public PaymentRepository(PharmacyDbContext context, ILogger<PaymentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddPaymentAsync(Payment payment)
        {
            try
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Payment was successfully added.");
                return payment.PaymentId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Payment to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Payment to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task<int> DeletePaymentAsync(Payment payment)
        {
            try
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Payment was successfully deleted.");
                return payment.PaymentId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Payment to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Payment to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            try
            {
                _logger.LogInformation("All Payments were found successfully.");
                return await _context.Payments
                    .Include(u => u.Sale)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Payments in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Payments from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving Payments information.");
            }
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("PaymentById was found successfully.");
                return await _context.Payments
                    .Include(u => u.Sale)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(u => u.PaymentId == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving PaymentById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving PaymentById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving PharmacyById information.");
            }
        }

        public async Task<int> UpdatePaymentAsync(Payment payment)
        {
            try
            {
                _context.Update(payment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Payment was successfully updated.");
                return payment.PaymentId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Payment {payment.PaymentId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Payment {payment.PaymentId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}
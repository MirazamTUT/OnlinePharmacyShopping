using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IPaymentRepository
    {
        Task<int> AddPaymentAsync(Payment payment);

        Task<Payment> GetPaymentByIdAsync(int id);

        Task<List<Payment>> GetAllPaymentsAsync();

        Task<int> UpdatePaymentAsync(Payment payment);

        Task<int> DeletePaymentAsync(Payment payment);
    }
}
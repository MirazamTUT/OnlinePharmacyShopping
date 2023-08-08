using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IPaymentService
    {
        Task<int> AddPaymentAsync(PaymentRequestDTO paymentRequestDTO);

        Task<PaymentResponseDTO> GetPaymentByIdAsync(int paymentId);

        Task<List<PaymentResponseDTO>> GetAllPaymentsAsync();

        Task<int> UpdatePaymentAsync(PaymentRequestDTO paymentRequestDTO, int paymentId);

        Task<int> DeletePaymentAsync(int paymentId);
    }
}
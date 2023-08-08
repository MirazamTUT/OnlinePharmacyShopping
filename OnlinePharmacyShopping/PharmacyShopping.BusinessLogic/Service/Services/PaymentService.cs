using AutoMapper;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddPaymentAsync(PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                _logger.LogInformation("Payment was successfully added.");
                return await _paymentRepository.AddPaymentAsync(_mapper.Map<Payment>(paymentRequestDTO));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Payment to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Payment to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeletePaymentAsync(int paymentId)
        {
            try
            {
                var paymentResult = await _paymentRepository.GetPaymentByIdAsync(paymentId);
                if (paymentResult is not null)
                {
                    _logger.LogInformation("Payment was successfully deleted.");
                    return await _paymentRepository.DeletePaymentAsync(paymentResult);
                }
                else
                {
                    throw new Exception("Object cannot be deleted.");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Payment to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Payment to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<PaymentResponseDTO> GetPaymentByIdAsync(int paymentId)
        {
            try
            {
                _logger.LogInformation("PaymentById was found successfully.");
                return _mapper.Map<PaymentResponseDTO>(await _paymentRepository.GetPaymentByIdAsync(paymentId));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving PaymentById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving PaymentById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PaymentResponseDTO>> GetAllPaymentsAsync()
        {
            try
            {
                _logger.LogInformation("All Payments were found successfully.");
                return _mapper.Map<List<PaymentResponseDTO>>(await _paymentRepository.GetAllPaymentsAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Payments in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Payments from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdatePaymentAsync(PaymentRequestDTO paymentRequestDTO, int paymentId)
        {
            try
            {
                var paymentResult = await _paymentRepository.GetPaymentByIdAsync(paymentId);
                if(paymentResult is not null)
                {
                    paymentResult = _mapper.Map<Payment>(paymentRequestDTO);
                    paymentResult.PaymentId = paymentId;
                    _logger.LogInformation("Payment was successfully updated.");
                    return await _paymentRepository.UpdatePaymentAsync(paymentResult);
                }
                else
                {
                    throw new Exception("Object cannot be updated.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Payment {paymentId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Payment {paymentId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}

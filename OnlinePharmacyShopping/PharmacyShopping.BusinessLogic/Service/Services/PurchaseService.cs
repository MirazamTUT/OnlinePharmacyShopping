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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PurchaseService> _logger;

        public PurchaseService(IPurchaseRepository purchaseRepository, ILogger<PurchaseService> logger, IMapper mapper, IMedicineRepository medicineRepository, ISaleRepository saleRepository)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
            _logger = logger;
            _medicineRepository = medicineRepository;
            _saleRepository = saleRepository;
        }

        public async Task<int> AddPurchaseAsync(PurchaseRequestDTO purchaseRequestDTO)
        {
            try
            {
                _logger.LogInformation("Purchase was successfully added.");
                var purchaseResult = _mapper.Map<Purchase>(purchaseRequestDTO);
                var medicine = await _medicineRepository.GetMedicineByIdAsync(purchaseResult.MedicineId);
                purchaseResult.TotalPrice = (double)purchaseResult.Amount * medicine.MedicinePrice;
                var id = await _purchaseRepository.AddPurchaseAsync(purchaseResult);
                await _saleRepository.AddingNewPriceToSaleTotalPriceAsync(purchaseResult.SaleId, purchaseResult.TotalPrice);
                return id;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Purchase to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Purchase to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeletePurchaseAsync(int purchaseId)
        {
            try
            {
                var purchaseResult = await _purchaseRepository.GetPurchaseByIdAsync(purchaseId);
                if (purchaseResult is not null)
                {
                    _logger.LogInformation("Purchase was successfully deleted.");
                    return await _purchaseRepository.DeletePurchaseAsync(purchaseResult);
                }
                else
                {
                    throw new Exception("Object cannot be deleted.");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Purchase to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Purchase to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<PurchaseResponseDTO> GetPurchaseByIdAsync(int purchaseId)
        {
            try
            {
                _logger.LogInformation("PurchaseById was found successfully.");
                return _mapper.Map<PurchaseResponseDTO>(await _purchaseRepository.GetPurchaseByIdAsync(purchaseId));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving PurchaseById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving PurchaseById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PurchaseResponseDTO>> GetAllPurchasesAsync()
        {
            try
            {
                _logger.LogInformation("All Purchases were found successfully.");
                return _mapper.Map<List<PurchaseResponseDTO>>(await _purchaseRepository.GetAllPurchasesAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Purchases in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Purchases from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdatePurchaseAsync(PurchaseRequestDTO purchaseRequestDTO, int purchaseId)
        {
            try
            {
                var purchaseResult = await _purchaseRepository.GetPurchaseByIdAsync(purchaseId);
                if(purchaseResult is not null)
                {
                    purchaseResult = _mapper.Map<Purchase>(purchaseRequestDTO);
                    purchaseResult.PurchaseId = purchaseId;
                    _logger.LogInformation("Purchase was successfully updated.");
                    return await _purchaseRepository.UpdatePurchaseAsync(purchaseResult);
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
                _logger.LogError($"An error occurred while updating Purchase {purchaseId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Purchase {purchaseId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}
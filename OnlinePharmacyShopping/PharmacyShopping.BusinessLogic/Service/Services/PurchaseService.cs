using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }

        public async Task<int> AddPurchaseAsync(PurchaseRequestDTO purchaseRequestDTO)
        {
            try
            {
                return await _purchaseRepository.AddPurchaseAsync(_mapper.Map<Purchase>(purchaseRequestDTO));
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
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
                    return await _purchaseRepository.DeletePurchaseAsync(purchaseResult);
                }
                else
                {
                    throw new Exception("Object cannot be deleted");
                }
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

        public async Task<PurchaseResponseDTO> GetPurchaseByIdAsync(int purchaseId)
        {
            try
            {
                return _mapper.Map<PurchaseResponseDTO>(await _purchaseRepository.GetPurchaseByIdAsync(purchaseId));
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PurchaseResponseDTO>> GetAllPurchasesAsync()
        {
            try
            {
                return _mapper.Map<List<PurchaseResponseDTO>>(await _purchaseRepository.GetAllPurchasesAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the info");
            }
            catch (Exception ex)
            {
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
                    return await _purchaseRepository.UpdatePurchaseAsync(purchaseResult);
                }
                else
                {
                    throw new Exception("Object cannot be updated");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it updating changes");
            }
        }
    }
}

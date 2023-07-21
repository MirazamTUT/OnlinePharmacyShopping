using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;

        public SalesService(ISalesRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        public async Task<int> AddSalesAsync(SalesRequestDTO salesRequestDTO)
        {
            try
            {
                return await _salesRepository.AddSalesAsync(_mapper.Map<Sales>(salesRequestDTO));
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteSalesAsync(int id)
        {
            try
            {
                var salesResult = await _salesRepository.GetSalesByCustomerIdAsync(id);
                if (salesResult is not null)
                {
                    return await _salesRepository.DeleteSalesAsync(salesResult);
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

        public async Task<List<SalesResponseDTO>> GetAllSalesAsync()
        {
            try
            {
                return _mapper.Map<List<SalesResponseDTO>>(await _salesRepository.GetAllSalesAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SalesResponseDTO> GetSalesByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<SalesResponseDTO>(await _salesRepository.GetSalesByCustomerIdAsync(id));
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateSalesAsync(SalesRequestDTO salesRequestDTO, int id)
        {
            try
            {
                var salesResult = await _salesRepository.GetSalesByCustomerIdAsync(id);
                if (salesResult is not null)
                {
                    salesResult = _mapper.Map<Sales>(salesRequestDTO);
                    salesResult.SaleId = id;
                    return await _salesRepository.UpdateSalesAsync(salesResult);
                }
                else
                {
                    throw new Exception("Object cannot be updated");
                }
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
                throw new Exception("Operation was failed when it was updating changes");
            }
        }
    }
}

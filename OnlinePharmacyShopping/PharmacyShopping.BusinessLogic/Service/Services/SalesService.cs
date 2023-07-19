using AutoMapper;
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
            return await _salesRepository.AddSalesAsync(_mapper.Map<Sales>(salesRequestDTO));
        }

        public async Task<int> DeleteSalesAsync(int id)
        {
            var salesResult = await _salesRepository.GetSalesByCustomerIdAsync(id);
            if(salesResult is not null) 
            {
                return await _salesRepository.DeleteSalesAsync(_mapper.Map<Sales>(salesResult));
            }
            else
            {
                throw new Exception("Delete uchun Sales topilmadi");
            }
        }

        public async Task<List<SalesResponseDTO>> GetAllSalesAsync()
        {
            return _mapper.Map<List<SalesResponseDTO>>(await _salesRepository.GetAllSalesAsync());
        }

        public async Task<SalesResponseDTO> GetSalesByIdAsync(int id)
        {
            return _mapper.Map<SalesResponseDTO>(await _salesRepository.GetSalesByCustomerIdAsync(id));
        }

        public async Task<int> UpdateSalesAsync(SalesRequestDTO salesRequestDTO, int id)
        {
            var salesResult = await _salesRepository.GetSalesByCustomerIdAsync(id);
            if(salesResult is not null)
            {
                salesResult=_mapper.Map<Sales>(salesRequestDTO);
                salesResult.SaleId = id;
                return await _salesRepository.UpdateSalesAsync(salesResult);
            }
            else
            {
                throw new Exception("Update uchun Sales Topilmadi");
            }
        }
    }
}

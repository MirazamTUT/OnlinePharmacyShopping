using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IReportService
    {
        Task<int> AddReportAsync(ReportRequestDTO reportRequestDTO);

        Task<int> UpdateReportAsync(ReportRequestDTO reportRequestDTO, int id);

        Task<int> DeleteReportAsync(int id);

        Task<ReportResponseDTO> GetReportByIdAsync(int id);

        Task<List<ReportResponseDTO>> GetAllReportsAsync();
    }
}

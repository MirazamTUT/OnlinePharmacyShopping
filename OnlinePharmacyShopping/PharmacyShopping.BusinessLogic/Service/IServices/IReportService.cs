using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IReportService
    {
        Task<int> AddReportAsync(ReportRequestDTO reportRequestDTO);

        Task<int> UpdateReportAsync(ReportRequestDTO reportRequestDTO, int Id);

        Task<int> DeleteReportAsync(int Id);

        Task<ReportResponseDTO> GetReportByIdAsync(int Id);

        Task<List<ReportResponseDTO>> GetAllReportsAsync();
    }
}

using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IReportRepository
    {
        Task<int> AddReportAsync(Report report);

        Task<Report> GetReportByIdAsync(int id);

        Task<List<Report>> GetAllReportsAsync();

        Task<int> DeleteReportAsync(Report report);

        Task<int> UpdateReportAsync(Report report);
    }
}

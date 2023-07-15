using Castle.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using PharmacyShopping.DataAccess.Repository.Repostories;

namespace PharmacyShopping.BusinessLogic.ServiceExtention
{
    public static class ServiceExtetions
    {
        public static void RegisterService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMedicineRepository, MedicineRepository>();
        }
    }
}

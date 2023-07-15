using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using PharmacyShopping.DataAccess.Repository.Repostories;

namespace PharmacyShopping.BusinessLogic.ServiceExtentions
{
    public static class ServiceExtentions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataBaseRepository, IDataBaseRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
        }
    }
}

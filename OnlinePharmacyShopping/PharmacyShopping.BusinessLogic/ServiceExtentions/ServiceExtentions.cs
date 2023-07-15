using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using PharmacyShopping.DataAccess.Repository.Repositories;

namespace PharmacyShopping.BusinessLogic.ServiceExtentions
{
    public static class ServiceExtentions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISalesRepository, SalesRepository>();
        }
    }
}

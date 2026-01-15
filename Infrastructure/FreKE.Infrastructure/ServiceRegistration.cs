using FreKE.Domain.Providers;
using FreKE.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddProviderServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserProvider, UserProvider>();

            return services;
        }
    }
}

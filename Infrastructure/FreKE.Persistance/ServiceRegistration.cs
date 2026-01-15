using FreKE.Application.Repositories;
using FreKE.Persistence.Contexts;
using FreKE.Persistence.Helpers;
using FreKE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<FreKEDbContext>(options =>
                options.UseNpgsql(connectionString));

            //dapper bağımlılığı
            services.AddSingleton<IFreKEDbHelper>(_ => new FreKEDbHelper(connectionString));
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IPriceOfferRepository, PriceOfferRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            return services;
        }
    }
}

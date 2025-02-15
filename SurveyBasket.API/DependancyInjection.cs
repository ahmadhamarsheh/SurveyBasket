

using Microsoft.EntityFrameworkCore;
using SurveyBasket.API.Persistence;

namespace SurveyBasket.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDependancies(this IServiceCollection services)
        {   
            services.AddScoped<IPollServices, PollServices>();
            return services;
        }
        public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;
        }
        public static IServiceCollection AddController(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
            return services;
        }
        public static IServiceCollection AddConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string not found");
            services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(conn));
            return services;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace SurveyBasket.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDependancies(this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddScoped<IPollServices, PollServices>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddMapsterConfig();
            services.AddController();
            services.AddSwagger();
            services.AddFluentValidation();
            services.AddConnectionString(configuration);
            services.AddAuthConfig();
            return services;
        }
        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;
        }
        private static IServiceCollection AddController(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
            return services;
        }
        private static IServiceCollection AddConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string not found");
            services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(conn));
            return services;
        }

        private static IServiceCollection AddAuthConfig(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}

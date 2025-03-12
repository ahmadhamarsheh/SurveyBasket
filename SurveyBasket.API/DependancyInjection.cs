using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.API.Authentication;
using SurveyBasket.API.Contract.Development;
using SurveyBasket.API.Errors;
using System.Text;

namespace SurveyBasket.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDependancies(this IServiceCollection services, IConfiguration configuration)
        {

            var AllowedOrigins = configuration.GetSection(DevEnvURL.GetDevEnvUrlFromConfig).Get<string[]>();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder
                => builder
                    .WithOrigins(AllowedOrigins!)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                )
            );
            services.AddScoped<IPollServices, PollServices>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.sectionName));
            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.sectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddMapsterConfig();
            services.AddController(); 
            services.AddSwagger();
            services.AddFluentValidation();
            services.AddConnectionString(configuration);
            services.AddAuthConfig(configuration);

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

        private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(JwtOptions.sectionName).Get<JwtOptions>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer( o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings?.Key!)),
                    ValidIssuer = settings?.Issuer,
                    ValidAudience = settings?.Audience,
                };
            }                
                );
            return services;
        }
    }
}

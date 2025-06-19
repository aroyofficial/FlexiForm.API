using FlexiForm.API.Commons.Interfaces;
using FlexiForm.API.Configurations;
using FlexiForm.API.Mappings;
using FlexiForm.API.Policies.Handlers;
using FlexiForm.API.Policies.Requirements;
using FlexiForm.API.Repositories.Implementations;
using FlexiForm.API.Repositories.Interfaces;
using FlexiForm.API.Services.Context;
using FlexiForm.API.Services.Implementations;
using FlexiForm.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using AuthorizationPolicy = FlexiForm.API.Constants.AuthorizationPolicy;

namespace FlexiForm.API.Extensions
{
    /// <summary>
    /// Provides extension methods for registering mapping profiles, repositories, services, etc. into the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Configures application settings and database connection for the application.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the configurations and services will be added.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance used to retrieve configuration values from appsettings.</param>
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<ConnectionString>(configuration.GetSection("ConnectionStrings"));
            services.Configure<TokenConfiguration>(configuration.GetSection("TokenConfiguration"));
            string primaryDBConnectionString = configuration.GetSection("ConnectionString:PrimaryDB").Value;
            services.AddScoped<IDbConnection>(connection => new SqlConnection(primaryDBConnectionString));
        }

        /// <summary>
        /// Configures JWT Bearer authentication using the settings defined in the application's configuration.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the authentication services will be added.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance containing the JWT token configuration section.</param>
        /// <param name="env">The <see cref="IWebHostEnvironment"/> instance containing the hosting environment information.</param>
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var tokenConfiguration = configuration.GetSection("TokenConfiguration").Get<TokenConfiguration>();
            var secretKey = Encoding.UTF8.GetBytes(tokenConfiguration.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = !env.IsDevelopment();
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfiguration.Issuer,
                    ValidAudience = tokenConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });
        }

        /// <summary>
        /// Configures authorization policies and registers authorization handlers for the application.
        /// </summary>
        /// <param name="services">The service collection to which the authorization configuration is added.</param>
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicy.ProfileOwner, policy =>
                    policy.Requirements.Add(new ProfileOwnerRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, ProfileOwnerHandler>();
        }

        /// <summary>
        /// Adds AutoMapper mapping profiles to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the AutoMapper profiles will be added.</param>
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<LoginResponseProfile>();
                config.AddProfile<RegistrationResponseProfile>();
                config.AddProfile<TokenPayloadProfile>();
                config.AddProfile<UserLookupRequestProfile>();
                config.AddProfile<UserProfile>();
                config.AddProfile<UserResponseProfile>();
            }, AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Adds repository interfaces and their corresponding implementations to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the repositories will be added.</param>
        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        /// <summary>
        /// Adds application service interfaces and their corresponding implementations to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
        }
    }
}

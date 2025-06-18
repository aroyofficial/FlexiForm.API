using FlexiForm.API.Configurations;
using FlexiForm.API.Mappings;
using FlexiForm.API.Repositories.Implementations;
using FlexiForm.API.Repositories.Interfaces;
using FlexiForm.API.Services.Implementations;
using FlexiForm.API.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FlexiForm.API
{
    /// <summary>
    /// Configures services and the application request pipeline.
    /// The <c>Startup</c> class is used by the runtime to set up dependencies,
    /// configuration settings, and middleware for the ASP.NET Core application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// Builds the application configuration by loading settings from JSON files and environment variables.
        /// </summary>
        /// <param name="env">Provides information about the web hosting environment, such as content root and environment name (e.g., Development, Staging, Production).</param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<UserMappingProfile>();
            }, AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionStrings"));

            string primaryDBConnectionString = Configuration.GetSection("ConnectionString:PrimaryDB").Value;
            services.AddScoped<IDbConnection>(connection => new SqlConnection(primaryDBConnectionString));

            // DIs for repositories
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // DIs for services
            services.AddScoped<IUserService, UserService>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddAuthorization();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

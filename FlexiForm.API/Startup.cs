using FlexiForm.API.Extensions;

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
            Environment = env;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the application hosting environment.
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplication(Configuration);
            services.ConfigureAuthentication(Configuration, Environment);
            services.AddMappingProfiles();
            services.AddCustomRepositories();
            services.AddCustomServices();
            services.ConfigureAuthorization();
            services.AddHttpContextAccessor();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

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

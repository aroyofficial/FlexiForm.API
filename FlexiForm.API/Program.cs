namespace FlexiForm.API
{
    /// <summary>
    /// Contains the application's entry point and host builder configuration.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Configures and builds the host for the web application using default settings.
        /// </summary>
        /// <param name="args">An array of command-line arguments used to configure the host.</param>
        /// <returns>An <see cref="IHostBuilder"/> configured with default settings and the specified startup class.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

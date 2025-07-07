namespace FlexiForm.API.Configurations
{
    /// <summary>
    /// Represents the application settings as defined in the configuration file (e.g., appsettings.json).
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the connection string settings for the application.
        /// </summary>
        public ConnectionString ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the token-related configuration, such as JWT settings.
        /// </summary>
        public TokenConfiguration TokenConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the SMTP configuration settings used for sending emails.
        /// </summary>
        public SMTPConfiguration SMTPConfiguration { get; set; }
    }
}

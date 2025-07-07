namespace FlexiForm.API.Configurations
{
    /// <summary>
    /// Represents the SMTP configuration settings used for sending emails.
    /// </summary>
    public class SMTPConfiguration
    {
        /// <summary>
        /// Gets or sets the SMTP server address.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the port number used to connect to the SMTP server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the username used for SMTP authentication.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password used for SMTP authentication.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SSL should be used for the SMTP connection.
        /// </summary>
        public bool UseSSL { get; set; }
    }
}

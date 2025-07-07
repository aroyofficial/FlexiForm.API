using FlexiForm.API.Configurations;
using FlexiForm.API.Internals;
using FlexiForm.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace FlexiForm.API.Services.Implementations
{
    /// <summary>
    /// Provides functionality to send emails using Gmail SMTP service.
    /// Implements the <see cref="IMailService"/> interface.
    /// </summary>
    public class GoogleMailService : IMailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _from;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleMailService"/> class.
        /// Configures the SMTP client using the specified <see cref="SMTPConfiguration"/>.
        /// </summary>
        /// <param name="configuration">
        /// The SMTP configuration options injected from the application's configuration settings.
        /// </param>
        public GoogleMailService(IOptions<SMTPConfiguration> configuration)
        {
            var smtpConfiguration = configuration.Value;
            _smtpClient = new SmtpClient(smtpConfiguration.Server, smtpConfiguration.Port);
            _smtpClient.Credentials = new NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password);
            _smtpClient.EnableSsl = smtpConfiguration.UseSSL;
            _from = new MailAddress(smtpConfiguration.Username);
        }

        /// <inheritdoc/>
        public async Task SendForgotPasswordMailAsync(MailPayload payload)
        {
            var to = new MailAddress(payload.ToEmail);
            using (var message = new MailMessage(_from, to))
            {
                message.Subject = "FlexiForm - Reset Password Request";
                message.Body = await GetBodyAsync(payload, "EmailTemplates/ForgotPassword.html");
                message.IsBodyHtml = true;

                _smtpClient.Send(message);
            }
        }

        /// <inheritdoc/>
        public async Task SendPasswordChangedAlertMailAsync(MailPayload payload)
        {
            var to = new MailAddress(payload.ToEmail);
            using (var message = new MailMessage(_from, to))
            {
                message.Subject = "FlexiForm - Password Changed Alert";
                message.Body = await GetBodyAsync(payload, "EmailTemplates/ResetPasswordConfirmation.html");
                message.IsBodyHtml = true;

                _smtpClient.Send(message);
            }
        }

        /// <summary>
        /// Asynchronously reads and processes an email template file, replacing macros with values from the provided payload.
        /// </summary>
        /// <param name="payload">
        /// The mail payload containing the recipient's email address and a dictionary of macros to replace in the template.
        /// </param>
        /// <param name="templatePath">
        /// The relative path to the email template file to be read and processed (e.g., "Templates/ForgotPassword.html").
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the processed HTML string with macros replaced by actual values.
        /// </returns>
        private static async Task<string> GetBodyAsync(MailPayload payload, string templatePath)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload), "Mail payload cannot be null.");
            }

            var to = new MailAddress(payload.ToEmail);
            var absoluteTemplatePath = $@"{Environment.CurrentDirectory}/{templatePath}";
            var body = await File.ReadAllTextAsync(absoluteTemplatePath);

            foreach (var (key, value) in payload.Macros)
            {
                body = body.Replace(("{{" + key + "}}").ToUpper(), value);
            }

            return body;
        }
    }
}

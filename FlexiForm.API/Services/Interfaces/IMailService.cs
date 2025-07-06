using FlexiForm.API.Internals;

namespace FlexiForm.API.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for sending email notifications.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends a <b>Forgot Password</b> email to the specified recipient using a predefined HTML template
        /// and dynamic macros for content replacement.
        /// </summary>
        /// <param name="payload">
        /// The email payload containing the recipient's email address and macro key-value pairs 
        /// to be replaced in the HTML template.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation of sending the email.
        /// </returns>
        Task SendForgotPasswordMailAsync(MailPayload payload);

        /// <summary>
        /// Sends an email notification to the user informing them that their password has been successfully changed.
        /// </summary>
        /// <param name="payload">
        /// The mail payload containing the recipient's email address and any dynamic macros to be replaced in the email template.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation of sending the password change alert email.
        /// </returns>
        Task SendPasswordChangedAlertMailAsync(MailPayload payload);
    }
}

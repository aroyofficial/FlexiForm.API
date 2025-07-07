namespace FlexiForm.API.Internals
{
    /// <summary>
    /// Represents the payload for sending an email, including the recipient and dynamic content macros.
    /// </summary>
    public class MailPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailPayload"/> class.
        /// </summary>
        public MailPayload()
        {
            Macros = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the recipient's email address.
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// Gets or sets the collection of macros to be replaced in the email template.
        /// The key is the macro placeholder, and the value is the replacement text.
        /// </summary>
        public IDictionary<string, string> Macros { get; set; }
    }
}

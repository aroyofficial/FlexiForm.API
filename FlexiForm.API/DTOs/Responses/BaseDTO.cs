namespace FlexiForm.API.DTOs.Responses
{
    /// <summary>
    /// Represents the base Data Transfer Object (DTO) with a common identifier.
    /// </summary>
    public abstract class BaseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the DTO.
        /// </summary>
        public int Id { get; set; }
    }
}

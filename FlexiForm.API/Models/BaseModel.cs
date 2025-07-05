namespace FlexiForm.API.Models
{
    /// <summary>
    /// Represents the base model for all entities, providing common audit and identity fields.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the internal numeric identifier of the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the globally unique identifier (GUID) of the entity.
        /// </summary>
        public Guid RowId { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who created the entity.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who last updated the entity.
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}

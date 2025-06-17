using Dapper;

namespace FlexiForm.API.Internals
{
    /// <summary>
    /// Represents a stored procedure to be executed using Dapper, including its name,
    /// parameters, transaction flag, and optional execution timeout.
    /// </summary>
    public class StoredProcedure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedure"/> class.
        /// Sets up an empty <see cref="DynamicParameters"/> collection for use with the stored procedure.
        /// </summary>
        public StoredProcedure()
        {
            Parameters = new DynamicParameters();
        }

        /// <summary>
        /// Gets or sets the name of the stored procedure to be executed.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parameters to be passed to the stored procedure.
        /// </summary>
        public DynamicParameters Parameters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the stored procedure should be executed within a transaction.
        /// </summary>
        public bool UseTransaction { get; set; }

        /// <summary>
        /// Gets or sets the optional timeout duration (in seconds) for the stored procedure execution.
        /// A null value indicates that the default command timeout should be used.
        /// </summary>
        public int? Timeout { get; set; }
    }
}

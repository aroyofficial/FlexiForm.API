using FlexiForm.API.Internals;

namespace FlexiForm.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines the base repository interface for executing and querying stored procedures.
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Executes a stored procedure asynchronously without returning a result.
        /// </summary>
        /// <param name="procedure">The <see cref="StoredProcedure"/> object containing the procedure name, parameters, and configuration.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ExecuteAsync(StoredProcedure procedure);

        /// <summary>
        /// Executes a stored procedure asynchronously and returns a collection of results of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the result objects to be returned.</typeparam>
        /// <param name="procedure">The <see cref="StoredProcedure"/> object containing the procedure name, parameters, and configuration.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and contains a collection of results of type <typeparamref name="T"/>.
        /// <br/>Returns an empty collection if no records are found.
        /// </returns>
        Task<IEnumerable<T>> QueryAsync<T>(StoredProcedure procedure);

        /// <summary>
        /// Executes a stored procedure asynchronously and returns exactly one result of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the result object to be returned.</typeparam>
        /// <param name="procedure">The <see cref="StoredProcedure"/> object containing the procedure name, parameters, and configuration.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and contains a single result of type <typeparamref name="T"/>.
        /// </returns>
        Task<T> QuerySingleAsync<T>(StoredProcedure procedure);

        /// <summary>
        /// Executes the specified stored procedure asynchronously and returns a single record of specified type.
        /// </summary>
        /// <typeparam name="T">The type of the result object to be returned.</typeparam>
        /// <param name="procedure">The <see cref="StoredProcedure"/> object containing the procedure name, parameters, and configuration.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and contains a single result of type <typeparamref name="T"/>.
        /// <br/>Returns the default value if no matching record is found.
        /// </returns>
        Task<T> QuerySingleOrDefaultAsync<T>(StoredProcedure procedure);

    }
}

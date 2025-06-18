using Dapper;
using FlexiForm.API.Internals;
using FlexiForm.API.Repositories.Interfaces;
using System.Data;
using System.Runtime.CompilerServices;

namespace FlexiForm.API.Repositories.Implementations
{
    /// <summary>
    /// Provides a base implementation for executing and querying stored procedures using Dapper.
    /// </summary>
    public sealed class BaseRepository : IBaseRepository
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class with the specified database connection.
        /// </summary>
        /// <param name="connection">The database connection to be used for executing stored procedures.</param>
        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync(StoredProcedure procedure)
        {
            if (procedure == null)
            {
                throw new ArgumentNullException(nameof(procedure));
            }

            await _ExecuteAsync(procedure, procedure.UseTransaction ? _connection.BeginTransaction() : null);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> QueryAsync<T>(StoredProcedure procedure)
        {
            if (procedure == null)
            {
                throw new ArgumentNullException(nameof(procedure));
            }

            return await _QueryAsync<T>(procedure, procedure.UseTransaction ? _connection.BeginTransaction() : null);
        }

        /// <inheritdoc/>
        public async Task<T> QuerySingleAsync<T>(StoredProcedure procedure)
        {
            if (procedure == null)
            {
                throw new ArgumentNullException(nameof(procedure));
            }

            return await _QuerySingleAsync<T>(procedure, procedure.UseTransaction ? _connection.BeginTransaction() : null);
        }

        /// <inheritdoc/>
        public async Task<T> QuerySingleOrDefaultAsync<T>(StoredProcedure procedure)
        {
            if (procedure == null)
            {
                throw new ArgumentNullException(nameof(procedure));
            }

            return await _QuerySingleOrDefaultAsync<T>(procedure, procedure.UseTransaction ? _connection.BeginTransaction() : null);
        }

        /// <summary>
        /// Internal helper method to execute a stored procedure asynchronously without returning a result.
        /// Commits or rolls back the transaction based on success or failure.
        /// </summary>
        /// <param name="procedure">The stored procedure to execute.</param>
        /// <param name="transaction">The optional transaction to use.</param>
        private async Task _ExecuteAsync(StoredProcedure procedure, IDbTransaction? transaction = null)
        {
            try
            {
                await _connection.ExecuteAsync(
                    procedure.Name,
                    procedure.Parameters,
                    transaction,
                    procedure.Timeout,
                    CommandType.StoredProcedure
                );
                transaction?.Commit();
            }
            catch (Exception)
            {
                transaction?.Rollback();
                transaction?.Dispose();
                throw;
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        /// <summary>
        /// Internal helper method to execute a stored procedure asynchronously and return a collection of results.
        /// </summary>
        /// <typeparam name="T">The type of result objects.</typeparam>
        /// <param name="procedure">The stored procedure to execute.</param>
        /// <param name="transaction">The optional transaction to use.</param>
        /// <returns>A collection of results of type <typeparamref name="T"/>.</returns>
        private async Task<IEnumerable<T>> _QueryAsync<T>(StoredProcedure procedure, IDbTransaction? transaction = null)
        {
            IEnumerable<T> result = null;

            try
            {
                result = await _connection.QueryAsync<T>(
                    procedure.Name,
                    procedure.Parameters,
                    transaction,
                    procedure.Timeout,
                    CommandType.StoredProcedure
                );
                transaction?.Commit();
            }
            catch (Exception)
            {
                transaction?.Rollback();
                transaction?.Dispose();
                throw;
            }
            finally
            {
                transaction?.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Executes the specified stored procedure asynchronously and returns exactly one result of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the result object to be returned.</typeparam>
        /// <param name="procedure">The <see cref="StoredProcedure"/> containing the procedure name, parameters, and configuration.</param>
        /// <param name="transaction">An optional transaction to be use.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and returns a single result of type <typeparamref name="T"/>.
        /// </returns>
        private async Task<T> _QuerySingleAsync<T>(StoredProcedure procedure, IDbTransaction? transaction = null)
        {
            T result = default;

            try
            {
                result = await _connection.QuerySingleAsync<T>(
                    procedure.Name,
                    procedure.Parameters,
                    transaction,
                    procedure.Timeout,
                    CommandType.StoredProcedure
                );
                transaction?.Commit();
            }
            catch (Exception)
            {
                transaction?.Rollback();
                transaction?.Dispose();
                throw;
            }
            finally
            {
                transaction?.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Internal helper method to execute a stored procedure asynchronously and return a single result or default value.
        /// </summary>
        /// <typeparam name="T">The type of the result object.</typeparam>
        /// <param name="procedure">The stored procedure to execute.</param>
        /// <param name="transaction">The optional transaction to use.</param>
        /// <returns>A result of type <typeparamref name="T"/> or the default value if not found.</returns>
        private async Task<T> _QuerySingleOrDefaultAsync<T>(StoredProcedure procedure, IDbTransaction? transaction = null)
        {
            T result = default;

            try
            {
                result = await _connection.QuerySingleOrDefaultAsync<T>(
                    procedure.Name,
                    procedure.Parameters,
                    transaction,
                    procedure.Timeout,
                    CommandType.StoredProcedure
                );
                transaction?.Commit();
            }
            catch (Exception)
            {
                transaction?.Rollback();
                transaction?.Dispose();
                throw;
            }
            finally
            {
                transaction?.Dispose();
            }

            return result;
        }
    }
}

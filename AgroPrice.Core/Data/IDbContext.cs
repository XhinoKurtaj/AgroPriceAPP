using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgroPrice.Core.Data
{
    /// <summary>
    /// Represents DB context
    /// </summary>
    public interface IDbContext
    {
        #region Methods

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        /// <summary>
        /// Saves all changes asynchronously made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Generate a script to create all tables for the current model.
        /// </summary>
        /// <returns>A SQL script.</returns>
        string GenerateCreateScript();

        /// <summary>
        /// Creates a LINQ query for the query type based on a raw SQL query.
        /// </summary>
        /// <typeparam name="TQuery">Query type.</typeparam>
        /// <param name="sql">The raw SQL query.</param>
        /// <returns>An IQueryable representing the raw SQL query.</returns>
        IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class;

        /// <summary>
        /// Creates a LINQ query for the entity based on a raw SQL query.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="sql">The raw SQL query.</param>
        /// <param name="parameters">The values to be assigned to parameters.</param>
        /// <returns>An IQueryable representing the raw SQL query.</returns>
        IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity;

        /// <summary>
        /// Detach an entity from the context.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="entity">Entity.</param>
        void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity;

        #endregion
    }
}

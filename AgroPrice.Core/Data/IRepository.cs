using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgroPrice.Core.Data
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        #region Methods

        /// <summary>
        /// Get entity by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Entity.</returns>
        TEntity GetById(params object[] id);

        /// <summary>
        /// Asynchronously get entity by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get entity by identifiers.
        /// </summary>
        /// <typeparam name="TEntity"> The type of the elements.</typeparam>
        /// <param name="ids">Identifiers.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> GetByIdAsync(object[] ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a LINQ query based on a raw SQL query.
        /// </summary>
        /// <typeparam name="TEntity"> The type of the elements.</typeparam>
        /// <param name="sql">
        ///     The raw SQL query. NB. A string literal may be passed here because <see cref="T:Microsoft.EntityFrameworkCore.RawSqlString" />
        ///     is implicitly convertible to string.
        /// </param>
        IQueryable<TEntity> FromSql(string sql);

        /// <summary>
        /// Creates a LINQ query based on a raw SQL query.
        /// </summary>
        /// <param name="sql">
        ///     The raw SQL query. NB. A string literal may be passed here because <see cref="T:Microsoft.EntityFrameworkCore.RawSqlString" />
        ///     is implicitly convertible to string.
        /// </param>
        /// <param name="parameters"> The values to be assigned to parameters. </param>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        /// <summary>
        /// Insert entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Asynchronously insert entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Insert entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously insert entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Edit entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Asynchronously update entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Edit entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously update entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Asynchronously delete entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously delete entities.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether IDbContext.SaveChanges() should be
        /// called after every operation.
        /// </summary>
        bool AutoSaveChanges { get; set; }

        /// <summary>
        /// Gets a table.
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a table with no-tracking enabled. Should be used only for read only operations.
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        #endregion
    }
}

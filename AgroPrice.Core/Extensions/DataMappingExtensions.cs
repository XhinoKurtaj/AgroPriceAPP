using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Core.Extensions
{
    public static class DataMappingExtensions
    {
        /// <summary>
        /// Maps the Id field of the BaseEntity to the PK of the table.
        /// Also sets the default value to: <code>newsequentialid()</code>.
        /// </summary>
        /// <typeparam name="TEntity">Entity to configure.</typeparam>
        /// <param name="builder">Model builder.</param>
        public static void HasKeyDefault<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.HasKey(model => model.Id);
            builder.Property(model => model.Id).HasDefaultValueSql("newsequentialid()");
        }

        /// <summary>
        /// Maps the entity to a default Table name, which is the entity name.
        /// </summary>
        /// <typeparam name="TEntity">Entity to configure.</typeparam>
        /// <param name="builder">Model builder.</param>
        public static void ToTableDefault<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.ToTable(typeof(TEntity).Name);
        }

        /// <summary>
        /// Maps the entity to a default table, with a default PK.
        /// Default table name is entity type name,
        /// and default PK is <see cref="BaseEntity" />.Id with a default value of: <code>newsequentialid()</code>.
        /// </summary>
        /// <typeparam name="TEntity">Entity to configure.</typeparam>
        /// <param name="builder">Model builder.</param>
        public static void MapDefaults<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.ToTableDefault();
            builder.HasKeyDefault();
        }
    }
}

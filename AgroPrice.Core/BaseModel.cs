using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Core
{
    /// <summary>
    /// Base class fro models
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the model identifier.
        /// </summary>
        public Guid Id { get; set; }
    }
}

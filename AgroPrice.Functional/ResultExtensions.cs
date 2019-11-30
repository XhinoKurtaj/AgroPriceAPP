using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Functional
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Adds an error to the <see cref="Result"/>.
        /// </summary>
        /// <param name="result">Result to append the error.</param>
        /// <param name="key">Key of the error.</param>
        /// <param name="message">Message of the error.</param>
        public static void AddError(this Result result, string key, string message)
        {
            result.Errors.Add(key, message);
        }

        /// <summary>
        /// Adds an error to the <see cref="Result"/>.
        /// </summary>
        /// <param name="result">Result to append the error.</param>
        /// <param name="message">Message of the error.</param>
        public static void AddError(this Result result, string message)
        {
            result.Errors.Add("", message);
        }

        /// <summary>
        /// Clears all errors.
        /// </summary>
        /// <param name="result">Result to clear errors from.</param>
        /// <param name="key">Key to clear.</param>
        public static void ClearErrors(this Result result, string key)
        {
            result.Errors.Remove(key);
        }
    }
}

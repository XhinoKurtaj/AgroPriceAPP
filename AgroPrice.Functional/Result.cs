using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPrice.Functional
{
    /// <summary>
    /// Represents the success state of a method.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets a list of errors.
        /// </summary>
        public MultiValueDictionary<string, string> Errors { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether there are no errors.
        /// </summary>
        public bool Success => !Errors.Any();

        /// <summary>
        /// Creates a new <see cref="Result"/> instance.
        /// </summary>
        protected Result()
        {
            Errors = new MultiValueDictionary<string, string>();
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance.
        /// </summary>
        /// <param name="error">Error message to append.</param>
        protected Result(string error) : this()
        {
            Errors.Add("", error);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance.
        /// </summary>
        /// <param name="key">Key for the error message to append.</param>
        /// <param name="error">Error message to append.</param>
        protected Result(string key, string error) : this()
        {
            Errors.Add(key, error);
        }

        /// <summary>
        /// Creates a new successful instance of the <see cref="Result"/> class.
        /// </summary>
        /// <returns><see cref="Result"/> without any errors (successful).</returns>
        public static Result Ok()
        {
            return new Result();
        }

        /// <summary>
        /// Creates a new successful instance of the <see cref="Result{TValue}"/> class.
        /// </summary>
        /// <typeparam name="T">Type of the value this result wraps.</typeparam>
        /// <param name="value">Value wrapped in the result.</param>
        /// <returns><see cref="Result{TValue}"/> without any errors (successful).</returns>
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> that contains 1 error.
        /// </summary>
        /// <param name="error">Error message to append.</param>
        /// <returns><see cref="Result"/> with 1 error.</returns>
        public static Result Fail(string error)
        {
            return new Result(error);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> that contains 1 error.
        /// </summary>
        /// <param name="key">Key for the error message to append.</param>
        /// <param name="error">Error message to append.</param>
        /// <returns><see cref="Result"/> with 1 error.</returns>
        public static Result Fail(string key, string error)
        {
            return new Result(key, error);
        }

        /// <summary>
        /// Creates a new <see cref="Result{TValue}"/> that contains 1 error.
        /// </summary>
        /// <typeparam name="T">Type of the value this result wraps.</typeparam>
        /// <param name="value">Value wrapped in the result.</param>
        /// <param name="error">Error message to append.</param>
        /// <returns><see cref="Result{TValue}"/> with 1 error.</returns>
        public static Result<T> Fail<T>(T value, string error)
        {
            return new Result<T>(value, error);
        }

        /// <summary>
        /// Creates a new <see cref="Result{TValue}"/> that contains 1 error.
        /// </summary>
        /// <typeparam name="T">Type of the value this result wraps.</typeparam>
        /// <param name="value">Value wrapped in the result.</param>
        /// <param name="key">Key for the error message to append.</param>
        /// <param name="error">Error message to append.</param>
        /// <returns><see cref="Result{TValue}"/> with 1 error.</returns>
        public static Result<T> Fail<T>(T value, string key, string error)
        {
            return new Result<T>(value, key, error);
        }

        /// <summary>
        /// Combines multiple <see cref="Result"/> instances into a single instance.
        /// </summary>
        /// <param name="results"><see cref="Result"/> instances to combine.</param>
        /// <returns>A <see cref="Result"/> containing all the errors from the other instances.</returns>
        public static Result Combine(params Result[] results)
        {
            var finalResult = new Result();
            foreach (var result in results)
            {
                if (result.Success) continue;

                foreach (var (key, value) in result.Errors)
                    foreach (var single in value)
                        finalResult.Errors.Add(key, single);
            }

            return finalResult;
        }
    }

    /// <summary>
    /// Represents the state and value returned from a method.
    /// </summary>
    /// <typeparam name="TValue">Type of the value to return.</typeparam>
    public class Result<TValue> : Result
    {
        /// <summary>
        /// Value wrapped in the result.
        /// </summary>
        public TValue Value { get; set; }

        protected internal Result(TValue value) : base()
        {
            Value = value;
        }

        protected internal Result(TValue value, string error) : this(value)
        {
            Errors.Add("", error);
        }

        protected internal Result(TValue value, string key, string error) : this(value)
        {
            Errors.Add(key, error);
        }
    }

    public class EnumerableResult<TValue> : Result<IEnumerable<TValue>>, IEnumerable<TValue>
    {
        protected internal EnumerableResult(IEnumerable<TValue> value) : base(value)
        {
        }

        protected internal EnumerableResult(IEnumerable<TValue> value, string error) : base(value, error)
        {
        }

        protected internal EnumerableResult(IEnumerable<TValue> value, string key, string error) : base(value, key, error)
        {
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

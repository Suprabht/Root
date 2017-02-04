

namespace SystemFrameWork.Error
{
    using System;
    /// <summary>
    /// The interface for the error class
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Gets the error unique identifier.
        /// </summary>
        /// <value>
        /// The error unique identifier.
        /// </value>
        string ErrorUniqueId { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        System.Collections.IDictionary Data { get; }

        /// <summary>
        /// Gets the help link.
        /// </summary>
        /// <value>
        /// The help link.
        /// </value>
        string HelpLink { get; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        long ErrorCode { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        string ErrorMessage { get; }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        string ExceptionMessage { get; }

        /// <summary>
        /// Gets the inner exception.
        /// </summary>
        /// <value>
        /// The inner exception.
        /// </value>
        string InnerException { get; }

        /// <summary>
        /// Gets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        string StackTrace { get; }

        /// <summary>
        /// Gets the target site.
        /// </summary>
        /// <value>
        /// The target site.
        /// </value>
        string TargetSite { get; }

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        ErrorType ErrorType { get; }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        DateTime DateTime { get; }
    }
}
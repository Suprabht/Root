
namespace SystemFrameWork.Error
{
    using System;
    using ExtendedMethords;

    /// <summary>
    /// This class is used for logging error
    /// </summary>
    public class Error : IError
    {
        #region Private Variable

        #endregion

        #region Properties
        /// <summary>
        /// Gets the error unique identifier.
        /// </summary>
        /// <value>
        /// The error unique identifier.
        /// </value>
        public string ErrorUniqueId { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public System.Collections.IDictionary Data { get; }

        /// <summary>
        /// Gets the help link.
        /// </summary>
        /// <value>
        /// The help link.
        /// </value>
        public string HelpLink { get; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public long ErrorCode { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        public string ExceptionMessage { get; }

        /// <summary>
        /// Gets the inner exception.
        /// </summary>
        /// <value>
        /// The inner exception.
        /// </value>
        public string InnerException { get; }

        /// <summary>
        /// Gets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        public string StackTrace { get; }

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        public ErrorType ErrorType { get; }

        /// <summary>
        /// Gets the target site.
        /// </summary>
        /// <value>
        /// The target site.
        /// </value>
        public string TargetSite { get; }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Error" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="targetSite">Target from where it is generated</param>
        /// <param name="data">The data.</param>
        public Error(string errorMessage, long errorCode, string exceptionMessage, string innerException, string stackTrace, ErrorType errorType, string targetSite = "", System.Collections.IDictionary data = null)
        {
            ErrorUniqueId = Guid.NewGuid().ToString();
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Data = data;
            ExceptionMessage = exceptionMessage;
            InnerException = innerException;
            StackTrace = stackTrace;
            ErrorType = errorType;
            TargetSite = targetSite;
            DateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// This constructor instantiate the error class 
        /// </summary>
        /// <param name="exception">Object of System.Exception is passed as parameter.</param>
        /// <param name="targetSite">Target from where it is generated</param>
        public Error(Exception exception, string targetSite = "")
        {
            ErrorUniqueId = Guid.NewGuid().ToString();
            Data = exception.Data;
            HelpLink = exception.HelpLink;
            ErrorCode = exception.HResult;
            ErrorMessage = exception.Message;
            ExceptionMessage = exception.InnerException.Message;
            InnerException = (exception.InnerException != null)? exception.InnerException.GetJsonString() : string.Empty;
            StackTrace = exception.StackTrace;
            ErrorType = ErrorType.ServerSideRuntime;
            TargetSite = targetSite;
            DateTime = DateTime.UtcNow;
        }

        #endregion

        #region private methods

        #endregion

        #region Public methods

        #endregion
    }
}

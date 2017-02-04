
namespace SystemFrameWork.Email
{
    using System.Collections.Generic;

    /// <summary>
    /// Class for IEmail
    /// </summary>
    public class Email : IEmail
    {


        #region Private Variable

        #endregion

        #region Properties

        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public List<string> To { get; }


        /// <summary>
        /// Gets the cc.
        /// </summary>
        /// <value>
        /// The cc.
        /// </value>
        public List<string> Cc { get; }


        /// <summary>
        /// Gets the BCC.
        /// </summary>
        /// <value>
        /// The BCC.
        /// </value>
        public List<string> Bcc { get;}


        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public string From { get; }


        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; }


        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="from">From.</param>
        /// <param name="bcc">The BCC.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="to">To.</param>
        public Email(string body, string subject, string @from, List<string> bcc, List<string> cc, List<string> to)
        {
            Body = body;
            Subject = subject;
            From = @from;
            Bcc = bcc;
            Cc = cc;
            To = to;
        }
        #endregion

        #region private methods

        #endregion

        #region Public methods

        #endregion
    }
}


namespace SystemFrameWork.Email
{
    using System.Collections.Generic;
    public interface IEmail
    {

        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        List<string> To { get; }


        /// <summary>
        /// Gets the cc.
        /// </summary>
        /// <value>
        /// The cc.
        /// </value>
        List<string> Cc { get; }


        /// <summary>
        /// Gets the BCC.
        /// </summary>
        /// <value>
        /// The BCC.
        /// </value>
        List<string> Bcc { get; }


        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        string From { get; }


        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        string Subject { get; }


        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        string Body { get; }
    }
}
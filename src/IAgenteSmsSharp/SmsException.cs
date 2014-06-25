using System;

namespace IAgenteSmsSharp
{
    /// <summary>
    /// A Short Message Sevice exception.
    /// </summary>
    [Serializable]
    public sealed class SmsException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SmsException(string message)
            : base(message)
        {
        }
        #endregion
    }
}

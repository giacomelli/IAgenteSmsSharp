using System.Diagnostics;

namespace IAgenteSmsSharp
{
    /// <summary>
    /// Represents a message callback sent from IAgenteSMS servers.
    /// </summary>
    [DebuggerDisplay("{Text} ({ExternalId})")]
    public sealed class MessageCallback
    {
        #region Properties
        /// <summary>
        /// Gets the external identifier.
        /// </summary>   
        public int ExternalId { get; internal set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public MessageStatus Status { get; internal set; }

        /// <summary>
        /// Gets the sender.
        /// </summary> 
        public string Sender { get; internal set; }

        /// <summary>
        /// Gets the receiver shortcode.
        /// </summary>
        public string ReceiverShortcode { get; internal set; }

        /// <summary>
        /// Gets the text.
        /// </summary> 
        public string Text { get; internal set; }
        #endregion
    }
}

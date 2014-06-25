using System.Web;

namespace IAgenteSmsSharp
{
    /// <summary>
    /// Defines an interface for a SMS client (http://en.wikipedia.org/wiki/SMS).
    /// </summary>
    public interface ISmsClient
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendMessage(Message message);

        /// <summary>
        /// Gets the status of message with specified external id.
        /// </summary>
        /// <param name="externalId">The external id previous specified when message was sent.</param>
        /// <returns>The message status.</returns>
        MessageStatus GetMessageStatus(int externalId);

        /// <summary>
        /// Gets the message callback in the HTTP request specified.
        /// <remarks>
        /// This callback is used by IAgenteSMS server to notify about message update on their side.
        /// </remarks>
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>The message callback</returns>
        MessageCallback GetMessageCallback(HttpRequestBase request);
    }
}

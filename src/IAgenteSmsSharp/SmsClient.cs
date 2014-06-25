using System;
using System.Collections.Generic;
using System.Web;
using HelperSharp;
using RestSharp;

namespace IAgenteSmsSharp
{
    /// <summary>
    /// The Short Message Sevice client.
    /// <remarks>
    /// This is the library's main entry point.
    /// </remarks>
    /// </summary>
    public class SmsClient : ISmsClient
    {
        #region Fields
        private string m_baseUrl;
        private string m_userName;
        private string m_password;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsClient"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="userName">The username.</param>
        /// <param name="password">The password.</param>
        public SmsClient(string baseUrl, string userName, string password)
        {
            ExceptionHelper.ThrowIfNullOrEmpty("baseUrl", baseUrl);
            ExceptionHelper.ThrowIfNullOrEmpty("userName", userName);
            ExceptionHelper.ThrowIfNullOrEmpty("password", password);

            m_baseUrl = baseUrl;
            m_userName = userName;
            m_password = password;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentException">
        /// Message needs at least one receiver to be sent.;message
        /// or
        /// Message needs a text to be sent.;message
        /// </exception>
        /// <exception cref="IAgenteSmsSharp.SmsException"></exception>
        public void SendMessage(Message message)
        {
            ExceptionHelper.ThrowIfNull("message", message);

            var receiversCount = message.Receivers.Count;

            if (receiversCount == 0)
            {
                throw new ArgumentException("Message needs at least one receiver to be sent.", "message");
            }

            if (String.IsNullOrWhiteSpace(message.Text))
            {
                throw new ArgumentException("Message needs a text to be sent.", "message");
            }

            var parameters = new Dictionary<string, string>();
            parameters.Add("celular", String.Join(", ", message.Receivers));
            parameters.Add("mensagem", message.Text);

            if (message.ExternalId.HasValue)
            {
                parameters.Add("codigosms", message.ExternalId.Value.ToString());
            }

            if (message.SendAt.HasValue)
            {
                parameters.Add("data", message.SendAt.Value.ToString("dd/MM/yyyy HH:mm:ss"));
            }

            var methodName = receiversCount == 1 ? "envio" : "lote";

            var status = Request<MessageStatus>(methodName, parameters, new MessageStatusParser());

            if (status.Code.Equals("ERRO", StringComparison.OrdinalIgnoreCase))
            {
                throw new SmsException(status.Description);
            }
        }

        /// <summary>
        /// Gets the status of message with specified external id.
        /// </summary>
        /// <param name="externalId">The external id previous specified when message was sent.</param>
        /// <returns>
        /// The message status.
        /// </returns>
        public MessageStatus GetMessageStatus(int externalId)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("codigosms", externalId.ToString());

            return Request(
                "consulta",
                parameters,
                new MessageStatusParser());
        }

        /// <summary>
        /// Gets the message callback in the HTTP request specified.
        /// <remarks>
        /// This callback is used by IAgenteSMS server to notify about message update on their side.
        /// </remarks>
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>
        /// The message callback
        /// </returns>
        public MessageCallback GetMessageCallback(HttpRequestBase request)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Helpers
        private TResult Request<TResult>(string methodName, Dictionary<string, string> parameters, IParser<TResult> parser)
        {
            var client = new RestClient(m_baseUrl);
            var request = new RestRequest(Method.POST);
            request.AddParameter("metodo", methodName);
            request.AddParameter("usuario", m_userName);
            request.AddParameter("senha", m_password);

            foreach (var p in parameters)
            {
                request.AddParameter(p.Key, p.Value);
            }

            var response = client.Post(request);
            var result = parser.Parse(response.Content);

            return result;
        }
        #endregion
    }
}

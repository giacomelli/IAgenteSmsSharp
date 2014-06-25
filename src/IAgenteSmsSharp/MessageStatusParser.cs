using System;
using System.Text.RegularExpressions;
using HelperSharp;

namespace IAgenteSmsSharp
{
    /// <summary>
    /// A MessageStatus parser.
    /// </summary>
    internal class MessageStatusParser : IParser<MessageStatus>
    {
        #region Fields
        private static readonly Regex ParseRegex = new Regex(@"([a-z]+)(\s-\s)*(.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        #endregion

        #region Methods
        /// <summary>
        /// Parses the content in the result.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The cresult.</returns>
        public MessageStatus Parse(string content)
        {
            ExceptionHelper.ThrowIfNullOrEmpty("content", content);

            var result = new MessageStatus();
            var match = ParseRegex.Match(content);

            if (match.Success)
            {
                result.Code = match.Groups[1].Value.ToUpperInvariant();

                if (match.Groups.Count >= 3)
                {
                    result.Description = match.Groups[3].Value;
                }
            }
            else
            {
                throw new FormatException("The content '{0}' can't be parsed as a MessageStatus.".With(content));
            }

            return result;
        }
        #endregion
    }
}

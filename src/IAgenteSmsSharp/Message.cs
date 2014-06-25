using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IAgenteSmsSharp
{
    /// <summary>
    /// Represents a message.
    /// </summary>
    [DebuggerDisplay("{Text} ({ExternalId})")]
    public sealed class Message
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message()
        {
            Receivers = new List<string>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>  
        public int? ExternalId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when message must be sent.
        /// </summary>  
        public DateTime? SendAt { get; set; }

        /// <summary>
        /// Gets the receivers phone numbers.
        /// </summary>
        public IList<string> Receivers { get; private set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
        #endregion
    }
}

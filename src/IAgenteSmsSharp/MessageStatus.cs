using System.Diagnostics;

namespace IAgenteSmsSharp
{
    /// <summary>
    /// Represents a message status.
    /// </summary>
    [DebuggerDisplay("{Code}: {Description}")]
    public sealed class MessageStatus
    {
        #region Properties
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>   
        public string Description { get; set; }
        #endregion
    }
}

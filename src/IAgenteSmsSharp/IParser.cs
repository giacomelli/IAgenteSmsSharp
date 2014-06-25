namespace IAgenteSmsSharp
{
    /// <summary>
    /// Defines an interface for a parser.
    /// </summary>
    /// <typeparam name="TResult">The result.</typeparam>
    internal interface IParser<TResult>
    {
        /// <summary>
        /// Parses the content in the result.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The cresult.</returns>
        TResult Parse(string content);
    }
}

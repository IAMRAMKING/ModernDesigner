namespace ModernDesigner.Serialization
{
    public enum ReaderState
    {
        Initial,
        /// <summary>
        /// Start reading nodes
        /// </summary>
        StartElement,
        /// <summary>
        /// Start reading the node value
        /// </summary>
        Value,
        /// <summary>
        /// Read nodes to complete
        /// </summary>
        EndElement,
        /// <summary>
        /// Successfully arrived at the end。
        /// </summary>
        EOF,
        /// <summary>
        /// There will be an error，To prevent reading operations from continuing。
        /// </summary>
        Error
    }
}

using System;

namespace ModernDesigner
{
    /// <summary>
    /// Method of size adjustment
    /// </summary>
    [Flags]
    public enum ResizeType
    {
        /// <summary>
        /// Use the same width
        /// </summary>
        SameWidth = 1,

        /// <summary>
        /// Use the same height
        /// </summary>
        SameHeight = 2
    }
}

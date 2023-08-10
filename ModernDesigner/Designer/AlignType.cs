using System;

namespace ModernDesigner
{
    /// <summary>
    /// Alignment
    /// </summary>
    [Flags]
    public enum AlignType
    {
        /// <summary>
        /// Left -handed alignment
        /// </summary>
        Left = 1,
        /// <summary>
        /// Right alignment
        /// </summary>
        Right = 2,
        /// <summary>
        /// Horizontal
        /// </summary>
        Center = 4,
        /// <summary>
        /// Top alignment
        /// </summary>
        Top = 8,
        /// <summary>
        /// Vertical center
        /// </summary>
        Middle = 16,
        /// <summary>
        /// Bottom -end alignment
        /// </summary>
        Bottom = 32,
    }

}

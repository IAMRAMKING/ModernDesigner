using System;
using System.Drawing;

namespace ModernDesigner
{
    internal static class PointExtensions
    {
        /// <summary>
        /// Calculate the distance between the two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static int Distance(this Point p1, Point p2)
        {
            // Cyber ​​Clue theorem calculation
            int x = p1.X - p2.X;
            int y = p1.Y - p2.Y;
            return (int)Math.Sqrt(x * x + y * y);
        }
    }
}

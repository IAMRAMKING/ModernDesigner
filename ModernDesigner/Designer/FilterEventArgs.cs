using System;
using System.Collections;

namespace ModernDesigner
{
    public class FilterEventArgs : EventArgs
    {
        public IDictionary Data { get; set; }

        public bool Caching { get; set; }
    }

}

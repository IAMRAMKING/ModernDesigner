using System;
using System.Collections.Generic;

namespace ModernDesigner.Serialization
{
    public interface IReader : IDisposable
    {
        string Name { get; }
        string Value { get; }
        Dictionary<string, string> Attributes { get; }
        ReaderState State { get; }

        bool Read();

    }
}

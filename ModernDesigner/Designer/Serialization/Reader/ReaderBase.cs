using System;
using System.Collections;
using System.Collections.Generic;

namespace ModernDesigner.Serialization
{
    public abstract class ReaderBase<T> : IReader where T : IDisposable
    {
        protected T reader;

        #region IReader Interface member

        public string Name { get; protected set; } = string.Empty;
        public string Value { get; protected set; } = string.Empty;
        public Dictionary<string, string> Attributes { get; protected set; } = new Dictionary<string, string>();
        public ReaderState State { get; protected set; } = ReaderState.Initial;

        public abstract bool Read();

        #endregion

        #region IDisposable Interface member

        public virtual void Dispose()
        {
            reader?.Dispose();
        }

        #endregion

    }
}

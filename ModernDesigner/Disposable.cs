using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesigner
{
    public abstract class Disposable : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: Release the hosting status (hosting object)
                }

                // TODO: Release unmanaged resources (unmanaged objects) and override finalizers
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: Only override finalizers if "Dispose(bool disposing)" has code to release unmanaged resources
        // ~Disposable()
        // {
        // // Do not change this code. Please put the cleanup code in the "Dispose(bool disposing)" method
        // Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Please put the cleanup code in the "Dispose(bool disposing)" method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

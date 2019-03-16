using System;
using System.Windows.Input;

namespace MvvmExtensions
{
    /// <summary>
    /// Changes cursor state for current application. Cursor automatically resets back to default when instance of this class is disposed
    /// </summary>
    public class CursorState : IDisposable
    {
        bool disposed = false;

        /// <summary>
        /// Override cursor state
        /// </summary>
        /// <param name="cursor"></param>
        public CursorState(Cursor cursor)
        {
            Mouse.OverrideCursor = cursor;
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Mouse.OverrideCursor = null;
            }

            disposed = true;
        }
    }
}

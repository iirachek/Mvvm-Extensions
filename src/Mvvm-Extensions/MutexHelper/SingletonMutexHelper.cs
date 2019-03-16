using System.Threading;

namespace MvvmExtensions.MutexHelper
{
    /// <summary>
    /// Intended for quick and easy usage at the application startup to determine if a second instance of an app is launched
    /// </summary>
    class SingletonMutexManager
    {
        private static SingletonMutexManager instance;
        private Mutex mutex;

        private SingletonMutexManager()
        {
        }

        public static SingletonMutexManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SingletonMutexManager();
                
                return instance;
            }
        }

        /// <summary>
        /// Name of the mutex
        /// </summary>
        public static string MutexName { get; set; }

        /// <summary>
        /// Attempt to claim mutex with specified <see cref="MutexName"/>
        /// </summary>
        /// <param name="timeoutMilliseconds">Mutex claim timeout</param>
        /// <returns>Returns true if mutex was successfully claimed by application</returns>
        public bool TryClaimMutex(int timeoutMilliseconds)
        {
            mutex = new Mutex(false, MutexName);
            bool hasHandle = mutex.WaitOne(timeoutMilliseconds, false);
            return hasHandle;
        }

        /// <summary>
        /// Release claimed mutex
        /// </summary>
        public void ReleaseMutex()
        {
            mutex?.ReleaseMutex();
            mutex?.Dispose();
            mutex = null;
        }
    }
}

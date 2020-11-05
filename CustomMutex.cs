using System;
using System.Threading;

namespace SyncPrimitives
{
    public class CustomMutex
    {
        private Semaphore _semaphore;
        public CustomMutex()
        {
            _semaphore = new Semaphore(1, 1);
        }

        public void Lock()
        {
            _semaphore.WaitOne();
        }

        public void Unlock()
        {
            try
            {
                _semaphore.Release();
            }
            catch (SemaphoreFullException e)
            {
                Console.WriteLine("An exception was thrown: ", e.Message);
            }
        }

        public void WriteToConsole()
        {
            Console.WriteLine(_semaphore.ToString());
        }

        public void Dispose() 
        {
            _semaphore.Close(); 
        }
    }
}

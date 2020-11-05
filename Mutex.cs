using System;
using System.Threading;

namespace SyncPrimitives
{
    class Mutex
    {
        private int threadID = 0;

        public void Lock()
        {
            if (Interlocked.CompareExchange(ref threadID, Thread.CurrentThread.ManagedThreadId, 0) != 0)
            {
                Thread.Sleep(50);
            }
        }

        public void Unlock()
        {
            if (Interlocked.CompareExchange(ref threadID, 0, Thread.CurrentThread.ManagedThreadId) != Thread.CurrentThread.ManagedThreadId)
                throw new Exception($"Error: the thread {Thread.CurrentThread.ManagedThreadId} cannot release mutex due to its lack");
        }
    }
}

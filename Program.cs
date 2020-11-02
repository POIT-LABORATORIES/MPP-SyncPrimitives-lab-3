using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncPrimitives
{
    class Program
    {
        public static int res = 0;
        public static CustomMutex mut = new CustomMutex();
        static void Main(string[] args)
        {
            int num1 = 10;
            int num2 = 10;
            Interlocked.CompareExchange(ref num1, 15, num2);
            Console.WriteLine(num1);

            var task1 = Task.Run(() => IncRes(5));
            var task2 = Task.Run(() => DecRes(5));
            Thread.Sleep(1000);
            mut.Dispose();
        }

        static void IncRes(int num)
        {
            Thread.Sleep(1000);
            mut.Lock();
            Console.WriteLine("Increment");
            for (int i = 0; i < num; i++)
            {
                Interlocked.Increment(ref res);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: res = {res}");
            }
            mut.Unlock();
            return;
        }

        static void DecRes(int num)
        {
            Thread.Sleep(1000);
            mut.Lock();
            Console.WriteLine("Decrement");
            for (int i = 0; i < num; i++)
            {
                Interlocked.Decrement(ref res);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: res = {res}");
            }
            mut.Unlock();
            return;
        }
    }
}

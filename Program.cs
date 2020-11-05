using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncPrimitives
{
    class Program
    {
        public static int res = 0;
        public static Mutex mut = new Mutex();
        static void Main(string[] args)
        {
            var task1 = Task.Run(() => IncRes(5));
            var task2 = Task.Run(() => DecRes(5));
            try
            {
                var task3 = Task.Run(() => WrongThread(5));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Thread.Sleep(1000);
        }

        static void IncRes(int num)
        {
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
            Thread.Sleep(100);
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
        static void WrongThread(int num)
        {
            try
            {
                mut.Unlock();
                for (int i = 0; i < num; i++)
                {
                    Interlocked.Decrement(ref res);
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: res = {res}");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return;
        }
    }
}

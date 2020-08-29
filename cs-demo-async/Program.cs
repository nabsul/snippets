using System;
using System.Threading.Tasks;

namespace TestAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Method1().Wait();
            Console.WriteLine();
            
            Method2().Wait();
            Console.WriteLine();
            
            Method3().Wait();
            Console.WriteLine();
        }

        static async Task Method1()
        {
            Console.WriteLine("Starting method 1");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("Starting method 1 - await 1 completed");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("Starting method 1 - await 2 completed");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("Starting method 1 - await 3 completed");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("Starting method 1 - await 4 completed");
        }

        static Task Method2()
        {
            Console.WriteLine("Starting method 2");
            var task1 = Task.Delay(TimeSpan.FromSeconds(1));

            var task2 = task1.ContinueWith((o) =>
            {
                Console.WriteLine("Starting method 2 - await 1 completed");
                return Task.Delay(TimeSpan.FromSeconds(1));
            });

            var task3 = task2.Unwrap().ContinueWith((o) =>
            {
                Console.WriteLine("Starting method 2 - await 2 completed");
                return Task.Delay(TimeSpan.FromSeconds(1));
            });

            var task4 = task3.Unwrap().ContinueWith((o) =>
            {
                Console.WriteLine("Starting method 2 - await 3 completed");
                return Task.Delay(TimeSpan.FromSeconds(1));
            });

            var task5 = task4.Unwrap().ContinueWith((o) =>
            {
                Console.WriteLine("Starting method 2 - await 4 completed");
                return Task.Delay(TimeSpan.FromSeconds(1));
            });

            return task5.Unwrap();
        }

        static Task Method3()
        {
            Console.WriteLine("Starting method 3");
            return Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith((o) =>
                {
                    Console.WriteLine("Starting method 3 - await 1 completed");
                    return Task.Delay(TimeSpan.FromSeconds(1));
                }).Unwrap()
                .ContinueWith((o) =>
                {
                    Console.WriteLine("Starting method 3 - await 3 completed");
                    return Task.Delay(TimeSpan.FromSeconds(1));
                }).Unwrap()
                .ContinueWith((o) =>
                {
                    Console.WriteLine("Starting method 3 - await 4 completed");
                    return Task.Delay(TimeSpan.FromSeconds(1));
                }).Unwrap();
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Початок програми.");
        using (var cancellationTokenSource = new CancellationTokenSource())
        {
            var timerTask = StartAsyncTimer(cancellationTokenSource.Token);
            Console.WriteLine("Для завершення програми введiть 'exit' i натиснiть Enter.");
            while (true)
            {
                var input = Console.ReadLine();
                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Введено 'exit'. Завершення програми.");
                    cancellationTokenSource.Cancel();
                    break;
                }
            }
            await timerTask;
        }
    }
    static async Task StartAsyncTimer(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"Повiдомлення: {DateTime.Now}");
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}

// Update Date : 2025-09-26
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static async Task<string> TaskAsync(int taskId)
    {
        Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }], Task[{ taskId }] : TaskAsync Started");

        await Task.Delay(1000 + new Random().Next(1, 10) * 500);

        Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }], Task[{ taskId }] : TaskAsync Finished");

        return $"Task[{ taskId }] : Done";
    }

    static async Task Main()
    {
        Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId } : Main Started");

        List<Task<string>> tasks = new List<Task<string>>();

        for (int i = 0; i < 10; i++)
        {
            tasks.Add(TaskAsync(i));
        }

        while (tasks.Count > 0)
        {
            // 가장 먼저 완료된 Task를 반환한다.
            var completedTask = await Task.WhenAny(tasks);

            // 결과를 가져와 출력한다.
            Console.WriteLine($"Main : { await completedTask }");

            // 작업이 완료된 Task를 제거한다.
            tasks.Remove(completedTask);
        }

        Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId } : Main Done");
    }
}

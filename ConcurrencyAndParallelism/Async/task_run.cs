// Update Date : 2025-09-27
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static string TaskAsync(int taskId)
    {
        Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }], Task[{ taskId }] : 작업 시작");

        // Do Something
        Thread.Sleep(1000 + taskId * 500);

        Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }], Task[{ taskId }] : 작업 완료");

        return $"Task[{ taskId }] Done";
    }

    static async Task Main()
    {
        List<Task<string>> tasks = new List<Task<string>>();

        for (int i = 0; i < 10; i++)
        {
            // TaskAsync()에 전달할 목적으로 값을 일단 캡처한다.
            int temp = i;

            // Task.Run()은 내부적으로 ThreadPool을 사용한다.
            // ThreadPool에 Task 기반 비동기 프로그래밍(TAP) 방식이 결합된 형태라고 보면 된다.
            var task = Task.Run(() => TaskAsync(temp));

            tasks.Add(task);
        }

        var results = await Task.WhenAll(tasks);

        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
}

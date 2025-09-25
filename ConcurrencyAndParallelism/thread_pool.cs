// Update Date : 2025-09-26
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static void ThreadMain(object? state)
    {
        int taskId = (int)state!;

        // 스레드 풀을 사용하면 같은 스레드 아이디가 나오는 것을 볼 수 있을 것이다.
        // 즉, 스레드를 재사용하고 있다는 의미이다.
        Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }], Task[{ taskId }] : 작업 시작");

        // Do Something;
        Thread.Sleep(2000);

        Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }], Task[{ taskId }] : 작업 완료");
    }

    static void Main()
    {
        // 스레드를 사용할 때마다 매번 생성하고 해제하는 건 비용이 크다.
        // 반복적인 작업을 짧게 여러 번 진행할 생각이라면 스레드 재사용을 위한 스레드 풀을 사용하는 것이 좋다.
        //
        // 직접 스레드를 생성하는 방식은 개발자가 수동으로 관리해야 하지만,
        // 스레드 풀을 사용하면 .NET 차원에서 스레드를 관리해준다.
        for (int i = 0; i < 10; i++)
        {
            int taskId = i;

            // ThreadPool.QueueUserWorkItem(ThreadMain, taskId);
            ThreadPool.QueueUserWorkItem(state => ThreadMain(taskId));

            Thread.Sleep(1000);
        }

        // 모든 작업이 완료될 때까지 대기한다.
        Thread.Sleep(3000);
    }
}

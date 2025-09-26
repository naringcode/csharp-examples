// Update Date : 2025-09-26
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static async Task TaskAsync1()
    {
        Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId } : TaskAsync1 Started");

        // Task.Delay()가 반환하는 것도 Task이다(Task이기 때문에 await 적용 가능).
        await Task.Delay(1500);

        Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId } : TaskAsync1 Finished");
    }

    static async Task TaskAsync2()
    {
        Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId } : TaskAsync2 Started");

        // Task.Delay()가 반환하는 것도 Task이다(Task이기 때문에 await 적용 가능).
        await Task.Delay(1500);

        Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId } : TaskAsync2 Finished");
    }

    static async Task Main()
    {
        // await 후 실행을 재개할 때는 스레드 풀에서 임의의 스레드를 꺼내서 사용한다.
        // Task 완료 후 코드를 재개할 때 스레드가 반드시 원래 스레드일 필요는 없다.
        Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId } : Main Started");

        // 아래 방식은 비동기 작업으로 수행된다 해도 결과적으로 동기 방식과 동일하다.
        // await TaskAsync1();
        // await TaskAsync2();

        // 올바로 작성한 비동기 코드는 이러하다(병렬식 구조).
        Task task1 = TaskAsync1();
        Task task2 = TaskAsync2();

        // Task.WhenAll()은 지정한 모든 Task가 완료될 때까지 기다리기 위한 메서드이다.
        // Task.WhenAll() 또한 Task를 반환한다(Task이기 때문에 await 적용 가능).
        await Task.WhenAll(task1, task2);

        Console.WriteLine($"{ Thread.CurrentThread.ManagedThreadId } : Main Done");
    }
}

// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static void ThreadMain()
    {       
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }] : { i }");

            Thread.Sleep(100); // 100ms
        }

        Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }] : Done");
    }

    static void Main()
    {
        // 현재 스레드의 ID를 출력한다.
        Console.WriteLine($"Main[{ Thread.CurrentThread.ManagedThreadId }]");

        // 스레드 생성
        Thread thread1 = new Thread(new ThreadStart(ThreadMain)); // 초기 스레드 생성 방식(명시적)
        Thread thread2 = new Thread(ThreadMain); // 컴파일러가 delegate 형식을 추론하는 방식(암시적)

        // 스레드 상태 확인(False)
        Console.WriteLine($"Thread 1 : { thread1.IsAlive} ");
        Console.WriteLine($"Thread 2 : { thread2.IsAlive} ");

        Console.WriteLine("Start");

        // 스레드를 실행하려면 명시적으로 Start()를 호출해야 한다.
        thread1.Start();
        thread2.Start();

        // 스레드 상태 확인(True)
        Console.WriteLine($"Thread 1 : { thread1.IsAlive } ");
        Console.WriteLine($"Thread 2 : { thread2.IsAlive } ");

        // 스레드 작업이 완료될 때까지 대기한다.
        thread1.Join();
        thread2.Join();

        Console.WriteLine($"Main[{ Thread.CurrentThread.ManagedThreadId }] : Done");

        // 스레드 상태 확인(False)
        Console.WriteLine($"Thread 1 : { thread1.IsAlive } ");
        Console.WriteLine($"Thread 2 : { thread2.IsAlive } ");
    }
}

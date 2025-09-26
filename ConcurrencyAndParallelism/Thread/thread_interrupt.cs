// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static void ThreadMain()
    {       
        try
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }] : { i }");

                Thread.Sleep(1000); // 1000ms
            }
        }
        catch (ThreadInterruptedException e) // 스레드가 강제 종료되면 ThreadInterruptedException 예외를 던짐.
        {
            Console.WriteLine($"예외 발생 : { e.Message }");
        }
        finally
        {
            Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }] : Done");
        }
    }

    static void Main()
    {
        // 스레드 생성
        Thread thread1 = new Thread(ThreadMain);

        Console.WriteLine("Start");

        // 스레드를 실행하려면 명시적으로 Start()를 호출해야 한다.
        thread1.Start();

        // 아무 키나 입력할 때까지 대기한다.
        Console.ReadKey();

        if (thread1.IsAlive == true)
        {
            // 스레드를 강제 종료한다.
            thread1.Interrupt();
        }

        // 스레드 작업이 완료될 때까지 대기한다.
        thread1.Join();

        Console.WriteLine($"Main[{ Thread.CurrentThread.ManagedThreadId }] : Done");
    }
}

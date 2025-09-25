// Update Date : 2025-09-26
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    // Semaphore 계열을 사용하면 동시에 임계 영역으로 접근 가능할 수 있는 스레드의 개수를 제한할 수 있다.
    // SemaphoreSlim은 동일 프로세스 내에서만 사용 가능하고, Semaphore는 Mutex와 동일하게 다른 프로세스와 연계해서 사용할 수 있다.
    // static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2); // 동시 접근 가능한 스레드의 초기 개수를 2개로 제한
    static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2, 6); // 동시 접근 가능한 스레드의 초기 개수는 2개, 최대 개수는 6개로 제한

    // Semaphore는 동시 작업에 제한을 둘 때 주로 사용한다(시스템 과부하 방지 용도).
    // - 파일입출력 제한
    // - 네트워크 통신 제한

    static void ThreadMain(object? threadId)
    {
        Console.WriteLine($"Thread[{ threadId }] : 공유 자원 접근 시도 중");

        // 임계 영역에 진입하기 전 잠금 자원 획득하기
        semaphoreSlim.Wait();

        try
        {
            Console.WriteLine($"Thread[{ threadId }] : 공유 자원 획득");

            // Do Something(파일 쓰기, DB 작업 등)
            Thread.Sleep(2000);
        }
        finally
        {
            Console.WriteLine($"Thread[{ threadId }] : 공유 자원 해제");

            // 사용이 끝났으면 반드시 Release()를 호출해야 한다.
            semaphoreSlim.Release();
        }
    }

    static void Main()
    {
        List<Thread?> threads = new List<Thread?>();

        for (int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(ThreadMain);

            threads.Add(thread);
            thread.Start(i);
        }

        // 완료 대기
        threads.ForEach(th => th?.Join());

        Console.WriteLine("Main Done");

        Console.WriteLine("--------------------------------------------------");

        threads.Clear();

        // 최대 개수는 6개까지 허용하게 했으니 Release()를 진행하여 진입 가능한 스레드의 개수를 늘릴 수 있다.
        semaphoreSlim.Release(); // 1개 늘리기
        semaphoreSlim.Release(2); // 2개 늘리기
        
        for (int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(ThreadMain);

            threads.Add(thread);
            thread.Start(i);
        }

        // 완료 대기(semaphoreSlim에 진입 가능한 스레드의 개수는 현재 5개임)
        threads.ForEach(th => th?.Join());

        Console.WriteLine("Main Done");
    }
}

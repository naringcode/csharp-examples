// Update Date : 2025-01-02
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any, Release-Any

class Program   
{
    static int sharedNumA = 0;
    static volatile int sharedNumB = 0;
    static int sharedNumC = 0;

    static void Main()
    {
        Task task1 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                sharedNumA++;
            }
        });

        Task task2 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                sharedNumA--;
            }
        });

        Task.WaitAll(task1, task2);

        // 일반 변수는 원자성을 보장하지 않는다.
        Console.WriteLine("int sharedNumA : {0}", sharedNumA);

        Console.WriteLine("--------------------------------------------------");

        task1 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                sharedNumB++;
            }
        });

        task2 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                sharedNumB--;
            }
        });

        Task.WaitAll(task1, task2);

        // C#에서의 volatile은 단순히 캐시가 아닌 메모리에서 최신 값을 읽는 것을 보장하기 위한 키워드이다.
        // 마찬가지로 가시성 및 원자성을 보장하지는 않는다.
        Console.WriteLine("volatile int sharedNumB : {0}", sharedNumB);

        Console.WriteLine("--------------------------------------------------");
        
        task1 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                // Interlocked.Add(ref sharedNumC, 1);
                Interlocked.Increment(ref sharedNumC);
            }
        });

        task2 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                // Interlocked.Add(ref sharedNumC, -1);
                Interlocked.Decrement(ref sharedNumC);
            }
        });

        Task.WaitAll(task1, task2);

        // C#에서 원자적인 연산을 사용하려면 Interlocked 클래스의 도움을 받거나 임계 영역을 보호하기 위한 도구를 써야 한다.
        // C++과 같은 atomic<T> 타입은 지원하지 않는다.
        Console.WriteLine("int sharedNumC with Interlocked : {0}", sharedNumC);
    }
}

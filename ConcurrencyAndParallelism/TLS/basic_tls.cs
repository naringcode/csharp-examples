// Update Date : 2025-01-03
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any, Release-Any

class Program
{
    // C#에서 스레드 고유의 영역인 TLS는 ThreadLocal<T>를 통해 사용할 수 있다.

    // static ThreadLocal<int> tls_Cnt = new ThreadLocal<int>();
    static ThreadLocal<int> tls_Cnt = new ThreadLocal<int>(() => 0); // 초깃값은 0으로 함.

    // static ThreadLocal<string> tls_Name = new ThreadLocal<string>();
    static ThreadLocal<string> tls_Name = new ThreadLocal<string>(() => $"hello, thread { Thread.CurrentThread.ManagedThreadId }");

    static void ThreadMain()
    {
        for (int i = 0; i < 1_000_000; i++)
        {
            tls_Cnt.Value++;

            Thread.Sleep(0);
        }
        
        // TLS 변수가 사용되지 않은 상태라면 IsValueCreated는 false이다.
        // 단 한 번이라도 사용했으면 해당 값은 true로 변한다(tls_Cnt는 위에서 증감 작업을 진행하기에 해당 값이 true임).
        if (tls_Name.IsValueCreated == false)
        {
            // tls_Name을 최초로 사용한 상황
            Console.WriteLine($"first time, tls_Name : { tls_Name }, tls_Cnt : { tls_Cnt }");
        }
        else
        {
            // tls_Name을 다시 재사용한 상황
            Console.WriteLine($"tls reuse, tls_Name : { tls_Name }, tls_Cnt : { tls_Cnt }");
        }
    }

    static void Run01()
    {
        // 스레드를 매번 할당하는 정책이라면 TLS의 값을 초기화하여 사용한다.
        for (int i = 0; i < 8; i++)
        {
            List<Thread> threads = new List<Thread>();

            for (int j = 0; j < Math.Max(Environment.ProcessorCount / 2, 2); j++)
            {
                var thread = new Thread(ThreadMain);
                thread.Start();

                threads.Add(thread);
            }

            threads.ForEach((th) => th.Join());

            Console.WriteLine($"Done : { i }");
        }
    }

    static void Run02()
    {
        // 스레드를 재사용하는 정책을 사용하면 TLS 값도 공유된다.
        for (int i = 0; i < 8; i++)
        {
            // Parallel은 내부에서 Task와 ThreadPool을 사용한다.
            // 또한 Parallel은 내부에서 여러 개의 Task를 가져와 병렬로 일감을 수행한다.
            // 즉, 각 Task는 재사용하는 특성이 있기 때문에 이전 TLS 영역을 공유해서 사용한다.
            Parallel.For(0, Math.Max(Environment.ProcessorCount / 2, 2), i => ThreadMain()); // 결과가 합산되어 나올 것임.

            // Parallel.For(), Parallel.ForEach(), Parallel.Invoke()는 동기적으로 동작한다.
            // 쉽게 말해서 모든 병렬 작업이 끝날 때까지 메서드를 반환하지 않는다.
            Console.WriteLine($"Done : { i }");
        }
    }

    static void Main()
    {
        Run01();

        Console.WriteLine("--------------------------------------------------");

        Run02();
    }
}

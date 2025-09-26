// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static int data = 0;

    // lock을 위한 잠금 객체
    // 잠금 객체는 외부에서 수정할 수 없게 readonly 한정자를 붙이는 것이 좋다.
    static readonly object locker = new object();

    static void ThreadMain()
    {
        for (int i = 0; i < 100; i++)
        {
            data++;

            Thread.Sleep(1);
        }
    }

    static void ThreadMonitorMain()
    {
        for (int i = 0; i < 10; i++)
        {
            // 임계 영역을 보호하기 위한 방법 중 하나는 Monitor를 쓰는 것이다.
            // 마찬가지로 잠금 객체를 요구한다.
            Monitor.Enter(locker);
            {
                data++;

                Thread.Sleep(1);
            }
            Monitor.Exit(locker);

            // 정석 코드
            // Monitor.Enter(locker);
            // try
            // {
            //     data++;
            // 
            //     Thread.Sleep(1);
            // }
            // finally
            // {
            //     Monitor.Exit(locker);
            // }
        }
    }

    static void ThreadMonitorTryMain()
    {
        // 간단하게 임계 영역을 보호하고자 한다면 lock을, 세밀하게 임계 영역을 보호하고자 한다면 Monitor를 쓰면 된다.
        // 아래 예시는 일정 시간 동안 Lock에 대한 자원을 획득하기 위해 시도하는 과정을 보여주고 있다.
        if (Monitor.TryEnter(locker, 1500)) // 1500ms가 지날 때까지 자원 획득 시도
        {
            try
            {
                Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }] : 잠금 자원 획득");

                // Do Something
                Thread.Sleep(1000); // 1초 동안 다른 작업
            }
            finally
            {
                Monitor.Exit(locker);
                
                Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }] : 잠금 자원 해제");
            }
        }
        else
        {
            Console.WriteLine($"Thread[{ Thread.CurrentThread.ManagedThreadId }] : 잠금 자원 획득 실패");
        }
    }

    static void Main()
    {
        List<Thread> threads = new List<Thread>();

        for (int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(ThreadMain);

            threads.Add(thread);
            thread.Start();
        }

        threads.ForEach(th => th.Join());

        // 기대하는 값은 1000이지만 임계 영역에 따른 레이스 컨디션에 의해서 1000보다 작은 값이 출력된다.
        // 임계 영역 : 여러 스레드가 동시에 접근하여 문제가 생길 수 있는 코드 블록
        Console.WriteLine($"data : { data }");

        Console.WriteLine("--------------------------------------------------");

        data = 0;
        threads.Clear();
        
        for (int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(ThreadMonitorMain);

            threads.Add(thread);
            thread.Start();
        }

        threads.ForEach(th => th.Join());

        // 임계 영역을 보호하면 우리가 원하는 값인 1000을 출력할 수 있다.
        Console.WriteLine($"data : { data }");

        Console.WriteLine("--------------------------------------------------");

        threads.Clear();

        for (int i = 0; i < 5; i++)
        {
            Thread thread = new Thread(ThreadMonitorTryMain);

            threads.Add(thread);
            thread.Start();
        }

        // 2개의 스레드만 잠금 자원을 획득할 것이다.
        threads.ForEach(th => th.Join());
    }
}

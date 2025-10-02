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

    static void ThreadLockMain()
    {
        for (int i = 0; i < 100; i++)
        {
            // 임계 영역을 보호하는 방법 중 하나는 lock을 쓰는 것이다.
            // lock은 잠금 객체를 요구한다.
            lock (locker) // lock은 일종의 구문 설탕(syntactic sugar)으로 실제로는 Monitor를 사용함.
            {
                data++;
            }

            Thread.Sleep(1);
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
            Thread thread = new Thread(ThreadLockMain);

            threads.Add(thread);
            thread.Start();
        }

        threads.ForEach(th => th.Join());

        // 임계 영역을 보호하면 우리가 원하는 값인 1000을 출력할 수 있다.
        Console.WriteLine($"data : { data }");
    }
}

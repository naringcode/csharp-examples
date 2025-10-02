// Update Date : 2025-01-02
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any, Release-Any

using System.Globalization;

class Program   
{
    class AutoEventLock
    {
        // 시그널 상태로 동작하는 커널 수준의 자료형
        AutoResetEvent _event = new AutoResetEvent(true); // 초깃값은 initial state임.

        public void Acquire() // Enter
        {
            // 시그널 상태가 true가 될 때까지 대기한다.
            // 또한 AutoResetEvent이기에 반응한 쪽에서 시그널 상태를 false로 되돌리는 작업까지 수행한다.
            _event.WaitOne();
        }

        public void Release()
        {
            // 시그널 상태를 true로 만든다.
            _event.Set();
        }
    }

    class ManualEventLock
    {
        // 시그널 상태로 동작하는 커널 수준의 자료형
        ManualResetEvent _event = new ManualResetEvent(true); // 초깃값은 initial state임.

        public void Acquire() // Enter
        {
            // 시그널 상태가 true가 될 때까지 대기한다.
            // ManualResetEvent는 시그널 상태를 false로 되돌리는 작업을 진행하지는 않는다.
            _event.WaitOne();

            // !!! 이 사이는 WaitOne()과 Reset() 사이의 시그널 상태는 true이기 때문에 해당 영역은 스레드 안전하지 않음. !!!

            // ManualResetEvent는 사용자가 직접 시그널 상태를 false로 되돌리는 작업을 수행해야 한다.
            _event.Reset();
        }

        public void Release()
        {
            // 시그널 상태를 true로 만든다.
            _event.Set();
        }
    }

    static int sharedNumA = 0;
    static int sharedNumB = 0;

    static void Main()
    {
        AutoEventLock autoEventLock = new AutoEventLock();

        Task task1 = Task.Run(() => {
            for (int i = 0; i < 100_000; i++)
            {
                autoEventLock.Acquire();

                sharedNumA++;

                autoEventLock.Release();
            }
        });

        Task task2 = Task.Run(() => {
            for (int i = 0; i < 100_000; i++)
            {
                autoEventLock.Acquire();

                sharedNumA--;

                autoEventLock.Release();
            }
        });

        Task.WaitAll(task1, task2);

        // AutoResetEvent는 해당 객체 차원에서 시그널 상태를 복원하는 작업도 진행하기에 정상적으로 0이 나온다.
        Console.WriteLine($"sharedNumA with event : { sharedNumA }");

        Console.WriteLine("--------------------------------------------------");
        
        ManualEventLock manualEventLock = new ManualEventLock();

        task1 = Task.Run(() => {
            for (int i = 0; i < 100_000; i++)
            {
                manualEventLock.Acquire();

                sharedNumB++;

                manualEventLock.Release();
            }
        });

        task2 = Task.Run(() => {
            for (int i = 0; i < 100_000; i++)
            {
                manualEventLock.Acquire();

                sharedNumB--;

                manualEventLock.Release();
            }
        });

        Task.WaitAll(task1, task2);

        // ManualResetEvent는 사용자가 직접 시그널 상태를 복원해야 하기에 WaitOne()과 Reset() 사이의 임계 영역이 다른 스레드와 공유된다.
        // 따라서 0이 아닌 값이 나온다.
        Console.WriteLine($"sharedNumB with event : { sharedNumB }");

        Console.WriteLine("--------------------------------------------------");

        ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        task1 = Task.Run(() => {
            manualResetEvent.WaitOne();

            Console.WriteLine("Here is task1");
        });

        task2 = Task.Run(() => {
            manualResetEvent.WaitOne();

            Console.WriteLine("Here is task2");
        });

        Task task3 = Task.Run(() => {
            manualResetEvent.WaitOne();

            Console.WriteLine("Here is task3");
        });

        Task waitTask = Task.Run(() => {
            int waitSec = 3;

            while (waitSec > 0)
            {
                Console.WriteLine($"Wait { waitSec } second(s)...");

                Thread.Sleep(1000);

                waitSec--;
            }

            // ManualResetEvent.WaitOne()으로 대기하고 있는 대상 전부가 한 번에 깨어난다.
            manualResetEvent.Set();
        });

        Task.WaitAll(task1, task2, task3, waitTask);

        Console.WriteLine("Done!");
    }
}

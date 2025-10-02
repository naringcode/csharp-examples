// Update Date : 2025-01-02
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any, Release-Any

using System.Diagnostics;

// Debug 모드로 테스트하면 별로 차이가 나지 않는데 이유는 아직 파악이 안 된 상태이다.
// 하지만 Release 모드로 테스트해보면 성능 차이가 확연히 드러난다.
class Program   
{
    // 일반적인 lock 계열의 기능을 사용하면 Read를 진행할 때도 상호배타적으로 동작한다.
    // 쓰기 작업은 상호배타적으로 동작해야 하지만, 읽기 작업끼리는 상호배타적으로 동작할 필요가 없기에 이는 자원 낭비라 볼 수 있다.
    static void Run01()
    {
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        List<Task> tasks = new List<Task>();
        List<int> itemList = new List<int>();

        object locked = new object();

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            Task readTask = Task.Run(() => {
                manualResetEvent.WaitOne();

                for (int i = 0; i < 200_000; i++)
                {
                    int[] tempArr;

                    // Read
                    lock (locked)
                    {
                        tempArr = itemList.ToArray();
                    }

                    foreach (var elem in tempArr)
                    {
                        // Do Something...
                    }

                    // Write
                    if (i % 100 == 0)
                    {
                        lock (locked)
                        {
                            itemList.Add(i);
                        }
                    }
                }

                Console.WriteLine("task done");
            });

            tasks.Add(readTask);
        }

        // Go
        manualResetEvent.Set();

        Task.WaitAll(tasks.ToArray());

        stopwatch.Stop();

        Console.WriteLine($"elapsed : { stopwatch.ElapsedMilliseconds }");
    }

    // 쓰기 작업만 상호배타적으로 작업하고, 읽기 작업은 공유하는 방식을 택하여 작성한 코드
    static void Run02()
    {
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        List<Task> tasks = new List<Task>();
        List<int> itemList = new List<int>();

        ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            Task task = Task.Run(() => {
                manualResetEvent.WaitOne();

                for (int i = 0; i < 200_000; i++)
                {
                    int[] tempArr;

                    // Read
                    rwLock.EnterReadLock();
                    {
                        tempArr = itemList.ToArray();
                    }
                    rwLock.ExitReadLock();

                    foreach (var elem in tempArr)
                    {
                        // Do Something...
                    }

                    // Write
                    if (i % 100 == 0)
                    {
                        rwLock.EnterWriteLock();
                        {
                            itemList.Add(i);
                        }
                        rwLock.ExitWriteLock();
                    }
                }

                Console.WriteLine("task done");
            });

            tasks.Add(task);
        }

        // Go
        manualResetEvent.Set();

        Task.WaitAll(tasks.ToArray());

        stopwatch.Stop();

        Console.WriteLine($"elapsed : { stopwatch.ElapsedMilliseconds }");
    }

    static void Main()
    {
        Run01();

        Console.WriteLine("--------------------------------------------------");

        Run02();
    }
}

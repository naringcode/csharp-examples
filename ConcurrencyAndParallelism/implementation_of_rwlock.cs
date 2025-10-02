// Update Date : 2025-01-03
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any, Release-Any

using System.Diagnostics;

// 32비트 자료형을 상위 16비트와 하위 16비트로 나눠서 사용할 것이다.
// 
// [XWWWWWWW][WWWWWWWW][RRRRRRRR][RRRRRRRR]
// X : UnusedFlag
// W : WriteFlag(Exclusive Lock Owner ThreadId) // 락을 현재 획득하고 있는 스레드의 ID
// R : ReadFlag(Shared Lock Count) // 락을 공유해서 사용하고 있는 카운트

// 동일 스레드의 Lock 정책
// W -> W (O)
// W -> R (O)
// R -> W (X)

// 멀티 스레드의 Lock 정책
// R -> R (O)
// W -> W (X)
// R -> W (X)
// W -> R (X)
class RWLock
{
    const int kEmptyFlag = 0;
    const int kWriteThreadMask = 0x7FFF_0000;
    const int kReadThreadMask  = 0x0000_FFFF;

    const int kMaxSpinCnt = 5000;

    int _lockFlag = kEmptyFlag;
    int _writeCnt = 0; // 동일한 스레드가 락을 재귀적으로 잡는 상황을 대비한 카운터

    // Exclusive
    public void WriteLock()
    {
        int desired = (Thread.CurrentThread.ManagedThreadId << 16) & kWriteThreadMask;

        // 이미 내가 lock을 잡고 있는 상태에서 재진입한 상황
        if (_lockFlag == desired)
        {
            // W -> W
            _writeCnt++;

            return;
        }

        // Lock에 대한 경합 시작
        while (true)
        {
            for (int i = 0; i < kMaxSpinCnt; i++)
            {
                // 아무도 소유 및 공유하고 있지 않아야 경합에 성공한다(lock의 소유자는 스레드 아이디임).
                if (Interlocked.CompareExchange(ref _lockFlag, desired, kEmptyFlag) == kEmptyFlag)
                {
                    _writeCnt++;

                    return;
                }
            }

            // kMaxSpinCount 내 소유권을 얻지 못 했으면 일단 대기 중인 다른 스레드에게 실행권을 양보한다.
            Thread.Yield();
        }
    }

    // Exclusive
    public void WriteUnlock()
    {
        // 동일 스레드 내에서 W 이후 R에 대한 Lock을 잡았을 경우 우선 R에 대한 Lock을 전부 풀어야 한다.
        if ((_lockFlag & kReadThreadMask) != 0)
        {
            // Lock을 제대로 풀지 않은 비정상적인 상황이다.
            Environment.Exit(-1);
        }

        int lockCnt = --_writeCnt;
        if (lockCnt == 0)
        {
            Interlocked.Exchange(ref _lockFlag, kEmptyFlag);
        }
    }

    // Shared
    public void ReadLock()
    {
        int currWriterThreadId = (Thread.CurrentThread.ManagedThreadId << 16) & kWriteThreadMask;

        // 이미 내가 lock을 잡고 있는 상태에서 재진입한 상황
        if (_lockFlag == currWriterThreadId)
        {
            // W -> R
            Interlocked.Increment(ref _lockFlag);

            return;
        }

        // Lock에 대한 경합 시작
        while (true)
        {
            for (int i = 0; i < kMaxSpinCnt; i++)
            {
                int expected = _lockFlag & kReadThreadMask;

                // 순수하게 ReadCount만 비교한다.
                if (Interlocked.CompareExchange(ref _lockFlag, expected + 1, expected) == expected)
                    return; // 성공적으로 카운트를 올렸으면 빠져나옴.
            }

            // kMaxSpinCount 내 소유권을 얻지 못 했으면 일단 대기 중인 다른 스레드에게 실행권을 양보한다.
            Thread.Yield();
        }
    }

    // Shared
    public void ReadUnlock()
    {
        // Interlocked.Decrement(ref _lockFlag), Interlocked.Add(ref _lockFlag, -1) 둘 다 감소 이전 값이 아닌 감소 후의 값을 반환한다.
        // 따라서 감소 이전의 값을 구하고자 한다면 +1을 해야 한다.
        if (Interlocked.Decrement(ref _lockFlag) + 1 == 0)
        {
            // 값이 음수로 떨어지면 비정상적인 상황이다.
            Environment.Exit(-1);
        }
    }
}

class Program
{
    // 쓰기 작업만 상호배타적으로 작업하고, 읽기 작업은 공유하는 방식을 택하여 작성한 코드
    static void Run01()
    {
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        List<Task> tasks = new List<Task>();
        List<int> itemList = new List<int>();

        RWLock rwLock = new RWLock();

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            Task task = Task.Run(() => {
                manualResetEvent.WaitOne();

                for (int i = 0; i < 200_000; i++)
                {
                    int[] tempArr;

                    // Read
                    rwLock.ReadLock();
                    {
                        tempArr = itemList.ToArray();
                    }
                    rwLock.ReadUnlock();

                    foreach (var elem in tempArr)
                    {
                        // Do Something...
                    }

                    // Write
                    if (i % 100 == 0)
                    {
                        rwLock.WriteLock();
                        {
                            itemList.Add(i);
                        }
                        rwLock.WriteUnlock();
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

    static void Run02()
    {
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        List<Task> tasks = new List<Task>();

        RWLock rwLock = new RWLock();
        int sharedNum = 0;

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < Math.Max(Environment.ProcessorCount / 2, 1); i++)
        {
            Task incTask = Task.Run(() => {
                manualResetEvent.WaitOne();

                for (int i = 0; i < 200_000; i++)
                {
                    // inc
                    rwLock.WriteLock();
                    {
                        sharedNum++;
                    }
                    rwLock.WriteUnlock();
                }

                Console.WriteLine("task done");
            });

            Task decTask = Task.Run(() => {
                manualResetEvent.WaitOne();

                for (int i = 0; i < 200_000; i++)
                {
                    // dec
                    rwLock.WriteLock();
                    {
                        sharedNum--;
                    }
                    rwLock.WriteUnlock();
                }

                Console.WriteLine("task done");
            });

            tasks.Add(incTask);
            tasks.Add(decTask);
        }

        // Go
        manualResetEvent.Set();

        Task.WaitAll(tasks.ToArray());

        stopwatch.Stop();

        Console.WriteLine($"elapsed : {stopwatch.ElapsedMilliseconds}, sharedNum : { sharedNum }");

    }

    static void Main()
    {
        Run01();

        Console.WriteLine("--------------------------------------------------");

        Run02();
    }
}

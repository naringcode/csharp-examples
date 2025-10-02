// Update Date : 2025-01-02
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any, Release-Any

class Program   
{
    class FlagSpinLock
    {
        // Interlocked.Exchange()에는 bool 타입을 받는 오버로딩 함수가 없다.
        // volatile bool _flag = false;

        // 따라서 int를 쓰도록 한다.
        volatile int _flag = 0; // 0 : unlocked, 1 : locked

        public void Acquire() // Enter()
        {
            // 0을 1로 바꾸는 것이 성공해야 lock에 대한 자원 획득이다.
            while (Interlocked.Exchange(ref _flag, 1) == 1) // 기존 값이 1이면 획득 불가
            {
                // 자원 획득 실패 시 휴식 정책
                // Thread.Sleep(1); // 최소 1ms 대기 후 실행을 중단하고 준비큐에 들어가서 대기하는 정책
                // Thread.Sleep(0); // 현 스레드보다 동일 혹은 높은 우선순위를 가진 스레드가 대기하고 있으면 실행권한을 양도하는 정책
                // Thread.Yield(); // 스레드 우선순위를 신경쓰지 않고 대기 중인 스레드가 있으면 실행권을 양도하기 위한 정책(하이퍼스레딩이 일어나 컨텍스트 스위칭이 발생하지 않을 수도 있음)

                // 한 번만 스핀하고 내부적으로 플랫폼 최적화된 하이퍼스레딩 관련 작업을 수행한다(busy-wait 상태로 CPU를 사용함).
                // 쉽게 생각해서 딱 한 번만 스핀하고, 경우에 따라선 같은 코어에서 동시에 돌아가고 있는 스레드에게 실행권을 양도하기 위한 힌트를 주는 정책이다.
                // CPU에 힌트를 던지는 것이기 때문에 무조건적인 양보를 하는 건 아니다.
                Thread.SpinWait(1);

                // 위에 나열된 자원 획득 실패에 따른 정책을 수립하지 않으면 순수하게 스레드의 슬라이스 타임을 모두 소모하는 루프문이 된다.
            }
        }

        public void Release() // Exit()
        {
            Interlocked.Exchange(ref _flag, 0);
        }
    }

    class CasSpinLock
    {
        volatile int _flag = 0;

        public void Acquire() // Enter()
        {
            while (true)
            {
                int expected = 0;
                int desired = 1;

                // 0을 1로 바꾸면 lock에 대한 자원을 획득한 것이다.
                // _flag 값이 expected라고 한다면, 해당 값을 desired로 바꾼다(반환하는 값은 이전 자신의 값임).
                if (Interlocked.CompareExchange(ref _flag, desired, expected) == expected)
                    break;

                Thread.SpinWait(desired);
            }
        }

        public void Release()
        {
            Interlocked.Exchange(ref _flag, 0);
        }
    }

    static int sharedNumA = 0;
    static int sharedNumB = 0;

    static void Main()
    {
        FlagSpinLock flagSpinLock = new FlagSpinLock();

        Task task1 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                flagSpinLock.Acquire();

                sharedNumA++;

                flagSpinLock.Release();
            }
        });

        Task task2 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                flagSpinLock.Acquire();

                sharedNumA--;

                flagSpinLock.Release();
            }
        });

        Task.WaitAll(task1, task2);

        Console.WriteLine($"sharedNumA with spin-lock : { sharedNumA }");

        CasSpinLock casSpinLock = new CasSpinLock();

        task1 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                casSpinLock.Acquire();

                sharedNumB++;

                casSpinLock.Release();
            }
        });

        task2 = Task.Run(() => {
            for (int i = 0; i < 10_000_000; i++)
            {
                casSpinLock.Acquire();

                sharedNumB--;

                casSpinLock.Release();
            }
        });

        Task.WaitAll(task1, task2);

        Console.WriteLine($"sharedNumB with spin-lock : { sharedNumB }");
    }
}

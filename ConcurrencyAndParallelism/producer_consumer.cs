// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static readonly object locker = new object();
    static bool isReady = false;

    static void Producer()
    {
        // Consumer 쪽이 먼저 진행 후 대기 상태가 되도록 한다.
        Thread.Sleep(1000);

        lock (locker)
        {
            Console.WriteLine("생산자 측에서 데이터 준비 중");

            // 데이터 생산 중
            Thread.Sleep(2000);

            // 생산 완료
            isReady = true;

            Console.WriteLine("생산자 측에서 데이터 생산 완료");

            // Wait()로 대기하고 있는 스레드를 깨운다.
            Monitor.Pulse(locker);
        }
    }

    static void Consumer()
    {
        lock (locker)
        {
            if (isReady == false)
            {
                Console.WriteLine("소비자 측에서 데이터를 기다림");

                // 잠금 자원에 대한 점유를 해제하고 Pulse() 혹은 PulseAll() 신호가 올 때까지 대기한다.
                Monitor.Wait(locker);
            }

            Console.WriteLine("소비자 측에서 생산된 데이터를 받음");
        }
    }

    static void Main()
    {
        Thread producer = new Thread(Producer);
        Thread consumer = new Thread(Consumer);

        producer.Start();
        consumer.Start();

        producer.Join();
        consumer.Join();

        Console.WriteLine("Main Done");
    }
}

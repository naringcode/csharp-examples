// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static void Main()
    {
        // 임계 영역을 보호하기 위한 방법 중 하나는 Mutex인데,
        // Mutex는 lock과 Monitor와는 다르게 단일 프로세스 뿐만 아니라 여러 프로세스에서 제어 접근이 가능하다는 특징이 있다.

        // 생각나는 용도는?
        // - 프로그램 중복 실행 방지
        // - 동일한 파일로의 접근 방지
        // - 하드웨어 자원에 대한 접근을 한 프로세스만 허용

        // 사용하고자 하는 Mutex 이름
        const string mutexName = "Global\\MyUniqueMutex";

        // using을 적용하면 해당 블록을 벗어났을 때 자동으로 적용된 대상의 Dispose()를 호출한다.
        // - Mutex는 IDisposable 인터페이스를 구현한 클래스임.
        //
        // 뮤텍스 점유 시도
        // - 첫 번째 인자 : 생성 시점에 스레드가 뮤텍스를 소유하지는 않음(false).
        // - 두 번째 인자 : 뮤텍스 이름으로 이를 통해 다른 프로세스와 연계하여 전역 뮤텍스로 사용할 수 있음.
        // - 세 번째 인자 : 뮤텍스가 새로 생성된 것인지 확인함.
        using (var mutex = new Mutex(false, mutexName, out bool isCreatedNew))
        {
            if (isCreatedNew)
            {
                Console.WriteLine("새로운 뮤텍스를 생성함");
            }
            else
            {
                Console.WriteLine("이미 존재하는 뮤텍스를 참조함");
            }

            try
            {
                // Mutex는 WaitOne()을 통해 자원을 점유한다.
                mutex.WaitOne();

                Console.WriteLine("뮤텍스를 점유하는 중");
            }
            catch (AbandonedMutexException ex)
            {
                // AbandonedMutexException은 일종의 신호이지 뮤텍스 점유 자체가 실패했다는 의미는 아니다.
                Console.WriteLine(ex.Message);
                Console.WriteLine("뮤텍스를 소유하던 스레드가 ReleaseMutex()를 호출하지 않고 종료된 상태에서 해당 뮤텍스를 점유함");
            }

            // 점유 후 무언가를 작업한다.
            Console.WriteLine("뮤텍스를 10초 동안 점유함");
            Thread.Sleep(10000);

            // 점유 해제
            mutex.ReleaseMutex();
            Console.WriteLine("뮤텍스 점유를 해제함");
        }
    }
}

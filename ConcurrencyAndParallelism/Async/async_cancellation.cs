// Update Date : 2025-09-26
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Runtime.CompilerServices;

class Program
{
    // 사용하는 것 자체는 동기 방식의 Enumerator와 유사하다.
    static async IAsyncEnumerable<int> TaskAsyncEnumerator()
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(1000);

            yield return i;
        }
    }

    // 비동기 작업을 취소하기 위한 예제 메서드
    static async IAsyncEnumerable<int> TaskAsyncEnumerator([EnumeratorCancellation] CancellationToken token)
    {
        for (int i = 0; i < 10; i++)
        {
            // 취소 신호가 왔을 때 내부에서 예외를 던지게 유도할 수 있다.
            // token.ThrowIfCancellationRequested();

            // 다음과 같이 사용자 측에서 취소 신호를 감지하고 직접 예외를 던지는 것도 가능하다.
            // if (token.IsCancellationRequested)
            // {
            //     throw new OperationCanceledException();
            // }

            // CancellationToken는 Task 기반 메서드의 인자로 전달할 수 있다.
            // 이 경우 Task.Delay()가 취소 신호를 감지하면 TaskCanceledException 예외를 던진다.
            await Task.Delay(1000, token);

            yield return i;
        }
    }

    static async Task Main()
    {
        // foreach 앞에 await 키워드를 붙이는 것이 핵심이다.
        await foreach (int i in TaskAsyncEnumerator())
        {
            Console.WriteLine($"async enumerator : { i }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 비동기 작업이나 멀티스레드 작업을 취소시킬 때 사용하는 신호 발생기
        // - CancellationTokenSource : 취소 신호를 발행하는 주체
        // - CancellationToken : 신호를 감시하기 위한 토큰
        CancellationTokenSource cts = new CancellationTokenSource();

        Thread thread = new Thread(() => {
            Console.ReadKey();

            // 아무 키나 누르면 취소 신호를 보낸다.
            cts.Cancel();
        });

        thread.Start();

        try
        {
            await foreach (int i in TaskAsyncEnumerator(cts.Token))
            {
                Console.WriteLine($"async enumerator : { i }");
            }
        }
        catch (Exception ex)
        {
            // 취소 신호를 감지하면 예외를 던지기 때문에 catch문이 필요하다.
            // 세부적인 조정이 필요하다면 Exception 유형을 분명히 명시하는 것이 좋다.
            Console.WriteLine("작업 취소 : " + ex.Message);
        }
        finally
        {
            // CancellationTokenSource는 사용 후 Dispose()를 호출하여 메모리를 정리하는 것이 원칙이다.
            // 이 작업이 귀찮다면 using 패턴을 쓰는 것이 좋다.
            cts.Dispose();

            // "CancellationTokenSource? cts"일 경우
            // cts = null;
        }

        thread.Join();
    }
}

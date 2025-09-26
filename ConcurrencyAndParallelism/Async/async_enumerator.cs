// Update Date : 2025-09-26
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

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

    static async Task Main()
    {
        // foreach 앞에 await 키워드를 붙이는 것이 핵심이다.
        await foreach (int i in TaskAsyncEnumerator())
        {
            // 1초마다 한 줄씩 출력한다.
            Console.WriteLine($"async enumerator : { i }");
        }
    }
}

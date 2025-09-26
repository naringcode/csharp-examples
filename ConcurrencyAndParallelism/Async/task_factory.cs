// Update Date : 2025-09-27
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static async Task Main()
    {
        // Task.Factory.StartNew()는 Task를 더욱 저수준으로 제어하는 방법을 제공한다.
        // 해당 메서드도 내부적으로는 ThreadPool을 사용하며, 사용자는 옵션 값을 통해 무엇을 제어할 것인지 결정할 수 있다.
        // 참고로 특별한 이유가 없다면 Task.Run()만으로도 충분하니 StartNew()는 정말 필요할 때만 사용하는 것이 좋다.

        // TaskCreationOptions.LongRunning을 지정하면 ThreadPool 대신 전용 스레드를 사용한다.
        // 스레드풀은 모두가 사용하는 공간이기에 작업이 오래 걸리 것으로 예상된다면 전용 스레드를 사용하는 것이 좋다.
        string result1 = await Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Long Task...");

            // 어떤 작업에 5초가 소요된다고 가정한다.
            Task.Delay(5000).Wait();

            return "LongRunning Done";
        }, TaskCreationOptions.LongRunning);

        Console.WriteLine($"Result : { result1 }");
        
        Console.WriteLine("--------------------------------------------------");

        // TaskCreationOptions.AttachedToParent 옵션을 적용하면 부모는 자식 Task의 작업이 모두 완료될 때까지 대기한다.
        // 테스트해봤을 때 자식 Task 뿐만 아니라 부모 Task도 StartNew()를 사용해야 한다.
        Task<string> parentTask = Task.Factory.StartNew(() =>
        {
            Task childTask = Task.Factory.StartNew(() =>
            {
                Task.Delay(5000).Wait();

                Console.WriteLine("Child Task Done");
            }, TaskCreationOptions.AttachedToParent);

            Task.Delay(1000).Wait();

            // 자식 Task가 완료될 때까지 반환을 미룬다.
            return "Parent Task Done";
        });

        string result2 = await parentTask;

        Console.WriteLine($"Result : { result2 }");
    }
}

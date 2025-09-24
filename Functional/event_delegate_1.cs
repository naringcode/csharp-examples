// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    delegate void BasicDelegate();

    class Publisher
    {
        // event를 사용하면 옵저버 패턴을 쉽게 구현할 수 있다.
        // delegate에 event를 적용하면 외부에서 "+=", "-="를 통한 구독 여부는 허용하되, 호출까지는 허용하지 않는다.
        // 다시 말해 구독 여부는 외부에서 결정하고, 실행 여부 자체는 클래스 내부에서만 진행하게 한정하여 안정성을 높일 수 있다.
        public event BasicDelegate? OnNotify;

        public void DoSomething()
        {
            Console.WriteLine("Do Something...");

            // 이벤트를 발생시켜 등록된 구독자들에게 알린다.
            // "BasicDelegate?" 타입을 대상으로 하는 "?." 연산자를 사용하기에 구독자가 없으면 Invoke()는 반응하지 않는다.
            OnNotify?.Invoke();
        }
    }

    static void Subscribe1()
    {
        Console.WriteLine("구독 1");
    }

    static void Subscribe2()
    {
        Console.WriteLine("구독 2");
    }

    static void Main()
    {
        Publisher publisher = new Publisher();

        // 구독하기
        publisher.OnNotify += Subscribe1;
        publisher.OnNotify += Subscribe2;

        // delegate에 event를 적용하면 외부에서 직접 실행하는 것이 불가하다.
        // 만약 event를 빼고 순수하게 delegate만 사용한다면 호출 가능하지만 이러면 코드 안정성이 떨어진다.
        // publisher.OnNotify();

        publisher.DoSomething();

        Console.WriteLine("-------------------------");

        // 해지하기
        publisher.OnNotify -= Subscribe1;

        publisher.DoSomething();

        Console.WriteLine("-------------------------");

        // 완전 해지하기
        publisher.OnNotify -= Subscribe2;

        publisher.DoSomething();
    }
}

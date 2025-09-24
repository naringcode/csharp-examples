// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    delegate int Operation(int x, int y);
    
    static int Add(int x, int y)
    {
        Console.WriteLine($"{ x } + { y } = { x + y }");

        return x + y;
    }

    static int Sub(int x, int y)
    {
        Console.WriteLine($"{ x } - { y } = { x - y }");

        return x - y;
    }

    static int Mul(int x, int y)
    {
        Console.WriteLine($"{ x } * { y } = { x * y }");

        return x * y;
    }

    static int ApplyOperation(Operation operation, int x, int y)
    {
        return operation(x, y);
    }

    static void Main()
    {
        // delegate 인스턴스를 생성 후 메서드 참조 저장
        Operation? operation = Add; // ?를 넣은 이유는 아래에 나와 있음.

        // C#의 delegate는 여러 메서드를 등록 후 호출할 수 있는 기능인 멀티캐스트를 지원한다.
        // delegate 체인에 메서드를 연결하려면 "+="를 쓰면 된다.
        operation += Sub;
        operation += Mul;
        operation += Add; // 중복 등록도 가능함.

        Console.WriteLine("--------------- 1 ---------------");

        // 저장된 메서드 호출
        // 반환 값이 있는 경우 마지막 메서드가 반환한 것을 받는다.
        var ret = operation(100, 30); // 등록한 순서대로 메서드가 실행

        Console.WriteLine($"{ ret }");
        
        Console.WriteLine("--------------- 2 ---------------");

        // 등록된 메서드를 제거하고자 한다면 "-="를 쓰면 된다.
        operation -= Mul;

        ret = operation(100, 30);

        Console.WriteLine($"{ ret }");

        Console.WriteLine("--------------- 3 ---------------");

        // 중복 등록된 것을 제거하려고 한다면 가장 마지막에 등록된 것을 제거한다.
        operation -= Add;

        ret = operation(100, 30);

        Console.WriteLine($"{ ret }");

        Console.WriteLine("--------------- 4 ---------------");

        // 메서드가 다수 등록된 delegate 인스턴스도 함수의 인자로 전달하는 것 역시 가능하다.
        ret = ApplyOperation(operation, 100, 30);

        Console.WriteLine($"{ ret }");

        Console.WriteLine("--------------- 5 ---------------");

        // 호출은 Invoke()를 통해서도 진행할 수 있다.

        // Invoke()로 호출로 delegate에 등록된 메서드들을 실행하는 것도 가능하다.
        operation.Invoke(100, 30);

        // delegate가 Nullable인 상태라면 Invoke()는 Null 조건부 연산자인 "?."와 연계해서 사용할 수 있다.
        // "?." 연산자는 객체가 null일 경우 null을 반환하고, 아닐 경우 멤버나 메서드를 반환한다.
        operation?.Invoke(100, 30); // 객체 타입이 "Operation?"이기에 사용 가능한 방법

        Console.WriteLine("--------------- 6 ---------------");

        operation -= Add;
        operation -= Sub;

        // delegate에 등록된 메서드가 없으면 null로 취급한다.
        if (operation == null)
        {
            Console.WriteLine("operation == null");
        }

        try
        {
            // delegate가 null인 상태에서 Invoke()를 호출하면 NullReferenceException 예외를 던진다.
            operation.Invoke(100, 30);
        } 
        catch (NullReferenceException ex)
        {
            Console.WriteLine(ex.Message);
        }

        // 하지만 "?." 연산자를 사용할 경우 delegate에 메서드가 등록되지 않은 상태에서 Invoke()를 호출할 경우
        // 함수 호출을 진행하지 않고 null을 반환한다.
        var ret2 = operation?.Invoke(100, 30); // ret2의 타입은 "int?"

        if (ret2 == null)
        {
            Console.WriteLine("ret2 == null");
        }

        // 아래 코드는 operation?.Invoke()와 동일한 기능을 나타내고 있다.
        // if (operation != null)
        // {
        //     operation.Invoke(100, 30);
        // }

        // 추가적으로...
        // delegate의 멀티캐스트 기능은 이벤트 처리나 옵저버 패턴을 구현할 때 유용하다.
    }
}

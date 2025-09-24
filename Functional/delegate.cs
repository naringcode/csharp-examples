// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
    // https://www.geeksforgeeks.org/c-sharp/c-sharp-delegates/

    // C#에서의 delegate(대리자)는 메서드 참조를 저장하거나 호출할 수 있는 타입이다.
    delegate void BasicDelegate();
    delegate int Operation(int x, int y);

    static void HelloWorld()
    {
        Console.WriteLine("Hello World!");
    }

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
        BasicDelegate basicDelegate = HelloWorld;
        Operation operation = Add;

        Console.WriteLine("--------------- 1 ---------------");

        // 저장된 메서드 호출
        basicDelegate();

        Console.WriteLine("--------------- 2 ---------------");

        var ret = operation(100, 200);

        // 반환 값이 있는 delegate를 통해 받은 값 출력
        Console.WriteLine($"{ ret }");

        Console.WriteLine("--------------- 3 ---------------");

        // 다른 메서드로 교체하고 호출까지 진행하기
        operation = Sub;

        Console.WriteLine($"{ operation(300, 350) }");

        Console.WriteLine("--------------- 4 ---------------");

        // 함수의 인자로 delegate를 전달하는 것도 가능하다.
        ret = ApplyOperation(operation, 100, 30);

        Console.WriteLine($"{ ret }");
        
        Console.WriteLine("--------------- 5 ---------------");

        // 함수의 매개변수가 delegate라는 것이 확실하다면 메서드 참조를 넘길 수 있다.
        ret = ApplyOperation(Mul, 100, 30);

        Console.WriteLine($"{ ret }");
    }
}

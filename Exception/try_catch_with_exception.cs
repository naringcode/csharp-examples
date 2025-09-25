// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static void Main()
    {
        int[] arr = [ 1, 2, 3 ];

        try
        {
            arr[5] = 100; // 인덱스를 벗어나서 내부에 throw 진행함.
        }
        catch (Exception ex) // Exception은 모든 예외 객체의 부모임.
        {
            Console.WriteLine($"예외 발생 : { ex.Message }");
        }

        Console.WriteLine("Hello World!");

        Console.WriteLine("--------------------------------------------------");
        
        try
        {
            arr[5] = 100; // 인덱스를 벗어나서 내부에 throw 진행함.
        }
        catch (IndexOutOfRangeException ex) // 인덱스를 벗어났을 때 발생하는 예외 객체는 IndexOutOfRangeException임.
        {
            Console.WriteLine($"예외 발생 : { ex.Message }");
        }

        Console.WriteLine("Hello World!");

        Console.WriteLine("--------------------------------------------------");

        object obj = "string";

        // 아래 try-catch문은 문제가 있는 코드이다.
        // try
        // {
        //     // 문자열을 double 타입으로 언박싱하는 건 불가능하기에 InvalidCastException 발생한다.
        //     double db = (double)obj;
        // }
        // catch (IndexOutOfRangeException ex) // 하지만 InvalidCastException를 받을 수 있는 catch문이 없기에 프로그램이 종료됨.
        // {
        //     Console.WriteLine($"예외 발생 : { ex.Message }");
        // }
        // 
        // Console.WriteLine("Hello World!");
        // 
        // Console.WriteLine("--------------------------------------------------");

        try
        {
            // 문자열을 double 타입으로 언박싱하는 건 불가능하기에 InvalidCastException 발생한다.
            double db = (double)obj;
        }
        catch (IndexOutOfRangeException ex) // 던져지는 예외 객체는 InvalidCastException이기 때문에 여기는 무시함.
        {
            Console.WriteLine($"예외 발생 1 : { ex.Message }");
        }
        catch (InvalidCastException ex) // 발생한 예외는 여기서 받음.
        {
            Console.WriteLine($"예외 발생 2 : { ex.Message }");
        }
        
        Console.WriteLine("Hello World!");

        Console.WriteLine("--------------------------------------------------");

        try
        {
            // 문자열을 double 타입으로 언박싱하는 건 불가능하기에 InvalidCastException 발생함.
            double db = (double)obj;
        }
        catch (IndexOutOfRangeException ex) // 던져지는 예외 객체는 InvalidCastException이기 때문에 여기는 무시함.
        {
            Console.WriteLine($"예외 발생 1 : { ex.Message }");
        }
        catch (Exception ex) // 어떠한 예외가 발생하는지 알 수 없을 때는 그냥 최상위 부모인 Exception으로 받으면 됨.
        {
            Console.WriteLine($"예외 발생 2 : { ex.Message }");
        }

        Console.WriteLine("Hello World!");
    }
}

// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static void ImplicitConversion1()
    {
        int a = 100; // 4 bytes
        float b = a; // 4 bytes (int -> float)

        Console.WriteLine($"a : { a }, b : { b }");
    }

    static void ImplicitConversion2()
    {
        int a = 200; // 4 bytes
        long b = a; // 8 bytes (int -> long)

        Console.WriteLine($"a : { a }, b : { b }");

    }

    static void ExplicitConversion1()
    {
        double a = 3.14f; // 8 bytes
        int b = (int)a; // 4 bytes (double -> int)
        float c = (float)a; // 4 bytes (double -> float)

        Console.WriteLine($"a : { a }, b : { b }, c : { c }");
    }

    static void ExplicitConversion2()
    {
        long a = 400; // 8 bytes
        int b = (int)a; // 4 bytes (long -> int)

        Console.WriteLine($"a : { a }, b : { b }");
    }

    static void ExplicitConversion3()
    {
        Kilometer km = new Kilometer(3.14);
        double value = (double)km;

        Console.WriteLine($"km : { value }");
    }

    // explicit conversion for user-defined type
    public class Kilometer
    {
        public Kilometer(double value) => Value = value;

        public double Value { get; set; }

        public static explicit operator double(Kilometer km)
        {
            return km.Value;
        }
    }

    static void Main()
    {
        // 데이터 손실이나 예외가 발생하는 상황이 없을 경우
        ImplicitConversion1();
        ImplicitConversion2();

        // 데이터 손실이나 예외가 발생하는 상황이 생기는 경우
        ExplicitConversion1();
        ExplicitConversion2();

        // 사용자 정의 타입도 operator를 통해 명시적 형변환을 위한 사양을 정의할 수 있다.
        ExplicitConversion3();
    }
}

// Update Date : 2025-09-13
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

public class Rect
{
    public Rect(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public int X { get; init; }
    public int Y { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
}

public static class MyUtils
{
    // static 클래스 내 static 메서드를 구성할 때 첫 번째 매개변수 앞에 this 키워드를 붙이면 대상 인스턴스의 메서드처럼 호출할 수 있다.
    // 제3자 입장에서 라이브러리 기능을 확장하고자 할 때 유용하다.
    public static void Print(this string str, string other)
    {
        Console.WriteLine($"{ str } { other }");
    }

    public static void Print(this Rect rect)
    {
        Console.WriteLine($"X : { rect.X }, Y : { rect.Y }, Width : { rect.Width }, Height : { rect.Height }");
    }
}

class Program
{
    static void Main()
    {
        string name = "John";
        Rect rect = new Rect(10, 15, 150, 200);

        // 아래 두 함수의 기능은 동일
        name.Print("Smith");
        MyUtils.Print(name, "Doe");

        // 아래 두 함수의 기능은 동일
        rect.Print();
        MyUtils.Print(rect);

        // 주의 : 확장 메서드의 이름이 겹치게 작성하지 않도록 한다.
    }
}

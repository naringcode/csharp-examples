// Update Date : 2025-09-13
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 9.0)
// Configuration : Debug-Any

class Program
{
    // 생성 후 상태 변경이 불가한 record 형식의 불변 객체
    public record class Point
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Before C# 9.0
        public Point WithX(int x)
        {
            return new Point(x, Y);
        }

        // Before C# 9.0
        public Point WithY(int y)
        {
            return new Point(X, y);
        }

        public void Print()
        {
            Console.WriteLine($"X : { X }, Y : { Y }");
        }
    }

    static void Main()
    {
        // https://learn.microsoft.com/ko-kr/dotnet/csharp/whats-new/csharp-version-history#c-version-9
        // https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/record#nondestructive-mutation
        // https://www.csharpstudy.com/latest/CS9-record.aspx
        Point p1 = new Point(100, 200);

        // 불가
        // p1.X = 100;

        Point p2 = p1 with { X = 300 };
        Point p3 = p1 with { Y = 400 };

        p1.Print();
        p2.Print();
        p3.Print();
        
        // For Equality Test
        Point p4 = p1.WithY(400);

        Console.WriteLine($"p3와 p4의 값은 같은가? { p3.Equals(p4) }"); // True
        Console.WriteLine($"p3와 p4의 주소 같은가? { ReferenceEquals(p3, p4) }"); // False
    }
}

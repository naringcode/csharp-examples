// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Runtime;

class Program
{
    static void Main()
    {
        object obj = "Hello C# Programming";

        // is 연산자는 객체가 특정 형식과 호환될 수 있는지를 검사한다.
        // 호환 가능하다면 true를, 불가능하다면 false를 반환한다.
        if (obj is string)
        {
            // 실질적인 사용을 하려면 형변환을 진행해야 한다.
            string str1 = (string)obj;

            Console.WriteLine($"obj is string succeeded : { str1 }");
        }
        else
        {
            Console.WriteLine($"obj is string failed");
        }

        // C# 7.0부터 지원하는 패턴 매칭을 통해 검사와 캐스팅을 동시에 처리하는 것도 가능하다.
        if (obj is string str2)
        {
            // 패턴 매칭을 사용하면 형변환하는 작업을 거치지 않아도 된다.
            Console.WriteLine($"obj is string str2 succeeded : { str2 }");
        }
        else
        {
            Console.WriteLine($"obj is string str2 failed");
        }

        // 여담으로 C# 8.0부터 지원하는 스위치 패턴 매칭을 통해 타입을 검사하는 것도 가능하다.
        string conv = obj switch
        {
            int => "정수",
            string s => $"문자열 : { s }",
            _ => "불명"
        };

        Console.WriteLine(conv);
    }
}

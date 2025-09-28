// Update Date : 2025-09-28
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Runtime.CompilerServices;

class Program
{
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/caller-information

    // Caller 계열의 Attribute를 사용하면 컴파일러가 호출자 정보를 치환해서 매개변수에 적용한다.
    // 해당 Atttribute 계열은 로그 시스템을 구성할 때 유용하게 사용할 수 있다.

    // 종류
    // - [CallerMemberName] : 호출자(메서드, 프로퍼티 등)를 문자열로 치환함.
    // - [CallerFilePath] : 호출한 파일의 경로를 문자열로 치환함.
    // - [CallerLineNumber] : 호출한 코드의 줄 번호를 정수로 치환함.
    // - [CallerArgumentExpression] : 메서드 호출 시 인자로 전달한 원래 식을 문자열로 치환함.

    static void MyLog(
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        Console.WriteLine($"호출자 : { memberName }");
        Console.WriteLine($"파일 경로 : { filePath }");
        Console.WriteLine($"호출한 줄 번호 : { lineNumber }");

        Console.WriteLine();
    }

    static void MyAssert(bool condition, [CallerArgumentExpression("condition")] string? expr = null)
    {
        Console.WriteLine($"MyAssert : { expr }");
    }

    static void CheckNotNull(object? value, [CallerLineNumber] int lineNumber = 0, [CallerArgumentExpression("value")] string? expr = null)
    {
        if (value == null)
        {
            Console.WriteLine($"value is null : { lineNumber }, { expr }");
        }
    }

    static void TestMethod()
    {
        MyLog();
    }

    static void Main()
    {
        MyLog();

        TestMethod();

        MyAssert(10 < 15);
        MyAssert(15 < 10);

        object? obj = null;

        CheckNotNull(obj);
    }
}

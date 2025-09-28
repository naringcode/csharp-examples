// Update Date : 2025-09-28
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

#define USER_DEFINE

using System.Diagnostics;

class Program
{
    // https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/attributes/general

    // Conditional은 조건부 컴파일 기호가 정의되어 있을 때만 메서드 호출이 가능하게 제어하기 위한 특성이다.
    // 이를 통해 if문으로 코드를 제어할 필요 없이, 조건에 부합하지 않으면 호출 자체를 빌드 과정에서 제거할 수 있다.
    [Conditional("DEBUG")]
    static void DebugPrint(string msg)
    {
        Console.WriteLine($"Debug : { msg }");
    }
     
    [Conditional("RELEASE")]
    static void ReleasePrint(string msg)
    {
        Console.WriteLine($"Release : { msg }");
    }

    [Conditional("USER_DEFINE")]
    static void UserPrint(string msg)
    {
        Console.WriteLine($"User : {msg}");
    }

    static void Main()
    {
        DebugPrint("Hello World!");
        ReleasePrint("Hello World!");
        UserPrint("Hello World!");
    }
}

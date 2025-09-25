// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static string DoUnboxingDouble(object obj)
    {
        try
        {
            double db = (double)obj;
        }
        catch (Exception ex)
        {
            // 예외를 다시 전달할 필요가 있다면 받은 예외를 throw로 던지면 된다.
            // throw ex;

            return $"예외 발생 : { ex.Message }";
        }
        finally
        {
            Console.WriteLine("finally");

            // 자원 정리 예시
            // obj = null;
        }

        return "정상 작동";
    }

    static void Main()
    {

        object strObj = "string";
        object doubleObj = 3.14;

        // finally는 예외 발생 여부와 상관 없이 항상 실행되는 블록을 작성할 때 사용된다.
        // 해당 블록은 주로 자원 정리를 진행할 때 사용된다(RAII와 유사한 목적으로 사용됨).
        string ret1 = DoUnboxingDouble(strObj);

        // catch로 받은 예외에서 반환한 문자열을 출력한다.
        Console.WriteLine(ret1);

        Console.WriteLine("--------------------------------------------------");

        string ret2 = DoUnboxingDouble(doubleObj);

        // 정상 작동
        Console.WriteLine(ret2);
    }
}

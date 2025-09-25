// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    class CustomException : Exception
    {
        public CustomException(string message, int errorCode)
            : base(message) // base를 통해 Exception의 생성자 호출
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }

    static void Main()
    {
        try
        {
            throw new CustomException("Custom Error", 404);
        }
        catch (CustomException ex)
        {
            Console.WriteLine($"Message : { ex.Message }, ErrorCode : { ex.ErrorCode }");
        }
    }
}

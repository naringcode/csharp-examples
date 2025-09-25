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
            arr[5] = 100; // 인덱스를 벗어나서 내부에 throw 진행
        }
        catch // 예외 객체를 받지 않아도 catch는 동작
        {
            Console.WriteLine("예외 발생");
        }

        Console.WriteLine("Hello World!");
    }
}

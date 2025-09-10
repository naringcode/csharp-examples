// Update Date : 2025-09-10
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static void Main()
    {
        string? str = Console.ReadLine();

        int score = int.Parse(str ?? "0");

        score = Math.Clamp(score, 0, 100);

        // C# 7.0 이상부터 지원하는 when을 적용한 Pattern Matching 스위치
        switch (score)
        {
            case 100:
                Console.WriteLine("Perfect!");
                break;

            case int s when score >= 90:
                Console.WriteLine($"A : { s }");
                break;

            case int s when score >= 80 && score < 90:
                Console.WriteLine($"B : { s }");
                break;

            case int s when score >= 70 && score < 80:
                Console.WriteLine($"C : { s }");
                break;

            case int s when score >= 60 && score < 70:
                Console.WriteLine($"D : { s }");
                break;

            default:
                Console.WriteLine($"F : { score }");
                break;
        }
    }
}

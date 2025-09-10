// Update Date : 2025-09-10
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Globalization;

class Program
{
    static void Main()
    {
        int[] arr = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

        // .. operator
        int[] subArr = arr[2..6]; // 30, 40, 50, 60

        foreach (int elem in subArr)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();

        subArr = arr[..6]; // 10, 20, 30, 40, 50, 60

        foreach (int elem in subArr)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();

        subArr = arr[3..]; // 40, 50, 60, 70, 80, 90, 100

        foreach (int elem in subArr)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();
        Console.WriteLine();

        // hat operator
        Console.WriteLine(arr[arr.Length - 1]); // 100
        Console.WriteLine(arr[^1]); // 100

        Console.WriteLine(arr[arr.Length - 2]); // 90
        Console.WriteLine(arr[^2]); // 90

        Console.WriteLine();

        for (int i = 1; i <= arr.Length; i++)
        {
            // ^0은 사용할 수 없다.
            Console.Write($"{ arr[^i] } "); // 100, 90, 80, 70, 60, 50, 40, 30, 20, 10
        }

        Console.WriteLine();
        Console.WriteLine();

        // mixed
        subArr = arr[2..^3]; // 30, 40, 50, 60, 70

        foreach (int elem in subArr)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();

        subArr = arr[..^3]; // 10, 20, 30, 40, 50, 60, 70

        foreach (int elem in subArr)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();

        subArr = arr[^3..]; // 80, 90, 100

        foreach (int elem in subArr)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();
    }
}

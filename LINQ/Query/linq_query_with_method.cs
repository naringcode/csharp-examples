// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    static void Main()
    {
        int[] arr = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ];

        // LINQ 쿼리 구문이 컴파일을 거치면 메서드 구문으로 전환되기에
        // 쿼리 구문 자체는 메서드 구문에 체이닝하여 사용하는 것이 가능하다.
        var evenNums = (from num in arr
                        where num % 2 == 0
                        select num).Take(3);

        // Take(3)이 적용되었기에 3개의 요소만 가져와 출력한다.
        foreach (int num in evenNums)
        {
            Console.WriteLine(num); // 2, 4, 6
        }
    }
}

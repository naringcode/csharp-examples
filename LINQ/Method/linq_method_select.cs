// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    class Student
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; } = "";
        public List<int> Scores { get; set; } = [];

        public override string ToString()
        {
            return $"Student - Id : { Id }, Name : { Name }, Age : { Age }";
        }
    }

    static void Main()
    {
        List<Student> students = [
            new Student { Id = 1, Age = 20, Name = "Alice",   Scores = [5, 3, 9] },
            new Student { Id = 2, Age = 22, Name = "Bob",     Scores = [8, 3, 2] },
            new Student { Id = 3, Age = 23, Name = "Charlie", Scores = [4, 4, 1] },
            new Student { Id = 4, Age = 21, Name = "David",   Scores = [5, 6, 2] },
            new Student { Id = 5, Age = 20, Name = "Eve",     Scores = [9, 8, 7] },
        ];

        // LINQ 메서드 구문 중 하나인 Select()는 데이터를 투영하는 작업을 진행한다.
        // - 데이터 형식 변경(int -> string 등)
        // - 데이터 구조 변경(특정 필드만 가져오거나 새로운 필드 추가)

        // 기능 자체는 쿼리 구문의 select와 동일하다.
        // Select()가 받는 건 Func<In, Out> 형태의 delegate이다.
        var results1 = students.Select(student => student.Age);

        foreach (var result in results1)
        {
            Console.WriteLine(result);
        }

        Console.WriteLine("--------------------------------------------------");

        // 익명 타입으로 다수의 필드를 묶어서 반환하는 유형
        var results2 = students.Select(student => new { MyName = student.Name, MyAge = student.Age });

        foreach (var result in results2)
        {
            Console.WriteLine(result);
        }

        Console.WriteLine("--------------------------------------------------");

        // ValueTuple로 다수의 필드를 묶어서 반환하는 유형
        var results3 = students.Select(student => (MyName: student.Name, MyAge: student.Age));

        foreach (var result in results3)
        {
            Console.WriteLine(result);
        }
    }
}

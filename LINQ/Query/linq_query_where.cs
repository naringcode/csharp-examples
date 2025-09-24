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

        public override string ToString()
        {
            return $"Student - Id : { Id }, Name : { Name }, Age : { Age }";
        }
    }

    static void Main()
    {
        List<Student> students = [
            new Student { Id = 1, Age = 20, Name = "Alice" },
            new Student { Id = 2, Age = 22, Name = "Bob" },
            new Student { Id = 3, Age = 23, Name = "Charlie" },
            new Student { Id = 4, Age = 21, Name = "David" },
            new Student { Id = 5, Age = 20, Name = "Eve" },
        ];

        // LINQ 쿼리 구문에서 조건을 명시하기 위해 사용하는 키워드는 where이다.
        // where 키워드는 from과 select절 사이에 위치한다.
        var results1 = from student in students
                       where student.Age >= 21
                       select student;

        foreach (var result in results1)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 조건을 부여할 때는 논리 연산자를 사용하여 조건을 결합할 수 있다.
        var results2 = from student in students
                       where student.Age >= 21 || student.Name == "Alice"
                       select student;

        foreach (var result in results2)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }");
        }
    }
}

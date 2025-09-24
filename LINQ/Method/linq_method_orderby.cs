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

        // LINQ 메서드 구문에서 데이터 정렬을 수행하는 기능은 OrderBy()를 통해서 진행할 수 있다.
        // 해당 메서드에 정렬하고자 하는 필드의 값만 전달하면 알아서 정렬이 수행된다.
        var results1 = students.OrderBy(student => student.Age);

        foreach (var result in results1)
        {
            Console.WriteLine($"{ result.Name } : { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 역순으로 정렬하고자 한다면 OrderBy() 대신 OrderByDescending()을 써야 한다.
        var results2 = students.OrderByDescending(student => student.Age);

        foreach (var result in results2)
        {
            Console.WriteLine($"{ result.Name } : { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 메서드 체이닝을 통해 정렬 도중 동일한 값에 대한 후속 정렬 기준은 Then()이나 ThenByDescending()으로 진행하면 된다.
        // 아래 코드는 나이는 오름차순, 동일한 나이가 있을 경우에는 이름 기준으로 내림차순 정렬한 결과를 받아온다.
        var results3 = students
                        .OrderBy(student => student.Age)
                        .ThenByDescending(student => student.Name);

        foreach (var result in results3)
        {
            Console.WriteLine($"{ result.Name } : { result.Age }");
        }
    }
}

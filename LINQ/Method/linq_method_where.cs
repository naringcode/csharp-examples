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

        // LINQ 메서드 구문에서 데이터 필터링을 적용하기 위해 사용하는 것은 Where()이다.
        // Where()을 적용하면 특정 조건을 만족하는 데이터를 필터링할 수 있다.
        // Where()에 적용되는 delegate는 bool을 반환하는 Predicate 형식을 충족해야 한다.
        var results1 = students.Where(student => student.Name.EndsWith("e"));

        foreach (var result in results1)
        {
            Console.WriteLine(result);
        }

        Console.WriteLine("--------------------------------------------------");

        // 메서드 체이닝을 통해 조건을 충족한 요소의 특정 필드만 Select()를 통해 투영해 가져오는 것도 가능하다.
        var results2 = students
                        .Where(student => student.Name.EndsWith("e"))
                        .Select(student => (student.Name, student.Age));

        foreach (var result in results2)
        {
            Console.WriteLine(result);
        }
    }
}

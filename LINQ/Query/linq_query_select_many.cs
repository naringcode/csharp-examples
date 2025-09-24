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

        // LINQ 메서드 구문을 보면 중첩 데이터를 평탄화하기 위한 SelectMany()를 지원한다.
        // LINQ 쿼리 구문 자체는 SelectMany에 해당하는 키워드를 지원하지 않는다.
        // 하지만 from절을 중첩해서 사용할 경우 컴파일러는 내부적으로 해당 구문을 SelectMany로 변환한다.
        var results = from student in students
                      from score in student.Scores
                      select score; // 중첩된 컬렉션을 하나의 평면 컬렉션으로 투영

        // ValueTuple은 요소를 분해해서 꺼낼 수 있다.
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }

        Console.WriteLine("--------------------------------------------------");

        // LINQ 쿼리 구문으로 구현하는 SelectMany에 또한 특정 필드를 묶어서 투영하는 것이 가능하다.
        var results2 = from student in students
                       from score in student.Scores
                       select (student.Name, score); // 중첩된 컬렉션을 하나의 평면 컬렉션으로 투영

        // ValueTuple은 요소를 분해해서 꺼낼 수 있다.
        foreach (var (studentName, score) in results2)
        {
            Console.WriteLine($"{ studentName } : { score }");
        }
    }
}

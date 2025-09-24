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

        // LINQ 메서드 구문에서 지원하는 SelectMany()를 사용하면 중첩 데이터를 평탄화할 수 있다.
        // 여기서 말하는 평탄화는 중첩된 컬렉션을 하나의 평면 컬렉션으로 변환하는 작업을 의미한다.
        // Select()와 마찬가지로 조건에 맞는 delegate를 넘겨야 한다.
        var results1 = students.SelectMany(student => student.Scores);

        // 평탄화 이전
        // [
        //     [5, 3, 9],
        //     [8, 3, 2],
        //     [4, 4, 1],
        //     [5, 6, 2],
        //     [9, 8, 7]
        // ]
        //
        // 평탄화 이후
        // [ 5, 3, 9, 8, 3, 2, 4, 4, 1, 5, 6, 2, 9, 8, 7 ]
        foreach (var result in results1)
        {
            Console.WriteLine(result);
        }

        Console.WriteLine("--------------------------------------------------");

        // 평탄화를 진행하면서 특정 필드와 묶고자 한다면 시퀀스를 풀어내는 작업과 특정 필드를 묶어서 반환하는 작업을 분리해야 한다.
        var results2 = students.SelectMany(
            student => student.Scores,  // 평탄화
            (student, score) => (student.Name, score) // 묶어서 반환
        );

        // 평탄화된 컬렉션과 특정 필드를 묶어서 투영한 것을 조회한다.
        foreach (var result in results2)
        {
            Console.WriteLine(result);
        }
    }
}

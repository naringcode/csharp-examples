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
        public string Gender { get; set; } = "";
        public string Name { get; set; } = "";

        public override string ToString()
        {
            return $"Student - Id : { Id }, Name : { Name }, Age : { Age }";
        }
    }

    class Score
    {
        public int StudentId { get; set; }
        public int ScoreValue { get; set; }
        public string Subject { get; set; } = "";
    }

    static void Main()
    {
        List<Student> students = [
            new Student { Id = 1, Age = 20, Gender = "F", Name = "Alice" },
            new Student { Id = 2, Age = 22, Gender = "M", Name = "Bob" },
            new Student { Id = 3, Age = 23, Gender = "M", Name = "Charlie" },
            new Student { Id = 4, Age = 21, Gender = "M", Name = "David" },
            new Student { Id = 5, Age = 20, Gender = "F", Name = "Eve" },
        ];

        List<Score> studentScores = [
            new Score { StudentId = 1, ScoreValue = 5, Subject = "Math" },
            new Score { StudentId = 1, ScoreValue = 3, Subject = "Science" },
            new Score { StudentId = 1, ScoreValue = 9, Subject = "History" },
            new Score { StudentId = 2, ScoreValue = 8, Subject = "Math" },
            new Score { StudentId = 2, ScoreValue = 3, Subject = "Science" },
            new Score { StudentId = 2, ScoreValue = 2, Subject = "History" },
            new Score { StudentId = 3, ScoreValue = 4, Subject = "Math" },
            new Score { StudentId = 3, ScoreValue = 4, Subject = "Science" },
            new Score { StudentId = 3, ScoreValue = 1, Subject = "History" },
            new Score { StudentId = 4, ScoreValue = 5, Subject = "Math" },
            new Score { StudentId = 4, ScoreValue = 6, Subject = "Science" },
            new Score { StudentId = 4, ScoreValue = 2, Subject = "History" },
            new Score { StudentId = 5, ScoreValue = 9, Subject = "Math" },
            new Score { StudentId = 5, ScoreValue = 8, Subject = "Science" },
            new Score { StudentId = 5, ScoreValue = 7, Subject = "History" },
        ];

        // LINQ 쿼리 구문의 join을 이용하면 두 개 이상의 컬렉션을 특정 키 기준으로 결합할 수 있다.
        // results의 타입을 보면 IEnumerable<(Student, Score)>인 것을 알 수 있다.
        var results = from student in students
                      join score in studentScores
                      on student.Id equals score.StudentId
                      select (student, score);

        // ValueTuple은 요소를 분해해서 꺼낼 수 있다.
        foreach (var (student, score) in results)
        {
            Console.WriteLine($"Student : { student.Name }, Subject : { score.Subject }, Score : { score.ScoreValue }");
        }
    }
}

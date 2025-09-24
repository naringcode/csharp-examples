// Update Date : 2025-09-25
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Collections;

class Program
{
    class Student
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } = "";
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
            new Student { Id = 1, Age = 20, Gender = "F", Name = "Alice",   Scores = [5, 3, 9] },
            new Student { Id = 2, Age = 22, Gender = "M", Name = "Bob",     Scores = [8, 3, 2] },
            new Student { Id = 3, Age = 23, Gender = "M", Name = "Charlie", Scores = [4, 4, 1] },
            new Student { Id = 4, Age = 21, Gender = "M", Name = "David",   Scores = [5, 6, 2] },
            new Student { Id = 5, Age = 20, Gender = "F", Name = "Eve",     Scores = [9, 8, 7] },
        ];

        // LINQ 쿼리 구문은 group by를 통해 데이터를 그룹화할 수 있다.
        // 데이터를 그룹화했으면 그룹을 기준으로 데이터를 구성해야 한다.
        var groupResults1 = from student in students
                            group student by student.Gender into ageGroup
                            select (Gender: ageGroup.Key, Students: ageGroup);

        foreach (var groupResult in groupResults1)
        {
            // Group Key
            Console.WriteLine($"Group : { groupResult.Gender }");

            // Group Value
            foreach (var student in groupResult.Students)
            {
                Console.WriteLine($"  { student.Name }, { student.Age }");
            }
        }

        Console.WriteLine("--------------------------------------------------");
        
        // 그룹 자체에도 조건을 거는 것이 가능하다.
        // 마찬가지로 조건을 거는 건 where절을 통해서 진행한다.
        var groupResults2 = from student in students
                            group student by student.Gender into ageGroup
                            where ageGroup.Count() >= 3 // 그룹에 포함된 학생 수가 3명 이상인 것만 필터링
                            select (Gender: ageGroup.Key, Count: ageGroup.Count(), Students: ageGroup);

        foreach (var groupResult in groupResults2)
        {
            // Group Key
            Console.WriteLine($"Group : { groupResult.Gender }, Count : { groupResult.Count }");

            // Group Value
            foreach (var student in groupResult.Students)
            {
                Console.WriteLine($"  { student.Name }, { student.Age }");
            }
        }
    }
}

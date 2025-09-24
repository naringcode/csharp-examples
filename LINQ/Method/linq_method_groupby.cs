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

        // LINQ 메서드 구문에서 데이터를 그룹화하기 위한 메서드는 GroupBy()이다.
        var groupResults1 = students.GroupBy(student => student.Gender);

        // 순회하며 결과를 조회하면 각 요소는 아예 그룹핑되어 있기 때문에 다소 신경써서 조회해야 한다.
        foreach (var group in groupResults1)
        {
            // Key는 그룹핑된 요소의 속성이다.
            Console.WriteLine($"Gender : { group.Key }");

            foreach (var student in group)
            {
                Console.WriteLine($"  { student }");
            }
        }

        Console.WriteLine("--------------------------------------------------");

        // 그룹핑을 진행할 때 메서드 체이닝을 걸어 그룹의 속성 값에 따라 정렬하는 것도 가능하다.
        var groupResults2 = students
                                .GroupBy(student => student.Gender)
                                .OrderByDescending(group => group.Key);

        foreach (var group in groupResults2)
        {
            Console.WriteLine($"Gender : { group.Key }");

            foreach (var student in group)
            {
                Console.WriteLine($"  { student }");
            }
        }
    }
}

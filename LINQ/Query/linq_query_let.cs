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

        // LINQ 쿼리 구문을 이용할 때 let을 통해 변수 선언할 수 있다.
        // let 키워드는 from과 select절 사이에 위치하며 한 번 변수를 선언하면 where절이나 select절에서 사용할 수 있다.
        var results1 = from student in students
                       let average = student.Scores.Average()
                       where average > 5
                       select (student.Name, student.Age, Average: average);
        
        foreach (var result in results1)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }, { result.Average }");
        }

        Console.WriteLine("--------------------------------------------------");

        // let 키워드를 여러 개 구성해서 변수를 다수 선언하는 것도 가능하다.
        //
        // 1. from student : student 조회
        // 2. let gender : gender 변수 선언
        // 3. where gender == "M" : 조건 판별
        // 4. 조건 판별이 성공하면 5번으로 아니면 1번으로 이동
        // 5. let average : average 변수 선언
        // 6. where average > 5 : 조건 판별
        // 7. 조건 판별이 성공하면 8번으로 아니면 1번으로 이동
        // 8. select : 결과 반영
        var results2 = from student in students
                       let gender = student.Gender
                       where gender == "M"
                       let average = student.Scores.Average()
                       where average > 3
                       select (student.Name, student.Age, Average: average);
        
        foreach (var result in results2)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }, { result.Average }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 아래 results3는 results2와 논리적으로 동일한 결과를 반환하다.
        // 하지만 let 키워드와 where을 어떻게 구성하냐에 따라 코드의 실행 로직이 달라진다는 것을 알고 있어야 한다.
        //
        // 1. from student : student 조회
        // 2. let gender : gender 변수 선언
        // 3. let average : average 변수 선언
        // 4. where gender == "M" && average > 3 : 조건 판별
        // 5. 조건 판별이 성공하면 6번으로 아니면 1번으로 이동
        // 6. select : 결과 반영
        var results3 = from student in students
                       let gender = student.Gender
                       let average = student.Scores.Average()
                       where gender == "M" && average > 3
                       select (student.Name, student.Age, Average: average);
        
        foreach (var result in results3)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }, { result.Average }");
        }
    }
}

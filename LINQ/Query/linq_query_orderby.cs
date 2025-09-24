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

        // LINQ 쿼리 구문은 orderby 키워드를 통한 데이터 정렬을 수행할 수 있다.
        // orderby 키워드는 select 위에 위치해야 한다.
        // 적용되는 기본 형식은 오름차순(ascending)이다.
        var results1 = from student in students
                       orderby student.Age /*ascending*/
                       select (student.Name, student.Age);

        foreach (var result in results1)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 내림차순 정렬을 하고자 한다면 descending을 사용하면 된다.
        var results2 = from student in students
                       orderby student.Age descending
                       select (student.Name, student.Age);

        foreach (var result in results2)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");
        
        // 문자열도 정렬 기준의 대상이 될 수 있다.
        var results3 = from student in students
                       orderby student.Name descending
                       select (student.Name, student.Age);

        foreach (var result in results3)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        // orderby로 정렬할 때 같은 값으로 정렬되는 것이 있을 경우 후속 정렬을 어떻게 할 것인지도 지정할 수 있다.
        // 아래 예시는 나이를 오름차순 정렬을 하는 도중 같은 나이가 같은 값이 있을 경우, 후속으로 평균을 기준으로 내림차순 정렬을 진행하는 코드이다.
        var results4 = from student in students
                       let average = student.Scores.Average()
                       orderby student.Age, average descending
                       select (student.Name, student.Age, Average: average);
        
        foreach (var result in results4)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }, { result.Average }");
        }
    }
}

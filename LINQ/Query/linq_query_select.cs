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

    class SelectedStudent
    {
        public int Age { get; set; }
        public string Name { get; set; } = "";

        public override string ToString()
        {
            return $"SelectedStudent - Name : { Name }, Age : { Age }";
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

        // 단순히 students를 조회하기 위한 용도의 LINQ
        // linqStudents는 IEnumerable<Student> 타입으로 되어 있다.
        var linqStudents = from student in students
                           select student;

        foreach (var student in linqStudents)
        {
            Console.WriteLine(student);
        }

        Console.WriteLine("--------------------------------------------------");

        // 특정 요소만 받아오는 것도 가능하다.
        // 이 경우 results1은 IEnumerable<string> 타입으로 구성된다.
        var results1 = from student in students
                       select student.Name;

        foreach (var name in results1)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine("--------------------------------------------------");

        // 이런 식으로 2종류의 필드를 대상으로 가져오는 것은 불가능하다.
        // var results2 = from student in students
        //                select student.Name, student.Age;

        // 2종류의 필드를 가져오는 방식 중 하나는 new를 통한 익명 객체를 사용하는 것이다.
        // 마우스 인스펙터를 통해 results2의 타입을 조회하면 컴파일러가 정의한 익명 객체 타입을 담은 열거형인 것을 알 수 있다.
        var results2 = from student in students
                       select new { student.Name, student.Age };

        foreach (var result in results2)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 익명 타입을 사용할 때 필드의 이름을 변경하는 것도 가능하다.
        var results3 = from student in students
                       select new { MyName = student.Name, student.Age };

        foreach (var result in results3)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.MyName }, { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 컴파일러가 생성한 익명 타입으로 객체를 만들면 직접적으로 추적하기가 어렵다.
        // 이러한 대안 중 하나는 익명 타입을 쓰지 않고 직접 정의한 클래스를 활용하는 것이다.
        var results4 = from student in students
                       select new SelectedStudent{ Name = student.Name, Age = student.Age };

        foreach (var result in results4)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        // 익명 타입에 대한 또 다른 대안으로는 ValueTuple이 있다.
        // result5의 타입을 조회하면 IEnumerable<(string, int)>인 것을 확인할 수 있다.
        var results5 = from student in students
                       select (student.Name, student.Age);

        foreach (var result in results5)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.Name }, { result.Age }");
        }
        
        Console.WriteLine("--------------------------------------------------");

        // 익명 타입에 대한 또 다른 대안으로는 ValueTuple이 있다.
        // 익명 타입을 사용할 때와 마찬가지로 ValueTuple을 사용할 때도 필드의 이름을 바꿀 수 있다.
        var results6 = from student in students
                       select (MyName: student.Name, student.Age);

        foreach (var result in results6)
        {
            Console.Write($"{ result } | ");
            Console.WriteLine($"{ result.MyName }, { result.Age }");
        }
    }
}

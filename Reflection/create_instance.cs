// Update Date : 2025-09-27
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Diagnostics;
using System.Reflection;

class Program
{
    class Person
    {
        private int PrivateId { get; set; } = 100;

        public string Name { get; set; } = "John";
        public int Age { get; set; } = 30;

        public void ShowInfo()
        {
            Console.WriteLine($"Name : { Name }, Age : { Age }");
        }
    }

    static void Main()
    {
        // Reflection은 프로그램 실행 중(런타임) 타입/메타데이터를 들여다보고 조작할 수 있게 해주는 기능을 말한다.

        // 타입 정보를 가져오려면 typeof, GetType() 등을 사용하면 된다.
        // Type type = typeof(Person);

        // 타입 정보를 알면 인스턴스를 생성할 수 있다.
        // object? instance = Activator.CreateInstance(type);
        Person instance = Activator.CreateInstance<Person>(); // 어떤 인스턴스를 생성할 것인지 명시하는 것도 가능함.

        // 클래스 이름 정보 조회
        Console.WriteLine($"Class Name : { instance }");

        // 메서드 호출 테스트
        instance.ShowInfo();
    }
}

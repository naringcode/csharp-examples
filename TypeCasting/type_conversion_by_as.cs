// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Runtime;

class Program
{
    class Animal;
    class Dog : Animal { }
    class Cat : Animal { }

    static void Main()
    {
        object obj = "Hello C# Programming";

        // as 연산자를 사용하면 object 타입을 대상으로 안전하게 unboxing을 진행할 수 있다.
        // as는 변환이 실패하면 null을 반환한다.
        string? conv1 = obj as string;
        int? conv2 = obj as int?; // 값 타입 자체는 nullable이 될 수 없기에 물음표 기호를 붙여서 "type?"임을 명시해야 함.

        if (conv1 != null)
        {
            Console.WriteLine($"as string : { conv1 }");
        }
        else
        {
            Console.WriteLine("as string failed");
        }

        if (conv2 != null)
        {
            Console.WriteLine($"as int? : { conv2 }");
        }
        else
        {
            Console.WriteLine("as int? failed");
        }

        // as를 사용하여 클래스 상속 간 변환도 확인할 수 있다.
        Animal animal = new Dog();

        Dog? dog = animal as Dog;
        Cat? cat = animal as Cat;

        if (dog != null)
        {
            Console.WriteLine("animal as Dog succeeded");
        }
        else
        {
            Console.WriteLine("animal as Dog failed");
        }

        if (cat != null)
        {
            Console.WriteLine("animal as Cat succeeded");
        }
        else
        {
            Console.WriteLine("animal as Cat failed");
        }
    }
}

// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Runtime;
using System.Runtime.InteropServices;

class Program
{
    // C#의 Generic은 where을 통해 제약조건을 걸어 타입 안정성을 강화할 수 있다.
    // - struct : 값 타입만 허용
    // - class : 참조 형식만 허용
    // - new() : 매개변수 없는 생성자를 가진 타입만 허용
    // - <ClassType> : 특정 클래스 상속 타입만 허용
    // - <InterfaceType> : 특정 인터페이스 구현 타입만 허용

    public abstract class Animal
    {
        public abstract void MakeSound();
    }

    public interface IFlyable
    {
        void Fly();
    }

    class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("멍멍");
        }
    }

    class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("야옹");
        }
    }

    class Bird : Animal, IFlyable
    {
        public override void MakeSound()
        {
            Console.WriteLine("짹짹");
        }

        public void Fly()
        {
            Console.WriteLine("날아가요");
        }
    }

    // 값 타입만 허용
    public static T Add<T>(T a, T b)
        where T : struct
    {
        // 타입 검사를 런타임에서 결정하게 한다(컴파일러는 타입 검사를 건너뛰기에 이를 체크하지 않음).
        dynamic x = a;
        dynamic y = b;

        return x + y;
    }

    // 참조 형식만 허용
    public static void Swap<T>(ref T a, ref T b)
        where T : class
    {
        T temp = a;
        a = b;
        b = temp;
    }

    // 다음과 같이 제약 조건을 여러 개 묶는 것도 가능하다.
    public static T CreateInstance<T>()
        where T : class, new() // "interface, new()"와 같은 제약 조건을 지정하는 것도 가능함.
    {
        return new T(); // 제약 조건으로 new()를 건 상태이기에 사용 가능함.
    }

    // 제약 조건의 대상으로 특정 베이스 클래스를 지정하면 해당 클래스의 기능을 사용할 수 있다.
    public static T CreateAnimal<T>()
        where T : Animal, new()
    {
        T instance = new T();
        instance.MakeSound(); // T가 Animal을 상속받아 구현된다는 것이 확실하기에 사용 가능함.

        return instance;
    }

    // 마찬가지로 인터페이스를 제약 조건으로 지정하면 베이스 클래스를 지정하는 것과 유사하게 해당 인터페이스의 기능을 사용할 수 있다.
    public static T CreateFlyableAnimal<T>()
        where T : IFlyable, new()
    {
        T instance = new T();
        instance.Fly(); // T가 IFlyable의 기능을 구현한다는 것이 확실하기에 사용 가능함.

        return instance;
    }

    // 다음과 같이 여러 제네릭 타입을 구성하는 것도 가능하다.
    public static void PrintAnimalsSound<T1, T2, T3>(T1 a, T2 b, T3 c)
        where T1 : Animal
        where T2 : Animal
        where T3 : Animal
    {
        a.MakeSound();
        b.MakeSound();
        c.MakeSound();
    }

    // 특정 인터페이스로 구현된 것만 받고자 할 경우 코드를 아래와 같이 구성하면 된다.
    public static void PrintFlyableAnimal<T>(T a)
        where T : IFlyable
    {
        a.Fly();
    }

    // 클래스 구현 중 제네릭 타입을 적용하는 것도 가능하다.
    class GenericValue<T> where T : struct
    {
        private T _value;

        public void SetValue(T value)
        {
            _value = value;
        }

        public T GetValue()
        {
            return _value;
        }
    }

    static void Main()
    {
        int num1 = 100;
        int num2 = 200;
        Console.WriteLine($"{ num1 } + { num2 } = { num1 + num2 }");

        Console.WriteLine("--------------------------------------------------");

        string str1 = "Hello";
        string str2 = "World";
        Swap(ref str1, ref str2);
        Console.WriteLine($"{ str1 } { str2 }");

        Console.WriteLine("--------------------------------------------------");

        Animal dog = CreateInstance<Dog>();
        Animal cat = CreateAnimal<Cat>();
        Animal bird = CreateFlyableAnimal<Bird>();
        PrintAnimalsSound(dog, cat, bird);

        Console.WriteLine("--------------------------------------------------");

        Dog? realDog = dog as Dog;
        Cat? realCat = cat as Cat;
        Bird? realBird = bird as Bird;

        if (realDog != null)
        {
            // Dog는 IFlyable를 구현하지 않았기에 사용할 수 없다.
            // PrintFlyableAnimal(realDog);
        }

        if (realCat != null)
        {
            // Cat은 IFlyable를 구현하지 않았기에 사용할 수 없다.
            // PrintFlyableAnimal(realCat);
        }

        if (realBird != null)
        {
            // Bird는 IFlyable를 받아서 구현했기에 사용할 수 있다.
            PrintFlyableAnimal(realBird);
        }

        Console.WriteLine("--------------------------------------------------");

        // GenericValue<int> genericValue1 = new GenericValue<int>();
        var genericValue1 = new GenericValue<int>();
        var genericValue2 = new GenericValue<float>();

        // string은 값 형식이 아니기에 에러가 발생한다.
        // var genericValue3 = new GenericValue<string>();

        genericValue1.SetValue(1000);
        genericValue2.SetValue(3.14f);
        Console.WriteLine($"{ genericValue1.GetValue() }, { genericValue2.GetValue() }");
    }
}

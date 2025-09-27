// Update Date : 2025-09-27
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Reflection;

class Program
{
    class Person
    {
        // Fields
        private int _privateId = 100;

        // Properties
        private int PrivateCode { get; set; } = 11223344;
        public string Name { get; set; } = "John";
        public int Age { get; set; } = 30;

        public void ShowInfo()
        {
            Console.WriteLine($"Id : { _privateId }, Name : { Name }, Age : { Age }");
        }
    }

    static void Main()
    {
        // Reflection은 프로그램 실행 중(런타임) 타입/메타데이터를 들여다보고 조작할 수 있게 해주는 기능을 말한다.

        // 타입 정보를 가져오려면 typeof, GetType() 등을 사용하면 된다.
        // Type type = typeof(Person);
        Person person = new Person();
        Type type = person.GetType();

        // 클래스 이름 정보 조회
        Console.WriteLine($"Class Name : { type.Name }");

        Console.WriteLine("--------------------------------------------------");

        // 공개된 모든 프로퍼티 정보 조회
        Console.WriteLine("Public Properties : ");

        foreach (var prop in type.GetProperties())
        {
            // private 한정한 건 보이지 않는다.
            Console.WriteLine($"\tName : { prop.Name }");
        }

        // 특정 프로퍼티 정보를 조회하고자 한다면 플래그 값을 지정해야 한다.
        Console.WriteLine("Non-Public Properties : ");

        foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            // Public 플래그를 지정하지 않았기에 public 한정자를 넣은 건 조회되지 않는다.
            Console.WriteLine($"\tName : { prop.Name }");
        }

        // 이런 원리를 이용해 모든 프로퍼티를 조회하는 것도 가능하다.
        Console.WriteLine("All Properties : ");

        foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            Console.WriteLine($"\tName : { prop.Name }");
        }

        // 특정 프로퍼티 정보 조회
        PropertyInfo? birthdayProp = type.GetProperty("Birthday");
        if (birthdayProp == null)
        {
            Console.WriteLine("No Birthday Property");
        }
        else
        {
            Console.WriteLine($"Birthday Property : { birthdayProp.GetValue(person) }");
        }

        PropertyInfo? ageProp = type.GetProperty("Age");
        if (ageProp == null)
        {
            Console.WriteLine("No Age Property");
        }
        else
        {
            Console.WriteLine($"Age Property : { ageProp.GetValue(person) }");
        }

        // 프로퍼티의 값을 쓰는 것도 가능하다.
        person.ShowInfo(); // 갱신 이전 정보 출력

        PropertyInfo? nameProp = type.GetProperty("Name");
        nameProp?.SetValue(person, "Alice");
        person.ShowInfo();

        ageProp?.SetValue(person, 50);
        person.ShowInfo();

        Console.WriteLine("--------------------------------------------------");

        // 변수에 해당하는 필드 정보도 Reflection을 통해 조회할 수 있다.
        // 혼동 주의 : 프로퍼티는 필드를 감싸는 get과 set을 포함한 축약형임(프로퍼티는 변수가 아닌 메서드에 해당함).
        Console.WriteLine("All Fields : ");

        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            Console.WriteLine($"\tName : { field.Name }");
        }

        // private 한정자를 적용했어도 필드를 가져와 값을 수정할 수 있다.
        FieldInfo? idField = type.GetField("_privateId", BindingFlags.Instance | BindingFlags.NonPublic);
        idField?.SetValue(person, 314);
        person.ShowInfo();

        Console.WriteLine("--------------------------------------------------");

        // 클래스의 메서드 정보를 조회
        Console.WriteLine("Public Methods : ");

        foreach (var method in type.GetMethods())
        {
            Console.WriteLine($"\tName : { method.Name }");
        }

        // 프로퍼티와 동일한 원리로 플래그를 지정하여 특정 조건을 충족하는 메서드 정보를 조회할 수 있다.
        Console.WriteLine("Non-Public Methods : ");

        foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            Console.WriteLine($"\tName : { method.Name }");
        }

        // 메서드 정보를 가져와 호출하는 것도 가능하다.
        MethodInfo? showMethod = type.GetMethod("ShowInfo");

        if (showMethod != null)
        {
            showMethod?.Invoke(person, null);
        }
    }
}

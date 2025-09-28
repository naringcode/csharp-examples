// Update Date : 2025-09-28
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Reflection;

class Program
{
    [AttributeUsage(AttributeTargets.Class)]
    class VersionInfoAttribute : Attribute
    {
        public string? Description { get; set; }

        public string Author { get; }
        public string Version { get; }
        public string Date { get; }

        public VersionInfoAttribute(string author, string version, string date)
        {
            Author = author;
            Version = version;
            Date = date;
        }
    }

    static void ReadVersionInfo(Type type, Func<VersionInfoAttribute, bool>? predicate = null)
    {
        var attributes = type.GetCustomAttributes(typeof(VersionInfoAttribute), false);

        foreach (VersionInfoAttribute attr in attributes)
        {
            if (predicate?.Invoke(attr) == false)
                continue;

            Console.WriteLine($"Date : { attr.Date }");
            Console.WriteLine($"Class : { type.Name }");
            Console.WriteLine($"Author : { attr.Author }");
            Console.WriteLine($"Version : { attr.Version }");
            Console.WriteLine($"Description : { attr.Description }");

            Console.WriteLine();
        }
    }

    static void ReadVersionInfo(Type[] types, Func<VersionInfoAttribute, bool>? predicate = null)
    {
        foreach (var type in types)
        {
            ReadVersionInfo(type, predicate);
        }
    }

    [VersionInfo("naringcode", "1.0.0", "2025-04-01", Description = "사용자 모델 클래스 생성")]
    class User
    { }

    [VersionInfo("naringcode", "1.0.1", "2025-04-02", Description = "장바구니 모델 클래스")]
    class Cart
    { }

    [VersionInfo("gildong", "1.0.1", "2025-04-03", Description = "카트에 담긴 세부 정보 모델")]
    class CartItem
    { }

    static void Main()
    {
        // 어셈블리의 모든 타입 정보를 가져온다.
        var types = Assembly.GetExecutingAssembly().GetTypes();

        ReadVersionInfo(types, (attr) => attr.Date.CompareTo("2025-04-02") >= 0);

        Console.WriteLine("--------------------------------------------------\n");

        ReadVersionInfo(types, (attr) => attr.Author == "naringcode");

        Console.WriteLine("--------------------------------------------------\n");

        ReadVersionInfo(types, (attr) => attr.Version == "1.0.1");
    }
}

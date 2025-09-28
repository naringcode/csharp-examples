// Update Date : 2025-09-28
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Reflection;

interface ITransformerAttribute<T>
{
    T Transform(T value);
}

[AttributeUsage(AttributeTargets.Property)]
class ToUpperAttribute : Attribute, ITransformerAttribute<string>
{
    public string Transform(string value)
    {
        return value.ToUpper();
    }
}

[AttributeUsage(AttributeTargets.Property)]
class ToLowerAttribute : Attribute, ITransformerAttribute<string>
{
    public string Transform(string value)
    {
        return value.ToLower();
    }
}

[AttributeUsage(AttributeTargets.Property)]
class LeftAttribute : Attribute, ITransformerAttribute<string>
{
    public int Length { get; }

    public LeftAttribute(int length)
    {
        Length = length;
    }

    public string Transform(string value)
    {
        if (value.Length > Length)
            return value.Substring(0, Length);

        return value;
    }
}

static class AttributeExtension
{
    public static void ApplyAttributes(this object obj)
    {
        var properties = obj.GetType().GetProperties();

        foreach (var property in properties)
        {
            var attributes = property.GetCustomAttributes().OfType<ITransformerAttribute<string>>();

            foreach (var attribute in attributes)
            {
                string? value = property.GetValue(obj) as string;

                if (value == null)
                    continue;

                property.SetValue(obj, attribute.Transform(value));
            }
        }
    }
}

class User
{
    [ToUpper]
    public string Email { get; set; } = "";

    [ToLower]
    public string Name { get; set; } = "";
    
    [Left(3)]
    public string Phone { get; set; } = "";

    public override string ToString()
    {
      return $"Email : { Email }, Name : { Name }, Phone : { Phone }";
    }
}

class Program
{
    static void Main()
    {
        User user = new User
        {
            Email = "Test@tEsT.Com",
            Name = "NaRinGcoDe",
            Phone = "010-1234-5678"
        };

        Console.WriteLine(user);

        Console.WriteLine("--------------------------------------------------");

        user.ApplyAttributes();

        Console.WriteLine(user);
    }
}

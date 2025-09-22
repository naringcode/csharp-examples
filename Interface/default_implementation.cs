// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

IAnimal dog = new Dog();
IAnimal cat = new Cat();
IAnimal bird = new Bird();

dog.MakeSound();
cat.MakeSound();
bird.MakeSound();

interface IAnimal
{
    string Name { get; set; }
    string Sound { get; set; }

    // C# 8.0부터는 인터페이스의 메서드에 기본 구현(default implementation)을 제공할 수 있다.
    // 인터페이스의 메서드의 구현은 암묵적으로 public으로 처리한다.
    void MakeSound()
    {
        Console.WriteLine($"{ Name }이(가) { Sound }");
    }
}

class Dog : IAnimal
{
    public string Name { get; set; } = "강아지";
    public string Sound { get; set; } = "멍멍";
}

class Cat : IAnimal
{
    public string Name { get; set; } = "고양이";
    public string Sound { get; set; } = "야옹";
}

class Bird : IAnimal
{
    public string Name { get; set; } = "새";
    public string Sound { get; set; } = "짹짹";

    // 이런 식으로 구현 클래스에서 동작을 제공하는 것도 가능하다.
    public void MakeSound()
    {
        Console.WriteLine($"{ Name }이(가) 힘차게 { Sound }");
    }
}

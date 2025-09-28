// Update Date : 2025-09-28
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

// https://learn.microsoft.com/ko-kr/dotnet/csharp/advanced-topics/reflection-and-attributes/creating-custom-attributes

// System.Attribute를 상속하여 사용자 수준에서 직접 Attribute를 구성할 수 있다.
class BasicCustomAttribute : Attribute
{
    public string? Description { get; set; }

    public BasicCustomAttribute()
    { }

    public BasicCustomAttribute(string name)
    { }
}

// 매개변수에만 적용 가능한 Attribute
// Inherited가 true라면 상속 시 Attribute가 전파되며, false라면 선언된 코드 요소에만 국한된다(기본은 true임).
// 전파 여부는 Reflection을 통해 확인할 수 있다.
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
class ParamAttribute : Attribute
{ }

// AttributeUsage로 타겟을 지정할 때는 OR(|)로 묶을 수 있다.
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
class ClassPropMethodAttribute : Attribute
{ }

// 동일한 Attribute를 여러 번 적용시키고자 한다면 AllowMultiple가 true여야 한다.
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
class MultipleAttribute : Attribute
{ }

// 가장 기본이 되는 형식
[BasicCustom]
class MyClassA
{ }

// Attribute의 생성자에 매개변수를 넣는 방식
[BasicCustom("Info")]
class MyClassB
{ }

// 생성자 매개변수는 ":"로, 속성(프로퍼티)은 "="로 지정할 수 있다.
[BasicCustom(name: "Info", Description = "Desc")]
class MyClassC
{ }

// 사용자가 정의한 ParamAttribute 예시
class MyClassD
{
    public MyClassD([Param] int param)
    { }

    // ParamAttribute는 Parameter에만 적용하게 한정했다.
    // [Param]
    // public void Print()
    // { }
}

// 사용자가 정의한 ClassPropMethodAttribute 예시
[ClassPropMethod]
class MyClassE
{
    [ClassPropMethod]
    public string Name { get; set; } = "";

    [ClassPropMethod]
    public void Print()
    { }
}
    
class Program
{
    static void Main()
    {

    }
}

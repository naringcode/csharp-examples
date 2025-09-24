// Update Date : 2025-09-24
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

using System.Collections;

class Program
{
    // Enumerator(열거자)
    // - 컬렉션을 순회(열거)하기 위한 반복자 객체로 IEnumerable과 IEnumerator를 통해서 구현
    // - 컬렉션을 순회하기 위해 MoveNext()와 Current를 사용(Reset()은 사용되지 않는 편임)
    // - yield return이나 yield break를 사용하면 컴파일러 차원에서 IEnumerator를 구현한 상태 기계를 생성
    // - enum 키워드와 이름은 비슷하지만 직접적인 연관성은 없으니 주의
    //  - enum은 열거형 타입으로 상수 집합을 정의, Enumerator는 컬렉션 순회를 위한 일종의 도우미 객체

    // public interface IEnumerator<out T> : IEnumerator, IDisposable
    // {
    //     T Current { get; }
    // }

    // public interface IEnumerator
    // {
    //     object Current { get; }
    //     bool MoveNext();
    //     void Reset();
    // }

    // IEnumerator 인터페이스를 구현하지 않았어도 구현부에서 yield return를 사용하면
    // 컴파일러 차원에서 IEnumerator나 IEnumerator<T>를 구현한 상태 기계 클래스를 생성한다(익명 혹은 비공개 클래스).
    static IEnumerator<int> SimpleNumberEnumerator()
    {
        yield return 100;
        yield return 200;
        yield return 300;
        yield return 400;
        yield return 500;
    }

    static IEnumerator SimpleCoroutine()
    {
        Console.WriteLine("Step 1");

        yield return "Paused 1";

        Console.WriteLine("Step 2");

        yield return "Paused 2";

        Console.WriteLine("Done");
    }

    // public interface IEnumerable<out T> : IEnumerable
    // {
    //     IEnumerator<T> GetEnumerator();
    // }

    // public interface IEnumerable
    // {
    //     IEnumerator GetEnumerator();
    // }

    // IEnumerable 인터페이스를 상속하면 반드시 GetEnumerator()를 구현하라고 명시할 수 있다.
    // "GetEnumerator"라는 이름은 약속된 명칭이기에 IEnumerable 인터페이스를 상속하지 않아도 foreach를 사용할 수 있긴 하다.
    class MyCollection // : IEnumerable<int>
    {
        // "GetEnumerator"는 약속된 메서드 명칭으로 토시 하나라도 틀리면 foreach에 적용할 수 없다.
        public IEnumerator<int> GetEnumerator()
        {
            yield return 111;
            yield return 222;
            yield return 333;
            yield return 444;
            yield return 555;
        }

        // IEnumerator IEnumerable.GetEnumerator()
        // {
        //     return GetEnumerator();
        // }
    }

    // 마찬가지로 IEnumerable 인터페이스를 구현하지 않았어도 컴파일러 차원에서 GetEnumerator()를 반환하는 상태 기계 클래스를 생성한다.
    // 1. 컴파일러 차원에서 상태 기계 클래스를 자체적으로 만들고
    // 2. IEnumerable의 인터페이스 구현부인 "IEnumerator<T> GetEnumerator()"를 구현
    // 
    // GetEnumerable()를 호출하면 상태 기계 클래스를 생성하는 것이고, 해당 클래스 내부에 GetEnumerator()가 있으니 foreach에 적용할 수 있다.
    static IEnumerable<int> GetEnumerable()
    {
        yield return 1100;
        yield return 2200;
        yield return 3300;
        yield return 4400;
        yield return 5500;
    }

    static void Main()
    {
        // IEnumerator<int> simpleNumEnum = SimpleNumberEnumerator();
        var simpleNumEnum = SimpleNumberEnumerator(); // 함수를 실행하는 것이 아닌 기능을 담은 객체를 반환하는 것임.

        // MoveNext()로 순회하고 Current로 조회한다.
        while (simpleNumEnum.MoveNext()) // MoveNext()이 반환하는 값의 자료형은 bool
        {
            Console.WriteLine(simpleNumEnum.Current);
        }

        Console.WriteLine("--------------------------------------------------");

        // Enumerator는 일반 C# 환경에서 코루틴을 적용하고자 할 때 유용하게 사용할 수 있다.
        // IEnumerator simpleCoroutine = SimpleCoroutine();
        var simpleCoroutine = SimpleCoroutine();

        while (simpleCoroutine.MoveNext())
        {
            Console.WriteLine("...while loop...");

            Console.WriteLine(simpleCoroutine.Current);
        }

        Console.WriteLine("--------------------------------------------------");

        MyCollection myCollection = new MyCollection();

        // 내부에서 GetEnumerator()를 호출한 다음, MoveNext()와 Current를 사용해 순회한다.
        foreach (var elem in myCollection)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();

        Console.WriteLine("--------------------------------------------------");

        // GetEnumerable() 쪽에 작성한 주석을 참고하도록 한다.
        foreach (var elem in GetEnumerable())
        {
            Console.Write($"{ elem } ");
        }
    }
}

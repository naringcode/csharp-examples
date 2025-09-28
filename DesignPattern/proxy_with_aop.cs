// Update Date : 2025-09-28
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

// AOP(Aspect-Oriented Programming, 관점 지향 프로그래밍)
// - 핵심 관심사와 부가적인 관심사(cross-cutting concerns, 횡단 관심사)를 분리해서 관리하는 프로그래밍 패러다임
// - 핵심 관심사 : 애플리케이션이 실제 수행해야 하는 비즈니스 로직임.
// - 횡단 관심사 : 여러 곳에서 공통으로 필요하지만 비즈니스 로직 자체는 아님(로깅, 보안, 트랜잭션 등).
//
// 여러 모듈이나 계층에 걸쳐 반복적으로 나타나는 공통된 기능을 횡단 관심사로 분리하면
// 중복 코드를 제거할 수 있으며, 가독성과 유지보수성을 높일 수 있다.

// Proxy 패턴(구조적으로는 Decorator와 유사함)
// - 객체 접근을 가로채는 중계자를 두는 방식
// - 실제 객체(핵심 관심사)에 접근하기 전후에 무언가(횡단 관심사)를 끼워넣을 수 있다는 점에서 AOP와 다소 겹치는 부분이 있음.

// Proxy vs Decorator
// - Proxy 패턴은 접근을 제어하거나 중계에 초점을, Decorator 패턴은 기능 확장 및 추가에 중점을 둠.
// - Proxy 패턴은 본체에 대한 수행 전후에 조건 검사 및 보안 여부를 진행하거나 무언가를 중계할 때, Decorator 패턴은 본체 기능에 추가적인 기능을 부여할 때 사용함.
// - Proxy 패턴은 객체 사용을 사용하기 전에 중간에 대리자를 두고, Decorator 패턴은 객체에 기능이 추가된 것임.
// - Proxy 패턴은 일반적으로 객체를 한 번만 감싸지만, Decorator 패턴은 여러 개를 중첩해서 합성함.
// - 두 패턴의 기능은 거의 유사하기에 의도에 따른 관점의 차이 정도로 보도록 하자.

// NuGet : https://github.com/castleproject/Core

using Castle.DynamicProxy;
using System.Diagnostics;

public class LoggingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        string className  = invocation.TargetType!.Name;
        string methodName = invocation.Method.Name;

        // 실행 및 로그
        Console.WriteLine($"[{ DateTime.Now }] Class : { className }, Method : { methodName } - Started");

        invocation.Proceed();
        
        Console.WriteLine($"[{ DateTime.Now }] Class : { className }, Method : { methodName } - Completed");
    }
}

public class TimingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        string className  = invocation.TargetType!.Name;
        string methodName = invocation.Method.Name;

        // 실행 및 로그
        var stopwatch = Stopwatch.StartNew();
        
        Console.WriteLine($"Started - Class : { className }, Method : { methodName }");

        invocation.Proceed();
        stopwatch.Stop();
        
        Console.WriteLine($"Completed - Class : { className }, Method : { methodName }, Execution Time : { stopwatch.ElapsedMilliseconds }");
    }
}

public static class ProxyExtensions
{
    public static T CreateProxy<T>(this T target, params IInterceptor[] interceptors)
        where T : class
    {
        var proxyGenerator = new ProxyGenerator();
        var myServiceProxy = proxyGenerator.CreateClassProxyWithTarget(target, interceptors);

        return myServiceProxy;
    }
}

public class MyService
{
    // ProxyGenerator를 통해 생성할 경우 virtual 키워드를 붙여야 한다.
    public virtual void DoSomething()
    {
        Thread.Sleep(10);

        Console.WriteLine("Do Something...");
    }

    public virtual int Add(int x, int y)
    {
        Thread.Sleep(10);

        Console.WriteLine("Add() Called");

        return x + y;
    }
}

class Program   
{
    static void Main()
    {
        var proxyGenerator = new ProxyGenerator();
        var myServiceProxy = proxyGenerator.CreateClassProxyWithTarget(new MyService(), new LoggingInterceptor());

        myServiceProxy.DoSomething();
        Console.WriteLine();

        int ret = myServiceProxy.Add(10, 20);
        Console.WriteLine($"result : { ret }");

        Console.WriteLine("--------------------------------------------------");
        
        var myServiceProxy2 = proxyGenerator.CreateClassProxyWithTarget(new MyService(), new LoggingInterceptor(), new TimingInterceptor());

        myServiceProxy2.DoSomething();
        Console.WriteLine();

        int ret2 = myServiceProxy2.Add(10, 20);
        Console.WriteLine($"result : { ret2 }");

        Console.WriteLine("--------------------------------------------------");

        var myServiceProxy3 = new MyService().CreateProxy(new LoggingInterceptor(), new TimingInterceptor());
        
        myServiceProxy3.DoSomething();
        Console.WriteLine();

        int ret3 = myServiceProxy3.Add(10, 20);
        Console.WriteLine($"result : { ret3 }");
    }
}

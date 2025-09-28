// Update Date : 2025-09-29
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

// NuGet : https://www.nuget.org/packages/microsoft.extensions.dependencyinjection/

using Microsoft.Extensions.DependencyInjection;

public interface ILogger
{
    void Log(string message);
}

public interface IMathService
{
    int Add(int x, int y);
}

public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[Log] { DateTime.Now } : { message }");
    }
}

public class MathService : IMathService
{
    public int Add(int x, int y)
    {
        return x + y;
    }
}

public class MyService
{
    private readonly IMathService _mathService;
    private readonly ILogger _logger;

    public MyService(IMathService mathService, ILogger logger)
    {
        _mathService = mathService;
        _logger = logger;
    }

    public int Add(int x, int y)
    {
        _logger.Log("Add() 호출");

        int ret = _mathService.Add(x, y);

        _logger.Log($"Add() 호출 결과 : { ret }");

        return ret;
    }
}

class Program   
{
    static void Main()
    {
        // 의존성 주입을 사용하는 이유 중 하나는 결합도를 낮추기 위함인데 클래스 자체를 전달하는 방식은 결합도가 높다.
        // 따라서 클래스를 전달하는 방식 대신 인터페이스 구현하여 의존성을 주입하는 방식으로 변경할 필요가 있다.
        // 의존성을 주입할 때는 결합도를 낮추기 위해 클래스보단 인터페이스를 사용하는 것이 일반적이다.
        // 만약 결합도를 낮출 필요가 없으면 인터페이스가 아닌 클래스를 적용하여 주입하는 경우도 있다.
        
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록 함수
        services.AddTransient<ILogger, Logger>(); // ILogger 서비스 등록(의존성 주입 시 Logger는 ILogger로 할당됨)
        services.AddTransient<IMathService, MathService>(); // IMathService 서비스 등록(의존성 주입 시 MathService는 IMathService로 할당됨)
        services.AddTransient<MyService>(); // MyService 서비스 등록

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성
        MyService myService = serviceProvider.GetRequiredService<MyService>();

        // 테스트
        myService.Add(10, 20);
    }
}

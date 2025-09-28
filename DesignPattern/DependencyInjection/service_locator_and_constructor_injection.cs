// Update Date : 2025-09-29
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

// NuGet : https://www.nuget.org/packages/microsoft.extensions.dependencyinjection/

using Microsoft.Extensions.DependencyInjection;

namespace A
{
    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[Log] { DateTime.Now } : { message }");
        }
    }
    
    public class MathService
    {
        private Logger _logger;
    
        public MathService(Logger logger)
        {
            _logger = logger;
        }
    
        public int Add(int x, int y)
        {
            _logger.Log("Add() 호출");
    
            int ret = x + y;
    
            _logger.Log($"Add() 호출 결과 : { ret }");
    
            return ret;
        }
    }
    
    public class MyService
    {
        private readonly MathService _mathService;
    
        public MyService(MathService mathService)
        {
            _mathService = mathService;
        }
    
        public int Add(int x, int y)
        {
            return _mathService.Add(x, y);
        }
    }
}

namespace B
{
    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[Log] { DateTime.Now } : { message }");
        }
    }
    
    public class MathService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
    
    public class MyService
    {
        private readonly MathService _mathService;
        private readonly Logger _logger;
    
        public MyService(MathService mathService, Logger logger)
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
}

class Program   
{
    static void RunA()
    {
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록 함수와 의존성 주입 생명주기
        // - AddTransient() : 의존성 주입 시 매번 새로운 인스턴스를 생성함.
        // - AddSingleton() : 애플리케이션 전체에서 하나의 인스턴스만 생성함.
        // - AddScoped() : 의존성 주입 시 지역이 다르면 새로운 인스턴스를 생성함(동일 스코프 내에선 싱글톤처럼 작동함).
        services.AddTransient<A.Logger>(); // Logger 서비스 등록
        services.AddTransient<A.MathService>(); // MathService 서비스 등록
        services.AddTransient<A.MyService>(); // MyService 서비스 등록

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성
        // Service Locator Pattern : 여러 개의 서비스를 Service Locator라는 중앙 관리 객체가 관리하고, 클라이언트는 이 로케이터를 통해 서비스에 접근하는 패턴
        // - 서비스 객체를 한 곳에서 관리
        // - 서비스 생성 및 생명주기를 로케이터가 관리
        // - 클라이언트는 특정 서비스 필요시 해당 로케이터에 요청
        A.MyService myService = serviceProvider.GetRequiredService<A.MyService>();

        // MathService를 넣어주는 부분이 보이지 않지만 기능을 사용할 수 있다.
        // 이는 서비스 생성 당시 서비스 로케이터가 DI 컨테이너를 통해 의존성 찾아서 주입해주기 때문에 그런 것이다(생성자 주입).
        myService.Add(10, 20);
    }

    static void RunB()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddTransient<B.Logger>();
        services.AddTransient<B.MathService>();
        services.AddTransient<B.MyService>();

        // 서비스 생성자 방식이 바뀌더라도 의존성을 알아서 찾은 다음 객체를 주입한다.
        // 즉, 서비스 제공 방식이 변동되더라도 사용하는 코드는 그대로 둔 상태로 작업하는 것이 가능하다.
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        B.MyService myService = serviceProvider.GetRequiredService<B.MyService>();

        myService.Add(100, 200);
    }

    static void Main()
    {
        RunA();

        Console.WriteLine("--------------------------------------------------");

        RunB();
    }
}

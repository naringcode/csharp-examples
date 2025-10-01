// Update Date : 2025-09-29
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

// NuGet : https://www.nuget.org/packages/microsoft.extensions.dependencyinjection/

using Microsoft.Extensions.DependencyInjection;

namespace A
{
    public class ServiceA
    {
        public ServiceA()
        {
            Console.WriteLine("===== ServiceA() =====");
        }
    }

    public class ServiceB
    { }

    public class MainService
    {
        public MainService(ServiceA serviceA, ServiceB serviceB)
        { }
    }
}

namespace B
{
    public class ServiceA
    {
        public ServiceA()
        {
            Console.WriteLine("===== ServiceA() =====");
        }
    }

    public class ServiceB
    {
        public ServiceB(ServiceA serviceA)
        { }
    }
    
    public class MainService
    {
        public MainService(ServiceA serviceA, ServiceB serviceB)
        { }
    }
}

namespace C
{
    public class SingletonClass
    {
        public SingletonClass()
        {
            Console.WriteLine("===== SingletonClass() =====");
        }
    }

    public class ScopedClass
    {
        public ScopedClass()
        {
            Console.WriteLine("===== ScopedClass() =====");
        }
    }

    public class TransientClass
    {
        public TransientClass()
        {
            Console.WriteLine("===== TransientClass() =====");
        }
    }
}

class Program   
{
    static void RunA()
    {
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록 함수
        services.AddTransient<A.ServiceA>();
        services.AddTransient<A.ServiceB>();
        services.AddTransient<A.MainService>();

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성
        A.MainService myService1 = serviceProvider.GetRequiredService<A.MainService>();
        A.MainService myService2 = serviceProvider.GetRequiredService<A.MainService>();
        A.MainService myService3 = serviceProvider.GetRequiredService<A.MainService>();

        // AddTransient() 방식은 의존성 주입 시 매번 새로운 인스턴스를 생성하기에 ServiceA 생성자가 3번 호출된다.
    }

    static void RunB()
    {
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록 함수
        services.AddSingleton<A.ServiceA>();
        services.AddTransient<A.ServiceB>();
        services.AddTransient<A.MainService>();

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성
        A.MainService myService1 = serviceProvider.GetRequiredService<A.MainService>();
        A.MainService myService2 = serviceProvider.GetRequiredService<A.MainService>();
        A.MainService myService3 = serviceProvider.GetRequiredService<A.MainService>();

        // AddSingleton() 방식은 애플리케이션 전체에서 하나의 인스턴스만 생성하는 방식으로 의존성을 주입하기에 MainService 생성자가 한 번 호출된다.
    }

    static void RunC()
    {
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록 함수
        services.AddTransient<B.ServiceA>();
        services.AddTransient<B.ServiceB>();
        services.AddTransient<B.MainService>();

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성
        B.MainService myService = serviceProvider.GetRequiredService<B.MainService>();

        // AddTransient() 방식을 상기하면 ServiceB와 MainService의 ServiceA가 서로 다른 인스턴스임을 알 수 있다.
    }

    static void RunD()
    {
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록 함수
        services.AddSingleton<B.ServiceA>();
        services.AddTransient<B.ServiceB>();
        services.AddTransient<B.MainService>();

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성
        B.MainService myService = serviceProvider.GetRequiredService<B.MainService>();

        // ServiceA는 AddSingleton() 방식으로 등록했기에 ServiceB와 MainService의 ServiceA는 같은 인스턴스이다.
    }

    static void RunE()
    {
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록 함수
        services.AddSingleton<C.SingletonClass>();
        services.AddScoped<C.ScopedClass>();
        services.AddTransient<C.TransientClass>();

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성

        Console.WriteLine("** 1 **");

        using (var scope = serviceProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<C.SingletonClass>();
            scope.ServiceProvider.GetRequiredService<C.ScopedClass>();
            scope.ServiceProvider.GetRequiredService<C.TransientClass>();

            scope.ServiceProvider.GetRequiredService<C.SingletonClass>();
            scope.ServiceProvider.GetRequiredService<C.ScopedClass>();
            scope.ServiceProvider.GetRequiredService<C.TransientClass>();
        }

        Console.WriteLine("** 2 **");

        using (var scope = serviceProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<C.SingletonClass>();
            scope.ServiceProvider.GetRequiredService<C.ScopedClass>();
            scope.ServiceProvider.GetRequiredService<C.TransientClass>();

            scope.ServiceProvider.GetRequiredService<C.SingletonClass>();
            scope.ServiceProvider.GetRequiredService<C.ScopedClass>();
            scope.ServiceProvider.GetRequiredService<C.TransientClass>();
        }

        Console.WriteLine("** 3 **");

        serviceProvider.GetRequiredService<C.SingletonClass>();
        serviceProvider.GetRequiredService<C.ScopedClass>();
        serviceProvider.GetRequiredService<C.TransientClass>();

        serviceProvider.GetRequiredService<C.SingletonClass>();
        serviceProvider.GetRequiredService<C.ScopedClass>();
        serviceProvider.GetRequiredService<C.TransientClass>();

        Console.WriteLine("** 4 **");

        using (var scope = serviceProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<C.SingletonClass>();
            scope.ServiceProvider.GetRequiredService<C.ScopedClass>();
            scope.ServiceProvider.GetRequiredService<C.TransientClass>();

            scope.ServiceProvider.GetRequiredService<C.SingletonClass>();
            scope.ServiceProvider.GetRequiredService<C.ScopedClass>();
            scope.ServiceProvider.GetRequiredService<C.TransientClass>();
        }

        // SingletonClass는 생에 한 번만 생성되고, ScopedClass는 스코프 단위로 생성되고, TransientClass는 매번 생성된다.
    }

    static void Main()
    {
        RunA();

        Console.WriteLine("--------------------------------------------------");

        RunB();

        Console.WriteLine("--------------------------------------------------");

        RunC();

        Console.WriteLine("--------------------------------------------------");

        RunD();

        Console.WriteLine("--------------------------------------------------");

        RunE();
    }
}

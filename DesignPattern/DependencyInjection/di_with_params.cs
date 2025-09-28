// Update Date : 2025-09-29
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

// NuGet : https://www.nuget.org/packages/microsoft.extensions.dependencyinjection/

using Microsoft.Extensions.DependencyInjection;

public interface IParamService
{
    void Print();
}

public class ParamService : IParamService
{
    int _num;

    public ParamService(int num)
    {
        _num = num;
    }

    public void Print()
    {
        Console.WriteLine($"[{ DateTime.Now }] : { _num }");
    }
}

public class MainService
{
    IParamService _paramService;

    public MainService(IParamService paramService)
    {
        _paramService = paramService;
    }

    public void Print()
    {
        _paramService.Print();
    }
}

class Program   
{
    static void Main()
    {
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록 함수
        services.AddTransient<IParamService>(provider =>
        {
            // provider는 BuildServiceProvider()로 반환한 provider와 동일하다.
            // 따라서 여기서도 서비스 로케이터 패턴으로 객체를 생성할 수 있다.
            // SomeService someService = provider.GetRequiredService<SomeService>();

            // Logger의 생성자는 인자를 받기 때문에 람다식을 통해 생성 방식을 전달해야 한다.
            return new ParamService(100);
        });
        services.AddTransient<MainService>();

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성
        MainService mainService = serviceProvider.GetRequiredService<MainService>();

        // 테스트
        mainService.Print();
    }
}

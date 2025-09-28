// Update Date : 2025-09-29
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

// NuGet : https://www.nuget.org/packages/microsoft.extensions.dependencyinjection/

using Microsoft.Extensions.DependencyInjection;

public class User
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}

public class Product
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}

public interface IRepository<T>
{
    void Add(T item);
    List<T> GetAllItems();
}

public class Repository<T> : IRepository<T>
{
    readonly List<T> _items = [];

    public void Add(T item)
    {
        _items.Add(item);
    }

    public List<T> GetAllItems()
    {
        return _items;
    }
}

public class MainService
{
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<Product> _productRepo;

    public MainService(IRepository<User> userRepo, IRepository<Product> productRepo)
    {
        _userRepo = userRepo;
        _productRepo = productRepo;
    }

    public void Run()
    {
        _userRepo.Add(new User { Name = "naring", Age = 20 });
        _userRepo.Add(new User { Name = "code", Age = 30 });

        foreach (var item in _userRepo.GetAllItems())
        {
            Console.WriteLine($"[User] Name : { item.Name }, Age : { item.Age }");
        }

        Console.WriteLine("--------------------------------------------------");

        _productRepo.Add(new Product { Name = "air circulator", Price = 40_000 });
        _productRepo.Add(new Product { Name = "computer", Price = 700_000 });

        foreach (var item in _productRepo.GetAllItems())
        {
            Console.WriteLine($"[Product] Name : { item.Name }, Price : { item.Price }");
        }
    }
}

class Program   
{
    static void Main()
    {
        // DI 컨테이너
        IServiceCollection services = new ServiceCollection();

        // 서비스 등록(Closed Generic, 타입 매개변수가 아직 구체적으로 채워지지 않은 제네릭 타입)
        // services.AddTransient<IRepository<User>, Repository<User>>();
        // services.AddTransient<IRepository<Product>, Repository<Product>>();

        // 서비스 등록(Open Generic, 타입 매개변수가 모두 구체적으로 채워진 제네릭 타입)
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>)); // Entity Framework의 ORM을 사용할 때 주로 사용하는 방식임.

        // 메인 서비스 등록
        services.AddTransient<MainService>();

        // DI 컨테이너 빌드
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // 서비스 로케이터 패턴으로 객체 생성
        MainService mainService = serviceProvider.GetRequiredService<MainService>();

        // 테스트
        mainService.Run();
    }
}

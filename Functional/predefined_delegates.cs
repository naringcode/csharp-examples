// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    // 사전에 정의된 deletage 유형
    // 찾아보면 더 있지만 중요한 건 사전에 정의된 delegate 유형이 있다는 것을 아는 것이니 여기까지만...
    // - Func
    // - Action
    // - Predicate
    // - Comparison
    // - Converter
    // - EventHandler
    
    static int Add(int x, int y)
    {
        Console.WriteLine($"{ x } + { y } = { x + y }");

        return x + y;
    }

    static int Sub(int x, int y)
    {
        Console.WriteLine($"{ x } - { y } = { x - y }");

        return x - y;
    }

    static bool IsEven(int x)
    {
        return x % 2 == 0;
    }

    public class Button
    {
        public event EventHandler? OnClicked;

        public void Click()
        {
            OnClicked?.Invoke(this, EventArgs.Empty);
        }
    }

    // https://learn.microsoft.com/ko-kr/dotnet/api/system.eventargs?view=net-9.0
    // 데이터를 전달하기 위한 커스텀 EventArgs를 구현할 때는 상속을 통해 구현하는 것이 일반적이다.
    public class ValueChangedEventArgs : EventArgs
    {
        public int OldValue { get; init; }
        public int NewValue { get; init; }

        public ValueChangedEventArgs(int oldValue, int newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class SomeValue
    {
        // public event EventHandler? OnValueChanged;
        public event EventHandler<ValueChangedEventArgs>? OnValueChanged;

        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (_value != value)
                {
                    ValueChangedEventArgs eventArgs = new ValueChangedEventArgs(_value, value);

                    _value = value;

                    OnValueChanged?.Invoke(this, eventArgs);
                }
            }
        }

        public SomeValue(int value = 0)
        {
            Value = value;
        }
    }

    static void Main()
    {
        // Func
        // - 반환값이 있는 메서드 참조
        // - 마지막 타입으로는 반환값 지정, 앞에 있는 타입은 매개변수 입력 타입
        // - 매개변수 입력은 최대 16개까지 지원
        Func<int, int, int> func = Add;

        var ret = func(100, 30);
        Console.WriteLine($"{ ret }");

        func += Sub;
        func += (int x, int y) => { return x - y; };

        ret = func(100, 40);
        Console.WriteLine($"{ ret }");

        Func<int> randFunc = () => new Random().Next(1, 100);

        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{ randFunc.Invoke() } ");
        }

        Console.WriteLine();
        Console.WriteLine("--------------------------------------------------");

        // Action
        // - 반환값이 없는 메서드 참조
        // - 매개변수는 최대 16개까지 지원
        Action<string, int>? action = null;

        action += (string name, int age) => { Console.WriteLine($"name : { name }, age : { age }"); };
        action += (string name, int age) => { Console.WriteLine($"이름 : { name }, 나이 : { age }"); };
        action += (string name, int age) => { Console.WriteLine($"名 : { name }, 年 : { age }"); };

        action?.Invoke("John", 25);

        Console.WriteLine("--------------------------------------------------");

        // Predicate
        // - 매개변수 1개를 받고 bool을 반환하는 메서드 참조
        // - 객체가 특정 조건을 만족하는지 검사하기 위해 사용
        // - FindAll, Exists, RemoveAll 등
        Predicate<int> isEvenPred = IsEven;

        Console.WriteLine($"is 100 even? { isEvenPred(100) }");
        Console.WriteLine($"is 111 even? { isEvenPred(111) }");

        Console.WriteLine("--------------------------------------------------");

        // Comparison
        // - 두 값을 비교해 정렬 순서를 결정하는 메서드 참조
        // - 오름차순 기준
        //  - 음수(-1) : x < y
        //  - 0 : x == y
        //  - 양수(+1) : x > y
        Comparison<int> comp = (int x, int y) => { return x.CompareTo(y); };
        
        Console.WriteLine($"compare 5 and 10 : { comp(5, 10) }");
        Console.WriteLine($"compare 10 and 10 : { comp(10, 10) }");
        Console.WriteLine($"compare 10 and 5 : { comp(10, 5) }");

        List<int> list = new List<int>();
        
        for (int i = 0; i < 10; i++)
        {
            list.Add(randFunc());
        }

        // 오름차순 정렬
        list.Sort(comp);

        foreach (int elem in list)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();

        // 내림차순 정렬
        comp = (int x, int y) => {
            // return y.CompareTo(x);

            if (x == y)
                return 0;

            if (x < y)
                return 1;

            return -1; // x > y
        };

        list.Sort(comp);

        foreach (int elem in list)
        {
            Console.Write($"{ elem } ");
        }

        Console.WriteLine();

        Console.WriteLine("--------------------------------------------------");

        // Converter
        // - 하나의 값을 다른 형식으로 변환하는 메서드 참조
        // - Func<TInput, TOutput>와 형태는 같지만 "자료 변환"이라는 의미를 명확히 전달
        Converter<int, string> intToString = (int x) => { return Convert.ToString(x); };

        object ret2 = intToString(123);
        var ret3 = ret2 switch
        {
            int x => $"정수 : { x }",
            string s => $"문자열 : { s }",
            _ => "불명"
        };

        Console.WriteLine(ret3);

        // 숫자 -> 문자열
        intToString = (int x) => { return $"Number { x }"; };
        list.Sort();

        List<string> strList = list.ConvertAll(intToString);

        Console.WriteLine(string.Join(", ", strList));

        Console.WriteLine("--------------------------------------------------");

        // https://medium.com/@serasiyasavan14/mastering-predefined-delegates-in-c-simplify-your-code-and-boost-productivity-ce602bbc014f
        // EventHandler
        // - .Net의 이벤트 패턴 표준을 표현하기 위한 delegate
        // - 이벤트를 발생시킨 sender와 이벤트에 적용되는 데이터를 전달
        // - 일반적으로 event 키워드와 함께 사용되어 UI나 비동기 작업을 진행
        Button button = new Button();

        button.OnClicked += (sender, args) => Console.WriteLine("Button Clicked! A");
        button.OnClicked += (sender, args) => Console.WriteLine("Button Clicked! B");

        button.Click();

        SomeValue someValue = new SomeValue(100);

        someValue.OnValueChanged += (sender, e) =>
        {
            Console.WriteLine($"Value Changed : { e.OldValue } -> { e.NewValue }");
        };

        someValue.Value = 200;
        someValue.Value = 300;
    }
}

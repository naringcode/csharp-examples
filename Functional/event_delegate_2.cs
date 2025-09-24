// Update Date : 2025-09-23
// OS : Windows 10 64bit
// Program : Visual Studio 2022
// Version : C#(.Net 8.0)
// Configuration : Debug-Any

class Program
{
    delegate void ValueChangedHandler(int result, string msg);

    class Calculator
    {
        // event 방식 예시
        // Calculator는 Notify를 통한 알림만 진행하고, 핸들러에 구독을 등록하는 방식은 외부에서 처리한다.
        public event ValueChangedHandler? OnValueChanged;

        private int _value;

        public void Add(int x)
        {
            _value += x;

            OnValueChanged?.Invoke(_value, $"{ x }을(를) 더해서 { _value }이(가) 되었다.");
        }

        public void Sub(int x)
        {
            _value -= x;
            
            OnValueChanged?.Invoke(_value, $"{ x }을(를) 빼서 { _value }이(가) 되었다.");
        }
    }

    static void Subscribe_OnValueChanged(int result, string msg)
    {
        Console.WriteLine($"현재 값 : { result }, 메시지 : { msg } ");
    }

    static void Main()
    {
        Calculator calculator = new Calculator();

        // 구독하기
        calculator.OnValueChanged += Subscribe_OnValueChanged;

        calculator.Add(10);
        calculator.Add(20);

        // 해지하기
        calculator.OnValueChanged -= Subscribe_OnValueChanged;

        calculator.Sub(5);
        calculator.Sub(7);
    }
}

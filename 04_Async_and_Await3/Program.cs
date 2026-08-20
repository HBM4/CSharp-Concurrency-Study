// 순서 제어 예시

//입력을 기다리는 중... : ...
//연산을 수행하는 중... : 연산용 임시 데이터
//출력을 표시하는 중... : 출력용 임시 데이터
//출력 완료
//입력 완료
//연산 완료

//모든 작업 완료
namespace _04_Async_and_Await3
{
    internal class Program
    {
        static async Task Main()
        {
            await RunOperations();
        }

        static async Task RunOperations()
        {
            // 입력, 연산, 출력을 하는 3개의 독립적인 작업
            // 각 메서드 호출 시점에 동기 코드(첫 Console.WriteLine)가 즉시 실행되고
            // await Task.Delay 지점에서 제어권이 반환되므로,
            // 아래 3줄이 먼저 순서대로(입력→연산→출력) 출력된다.
            Task<string> inputTask = InputAsync();
            Task<int> computationTask = CalculateAsync("연산용 임시 데이터");
            Task outputTask = OutputAsync("출력용 임시 데이터");

            // 이후 각 Task는 지정된 Delay 시간이 지나야 완료된다.
            // 출력(1초), 입력(1초)은 지연 시간이 같지만
            // 출력 작업이 나중에 시작되었음에도 먼저 완료 로그를 남기는 이유는
            // 스레드 풀 스케줄링과 타이머 콜백 처리 순서에 따라
            // 완료 순서가 근소하게 뒤바뀔 수 있기 때문이다(비결정적 요소 존재).
            // 연산(2초)은 지연 시간이 가장 길기 때문에 가장 마지막에 완료된다.

            // 모든 작업이 완료될 때까지 기다립니다.
            await Task.WhenAll(inputTask, computationTask, outputTask);

            Console.WriteLine($"\n입력 결과: {inputTask.Result}");
            Console.WriteLine($"연산 결과: {computationTask.Result}");
            Console.WriteLine("모든 작업 완료");
        }

        static async Task<string> InputAsync()
        {
            Console.WriteLine("입력을 기다리는중...: ...");
            await Task.Delay(1000);
            Console.WriteLine("입력 완료");

            return "입력 데이터";
        }

        static async Task<int> CalculateAsync(string data)
        {
            Console.WriteLine($"연산을 수행하는 중...: {data}");
            await Task.Delay(2000);
            Console.WriteLine("연산 완료");

            return 100;
        }

        static async Task OutputAsync(string data)
        {
            Console.WriteLine($"출력을 표시하는 중...: {data}");
            await Task.Delay(1000);
            Console.WriteLine("출력 완료");
        }
    }
}

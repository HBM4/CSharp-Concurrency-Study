using System.ComponentModel;

namespace _07_Task_CancellationToken
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // CancellationTokenSource를 사용하여 CancellationToken 생성
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task task = Task.Run(() => DoWork(cancellationToken), cancellationToken);

            // 특정 이벤트를 받으면 CancellationToken을 취소하여 Task 종료
            // 이 예제에서는 간단히 Enter 키를 입력하면 Task가 종료되도록 설정
            Console.WriteLine("Task를 취소하려면 엔터~");
            Console.ReadLine();

            cancellationTokenSource.Cancel(); // CancellationToken 취소

            try
            {
                // Task가 완료될 때까지 대기
                task.Wait();
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Any(e => e is OperationCanceledException))
                {
                    Console.WriteLine("Throw Cancellation: 예외 발생으로 Task가 종료되었습니다.");
                }
                else
                {
                    throw; // 다른 예외는 다시 던짐
                }
            }

            Console.ReadLine(); // Main thread가 종료되지 않도록 대기
        }

        static void DoWork(CancellationToken cancellationToken)
        {
            while (true)
            {
                // 작업 수행
                Console.WriteLine("Working...");
                Thread.Sleep(100);

                // 특정 이벤트를 확인하거나 외부 조건을 체크하여 Task 종료
                // 여기서는 cancellationToken.ThrowIfCancellationRequested()를 사용
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}

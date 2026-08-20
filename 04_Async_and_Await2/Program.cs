// 데이터 보호 예제
namespace _04_Async_and_Await2
{
    internal class Program
    {
        static int sharedData = 0;
        static object lockKey = new object();

        static async Task Main()
        {
            await RunTasks();
        }

        static async Task RunTasks()
        {
            var tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => IncrementSharedData());
                // Task.Run을 사용하여 IncrementSharedData 메서드를 비동기적으로 실행
            }

            await Task.WhenAll(tasks); // 모든 Task가 완료될 때까지 대기

            Console.WriteLine($"최종 공유 데이터 값: {sharedData}");
        }

        static void IncrementSharedData()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (lockKey) // 해당 Line 존재 여부에 따라 달라짐.
                {
                    sharedData++;
                }
                // 최종 공유 데이터 값: 403560 <= lock 없이
                // 최종 공유 데이터 값: 500000 <= lock 사용
            }
        }
    }
}

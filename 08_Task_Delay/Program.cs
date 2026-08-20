// Delay()와 Sleep()의 차이를 보이는 예제
// - Task.Delay(): 비동기 대기, 대기 중 스레드를 반환하여 CPU/스레드 자원을 점유하지 않음
// - Thread.Sleep(): 동기 대기, 대기 중에도 스레드를 점유하여 CPU 자원을 낭비함
// => 무한 루프와 결합 시 Sleep 방식은 스레드가 계속 blocking 상태가 되어
//    새로운 스레드 생성/컨텍스트 스위칭 비용 등으로 CPU 사용률이 100%에 가깝게 치솟을 수 있음

namespace _08_Task_Delay
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("비동기함수(a) or 스레드함수(t) 선택: ");

            string mode = Console.ReadLine();

            switch (mode)
            {
                case "a":
                    // 비동기 방식: Task.Delay 기반 - CPU를 거의 사용하지 않음
                    Console.WriteLine("=== 비동기(async) 확인 시작 ===");
                    RunThread1();
                    break;
                case "t":
                    // 동기 방식: Thread.Sleep 기반 - CPU 사용률이 급격히 상승할 수 있음
                    Console.WriteLine("=== 스레드(thread) 확인 시작 ===");
                    RunThread2();
                    break;
            }

            // 메인 스레드는 별도 작업 없이 대기 상태로 남음
            // (백그라운드 스레드에서 무한 루프가 계속 동작 중)
            Console.WriteLine("실행 중...");
        }

        static void RunThread1()
        {
            long count = 0;

            // 새로운 스레드를 생성하여 무한 루프 실행
            // 루프 내부에서 비동기 함수(RunTaskDelayStep1)를 반복 호출
            // await 지점에서 실제 스레드는 블로킹되지 않고 반환됨
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    // async void 메서드 호출 - fire-and-forget 형태
                    // 내부적으로 Task.Delay(1000)이 끝날 때까지 스레드를 점유하지 않음
                    RunTaskDelayStep1();
                    ++count;
                }
            });

            thread.Start();
        }

        static void RunThread2()
        {
            long count = 0;

            // 새로운 스레드를 생성하여 무한 루프 실행
            // 루프 내부에서 동기 함수(RunThreadSleepStep1)를 반복 호출
            // Thread.Sleep(1000) 동안 해당 스레드는 완전히 블로킹됨
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    // 동기 대기 - 이 스레드는 1초 동안 실행이 멈춘 채로 유지됨
                    // 하지만 while(true) 특성상 매 반복마다 새로운 대기가 발생하며
                    // 스레드 스케줄링/컨텍스트 전환 부하가 누적되어 CPU 사용률 급증 원인이 됨
                    RunThreadSleepStep1();
                    ++count;
                }
            });
            thread.Start();
        }

        // 1. 비동기 방식 - 효율적 (CPU를 차단하지 않음)
        static async void RunTaskDelayStep1()
        {
            // Task 생성만 하고 즉시 반환 가능한 구조
            Task t = RunTaskDelayStep2();

            // await로 비동기 대기: 현재 스레드를 반환하고
            // 나중에 Delay가 끝나면 스레드 풀에서 다른 스레드가 이어서 처리
            // => CPU를 점유하지 않고 대기하는 것이 핵심 포인트
            await t; // 1. 비동기 대기
            // t.Wait(); // 2. 동기 대기 (CPU 차단) - 이 주석 처리된 코드로 바꾸면 블로킹 발생
        }

        static async Task RunTaskDelayStep2()
        {
            // Task.Delay: 타이머 기반 비동기 대기
            // 실제 스레드를 점유하지 않고, 콜백 방식으로 1초 후 재개됨
            await Task.Delay(1000); // 1. 비동기 대기
            // Thread.Sleep(1000); // 2. 동기 대기 (CPU 차단) - 비교용 주석
        }

        // 2. 동기 방식 - 비효율적 (CPU를 차단함)
        static void RunThreadSleepStep1()
        {
            RunThreadSleepStep2(); // 동기 대기
        }

        static void RunThreadSleepStep2()
        {
            // Thread.Sleep: 호출한 스레드를 물리적으로 블로킹시킴
            // 대기하는 동안 해당 스레드는 다른 작업을 전혀 수행할 수 없음
            // 무한 루프에서 반복 호출 시, 스레드 생성/소멸 및 컨텍스트 스위칭이
            // 지속적으로 발생하여 CPU 사용률을 100%에 가깝게 만드는 주요 원인이 됨
            Thread.Sleep(1000); // 동기 대기 (CPU 차단)
        }
    }
}

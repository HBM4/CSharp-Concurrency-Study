namespace _02_Blocking
{
    internal class Program
    {
        // 블록킹 함수(Blocking Function)이자 동기(Synchronous) 작업
        // 함수가 완료될 때까지 호출측에 제어를 반환하지 않음 (Thread.Sleep으로 스레드 자체가 멈춤)
        // 따라서 순차적으로 실행되어 작업들이 겹쳐서 실행되지 않음
        static void JobSync()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Sync : {i}");
                Thread.Sleep(500);
            }
            Console.WriteLine();
        }

        // 넌블록킹 함수(Non-blocking Function)이자 비동기(Asynchronous) 작업
        // await 지점에서 제어를 즉시 호출측에 반환하여(리턴), 호출측은 완료를 기다리지 않고 다음 코드를 계속 실행함
        // 따라서 여러 작업이 동시에 병렬로 겹쳐서 실행될 수 있음
        static async void JobAsync()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Async : {i}");
                await Task.Delay(500);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // JobSync는 블록킹 함수이므로 첫 번째 호출이 완료된 후에야
            // 두 번째 호출이 시작됨 (순차 실행, 동기 작업)
            JobSync();
            JobSync();

            // JobAsync는 넌블록킹 함수이므로 첫 번째 호출이 완료되기 전에
            // 바로 제어가 반환되어 두 번째 호출이 곧바로 실행됨
            // 두 작업이 겹쳐서 동시에 진행됨 (비동기 작업)
            JobAsync();
            JobAsync();


            // 현재는 Async 종료 방지를 위해 Console.ReadLine() 같은 임시방편으로 메인 스레드를 붙잡아 두는 수밖에 없다.
            // 실제 프로덕션 코드에서는 이벤트 핸들러가 아닌 이상 async void보다는 async Task를 사용하는 것이 좋다.
            Console.ReadLine();
        }
    }
}

//Sync : 1
//Sync : 2
//Sync : 3
//Sync : 4
//Sync : 5

//Sync : 1
//Sync : 2
//Sync : 3
//Sync : 4
//Sync : 5

//Async : 1
//Async : 1
//Async : 2
//Async : 2
//Async : 3
//Async : 3
//Async : 4
//Async : 4
//Async : 5
//Async : 5
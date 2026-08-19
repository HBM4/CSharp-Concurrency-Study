    namespace _04_Async_and_Await
    {
        internal class Program
        {
            // [비동기 메서드 3가지 비교]
            // 1. async void: 반환 값이 없고, 호출자가 완료를 기다릴 수 없음(Fire-and-forget).
            //    - 예외가 발생하면 호출자가 catch할 수 없어 앱이 크래시될 위험이 있음.
            //    - 주로 이벤트 핸들러(버튼 클릭 등) 외에는 사용을 지양해야 함.
            static async void AsyncVoidExample() // 비동기 void 메서드
            {
                Console.WriteLine($"비동기 작업 시작: {Thread.CurrentThread.ManagedThreadId}");
                // await 전은 메인 스레드에서 실행되고, await 이후에는 다른 쓰레드가 실행함.
                await Task.Delay(1000); // 1초 지연
                Console.WriteLine($"비동기 작업 완료: {Thread.CurrentThread.ManagedThreadId}");
            }

            // 2. async Task: 반환 값은 없지만, 호출자가 await로 완료 시점을 기다릴 수 있음.
            //    - 내부에서 발생한 예외가 Task에 담겨 호출자에게 전파되므로 안전하게 처리 가능.
            //    - "결과 값 없이 비동기 작업만 수행"할 때 사용.
            static async Task AsyncTaskExample() // 비동기 Task 메서드
            {
                Console.WriteLine($"비동기 작업 시작: {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(1000); // 1초 지연
                Console.WriteLine($"비동기 작업 완료: {Thread.CurrentThread.ManagedThreadId}");
            }

            // 3. async Task<T>: 비동기 작업 완료 후 결과 값(T)을 반환.
            //    - Task와 마찬가지로 예외 전파가 안전하며, await로 결과 값을 받을 수 있음.
            //    - "비동기 작업 후 결과가 필요한 경우"에 사용.
            static async Task<int> AsyncTaskWithResultExample() // 비동기 Task<int> 메서드
            {
                Console.WriteLine($"비동기 작업 시작: {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(1000); // 1초 지연
                Console.WriteLine($"비동기 작업 완료: {Thread.CurrentThread.ManagedThreadId}");

                return 100; // 결과 반환
            }

            static async Task Main()
            {
                AsyncVoidExample();
                Console.WriteLine($"메인 함수 계속 실행1... : {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("====================================");

                await AsyncTaskExample();
                // 위와 같은 코드:
                // Task t = AsyncTaskExample();
                // t.Wait();
                Console.WriteLine($"메인 함수 계속 실행2... : {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("====================================");

                int result = await AsyncTaskWithResultExample();
                Console.WriteLine($"결과: {result}, 메인 함수 계속 실행3... : {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("====================================");

                Console.ReadLine(); // 비동기 void 함수는 예외를 처리할 수 없으므로, 프로그램이 종료되지 않도록 대기
            }
        }
    }

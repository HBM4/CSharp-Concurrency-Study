namespace _05_Parallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine("병렬 처리 전:");

            foreach (var number in numbers)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();


            // System.Threading.Tasks 네임스페이스의 Parallel.For는
            // 일반 for 루프를 여러 스레드에서 동시에 실행하는 기능입니다.

            // 첫 번째 인자 0은 시작 인덱스,

            // 두 번째 인자 numbers.Length는 종료 인덱스(미포함)입니다.

            // 세 번째 인자는 각 반복마다 실행할 람다식(delegate)이며,
            // 매개변수 i는 현재 반복 중인 인덱스 값입니다.

            // 순차적으로 i = 0, 1, 2, ... 순서로 실행되는 것이 아니라
            // 여러 스레드가 동시에 서로 다른 i 값을 처리합니다.
            Parallel.For(0, numbers.Length, i =>
            {
                numbers[i] = numbers[i] * numbers[i];
            });

            Console.WriteLine("Parallel.For 처리 후:");
            foreach (var number in numbers)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();

            // Parallel.ForEach 테스트를 위해 배열을 초기값으로 재설정
            numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


            // Parallel.ForEach는 컬렉션(IEnumerable)의 각 요소를
            // 여러 스레드에서 동시에 병렬로 순회하며 처리하는 기능입니다.

            // 첫 번째 인자 numbers는 순회할 대상 컬렉션입니다.

            // 두 번째 인자는 각 요소마다 실행할 람다식(delegate)이며,
            // 매개변수 number는 현재 처리 중인 요소 값,
            // state는 병렬 루프의 상태를 제어할 수 있는
            // ParallelLoopState 객체입니다.
            // (예: state.Break(), state.Stop() 등으로 루프를 중단할 수 있음)

            // Parallel.For와 마찬가지로 순차적으로 처리되는 것이 아니라
            // 여러 스레드가 동시에 서로 다른 요소를 처리합니다.
            Parallel.ForEach(numbers, (number, state) =>
            {
                numbers[number - 1] = number * number;
            });

            Console.WriteLine("Parallel.ForEach 처리 후:");
            foreach (var number in numbers)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();
        }
    }
}

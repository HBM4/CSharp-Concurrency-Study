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
                Thread.Sleep(2000);
            });

            Console.WriteLine("병렬 처리 후:");
            foreach (var number in numbers)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();
        }
    }
}

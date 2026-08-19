namespace _03_SimpleThreadApp2
{
    class SimpleWorker
    {
        int count = 0;

        public void PrintInteger()
        {
            // 두 쓰레드가 같은 count 필드를 공유하여 각각 100만 번씩,
            // 총 200만 번 증가시키는 것을 기대함
            for(int i=1; i<=1_000_000; ++i)
            {
                // ++count; 는 한 줄이지만 실제로는 아래 세 단계로 나뉘어 실행됨
                // 1. count 값을 읽음
                // 2. 읽은 값을 1 증가시킴
                // 3. 증가된 값을 count에 다시 저장함
                //
                // 두 쓰레드가 동시에 이 작업을 수행하면 아래와 같은 문제(경쟁 상태, Race Condition)가 발생할 수 있음
                // 예) 현재 count = 10 인 상황에서
                //   t1이 count를 읽음 -> 10
                //   t2도 count를 읽음 -> 10
                //   t1이 11을 저장
                //   t2도 11을 저장
                // 원래는 두 번 증가했으니 12가 되어야 하지만 실제로는 11만 됨
                //
                // 따라서 두 쓰레드가 동시에 count를 수정하면서 일부 증가 연산이 누락되고,
                // 최종 결과는 매 실행마다 다르게 나오며 2,000,000보다 작은 값이 출력됨
                // (예: result: 305997, result: 1057852 등)

                // lock (this)로 임계 영역(Critical Section)을 설정하여
                // 한 번에 하나의 쓰레드만 ++count 연산을 수행하도록 보장함
                // 이렇게 하면 두 쓰레드가 동시에 count를 읽고 쓰는 경쟁 상태를 방지할 수 있음
                // 단, this를 락 대상으로 사용하는 것은 외부에서 해당 인스턴스를 락 용도로
                // 사용할 위험이 있으므로, 실무에서는 private readonly object 형태의
                // 별도 락 전용 객체를 만들어 사용하는 것이 더 안전함
                lock (this)
                {
                    ++count;
                }
            }

            Console.WriteLine($"result: {count}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";

            var simWorker = new SimpleWorker();
            // 이전 코드와 달리 t1, t2가 동일한 simWorker 인스턴스(같은 count 필드)를 공유함
            var t1 = new Thread(simWorker.PrintInteger);
            var t2 = new Thread(simWorker.PrintInteger);
            t1.Start();
            t2.Start();

            Console.WriteLine("Main 함수 종료");
        }
    }
}

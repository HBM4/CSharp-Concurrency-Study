namespace _03_SimpleThreadApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 메인 쓰레드 ID와 이름 설정 및 출력하기
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";
            PrintThreadInfo();
            //ID:[1], Name:MainThread
            Console.WriteLine("================================");


            // 메인 쓰레드에서 메소드 호출
            var simThread = new SimpleThread();
            simThread.PrintInteger();
            //ID[1] : 1
            //ID[1] : 2
            //ID[1] : 3
            //ID[1] : 4
            //ID[1] : 5
            //ID[1] : 6
            //ID[1] : 7
            //ID[1] : 8
            //ID[1] : 9
            Console.WriteLine("================================");


            // 서브 쓰레드에서 메소드 호출
            var simWorker = new SimpleWorker();
            var t1 = new Thread(simWorker.PrintInteger); // 의미: 새로운 스레드를 생성하고, 그 스레드에서 simWorker.PrintInteger 메서드를 실행하도록 지정
            var t2 = new Thread(simWorker.PrintInteger);
            t1.IsBackground = true; // 백그라운드 쓰레드로 전환
            t2.IsBackground = true; // 백그라운드 쓰레드로 전환
            t1.Start();
            t2.Start();
            Console.WriteLine("[Enter] 키를 누르면 중간에 종료");
            Console.ReadLine();
            Console.WriteLine("Main 함수 종료 ================================");
            //ID[4] : 1
            //ID[3] : 1
            //ID[4] : 2
            //ID[3] : 2
            //ID[3] : 3
            //ID[4] : 3
            //ID[3] : 4
            //ID[4] : 4
            //ID[3] : 5
            //ID[4] : 5
            //ID[3] : 6
            //ID[4] : 6
            //ID[3] : 7
            //ID[4] : 7
            //ID[3] : 8
            //ID[4] : 8
            //ID[3] : 9
            //ID[4] : 9
            //ID[3] : 10
            //ID[4] : 10
        }

        static void PrintThreadInfo()
        {
            Console.WriteLine($"ID:[{Thread.CurrentThread.ManagedThreadId}], Name:[{Thread.CurrentThread.Name}]");
        }
    }

    class SimpleThread
    {
        public void PrintInteger()
        {
            for(int i=1; i < 10; i++)
            {
                Console.WriteLine($"ID:[{Thread.CurrentThread.ManagedThreadId}], Value:[{i}]");
                Thread.Sleep(500);
            }
        }
    }

    class SimpleWorker
    {
        public void PrintInteger()
        {
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine($"ID:[{Thread.CurrentThread.ManagedThreadId}], Value:[{i}]");
                Thread.Sleep(500);
            }
        }
    }
}

using System.Diagnostics;

namespace _01_Stack_and_Heap
{
    internal class Program
    {
        // 값 형식(value type) 구조체
        // 변수가 객체 자체로 Stack 메모리에 자리 잡으며,
        // 변수끼리의 대입은 값 복사가 된다.
        struct Points
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Points(int x = 0, int y = 0)
            {
                X = x;
                Y = y;
            }
            public override string ToString()
            {
                return $"({X},{Y})";
            }
        }

        // 참조 형식(reference type) 클래스
        // 변수는 참조(주소)를 Stack 메모리에 저장하고,
        // 실제 객체는 Heap 메모리에 저장된다.
        // 변수끼리의 대입은 참조 복사가 되어 같은 객체를 가리키게 된다.
        class Pointc
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Pointc(int x = 0, int y = 0)
            {
                X = x;
                Y = y;
            }
            public override string ToString()
            {
                return $"({X},{Y})";
            }
        }

        static void Main()
        {
            // 쓰레드 사용에서 중요한 메모리는 Stack과 Heap 메모리이다.
            // Stack 메모리는 쓰레드가 소유하여 쓰레드 간에 독립적이며,
            // Heap 메모리는 프로세스가 소유하여 쓰레드 간에 공유된다.

            // int는 값 형식(value type)이므로 Stack 메모리에 값 자체가 저장되고,
            // n2 = n1 대입 시 값 복사가 일어난다. (n1, n2는 서로 독립적)
            int n1 = 10;
            int n2 = n1;

            // Points는 struct(값 형식)이므로 Stack 메모리에 값 자체가 저장되고,
            // ps2 = ps1 대입 시 값 복사가 일어난다. (ps1, ps2는 서로 독립적)
            Points ps1 = new Points(1, 2);
            Points ps2 = ps1;

            // Pointc는 class(참조 형식)이므로 Heap 메모리에 객체가 생성되고
            // 변수 pc1, pc2에는 참조(주소)만 Stack에 저장된다.
            // pc2 = pc1 대입 시 참조 복사가 일어나 같은 객체를 가리킨다.
            Pointc pc1 = new Pointc(1, 2);
            Pointc pc2 = pc1;

            n2 = 50;         // n1에는 영향 없음 (값 복사)
            ps2.X = 5;       // ps1에는 영향 없음 (값 복사)
            pc2.X = 5;       // pc1도 같이 변경됨 (참조 복사, 같은 객체를 공유)

            Console.WriteLine($"{n1} : {n2}");
            Console.WriteLine($"{ps1} : {ps2}");
            Console.WriteLine($"{pc1} : {pc2}");
        }
        //10 : 50
        //(1,2) : (5,2)
        //(5,2) : (5,2)

        // 참고: .NET의 형식은 크게 '값 형식'과 '참조 형식'으로 구분되며,
        // 이를 주의해서 사용하지 않으면 의도치 않은 공유로 인한 '버그'로 이어질 수 있다.
        // 객체 복사를 구현(ICloneable 인터페이스 구현)할 때는
        // System.Object의 얕은 복사(MemberwiseClone) 동작을 참고하여 주의 깊게 구현해야 한다.
    }
}
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _06_Ui_Thread
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 버튼을 코드로 생성하여 창에 추가
            Button button = new Button();
            button.Content = "Click Me";
            // 클릭 이벤트에 async 람다 등록, UI 스레드를 막지 않고 비동기 처리
            button.Click += async (sender, e) => await HandleButtonClickAsync(this);

            // 버튼을 창의 Content로 설정
            this.Content = button;
        }

        static async Task HandleButtonClickAsync(Window window)
        {
            for (int i=1; i<=10; i++)
            {
                // 1초 대기 (비동기, UI 스레드를 점유하지 않음)
                await Task.Delay(1000);

                // Task.Delay 이후 콜백은 다른 스레드에서 실행될 수 있으므로
                // Dispatcher.Invoke로 UI 스레드에서 안전하게 UI 갱신
                window.Dispatcher.Invoke(() =>
                {
                    // 현재 스레드 ID와 카운트를 창 제목에 표시
                    window.Title = $"[{Thread.CurrentThread.ManagedThreadId}]Count : " + i;
                });
            }
        }
    }
}
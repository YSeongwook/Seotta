using Seotta;
using System;
using System.Windows.Forms;

namespace Seotta
{
    static class Program
    {
        // STA(Single Thread Apartment) Thread 모델로 실행
        [STAThread]
        static void Main()
        {
            // Application에 대한 Visual Style 적용
            Application.EnableVisualStyles();
            // Application에 대한 Text Rendering 방식 결정
            Application.SetCompatibleTextRenderingDefault(false);
            // 새로운 Form1객체 생성
            Application.Run(new Opening());
        }
    }
}
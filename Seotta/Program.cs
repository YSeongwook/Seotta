using Seotta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Application.Run(new Form1());

            // 섰다 시스템 구현
            // cpu 패는 안보이다가 베팅하고나서 패 보여줘야함
            // 1장 받고 베팅, 2장 다 받고 베팅
            // 우측에 플레이어 족보 보여주기
            // 족보 보여주고 족보에서 순위도 보여주기
        }
    }
}

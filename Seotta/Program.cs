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

            // cpu 패는 안보이다가 베팅하고나서 패 보여줘야함(완)
            // 1장 받고 베팅, 2장 다 받고 베팅
            // 우상단 도움말, 우중단1 족보도우미, 우중단2 족보 설명, 우중단3 보유머니 우하단 베팅 버튼(컴포넌트 등록 완료)
            // 우중단1 족보도우미, 현재 패의 족보만 클릭되게하고 나머지는 클릭 안되게(완)
            // 우측 족보 도우미에 족보 설명 출력
            // 베팅 버튼에 이벤트 연결
            // 베팅 버튼 클릭 시 game.GetTimer(2).Start();
        }
    }
}

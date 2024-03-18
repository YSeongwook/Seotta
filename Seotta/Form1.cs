using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using static System.Console;

namespace Seotta
{
    public partial class Form1 : Form
    {
        Game game;

        string[] jokbo = {"1.38광땡", "2.광땡", "3.땡", "4.알리", "5.독사", "6.구삥", "7.장삥",
            "8.장사", "9.세륙", "10.갑오", "11.끗,망통"," * 구사", "* 땡잡이", "* 암행어사"};

        public Form1()
        {
            InitializeComponent();

            // 폼을 화면에 가운데에 생성
            this.StartPosition = FormStartPosition.CenterScreen;

            // 폼의 배경색을 검정색으로 설정
            this.BackColor = Color.Black;

            // KeyPreview를 true로 설정하여 폼이 키 이벤트를 먼저 받도록 함
            this.KeyPreview = true;

            CreateJokboButton();
        }

        private void CreateJokboButton()
        {
            // 패널 생성
            Panel panel = new Panel();
            panel.Location = new Point(1278, 50); // 좌상단 위치 설정
            panel.Size = new Size(380, 250); // 크기 설정
            this.Controls.Add(panel); // 폼에 패널 추가

            // 버튼 생성 및 패널에 추가
            int buttonWidth = 118;
            int buttonHeight = 40;
            int gap = 5; // 버튼 간격
            int row = 5;
            int col = 3;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Button button = new Button();
                    button.Text = $"Button {i * col + j + 1}";
                    button.ForeColor = Color.White;
                    button.TextAlign = ContentAlignment.MiddleLeft;
                    button.Padding = new Padding(8, 0, 0, 0);
                    button.Location = new Point(10 + j * (buttonWidth + gap), 30 + i * (buttonHeight + gap)); // 좌상단 위치 설정
                    button.Size = new Size(buttonWidth, buttonHeight); // 크기 설정
                    panel.Controls.Add(button); // 패널에 버튼 추가
                }
            }

            Button jokboLabel = (Button)panel.Controls[0];
            jokboLabel.TextAlign = ContentAlignment.MiddleCenter;
            jokboLabel.FlatStyle = FlatStyle.Flat;
            jokboLabel.FlatAppearance.BorderSize = 0; // 테두리 크기를 0으로 설정하여 테두리를 없앰
            jokboLabel.Text = "족보";
            jokboLabel.Font = new Font("Consolas", 22F);

            for (int i = 1; i < panel.Controls.Count; i++)
            {
                panel.Controls[i].Font = new Font("Consolas", 14F);
                panel.Controls[i].Text = jokbo[i - 1];
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(this, pae1, pae2, pae3, pae4, gameProgress);
            game.StartGame();

            this.Focus();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            gameProgress.KeyDown += new KeyEventHandler(GameProgress_KeyDown);  // 이벤트 핸들러 만들어서 등록하니 엔터키 이벤트 성공
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 엔터 키의 기본 동작을 막음
                game.ResetPae();     // 패 초기화
                game.SelectPae();    // 패 선택
            }
        }

        private void GameProgress_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 엔터 키의 기본 동작을 막음
                game.ResetPae();     // 패 초기화
                game.SelectPae();    // 패 선택
            }
        }

        private void JokboHelpBtn_BtnClick(object sender, EventArgs e)
        {
            gameProgress.Text = "족보 도움말\r\n\r\n" +
                "섯다에서 사용되는 화투는 1월부터 10월까지의 패 중 피를 제외한 두 장씩 모두 20장입니다.\r\n" +
                "이 20장에서 두 장을 사용하여 족보를 만들어 베팅을 하게 됩니다.\r\n" +
                "게임 중 족보는 게임화면 우측 상단에서 확인 할 수 있습니다.\r\n\r\n" +
                "삼팔광땡\r\n섯다에서 가능한 3광과 8광으로 된 최강의 족보입니다.\r\n\r\n" +
                "광땡\r\n셋 중 2개만 모으면 최고의 족보로,\r\n1광과 3광 또는 1광과 8광으로 된 삼팔 광땡 다음으로 좋은 족보입니다\r\n\r\n" +
                "땡\r\n같은 월의 조합입니다.\r\n땡 끼리 만났을 경우에는 장땡(10월) ~ 삥땡(1월) 순으로 숫자가 높을수록 이깁니다.\r\n\r\n" +
                "알리\r\n순서와 패 상관없이 1월과 2월의 조합입니다.\r\n\r\n" +
                "독사\r\n순서와 패 상관없이 1월과 4월의 조합입니다.\r\n\r\n" +
                "구삥\r\n순서와 패 상관없이 1월과 9월의 조합입니다.\r\n\r\n" +
                "장삥\r\n순서와 패 상관없이 1월과 10월의 조합입니다.\r\n\r\n" +
                "장사\r\n순서와 패 상관없이 4월과 10월의 조합입니다.\r\n\r\n" +
                "세륙\r\n순서와 패 상관없이 4월과 6월의 조합입니다.\r\n\r\n" +
                "갑오\r\n두 장의 숫자를 합한 수의 끝자리 숫자(끗수)가 9인 경우입니다.\r\n(예: 1+8, 2+7, 3+6, 4+5 )\r\n\r\n" +
                "끗\r\n끗수가 8부터 1일 때 각각 8끗, … , 1 끗이라고 하며 숫자가 높을수록 좋은 족보입니다.\r\n\r\n" +
                "망통\r\n2월과 8월의 조합으로 끗수가 0인 가장 낮은 족보 입니다\r\n\r\n" +
                "특수 기능 족보\r\n일반 족보 상황 중, 특정 족보에만 적용되는 특수 기능입니다.\r\n\r\n" +
                "땡잡이\r\n3월과 7월의 조합으로서 구땡 이하의 족보를 이길(잡을) 수 있습니다.\r\n장땡과 광땡,무적의 삼팔 광땡은 잡을 수 없습니다.\r\n만약 상대방 중에 땡이 없다면 끗수가 0이므로망통으로 계산됩니다.\r\n\r\n" +
                "구사\r\n4월과 9월의 조합으로서 알리 이하의 족보와\r\n(즉, 상대방의 족보 중 가장 높은 것이 알리 이하일 때)\r\n이번 판을 물리고 재경기를 할 수 있습니다.\r\n\r\n" +
                "멍텅구리 구사\r\n열자리 4월과 열자리 9월로 된 조합으로서 9땡 이하의 족보와\r\n(즉, 상대방의 족보 중 가장 높은 것이 9땡 이하 일 때) 재경기를 할 수 있습니다\r\n\r\n" +
                "암행어사\r\n열자리 4월과 열자리 7로 된 조합으로서 일삼광땡 혹은 일팔광땡을 이길 수 있습니다.\r\n만약 상대방 중에 광땡이 없다면 1끗으로 계산 됩니다";
        }

        private void BetHelpBtn_BtnClick(object sender, EventArgs e)
        {
            gameProgress.Text = "베팅 도움말\r\n\r\n" +
                "오른쪽 버튼에 있는 숫자는 숫자 키패드에 대응합니다.\r\n" +
                "즉, 마우스 클릭 대신에 숫자키를 이용하여 해당 베팅을 할 수 있습니다.\r\n\r\n" +
                "콜(7): 앞 사람이 베팅한 금액만큼 레이스 머니를 받습니다.\r\n" +
                "삥(4): 기본 머니 만큼 베팅합니다.\r\n" +
                "다이(8): 앞사람이 베팅한 금액을 받지 않고, 이번 판을 포기합니다.\r\n" +
                "체크(5): 머니를 베팅하지 않고 다음 사람의 턴으로 넘깁니다.\r\n" +
                "따당(1): 앞사람이 베팅한 금액의 2배를 베팅합니다.\r\n" +
                "하프(0): 앞사람이 베팅한 금액을 받고, 이를 포함한 전체 판돈의 1/2를 추가로 베팅합니다.\r\n";
        }
    }
}

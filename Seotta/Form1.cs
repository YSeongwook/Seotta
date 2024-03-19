using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using static System.Console;
using static System.Windows.Forms.LinkLabel;

namespace Seotta
{
    public partial class Form1 : Form
    {
        Game game;
        private Button previousButton = null; // 이전에 선택된 버튼을 저장할 변수

        string[] jokbo = {"1.38광땡", "2.광땡", "3.땡", "4.알리", "5.독사", "6.구삥", "7.장삥",
            "8.장사", "9.세륙", "10.갑오", "11.끗,망통","* 구사", "* 땡잡이", "* 암행어사"};

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

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(this, pae1, pae2, pae3, pae4, gameProgress);
            game.StartGame();

            this.Focus();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            gameProgress.KeyDown += new KeyEventHandler(GameProgress_KeyDown);  // 이벤트 핸들러 만들어서 등록하니 엔터키 이벤트 성공
            AttachButtonClickHandlers(this);                                    // 버튼 클릭 시 색 변환 이벤트 등록
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;  // 엔터 키의 기본 동작을 막음
                // game.PrintPae();            // 패 선택
                game.GetTimer(2).Start();
            }
        }

        private void GameProgress_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;  // 엔터 키의 기본 동작을 막음
                // game.PrintPae();            // 패 선택
                game.GetTimer(2).Start();
            }
        }

        // 족보 버튼 동적 생성
        private void CreateJokboButton()
        {
            // 패널 생성
            Panel panel = new Panel();
            panel.Location = new Point(1278, 50);   // 좌상단 위치 설정
            panel.Size = new Size(380, 250);        // 크기 설정
            this.Controls.Add(panel);               // 폼에 패널 추가

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
                    button.ForeColor = Color.White;
                    button.TextAlign = ContentAlignment.MiddleLeft;
                    button.TabStop = false;
                    button.Padding = new Padding(8, 0, 0, 0);
                    button.Location = new Point(10 + j * (buttonWidth + gap), 30 + i * (buttonHeight + gap));
                    button.Size = new Size(buttonWidth, buttonHeight);  // 크기 설정
                    panel.Controls.Add(button);                         // 패널에 버튼 추가
                }
            }

            Button jokboLabel = (Button)panel.Controls[0];  // 형변환 해야 접근 가능
            jokboLabel.TextAlign = ContentAlignment.MiddleCenter;
            jokboLabel.FlatStyle = FlatStyle.Flat;
            jokboLabel.TabStop = false;
            jokboLabel.FlatAppearance.BorderSize = 0;       // 테두리 크기를 0으로 설정하여 테두리를 없앰
            jokboLabel.Text = "족보";
            jokboLabel.Font = new Font("Consolas", 22F);

            for (int i = 1; i < panel.Controls.Count; i++)
            {
                panel.Controls[i].Font = new Font("Consolas", 14F);
                panel.Controls[i].Text = jokbo[i - 1];
            }
        }

        // 족보 도움말 버튼 클릭 시 도움말 출력
        private void JokboHelpBtn_BtnClick(object sender, EventArgs e)
        {
            // 파일 경로 지정
            string filePath = "jokbo_help.txt";

            // 함수 호출로 파일 내용을 텍스트 박스에 표시
            game.DisplayTextFromFile(filePath, gameProgress);
        }

        // 베팅 도움말 버튼 클릭 시 도움말 출력
        private void BetHelpBtn_BtnClick(object sender, EventArgs e)
        {
            // 파일 경로 지정
            string filePath = "betting_help.txt";

            // 함수 호출로 파일 내용을 텍스트 박스에 표시
            game.DisplayTextFromFile(filePath, gameProgress);
        }

        // 버튼 클릭 이벤트 핸들러
        private void AttachButtonClickHandlers(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // 현재 컨트롤이 버튼인 경우
                if (control is Button button)
                {
                    // 버튼의 Click 이벤트 핸들러에 AttachButtonClick 메서드를 연결
                    button.Click += AttachButtonClick;
                }
                // 컨트롤이 다른 컨테이너인 경우 재귀적으로 호출하여 하위 컨트롤에 대해 핸들러를 연결
                else if (control.HasChildren)
                {
                    AttachButtonClickHandlers(control);
                }
            }
        }

        // 버튼 클릭 이벤트
        private void AttachButtonClick(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            currentButton.BackColor = Color.Gray;
            currentButton.ForeColor = Color.Black;

            // 이전 버튼의 색상 원래대로 변경
            if (previousButton != null && previousButton != currentButton)
            {
                previousButton.BackColor = Color.Black;
                previousButton.ForeColor = Color.White;
            }

            // 현재 버튼을 이전 버튼으로 저장
            previousButton = currentButton;
        }
    }
}

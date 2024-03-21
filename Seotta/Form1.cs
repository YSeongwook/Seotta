using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seotta
{
    public partial class Form1 : Form
    {
        Game game;
        Button previousButton = null; // 이전에 선택된 버튼을 저장할 변수
        Panel jokboPanel;

        bool isButtonClickable = true;

        public Button GetPreviousButton()
        {
            return previousButton;
        }

        public Form1()
        {
            InitializeComponent();
            CreateJokboButton();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(this, pae1, pae2, pae3, pae4, gameProgress, jokboHelper, jokboPanel, cpuLabel, playerLabel, cpuMoneyLabel, playerMoneyLabel, currentPotBox);
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
                game.SecondDraw();
            }

            // Spacebar 키를 눌렀을 때
            if (e.KeyCode == Keys.Space)
            {
                game.ShowDown();
            }

            // Escape 키를 누르면 폼 종료
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            // Tab 키를 누르면 재시작
            if (e.KeyCode == Keys.Tab)
            {
                game.RestartGame();
            }

            // 숫자 키패드를 눌러 베팅 가능
            if (e.KeyCode == Keys.NumPad7)
            {
                BetButton_Click(betBtn7, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.NumPad8)
            {
                BetButton_Click(betBtn8, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                BetButton_Click(betBtn4, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.NumPad5)
            {
                BetButton_Click(betBtn5, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.NumPad1)
            {
                BetButton_Click(betBtn1, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.NumPad0)
            {
                BetButton_Click(betBtn0, EventArgs.Empty);
            }
        }

        private void GameProgress_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;  // 엔터 키의 기본 동작을 막음
                game.SecondDraw();
            }

            // Spacebar 키를 눌렀을 때
            if (e.KeyCode == Keys.Space)
            {
                game.ShowDown();
            }

            // Escape 키를 누르면 폼 종료
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            // Tab 키를 누르면 재시작
            if (e.KeyCode == Keys.Tab)
            {
                // game.RestartGame();
            }
        }

        // 족보 버튼 동적 생성
        private void CreateJokboButton()
        {
            string[] jokbo = {"족보", "1.38광땡", "2.광땡", "3.땡", "4.알리", "5.독사", "6.구삥", "7.장삥",
                            "8.장사", "9.세륙", "10.갑오", "11.끗,망통","* 구사", "* 땡잡이", "* 암행어사"};

            // 패널 생성
            jokboPanel = new Panel();
            jokboPanel.Location = new Point(1278, 50);   // 좌상단 위치 설정
            jokboPanel.Size = new Size(380, 250);        // 크기 설정
            this.Controls.Add(jokboPanel);               // 폼에 패널 추가

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
                    JokboButton button = new JokboButton();
                    button.Font = new Font("Consolas", 14F);
                    button.ForeColor = Color.White;
                    button.TextAlign = ContentAlignment.MiddleLeft;
                    button.TabStop = false;
                    button.Padding = new Padding(8, 0, 0, 0);
                    button.Location = new Point(10 + j * (buttonWidth + gap), 30 + i * (buttonHeight + gap));
                    button.Size = new Size(buttonWidth, buttonHeight);  // 크기 설정
                    button.Click += HightlightJokboButton;              // 강조 이벤트 핸들러 추가
                    jokboPanel.Controls.Add(button);                    // 패널에 버튼 추가
                }
            }

            for (int i = 0; i < jokboPanel.Controls.Count; i++)
            {
                jokboPanel.Controls[i].Text = jokbo[i];
            }

            Button jokboLabel = (Button)jokboPanel.Controls[0];  // 형변환 해야 접근 가능
            jokboLabel.TextAlign = ContentAlignment.MiddleCenter;
            jokboLabel.FlatStyle = FlatStyle.Flat;
            jokboLabel.TabStop = false;
            jokboLabel.Name = "족보 레이블";
            jokboLabel.FlatAppearance.BorderSize = 0;       // 테두리 크기를 0으로 설정하여 테두리를 없앰
            jokboLabel.Font = new Font("Consolas", 22F, FontStyle.Italic);
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
                if (control is Button button && (control.Name.Contains("betBtn") || control.Name.Contains("HelpBtn")))
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

        // 족보 버튼 강조 이벤트 핸들러
        public void HightlightJokboButton(object sender, EventArgs e)
        {
            JokboButton clickedButton = (JokboButton)sender;
            string jokbo = clickedButton.Text;  // 버튼의 텍스트(족보)를 가져옴

            // 강조 효과를 위한 코드 추가
            HighlightJokboButton(jokboPanel, clickedButton.Text);

            // 이후 족보에 따른 다른 작업 수행
        }

        // 족보 버튼 강조 이벤트
        public void HighlightJokboButton(Panel panel, string jokbo)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is JokboButton jokboBtn)
                {
                    // 패널에 있는 족보 버튼인 경우 배경색을 변경
                    if (jokboBtn.Text.Equals(jokbo) && !(jokboBtn.Name.Equals("족보 레이블")))
                    {
                        jokboBtn.BackColor = Color.Gray;
                        jokboBtn.ForeColor = Color.Black;
                    }    
                    else
                    {
                        // 다른 족보 버튼의 배경색을 원래 상태로 변경
                        jokboBtn.BackColor = Color.Black;
                        jokboBtn.ForeColor = Color.White;
                    }
                }
            }
        }

        // 베팅 버튼 클릭 이벤트 핸들러
        private async void BetButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int buttonNumber = Convert.ToInt32(clickedButton.Name.Substring(6, 1));

            if (!isButtonClickable)
                return; // 버튼이 이미 클릭 가능한 상태가 아니면 클릭 이벤트를 무시

            switch (buttonNumber)
            {
                case 7:
                    // 콜 처리
                    game.GetBettingSystem().PlayerBetting(7);
                    break;
                case 8:
                    // 다이 처리
                    game.GetBettingSystem().PlayerBetting(8);
                    break;
                case 4:
                    // 삥 처리
                    break;
                case 5:
                    // 체크 처리
                    break;
                case 1:
                    // 따당 처리
                    break;
                case 0:
                    // 하프 or 올인 처리
                    game.GetBettingSystem().PlayerBetting(0);
                    break;
            }

            // 클릭 후 버튼 비활성화
            isButtonClickable = false;

            // 일정 시간 대기 후 버튼 다시 활성화
            await Task.Delay(5000); // 5초 대기
            isButtonClickable = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;

namespace Seotta
{
    public class Game
    {
        #region Variable

        Form1 form;
        TextBox pae1;
        TextBox pae2;
        TextBox pae3;
        TextBox pae4;
        TextBox gameProgress;
        TextBox jokboHelper;
        Panel jokboPanel;
        Label cpuLabel;
        Label cpuMoneyLabel;
        Label playerLabel;
        Label playerMoneyLabel;
        TextBox currentPotBox;
        Button previousButton;

        Timer timer1;
        Timer timer2;

        BettingSystem bettingSystem;

        // Ascii Art가 담겨있는 텍스트 파일명 리스트
        private List<string> asciiText = new List<string>();

        // Ascii Art 출력에 사용할 인덱스 변수
        private int[] cpuIndex = new int[2];
        private int[] playerIndex = new int[2];

        // 패 객체 20개 리스트
        private List<Pae> pae = new List<Pae>();

        // cpu, player 패 리스트
        private Pae[] cpuPae;
        private Pae[] playerPae;
        public int CpuLevel { get; set; }
        public int PlayerLevel { get; set; }
        public string CpuJokbo { get; set; }
        public string PlayerJokbo { get; set; }

        private string seon = "컴퓨터";     // 선을 결정하는 변수
        private bool reGame = false;
        private int cpuMoney;               // cpu 소지금
        private int playerMoney;            // 플레이어 소지금
        private int currentPot;             // 현재 판돈
        private int cpuBettingMoney;        // cpu가 직전에 베팅한 금액
        private int playerBettingMoney;     // cpu가 직전에 베팅한 금액

        private int turn;
        private bool endBetting = false;

        public bool ReGame
        {
            get { return reGame; }
            set { reGame = value; }
        }

        #endregion

        public Game(Form1 form, TextBox pae1, TextBox pae2, TextBox pae3, TextBox pae4, TextBox gameProgress,
            TextBox jokboHelper, Panel jokboPanel, Label cpuLabel, Label playerLabel, Label cpuMoneyLabel, Label playerMoneyLabel, TextBox currentPotBox)
        {
            this.form = form;
            this.pae1 = pae1;
            this.pae2 = pae2;
            this.pae3 = pae3;
            this.pae4 = pae4;
            this.gameProgress = gameProgress;
            this.jokboHelper = jokboHelper;
            this.jokboPanel = jokboPanel;
            this.cpuLabel = cpuLabel;
            this.cpuMoneyLabel = cpuMoneyLabel;
            this.playerLabel = playerLabel;
            this.playerMoneyLabel = playerMoneyLabel;
            this.currentPotBox = currentPotBox;

            // 타이머1 설정
            timer1 = new Timer();
            timer1.Interval = 1; // 1밀리초마다 변경
            timer1.Tick += Timer1_Tick;

            // 타이머2 설정
            timer2 = new Timer();
            timer2.Interval = 1; // 1밀리초마다 변경
            timer2.Tick += Timer2_Tick;

            InitMoney();

            bettingSystem = new BettingSystem(this, gameProgress, seon, cpuMoney, playerMoney, currentPot, cpuBettingMoney, playerBettingMoney);
        }

        #region Getter

        public Timer GetTimer(int i)
        {
            if (i == 1)
            {
                return timer1;
            }
            else
            {
                return timer2;
            }
        }

        public void CheckEndBetting()
        {
            endBetting = true;
        }

        public void InitIndex()
        {
            for (int i = 0; i < 2; i++)
            {
                cpuIndex[i] = 0;
                playerIndex[i] = 0;
            }
        }

        public Pae[] GetCpuPae()
        {
            return cpuPae;
        }

        public Pae[] GetPlayerPae()
        {
            return playerPae;
        }

        public Panel GetJokboPanel()
        {
            return jokboPanel;
        }

        public int GetTurn()
        {
            return this.turn;
        }

        public int GetCpuMoney()
        {
            return this.cpuMoney;
        }

        public int GetPlayerMoney()
        {
            return this.playerMoney;
        }

        public BettingSystem GetBettingSystem()
        {
            return bettingSystem.GetBettingSystem();
        }

        #endregion

        // 소지금, 베팅금, 판돈 초기화
        public void InitMoney()
        {
            cpuMoney = 10000;
            playerMoney = 10000;
            currentPot = 0;
            cpuBettingMoney = 0;
            playerBettingMoney = 0;
        }

        // 게임 시작
        public void StartGame()
        {
            cpuPae = new Pae[2];
            playerPae = new Pae[2];

            turn = 1;

            // 패의 이름(1광, 1띠, etc..)가 담겨 있는 파일을 매개변수로 Pae 클래스 객체 20개 생성(Pae 클래스 객체를 사용하여 족보 계산)
            ReadPaeFromFile("Pae_Name.txt");

            GameLoop(); // 게임 진행
        }

        public async void GameLoop()
        {
            ResetPae();
            SelectPae();

            if (reGame)
            {
                gameProgress.Text = "재경기합니다.";
                gameProgress.AppendText($"\r\n{seon}가 선입니다.");
            }
            else
            {
                // 게임 안내 문구 출력
                DisplayTextFromFile("game_start.txt", gameProgress);

                await Task.Delay(2000);
                gameProgress.Text = $"{seon}가 선입니다.";
            }

            PrintPae(); // 현재 패 1장씩 출력
            cpuLabel.Visible = true;
            cpuMoneyLabel.Visible = true;
            playerLabel.Visible = true;
            playerMoneyLabel.Visible = true;

            bettingSystem.Betting(seon);
            // 스페이스바 이벤트에 있는 메서드(패 공개, 족보 비교, 소지금 분배(만들어야함)
        }

        // 게임 재시작
        public void RestartGame()
        {
            // Pae 배열 초기화
            pae.Clear();

            // cpuPae 및 playerPae 배열 초기화
            cpuPae = new Pae[2];
            playerPae = new Pae[2];

            // endBetting 변수 초기화
            endBetting = false;

            // cpuIndex 및 playerIndex 배열 초기화
            InitIndex();

            // TextBox 초기화
            gameProgress.Clear();
            jokboHelper.Clear();
            form.HighlightJokboButton(jokboPanel, "");

            reGame = true;

            // 버튼 색 복구
            ChangeAllButtonColors(Color.Black, Color.White);

            // 턴 초기화
            bettingSystem.turn = 1;

            // 선 변경
            ChangeSeon();

            // 게임을 다시 시작
            StartGame();
        }

        public void ChangeSeon()
        {
            if (seon.Equals("컴퓨터")) seon = "플레이어";
            else seon = "컴퓨터";
        }

        public void ChangeAllButtonColors(Color backColor, Color foreColor)
        {
            foreach (Control control in form.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = backColor;
                    button.ForeColor = foreColor;
                }
            }
        }

        #region ReadFile

        // Pae 객체 20개 생성(1광 ~ 10띠)
        public void ReadPaeFromFile(string filePath)
        {
            List<string> paeName = new List<string>();

            // Ascii Art가 담겨진 텍스트 파일명 불러오기
            asciiText.Clear();
            for (int i = 1; i <= 20; i++) asciiText.Add($"{i}.txt");

            foreach (string str in File.ReadLines(filePath))
            {
                paeName.Add(str); // 리스트에 문자열 추가
            }

            // PaeMonth, PaeName을 필드로 가지는 객체 생성
            // pae[0] => ("1", "1광", "아스키 아트 텍스트 파일 이름" , "아스키 아트 전체 텍스트")
            for (int i = 0; i < asciiText.Count; i++)
            {
                if (i == 0)
                {
                    pae.Add(new Pae((i + 1).ToString(), paeName[i], asciiText[i]));
                }
                else
                {
                    pae.Add(new Pae((i / 2 + 1).ToString(), paeName[i], asciiText[i]));
                }
            }
        }

        // 문자열 파일 읽어 출력
        public void DisplayTextFromFile(string filePath, TextBox targetTextBox)
        {
            try
            {
                // 지정된 파일의 모든 텍스트를 읽는다.
                string fileContent = File.ReadAllText(filePath);

                // 읽어온 텍스트를 대상 텍스트 박스에 표시
                targetTextBox.Text = fileContent;
            }
            catch (Exception ex)
            {
                // 오류 발생 시 처리
                MessageBox.Show($"파일을 읽는 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ascii Art 텍스트 파일 읽기
        public string[] ReadAllLinesFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllLines(fileName);
            }
            else
            {
                WriteLine($"{fileName} 파일을 불러올 수 없습니다.");

                return new string[0];
            }
        }

        #endregion

        // 각 패 텍스트 박스 초기화
        public void ResetPae()
        {
            pae1.Clear();
            pae2.Clear();
            pae3.Clear();
            pae4.Clear();
        }

        // 패 선택
        public void SelectPae()
        {
            Random rand = new Random();

            for (int i = 0; i < 4; i++)
            {
                int randIndex;
                do
                {
                    randIndex = rand.Next(0, pae.Count);
                } while (pae[randIndex] == null);

                if (i < 2)
                {
                    // cpu 패 선택
                    cpuPae[i] = pae[randIndex];
                }
                else
                {
                    // 플레이어 패 선택
                    playerPae[i - 2] = pae[randIndex];
                }

                pae.RemoveAt(randIndex); // 중복된 값 제거
            }
        }

        public void PrintPae()
        {
            if (!timer1.Enabled)
            {
                timer2.Stop();
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                timer2.Start();
            }
        }

        public void SecondDraw()
        {
            timer2.Start();
            jokboHelper.Clear();
            DisplayJokboHelper(jokboHelper, playerPae, "플레이어");
        }

        // 결과 공개
        public async void ShowDown()
        {
            gameProgress.Text = "패를 공개합니다.";
            await Task.Delay(2000);
            CheckEndBetting();
            ResetPae();
            InitIndex();
            PrintPae();
            DisplayJokboHelper(gameProgress, GetCpuPae(), "컴퓨터");
            gameProgress.AppendText("\r\n");
            DisplayJokboHelper(gameProgress, GetPlayerPae(), "플레이어");
            DisplayJokboHelper(jokboHelper, GetPlayerPae(), "플레이어");
            CompareJokbo(CpuJokbo, PlayerJokbo);

            await Task.Delay(3000);
            RestartGame();
        }

        #region Timer

        // PrintResult(), cpu 패도 모두 출력하고 비교하여 결과 산출, 1밀리초마다 이벤트 발생
        private void Timer1_Tick(object sender, EventArgs e)
        {
            // 베팅이 끝나지 않았다면
            if (!endBetting)
            {
                DisplayLinesBeforeBetting(0, pae1, cpuPae[0], true);
                DisplayLinesBeforeBetting(0, pae3, playerPae[0], false);
            }
            else
            {
                DisplayLines(0, pae1, cpuPae[0], true);
                DisplayLines(0, pae3, playerPae[0], false);
                DisplayLines(1, pae2, cpuPae[1], true);
                DisplayLines(1, pae4, playerPae[1], false);
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            // 베팅이 끝나지 않았다면
            if (!endBetting)
            {
                DisplayLinesBeforeBetting(1, pae2, cpuPae[1], true);
                DisplayLinesBeforeBetting(1, pae4, playerPae[1], false);
            }
        }

        #endregion

        #region Print Ascii Art

        // Ascii Art 출력
        private void DisplayLines(int index, TextBox textBox, Pae pae, bool isCpu)
        {
            if (isCpu)
            {
                if (cpuIndex[index] < pae.AsciiArt.Length)
                {
                    // ASCII ART를 한줄 씩 TextBox에 추가
                    textBox.AppendText(pae.AsciiArt[cpuIndex[index]] + Environment.NewLine);
                    cpuIndex[index]++;
                }
                else
                {
                    if (index == 0)
                    {
                        timer1.Stop();
                        timer2.Start();
                    }
                    else
                    {
                        timer2.Stop();
                    }
                }
            }
            else
            {
                if (playerIndex[index] < pae.AsciiArt.Length)
                {
                    // Ascii Art를 한줄 씩 TextBox에 추가
                    textBox.AppendText(pae.AsciiArt[playerIndex[index]] + Environment.NewLine);
                    playerIndex[index]++;
                }
                else
                {
                    if (index == 0)
                    {
                        timer1.Stop();
                        timer2.Start();
                    }
                    else
                    {
                        timer2.Stop();
                    }
                }
            }
        }

        // Ascii Art 출력(cpu는 뒷면 출력)
        private void DisplayLinesBeforeBetting(int index, TextBox textBox, Pae pae, bool isCpu)
        {
            if (isCpu)
            {
                if (cpuIndex[index] < pae.AsciiArt.Length)
                {
                    // ASCII ART를 한줄 씩 TextBox에 추가
                    textBox.AppendText(pae.BackAsciiArt[cpuIndex[index]] + Environment.NewLine);
                    cpuIndex[index]++;
                }
                else
                {
                    if (index == 0)
                    {
                        timer1.Stop();
                    }
                    else
                    {
                        timer2.Stop();
                    }
                }
            }
            else
            {
                if (playerIndex[index] < pae.AsciiArt.Length)
                {
                    // ASCII ART를 한줄 씩 TextBox에 추가
                    textBox.AppendText(pae.AsciiArt[playerIndex[index]] + Environment.NewLine);
                    playerIndex[index]++;
                }
                else
                {
                    if (index == 0)
                    {
                        timer1.Stop();
                    }
                    else
                    {
                        timer2.Stop();
                    }
                }
            }
        }

        #endregion

        #region Compare Jokbo

        // 족보 도우미에 족보 설명 출력
        public void DisplayJokboHelper(TextBox textBox, Pae[] pae, string name)
        {
            JokboHelper jokboHelper = new JokboHelper(form, this);
            jokboHelper.DisplayJokboHelper(textBox, pae, name);
        }

        // 컴퓨터와 플레이어 족보 비교
        public void CompareJokbo(string cpuJokbo, string playerJokbo)
        {
            JokboComparer jokboComparer = new JokboComparer(this, gameProgress);
            jokboComparer.GetWinner(cpuJokbo, playerJokbo);
        }

        #endregion
    }
}
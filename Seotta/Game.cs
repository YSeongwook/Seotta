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

        private bool endBetting = false;

        #endregion

        private string seon = "컴퓨터";    // 선을 결정하는 변수
        private bool reGame = false;
        private int cpuMoney;           // cpu 소지금
        private int playerMoney;        // 플레이어 소지금
        private int currentPot;         // 현재 판돈
        private int cpuBettingMoney;    // cpu가 직전에 베팅한 금액
        private int playerBettingMoney; // cpu가 직전에 베팅한 금액
        public bool ReGame
        {
            get { return reGame; }
            set { reGame = value; }
        }
        private int turn;

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

        #endregion

        // 게임 시작
        public void StartGame()
        {
            cpuPae = new Pae[2];
            playerPae = new Pae[2];

            turn = 1;

            // 패의 이름(1광, 1띠, etc..)가 담겨 있는 파일을 매개변수로 Pae 클래스 객체 20개 생성(Pae 클래스 객체를 사용하여 족보 계산)
            ReadPaeFromFile("Pae_Name.txt");

            GameLoop();
        }

        // 소지금, 베팅금, 판돈 초기화
        public void InitMoney()
        {
            cpuMoney = 10000;
            playerMoney = 10000;
            currentPot = 0;
            cpuBettingMoney = 0;
            playerBettingMoney = 0;
        }

        public async void GameLoop()
        {
            ResetPae();
            SelectPae();

            if (reGame)
            {
                gameProgress.Text = "재경기합니다.";
                gameProgress.AppendText ($"\r\n{seon}가 선입니다.");
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

            Betting(seon);
            // cpu나 player 둘중 하나라도 다이 한다면 RestartGame()
            // 엔터키 이벤트에 있는 메서드
            // betting
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

            // 베팅 버튼 색 원래대로 돌리기
            // 이전 버튼의 색상 원래대로 변경
            
            if(form.GetPreviousButton() != null)
            {
                previousButton = form.GetPreviousButton();
                previousButton.BackColor = Color.Black;
                previousButton.ForeColor = Color.White;
            }
            

            // 게임을 다시 시작
            StartGame();
        }

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
            if(!timer1.Enabled)
            {
                timer2.Stop();
                timer1.Start();
            } else
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
        public void ShowDown()
        {
            CheckEndBetting();
            ResetPae();
            InitIndex();
            PrintPae();
            DisplayJokboHelper(gameProgress, GetCpuPae(), "컴퓨터");
            gameProgress.AppendText("\r\n");
            DisplayJokboHelper(gameProgress, GetPlayerPae(), "플레이어");
            DisplayJokboHelper(jokboHelper, GetPlayerPae(), "플레이어");
            CompareJokbo(CpuJokbo, PlayerJokbo);
        }

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

        public async void Betting(string seon)
        {
            // 누가 선인지 판별해서 먼저 베팅
            if (seon.Equals("컴퓨터"))
            {
                CpuBetting(cpuPae, turn);
            } 
            else
            {
                await Task.Delay(3000);
                gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
            }
        }

        #region CpuBetting
        // CPU 베팅 메서드
        public void CpuBetting(Pae[] pae, int turn)
        {
            // 첫번째 베팅
            if (turn == 1)
            {
                switch (pae[0].PaeName)
                {
                    // 첫 패에 따라 다른 베팅 진행
                    case "1광":
                    case "4열끗":
                    case "1띠":
                    case "4띠":
                    case "9열끗":
                    case "10열끗":
                    case "10띠":
                    case "9띠":
                        // 하프
                        CpuHalfBetting();
                        break;
                    case "3광":
                    case "8광":
                        CpuCallBetting(80); // 80% 확률로 콜
                        break;
                    case "2열끗":
                    case "2띠":
                    case "6열끗":
                    case "6띠":
                        CpuCallBetting(60); // 60% 확률로 콜
                        break;
                    case "7열끗":
                    case "8띠":
                    case "7띠":
                    case "5열끗":
                    case "5띠":
                    case "3띠":
                        CpuCallBetting(40); // 40% 확률로 콜
                        break;
                }
            }
            // 2번째 베팅
            else
            {
                // 현재 CPU의 족보에 따라 다른 베팅 진행
                switch (CpuJokbo)
                {
                    case "38광땡":
                    case "13광땡":
                    case "18광땡":
                    case "장땡":
                    case "멍텅구리구사":
                    case "9땡":
                    case "8땡":
                    case "7땡":
                    case "6땡":
                    case "5땡":
                    case "4땡":
                    case "3땡":
                    case "2땡":
                    case "1땡":
                    case "구사":
                    case "알리":
                    case "독사":
                        // 하프
                        CpuHalfBetting();
                        break;
                    case "구삥":
                    case "장삥":
                    case "장사":
                    case "세륙":
                    case "갑오(9끗)":
                        // 콜
                        CpuCallBetting(100);
                        break;
                    case "땡잡이":
                        // 상황에 따라 콜 또는 다이
                        if (cpuMoney > playerMoney)
                            CpuCallBetting(100);
                        else
                            CpuDie();
                        break;
                    case "8끗":
                    case "7끗":
                    case "6끗":
                    case "5끗":
                        // 체크
                        CpuCheckBetting();
                        break;
                    case "4끗":
                    case "3끗":
                    case "2끗":
                    case "1끗":
                    case "망통(0끗)":
                        // 다이
                        CpuDie();
                        break;
                    case "암행어사":
                        // 상황에 따라 체크 또는 다이
                        if (cpuMoney > playerMoney)
                            CpuCheckBetting();
                        else
                            CpuDie();
                        break;
                }
                if(seon.Equals("플레이어")) this.seon = "컴퓨터";
            }
        }

        // 하프 베팅 메서드
        private async void CpuHalfBetting()
        {
            // 선이면 첫 베팅 시 playerBettingMoney는 0
            int betAmount = playerBettingMoney + (currentPot / 2);
            // 소지금에서 베팅만큼 제출
            cpuMoney -= betAmount;
            // 베팅금 저장
            cpuBettingMoney = betAmount;

            await Task.Delay(2000);
            gameProgress.Text = "컴퓨터가 하프 했습니다.";
            await Task.Delay(1000);
            gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
        }

        // 콜 베팅 메서드
        private async void CpuCallBetting(int probability)
        {
            Random random = new Random();
            int bettingProbability = random.Next(0, 100);

            // 매개변수로 넣은 수보다 작으면, 80을 넣으면 80%로 실행
            if (bettingProbability <= probability)
            {
                // 앞서 베팅한 플레이어의 베팅금만큼 베팅
                int betAmount = playerBettingMoney;
                // 소지금에서 베팅만큼 제출
                cpuMoney -= betAmount;
                // 베팅금 저장
                cpuBettingMoney = betAmount;

                await Task.Delay(2000);
                gameProgress.Text = "컴퓨터가 콜 했습니다.";
                await Task.Delay(1000);
                gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
            }
            else
            {
                CpuDie();
            }
        }

        // 체크 베팅 메서드
        private async void CpuCheckBetting()
        {
            // 선이면 첫 베팅 시 playerBettingMoney는 0
            int betAmount = 0;
            // 소지금에서 베팅만큼 제출
            cpuMoney -= betAmount;
            // 베팅금 저장
            cpuBettingMoney = betAmount;

            await Task.Delay(2000);
            gameProgress.Text = "컴퓨터가 체크 했습니다.";
            await Task.Delay(1000);
            gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
        }

        // 다이 베팅 메서드
        private async void CpuDie()
        {
            await Task.Delay(2000);
            gameProgress.Text = "컴퓨터가 다이 했습니다.";

            ChangeSeon();

            await Task.Delay(2000);
            RestartGame();
        }

        #endregion

        // playerBetting() 만들어야하는데 위의 CpuBetting들이랑 겹치는걸 매개변수를 잘 넘겨서 구분하면 메소드를 줄일 수 있을 것 같다.

        public async void PlayerBetting(int betBtn)
        {
            int betAmount;

            switch (betBtn)
            {
                case 7: // 콜
                    PlayerCallBetting();
                    break;
                case 8: // 다이
                    PlayerDieBetting();
                    break;
                case 4:
                    // 삥
                    break;
                case 5:
                    // 체크
                    break;
                case 1:
                    // 따당
                    break;
                case 0:
                    PlayerHalfBetting();
                    break;
            }

            if(betBtn != 8)
            {
                if (seon.Equals("컴퓨터"))
                {
                    if (turn == 2)
                    {
                        // 패 공개
                        await Task.Delay(2000);
                        gameProgress.Text = "각자의 패를 공개합니다.";
                        await Task.Delay(1000);
                        ShowDown();
                        this.seon = "플레이어";
                    }
                    else
                    {
                        SecondDraw();
                        turn++;
                        await Task.Delay(1000);
                        DisplayJokboHelper(gameProgress, GetCpuPae(), "컴퓨터");
                        CpuBetting(cpuPae, turn);
                    }
                }
                else
                {
                    if (turn == 2)
                    {
                        SecondDraw();
                        await Task.Delay(1000);
                        DisplayJokboHelper(gameProgress, GetCpuPae(), "컴퓨터");
                        CpuBetting(cpuPae, turn);
                    }
                    else
                    {
                        SecondDraw();
                        turn++;
                        await Task.Delay(1000);
                        DisplayJokboHelper(gameProgress, GetCpuPae(), "컴퓨터");
                        CpuBetting(cpuPae, turn);
                    }
                }
            }
        }

        public void PlayerCallBetting()
        {
            // 앞서 베팅한 플레이어의 베팅금만큼 베팅
            int betAmount = cpuBettingMoney;
            // 소지금에서 베팅만큼 제출
            playerMoney -= betAmount;
            // 베팅금 저장
            playerBettingMoney = betAmount;

            gameProgress.Text = "콜 했습니다.";
            // gameProgress.AppendText($"\r\n{seon}, {turn}, cpu: {CpuJokbo}"); // 현재 테스트 결과 cpuJokbo 공백
        }

        public void PlayerHalfBetting()
        {
            // 하프 or 올인
            // 선이면 첫 베팅 시 playerBettingMoney는 0
            int betAmount = cpuBettingMoney + (currentPot / 2);
            // 소지금에서 베팅만큼 제출
            playerMoney -= betAmount;
            // 베팅금 저장
            playerBettingMoney = betAmount;

            gameProgress.Text = "하프 했습니다.";
        }

        public async void PlayerDieBetting()
        {
            gameProgress.Text = "다이 했습니다.";
            ChangeSeon();

            await Task.Delay(2000);
            RestartGame();
        }

        public void ChangeSeon()
        {
            if (this.seon.Equals("컴퓨터")) seon = "플레이어";
            else seon = "컴퓨터";
        }
    }
}
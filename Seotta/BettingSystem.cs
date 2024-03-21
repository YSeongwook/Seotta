using System.Threading.Tasks;
using System;
using static System.Console;
using System.Windows.Forms;

namespace Seotta
{
    public class BettingSystem
    {
        Game game;
        TextBox gameProgress;

        string seon;

        private int cpuMoney;           // cpu 소지금
        private int playerMoney;        // 플레이어 소지금
        private int currentPot;         // 현재 판돈
        private int cpuBettingMoney;    // cpu가 직전에 베팅한 금액
        private int playerBettingMoney; // cpu가 직전에 베팅한 금액

        public int turn { get; set; }

        public BettingSystem GetBettingSystem()
        {
            return this;
        }

        public BettingSystem(Game game, TextBox gameProgess, string seon, int cpuMoney, int playerMoney, int currentPot, int cpuBettingMoney, int playerBettingMoney)
        {
            this.game = game;
            this.gameProgress = gameProgess;
            this.seon = seon;
            this.cpuMoney = cpuMoney;
            this.playerMoney = playerMoney;
            this.currentPot = currentPot;
            this.cpuBettingMoney = cpuBettingMoney;
            this.playerBettingMoney = playerBettingMoney;
            this.turn = 1;
        }

        public async void Betting(string seon)
        {
            // 누가 선인지 판별해서 먼저 베팅
            if (seon.Equals("컴퓨터"))
            {
                await Task.Delay(1000);
                CpuBetting(game.GetCpuPae(), game.GetTurn());
            }
            else
            {
                await Task.Delay(3000);
                gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
            }
        }

        #region CpuBetting
        // CPU 베팅 메서드
        public async void CpuBetting(Pae[] pae, int turn)
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
                switch (game.CpuJokbo)
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
                        {
                            CpuCallBetting(100);
                        }
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

            await Task.Delay(1000);
            gameProgress.Text = "컴퓨터가 베팅합니다.";
            await Task.Delay(2000);
            gameProgress.Text = "컴퓨터가 하프 했습니다.";

            if (seon.Equals("컴퓨터"))
            {
                await Task.Delay(1000);
                gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
            }

            if (seon.Equals("플레이어"))
            {
                if (turn == 1)
                {
                    await Task.Delay(1000);
                    gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
                    turn++;
                }
                else if (turn == 2)
                {
                    await Task.Delay(2000);
                    game.ShowDown();
                }
            }
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

                await Task.Delay(1000);
                gameProgress.Text = "컴퓨터가 베팅합니다.";
                await Task.Delay(2000);
                gameProgress.Text = "컴퓨터가 콜 했습니다.";

                if (seon.Equals("컴퓨터"))
                {
                    await Task.Delay(1000);
                    gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
                }

                if (seon.Equals("플레이어"))
                {
                    if (turn == 1)
                    {
                        await Task.Delay(1000);
                        gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
                        turn++;
                    }
                    else if (turn == 2)
                    {
                        await Task.Delay(2000);
                        game.ShowDown();
                    }
                }
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

            await Task.Delay(1000);
            gameProgress.Text = "컴퓨터가 베팅합니다.";
            await Task.Delay(2000);
            gameProgress.Text = "컴퓨터가 체크 했습니다.";

            if (seon.Equals("컴퓨터"))
            {
                await Task.Delay(1000);
                gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
            }

            if (seon.Equals("플레이어"))
            {
                if (turn == 1)
                {
                    await Task.Delay(1000);
                    gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
                    turn++;
                } else if(turn == 2)
                {
                    await Task.Delay(2000);
                    game.ShowDown();
                }
            }
        }

        // 다이 베팅 메서드
        private async void CpuDie()
        {
            await Task.Delay(1000);
            gameProgress.Text = "컴퓨터가 베팅합니다.";
            await Task.Delay(2000);
            gameProgress.Text = "컴퓨터가 다이 했습니다.";

            await Task.Delay(1000);
            game.RestartGame();
        }

        #endregion

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

            if (betBtn != 8)
            {
                if (seon.Equals("컴퓨터"))
                {
                    if (turn == 2)
                    {
                        // 패 공개
                        await Task.Delay(2000);
                        game.ShowDown();
                        this.seon = "플레이어";
                    }
                    else
                    {
                        game.SecondDraw();
                        await Task.Delay(1000);
                        game.DisplayJokboHelper(gameProgress, game.GetCpuPae(), "컴퓨터");
                        CpuBetting(game.GetCpuPae(), ++turn);
                    }
                }
                else // 플레이어가 선일 경우
                {
                    if (turn == 2)
                    {
                        game.SecondDraw();
                        await Task.Delay(1000);
                        game.DisplayJokboHelper(gameProgress, game.GetCpuPae(), "컴퓨터");
                        CpuBetting(game.GetCpuPae(), turn);
                    }
                    else
                    {
                        game.DisplayJokboHelper(gameProgress, game.GetCpuPae(), "컴퓨터");
                        CpuBetting(game.GetCpuPae(), turn);
                        await Task.Delay(4000);
                        game.SecondDraw();
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

            await Task.Delay(2000);
            game.RestartGame();
        }
    }
}

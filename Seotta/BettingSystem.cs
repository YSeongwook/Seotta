using System.Threading.Tasks;
using System;
using System.Windows.Forms;

namespace Seotta
{
    public class BettingSystem
    {
        Game game;
        TextBox gameProgress;
        TextBox currentPotBox;
        Label cpuMoneyLabel;
        Label playerMoneyLabel;

        string seon;            // 누가 먼저 베팅할 지정하는 선 변수
        int cpuBettingMoney;    // cpu가 직전에 베팅한 금액
        int playerBettingMoney; // cpu가 직전에 베팅한 금액
        const int Ante = 1000000;

        public int turn { get; set; }
        public int CpuMoney { get; set; }// cpu 소지금
        public int PlayerMoney { get; set; }// 플레이어 소지금
        public int CurrentPot { get; set; }// 현재 판돈

        public BettingSystem GetBettingSystem()
        {
            return this;
        }

        public BettingSystem(Game game, TextBox gameProgess, string seon, int cpuMoney, int playerMoney, int currentPot, int cpuBettingMoney, int playerBettingMoney)
        {
            this.game = game;
            this.gameProgress = gameProgess;
            this.seon = seon;
            this.CpuMoney = cpuMoney;
            this.PlayerMoney = playerMoney;
            this.CurrentPot = currentPot;
            this.cpuBettingMoney = cpuBettingMoney;
            this.playerBettingMoney = playerBettingMoney;
            this.currentPotBox = game.GetCurrentPotBox();
            this.cpuMoneyLabel = game.GetCpuMoneyLabel();
            this.playerMoneyLabel = game.GetPlayerMoneyLabel();
            this.turn = 1;
        }

        public async void Betting(string _seon)
        {
            this.seon = _seon; // 누가 선인지 판별해서 먼저 베팅

            // 판돈 100만씩 지불
            await Task.Delay(1000);
            CurrentPot = Ante * 2;
            CpuMoney -= Ante;
            PlayerMoney -= Ante;
            CalculateMoney();

            if (seon.Equals("컴퓨터"))
            {
                await Task.Delay(1000);
                CpuBetting(game.GetCpuPae(), turn);
            }
            else
            {
                await Task.Delay(2000);
                gameProgress.Text = "플레이어의 차례입니다. 베팅해주세요.";
            }
        }

        public void CalculateMoney()
        {
            cpuMoneyLabel.Text = $"{ToKoreanCurrency(CpuMoney)}";
            playerMoneyLabel.Text = $"{ToKoreanCurrency(PlayerMoney)}";
            currentPotBox.Text = $"현재 판돈: {ToKoreanCurrency(CurrentPot)}";
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
                        CpuHalfBetting(100);
                        break;
                    case "3광":
                    case "8광":
                        CpuHalfBetting(80); // 80% 확률로 하프
                        break;
                    case "2열끗":
                    case "2띠":
                    case "6열끗":
                    case "6띠":
                        CpuHalfBetting(60); // 60% 확률로 하프
                        break;
                    case "7열끗":
                    case "8열끗":
                    case "7띠":
                    case "5열끗":
                    case "5띠":
                    case "3띠":
                        CpuHalfBetting(40); // 40% 확률로 하프
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
                        // 하프
                        CpuHalfBetting(100);
                        break;
                    case "구사":
                    case "알리":
                    case "독사":
                        // 하프
                        CpuHalfBetting(80);
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
                        if (CpuMoney > PlayerMoney)
                        {
                            CpuCallBetting(80);
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
                        if (CpuMoney > PlayerMoney)
                            CpuCheckBetting();
                        else
                            CpuDie();
                        break;
                }
            }
        }

        // 하프 베팅 메서드
        private async void CpuHalfBetting(int probability)
        {
            Random random = new Random();
            int bettingProbability = random.Next(0, 100);

            // 매개변수로 넣은 수보다 작으면, 80을 넣으면 80%로 실행
            if (bettingProbability <= probability)
            {
                // 선이면 첫 베팅 시 playerBettingMoney는 0
                int betAmount = playerBettingMoney + (CurrentPot / 2);
                // 소지금에서 베팅만큼 제출
                CpuMoney -= betAmount;
                // 베팅금 저장
                cpuBettingMoney = betAmount;

                await Task.Delay(1000);
                gameProgress.Text = "컴퓨터가 베팅합니다.";
                await Task.Delay(2000);
                gameProgress.Text = "컴퓨터가 하프 했습니다.";

                // 현재 판돈에 저장
                CurrentPot += betAmount;
                CalculateMoney();

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
            } else
            {
                if (seon.Equals("플레이어"))
                {
                    CpuDdadangBetting();
                }
            }
        }

        private async void CpuDdadangBetting()
        {
            // 앞서 베팅한 플레이어의 베팅금만큼 베팅
            int betAmount = playerBettingMoney * 2;
            // 소지금에서 베팅만큼 제출
            CpuMoney -= betAmount;

            // 베팅금 저장
            cpuBettingMoney = betAmount;

            await Task.Delay(1000);
            gameProgress.Text = "컴퓨터가 베팅합니다.";
            await Task.Delay(2000);
            gameProgress.Text = "컴퓨터가 따당 했습니다.";

            // 현재 판돈에 저장
            CurrentPot += betAmount;
            CalculateMoney();

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
                CpuMoney -= betAmount;

                // 베팅금 저장
                cpuBettingMoney = betAmount;

                await Task.Delay(1000);
                gameProgress.Text = "컴퓨터가 베팅합니다.";
                await Task.Delay(2000);
                gameProgress.Text = "컴퓨터가 콜 했습니다.";

                // 현재 판돈에 저장
                CurrentPot += betAmount;
                CalculateMoney();

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
            CpuMoney -= betAmount;
            // 베팅금 저장
            cpuBettingMoney = betAmount;

            await Task.Delay(1000);
            gameProgress.Text = "컴퓨터가 베팅합니다.";
            await Task.Delay(2000);
            gameProgress.Text = "컴퓨터가 체크 했습니다.";

            // 현재 판돈에 저장
            CurrentPot += betAmount;
            CalculateMoney();

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
            PlayerMoney += CurrentPot;
            game.RestartGame();
        }

        #endregion

        public async void PlayerBetting(int betBtn)
        {
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
                    PlayerBbingBetting();
                    break;
                case 5:
                    // 체크
                    PlayerCheckBetting();
                    break;
                case 1:
                    // 따당
                    PlayerDdadangBetting();
                    break;
                case 0:
                    // 하프 or 올인
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
            // 소지금에서 베팅금만큼 지출
            PlayerMoney -= betAmount;
            // 베팅금 저장
            playerBettingMoney = betAmount;

            gameProgress.Text = "콜 했습니다.";

            // 현재 판돈에 저장
            CurrentPot += betAmount;
            CalculateMoney();
        }

        public void PlayerHalfBetting()
        {
            // 하프 or 올인
            // 선이면 첫 베팅 시 playerBettingMoney는 0
            int betAmount = cpuBettingMoney + (CurrentPot / 2);
            // 소지금에서 베팅금만큼 지출
            PlayerMoney -= betAmount;
            // 베팅금 저장
            playerBettingMoney = betAmount;

            gameProgress.Text = "하프 했습니다.";

            // 현재 판돈에 저장
            CurrentPot += betAmount;
            CalculateMoney();
        }

        public void PlayerCheckBetting()
        {
            // 앞서 베팅한 플레이어의 베팅금의 2배 베팅
            int betAmount = 0;
            // 소지금에서 베팅금만큼 지출
            PlayerMoney -= betAmount;
            // 베팅금 저장
            playerBettingMoney = betAmount;

            gameProgress.Text = "체크 했습니다.";

            // 현재 판돈에 저장
            CurrentPot += betAmount;
            CalculateMoney();
        }

        public  void PlayerDdadangBetting()
        {
            // 앞서 베팅한 플레이어의 베팅금의 2배 베팅
            int betAmount = cpuBettingMoney * 2;
            // 소지금에서 베팅금만큼 지출
            PlayerMoney -= betAmount;
            // 베팅금 저장
            playerBettingMoney = betAmount;

            gameProgress.Text = "따당 했습니다.";

            // 현재 판돈에 저장
            CurrentPot += betAmount;
            CalculateMoney();
        }

        public void PlayerBbingBetting()
        {
            // 기본금 만큼 베팅
            int betAmount = 1000000;
            // 소지금에서 베팅금만큼 지출
            PlayerMoney -= betAmount;
            // 베팅금 저장
            playerBettingMoney = betAmount;

            gameProgress.Text = "삥 했습니다.";

            // 현재 판돈에 저장
            CurrentPot += betAmount;
            CalculateMoney();
        }

        public async void PlayerDieBetting()
        {
            gameProgress.Text = "다이 했습니다.";

            await Task.Delay(2000);
            CpuMoney += CurrentPot;
            game.RestartGame();
        }

        public string ToKoreanCurrency(int amount)
        {
            if (amount < 10000)
            {
                return $"{amount}전"; // 만전 미만은 그대로 표현
            }
            else if (amount < 100000000)
            {
                int unit = amount / 10000; // 만단위 계산
                int remainder = amount % 10000; // 만단위를 제외한 나머지 계산
                if (remainder == 0)
                {
                    return $"{unit}만전"; // 만단위로 표현
                }
                else
                {
                    return $"{unit}만 {remainder}전"; // 만단위와 나머지를 함께 표현
                }
            }
            else
            {
                int unit = amount / 100000000; // 억단위 계산
                int manRemainder = amount % 100000000; // 억단위를 제외한 나머지 계산
                int manUnit = manRemainder / 10000; // 만단위 계산
                int manRemainderRemainder = manRemainder % 10000; // 만단위를 제외한 나머지 계산
                if (manRemainderRemainder == 0)
                {
                    if(manUnit == 0)
                    {
                        return $"{unit}억전"; // 억단위와 만단위 표현
                    }
                    return $"{unit}억 {manUnit}만전"; // 억단위와 만단위 표현
                }
                else
                {
                    return $"{unit}억 {manUnit}만 {manRemainderRemainder}전"; // 억단위, 만단위, 나머지 표현
                }
            }
        }

        // 94 재경기, 재경기하면 판돈 그대로 유지
    }
}

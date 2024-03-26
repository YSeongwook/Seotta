using Newtonsoft.Json;
using Seotta;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// 족보로 승패 결정하는 클래스
public class JokboComparer
{
    Game game;
    TextBox gameProgress;
    BettingSystem bettingSystem;

    Dictionary<string, int> jokboLevels;

    public JokboComparer(Game game, TextBox gameProgress, string filePath)
    { 
        this.game = game;
        this.gameProgress = gameProgress;
        this.bettingSystem = game.GetBettingSystem();
        LoadDescriptionsFromJson(filePath);
    }

    void LoadDescriptionsFromJson(string filePath)
    {
        // JSON 파일에서 문자열을 읽어옴
        string jsonText = File.ReadAllText(filePath);

        // JSON 문자열을 딕셔너리로 변환
        this.jokboLevels = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonText);
    }

    public void GetWinner(string cpuJokbo, string playerJokbo)
    {
        string cpuFail = "";
        string playerFail = "";

        // CPU와 플레이어의 족보 등급 계산
        int cpuLevel = jokboLevels[cpuJokbo];
        int playerLevel = jokboLevels[playerJokbo];

        // 특수패인 경우에 대한 예외 처리
        if (cpuJokbo.Equals("암행어사"))
        {
            // 암행어사가 하나라도 있다면 땡잡이는 있을 수 없다.
            if (playerJokbo.Contains("광땡"))
            {
                // cpu win
            }
            else
            {
                cpuFail = "암행어사 => ";
                cpuJokbo = "1끗";
                cpuLevel = 1;
            }

        }
        else if (playerJokbo.Equals("암행어사"))
        {
            if (cpuJokbo.Contains("광땡"))
            {
                // player win
            }
            else
            {
                playerFail = "암행어사 => ";
                playerJokbo = "1끗";
                playerLevel = 1;
            }
        }

        // 땡잡이가 나온 경우 광땡은 18광땡만 나올 수 있다.
        if (cpuJokbo.Equals("땡잡이"))
        {
            if (playerJokbo.Equals("18광땡"))     // 땡잡이는 광땡을 잡을 수 없다.
            {
                // player win
            }
            else if (playerJokbo.Equals("장땡")) // 땡잡이는 장땡을 잡을 수 없다.
            {
                // player win
            }
            else if (playerJokbo.Contains("땡")) // 땡잡이는 9땡 ~ 1땡을 잡을 수 있다.
            {
                // cpu win
            }
            // 상대가 땡이 아닌 경우
            else
            {
                cpuFail = "땡잡이 => ";
                cpuJokbo = "망통(0끗)";
                cpuLevel = 1;
            }
        }
        else if (playerJokbo.Equals("땡잡이"))
        {
            if (cpuJokbo.Equals("18광땡"))     // 땡잡이는 광땡을 잡을 수 없다.
            {
                // cpu win
            }
            else if (cpuJokbo.Equals("장땡")) // 땡잡이는 장땡을 잡을 수 없다.
            {
                // cpu win
            }
            else if (cpuJokbo.Contains("땡")) // 땡잡이는 9땡 ~ 1땡을 잡을 수 있다.
            {
                // player win
            }
            // 상대가 땡이 아닌 경우
            else
            {
                playerFail = "땡잡이 => ";
                playerJokbo = "망통(0끗)";
                playerLevel = 1;
            }
        }

        // 현재 구사에서 오류가 생김

        if (cpuJokbo.Equals("멍텅구리구사"))
        {
            // 멍텅구리 구사의 상대가 9땡보다 높은 경우 3끗으로 처리
            if (playerLevel > jokboLevels["9땡"])
            {
                cpuFail = "멍텅구리구사 => ";
                cpuJokbo = "3끗";
                cpuLevel = 1; 
            }
            else
            {
                // 재경기
                gameProgress.AppendText("\r\n구사 재경기");
                game.RestartGame();
                game.ReGame = true;
            }
        }

        if (playerJokbo.Equals("멍텅구리구사"))
        {
            if (cpuLevel > jokboLevels["9땡"])
            {
                playerFail = "멍텅구리구사 => ";
                playerJokbo = "3끗";
                playerLevel = 1;
            }
            else
            {
                // 재경기
                gameProgress.AppendText("\r\n구사 재경기");
                game.RestartGame();
                game.ReGame = true;
            }
        }

        if (cpuJokbo.Equals("구사"))
        {
            if (playerLevel > jokboLevels["알리"])
            {
                cpuFail = "구사 => ";
                cpuJokbo = "3끗";
                cpuLevel = 1; // 구사의 상대가 알리보다 높은 경우 3끗으로 처리
            }
            else
            {
                // 재경기
                gameProgress.AppendText("\r\n구사 재경기");
                game.RestartGame();
                game.ReGame = true;
            }
        }

        if (playerJokbo.Equals("구사"))
        {
            if (cpuLevel > jokboLevels["알리"])
            {
                playerFail = "구사 => ";
                playerJokbo = "3끗";
                playerLevel = 1; // 구사의 상대가 알리보다 높은 경우 3끗으로 처리
            }
            else
            {
                // 재경기
                gameProgress.AppendText("\r\n구사 재경기");
                game.RestartGame();
                game.ReGame = true;
            }
        }

        DisplayWinnder(cpuJokbo, playerJokbo, cpuFail, playerFail, cpuLevel, playerLevel);
    }

    // 족보의 앞자리 월 수를 가져오는 메서드
    private int GetMonth(string jokbo)
    {
        if (jokbo.Equals("갑오(9끗)"))
        {
            return 9;
        }
        else if (jokbo.Equals("망통(0끗)"))
        {
            return 0;
        }
        else
        {
            return int.Parse(jokbo.Substring(0, 1));
        }
    }

    // CPU와 플레이어의 족보 등급을 비교하여 승패 결정
    public void DisplayWinnder(string cpuJokbo, string playerJokbo, string cpuFail, string playerFail, int cpuLevel, int playerLevel)
    {
        string winnerMessage = "";

        // 플레이어 승리
        if (playerLevel > cpuLevel)
        {
            winnerMessage = "\r\n플레이어가 승리했습니다.";
            // 플레이어 판돈 획득
            bettingSystem.PlayerMoney += bettingSystem.CurrentPot;
        }
        // 컴퓨터 승리
        else if (playerLevel < cpuLevel)
        {
            winnerMessage = "\r\n컴퓨터가 승리했습니다.";
            // 컴퓨터 판돈 획득
            bettingSystem.CpuMoney += bettingSystem.CurrentPot;
        }
        else
        {
            // 족보 레벨이 같은 경우, 족보의 앞자리 월 수를 비교
            int playerMonth = GetMonth(playerJokbo);
            int cpuMonth = GetMonth(cpuJokbo);

            if (playerMonth > cpuMonth)
            {
                winnerMessage = "\r\n플레이어가 승리했습니다.";
                bettingSystem.PlayerMoney += bettingSystem.CurrentPot;
            }
            // 컴퓨터 승리
            else if (playerMonth < cpuMonth)
            {

                winnerMessage = "\r\n컴퓨터가 승리했습니다.";
                bettingSystem.CpuMoney += bettingSystem.CurrentPot;
            }
            else
            {
                // 둘 다 월 수가 같은 경우에는 재경기
                gameProgress.Text = $"컴퓨터({cpuFail}{cpuJokbo}) vs 플레이어({playerFail}{playerJokbo}\r\n재경기 합니다.)";
                game.RestartGame();
                game.ReGame = true;
            }
        }

        gameProgress.Text = $"컴퓨터({cpuFail}{cpuJokbo}) vs 플레이어({playerFail}{playerJokbo})\r\n{winnerMessage}";
    }
}
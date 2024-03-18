using System.Windows.Forms;
using System;
using static System.Console;
using System.IO;

namespace Seotta
{
    public class Pae
    {
        // 족보 계산 후 우중단에 족보 버튼 클릭 이벤트 및 족보 도우미에 해당 족보 내용 출력

        string[] paeNameArr = { "1광", "1띠", "2열끗", "2띠", "3광", "3띠", "4열끗", "5띠",
            "6열끗", "6띠", "7열끗", "7띠", "8광", "8열끗", "9열끗", "9띠", "10열끗", "10띠" };

        public string PaeNum { get; set; }
        public string PaeName { get; set; }
        public int PaeLevel { get; set; }
        public string SpecialPae;

        public Pae(string paeNum, string paeName)
        {
            this.PaeNum = paeNum;
            this.PaeName = paeName;
        }

        public void ReadPaeFromFile(string filePath)
        {
            Pae[] pae = new Pae[20];

            for (int i = 0; i < pae.Length; i++)
            {
                if(i % 2 == 0)
                {
                    pae[i].PaeNum = $"{i}";
                } else
                {
                    pae[i].PaeNum = $"{i + 1}";
                }

                pae[i].PaeName = paeNameArr[i];
            }
        }

        public void PrintPaeArr(TextBox gameProgress, Pae[] pae)
        {
            for (int i = 0; i < pae.Length; i++)
            {
                gameProgress.AppendText($"{pae[i].PaeNum}, {pae[i].PaeName}");
            }
        }

        // 출력은 PaeNum으로 "{pae.PaeNum}.txt"을 이용해 출력
        // 족보 계산은 PaeNum, PaeName으로 계산

        /*
        PaeName을 더해서
        3광8광, 8광3광 => 38광땡으로 변경, level 15
        4열끗7열끗, 7열끗4열끗 => 74암행어사, level 14, 광땡 없을 경우 1끗 level 1
        1광3광, 3광1광, 1광8광, 8광1광 => 13광땡, 18광땡, level 13
        10열끗10띠, 10띠10열끗 => 장땡, level 12
        4열끗9열끗, 9열끗4열끗 => 멍텅구리구사, level 11, 장땡 이상일 경우 3끗, level 1
        3광7열끗, 7열끗3광 => 땡잡이, level 10, 땡 없을 경우 망통, level 1
        1광1띠, 1띠1광, 2열끗2띠, 2띠2열끗, 3광3띠, 3띠3광, 4열끗4띠, 4띠4열끗, 5열끗5띠, 5띠5열끗
        6열끗6띠, 6띠6열끗, 7열끗7띠, 7띠7열끗, 8광8열끗, 8열끗8광, 9열끗9띠, 9띠9열끗 => 땡, level 9, 이 경우 PaeNum 더해서 더 큰 숫자가 이기는걸로
        4열끗9띠, 9띠4열끗, 4띠9열끗, 9열끗4띠, 4띠9띠, 9띠4띠 => 49파토, level 8, 땡 이상이 나올 경우 3끗, level 1
        1광2열끗, 1광2띠, 1띠2열끗, 1띠2띠, 2열끗1광, 2열끗1띠, 2띠1광, 2띠1띠 => 알리, level 7
        1광4열끗, 1광4띠, 1띠4열끗, 1띠4띠, 4열끗1광, 4열끗1띠, 4띠1광, 4띠1열끗 => 독사, level 6
        9열끗1광, 9열끗1띠, 9띠1광, 9띠1띠, 1광9열끗, 1광9띠, 1띠9열끗, 1띠9띠 => 구삥, level 5
        10열끗1광, 10열끗1띠, 10띠1광, 10띠1띠, 1광10열끗, 1광10띠, 1띠10열끗, 1띠10띠 => 장삥, level 4
        10열끗4열끗, 10열끗4띠, 10띠4열끗, 10띠4띠, 4열끗10열끗, 4열끗10띠, 4띠10열끗, 4띠10띠 => 장사, level 3
        4열끗6열끗, 4열끗6띠, 4띠6열끗, 4띠6띠, 6열끗4열끗, 6열끗4띠, 6띠4열끗, 6띠4띠 => 세륙, level 2
        외 나머지 끗 => 숫자로 비교, 위 경우 모두 아닐 시 숫자를 더해서 10으로 나눈 나머지 끗, level 1, 둘 다 level 1인 경우 두 패의 PaeNum을 더해 10으로 나눈 수가 큰 쪽이 이김
        */
    }
}

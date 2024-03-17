using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static System.Console;

namespace Seotta
{
    public class Game
    {
        private Form1 form;
        private TextBox pae1;
        private TextBox pae2;
        private TextBox pae3;
        private TextBox pae4;
        private TextBox gameProgress;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;

        // ASCII ART를 담은 string 배열을 원소로 가지는 리스트
        private List<string[]> lines = new List<string[]>();

        // ASCII ART가 담은 텍스트 파일명 리스트
        private List<string> asciiText = new List<string>();

        private int[] currentIndex = new int[4];

        public Game(Form1 form, TextBox pae1, TextBox pae2, TextBox pae3, TextBox pae4, TextBox gameProgress)
        {
            this.form = form;
            this.pae1 = pae1;
            this.pae2 = pae2;
            this.pae3 = pae3;
            this.pae4 = pae4;
            this.gameProgress = gameProgress;

            // 타이머1 설정
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1; // 1밀리초마다 변경
            timer1.Tick += Timer1_Tick;

            // 타이머2 설정
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1; // 1밀리초마다 변경
            timer2.Tick += Timer2_Tick;

            form.Focus();
            form.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                ResetPae();     // 패 초기화
                SelectPae();    // 패 선택
            }
        }

        public void StartGame()
        {
            gameProgress.AppendText("섰다 게임에 오신 여러분을 환영합니다!\r\n\r\n" +
                "이 게임은 전통적인 한국 카드 게임인 '섰다'를 즐길 수 있는 프로그램입니다.\r\n" +
                "여러분은 컴퓨터와 대결을 펼치게 됩니다.\r\n" +
                "무작위로 섞인 화투 패로 상대를 이기고, 전략적인 베팅과 판단으로 승리를 거두세요.\r\n" +
                "즐거운 시간을 보내고 운을 시험해보세요!");

            ResetPae();
            SelectPae();

            // 타이머 시작
            timer1.Start();
        }

        // ASCII ART 텍스트 파일 불러오기
        private string[] ReadAllLinesFromFile(string fileName)
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

        // ASCII ART 출력
        private void Timer1_Tick(object sender, EventArgs e)
        {
            DisplayLines(0, pae1);
            DisplayLines(2, pae3);
        }

        // ASCII ART 출력
        private void Timer2_Tick(object sender, EventArgs e)
        {
            DisplayLines(1, pae2);
            DisplayLines(3, pae4);
        }

        private void DisplayLines(int index, TextBox textBox)
        {
            if (currentIndex[index] < lines[index].Length)
            {
                // ASCII ART를 한줄 씩 TextBox에 추가
                textBox.AppendText(lines[index][currentIndex[index]] + Environment.NewLine);
                currentIndex[index]++;
            }
            else
            {
                if (index == 0 || index == 2)
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

        // 패 선택
        public void SelectPae()
        {
            Random rand = new Random();
            lines.Clear();

            // ASCII ART가 담겨진 텍스트 파일명 불러오기
            asciiText.Clear();
            for (int i = 1; i <= 20; i++) asciiText.Add($"{i}.txt");

            for (int i = 0; i < 4; i++)
            {
                int randIndex;
                do
                {
                    randIndex = rand.Next(0, asciiText.Count);
                } while (asciiText[randIndex] == null);

                lines.Add(ReadAllLinesFromFile(asciiText[randIndex]));
                asciiText.RemoveAt(randIndex); // 중복된 값 제거
                currentIndex[i] = 0;

                // 하단의 텍스트 박스에 포커스 설정
                // gameProgress.Focus();
            }
        }

        public void ResetPae()
        {
            // 각 패 텍스트 박스 초기화
            pae1.Clear();
            pae2.Clear();
            pae3.Clear();
            pae4.Clear();

            currentIndex = new int[4];

            // 타이머2 정지
            timer2.Stop();

            // 타이머1 시작
            timer1.Start();
        }
    }
}

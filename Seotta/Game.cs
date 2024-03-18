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
        Form1 form;
        TextBox pae1;
        TextBox pae2;
        TextBox pae3;
        TextBox pae4;
        TextBox gameProgress;

        System.Windows.Forms.Timer timer1;
        System.Windows.Forms.Timer timer2;

        Pae[] pae;

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

            pae = new Pae[20];

            // 타이머1 설정
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1; // 1밀리초마다 변경
            timer1.Tick += Timer1_Tick;

            // 타이머2 설정
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1; // 1밀리초마다 변경
            timer2.Tick += Timer2_Tick;
        }

        public void StartGame()
        {
            DisplayTextFromFile("game_start.txt", gameProgress);

            ResetPae();
            PrintPae();

            // 타이머 시작
            timer1.Start();

            ReadPaeFromFile("Pae_Name.txt");
        }

        public void ReadPaeFromFile(string filePath)
        {
            List<string> paeName = new List<string>();

            foreach (string str in File.ReadLines(filePath))
            {
                paeName.Add(str); // 리스트에 문자열 추가
            }

            for (int i = 0; i < pae.Length; i++)
            {
                if (i == 0)
                {
                    pae[i] = new Pae((i + 1).ToString(), paeName[i]);
                }
                else
                {
                    pae[i] = new Pae((i / 2 + 1).ToString(), paeName[i]);
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

        // 패 선택
        public void SelectPae()
        {

        }

        // 패 출력, 패 선택 메소드에서 4장을 선택하면 그 4장이 담기고 출력만 하게 끔 메소드 만들어야함
        public void PrintPae()
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

        // ASCII ART 출력
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
    }
}

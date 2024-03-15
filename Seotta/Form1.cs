using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using static System.Console;

namespace Seotta
{
    public partial class Form1 : Form
    {
        private Timer timer1;
        private Timer timer2;

        private string[] lines1;
        private string[] lines2;
        private string[] lines3;
        private string[] lines4;

        // ASCII ART를 담는 string 배열을 원소로 가지는 리스트 생성
        private List<string[]> lines = new List<string[]>();

        // ASCII ART가 그려진 텍스트 파일명 리스트 생성
        private List<string> asciiText = new List<string>();

        private int currentIndex1 = 0;
        private int currentIndex2 = 0;
        private int currentIndex3 = 0;
        private int currentIndex4 = 0;

        public Form1()
        {
            InitializeComponent();

            // 폼을 화면에 가운데에 생성
            this.StartPosition = FormStartPosition.CenterScreen;

            // 폼의 배경색을 검정색으로 설정
            this.BackColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 타이머1 설정
            timer1 = new Timer();
            timer1.Interval = 1; // 1 밀리초마다 변경
            timer1.Tick += Timer1_Tick;

            // 타이머2 설정
            timer2 = new Timer();
            timer2.Interval = 1; // 1 밀리초마다 변경
            timer2.Tick += Timer2_Tick;

            ResetPae();
            SelectPae();

            // 타이머 시작
            timer1.Start();

            this.Focus();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                // 카드를 다시 출력하는 메서드 호출
                ResetPae();
                SelectPae();
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
                WriteLine($"File not found: {fileName}");
                return new string[0];
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (currentIndex1 < lines[0].Length && currentIndex3 < lines[2].Length)
            {
                pae1.AppendText(lines[0][currentIndex1] + Environment.NewLine);
                pae3.AppendText(lines[2][currentIndex3] + Environment.NewLine);

                if (currentIndex3 < lines[0].Length)
                    currentIndex1++;

                if (currentIndex3 < lines[2].Length)
                    currentIndex3++;
            }
            else
            {
                // 타이머1 중지 및 타이머2 시작
                timer1.Stop();
                timer2.Start();
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (currentIndex2 < lines[1].Length && currentIndex4 < lines[3].Length)
            {
                pae2.AppendText(lines[1][currentIndex2] + Environment.NewLine);
                pae4.AppendText(lines[3][currentIndex4] + Environment.NewLine);

                if (currentIndex2 < lines[1].Length)
                    currentIndex2++;

                if (currentIndex4 < lines[3].Length)
                    currentIndex4++;
            }
            else
            {
                // 타이머2 중지
                timer2.Stop();
            }
        }

        public void SelectPae()
        {
            Random rand = new Random();

            lines.Add(lines1);
            lines.Add(lines2);
            lines.Add(lines3);
            lines.Add(lines4);

            // 이전에 선택된 아스키 아트들을 저장할 리스트 생성
            List<string> selectedArt = new List<string>();

            // ASCII ART 텍스트 파일명 저장
            for (int i = 1; i <= 20; i++)
            {
                asciiText.Add(i + ".txt");
            }

            // 4개의 패를 선택
            for (int i = 0; i < 4; i++)
            {
                // 중복이 발생할 경우 다시 선택
                string selected;
                do
                {
                    int randNum = rand.Next(0, asciiText.Count - 1);
                    selected = asciiText[randNum];
                } while (selectedArt.Contains(selected));

                lines[i] = ReadAllLinesFromFile(selected);
                asciiText.Remove(selected);
                selectedArt.Add(selected);
            }

            // currentIndex1, currentIndex2, currentIndex3, currentIndex4 초기화
            currentIndex1 = 0;
            currentIndex2 = 0;
            currentIndex3 = 0;
            currentIndex4 = 0;

            // 하단의 텍스트 박스에 포커스 설정
            // gameProgress.Focus();
        }

        private void ResetPae()
        {
            // 각 패 텍스트 박스 초기화
            pae1.Clear();
            pae2.Clear();
            pae3.Clear();
            pae4.Clear();

            lines.Clear();

            // 타이머2 정지
            timer2.Stop();

            // 타이머1 시작
            timer1.Start();
        }
    }
}

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

        // ASCII ART를 담은 string 배열을 원소로 가지는 리스트 생성
        private List<string[]> lines = new List<string[]>();

        // ASCII ART가 담은 텍스트 파일명 리스트 생성
        private List<string> asciiText = new List<string>();

        private int[] currentIndex = new int[4];

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
                ResetPae();     // 패 초기화
                SelectPae();    // 패 선택
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
            // 이전에 선택된 ASCII ART들을 저장할 리스트 생성
            List<string> selectedArt = new List<string>();

            Random rand = new Random();

            lines.Add(lines1);
            lines.Add(lines2);
            lines.Add(lines3);
            lines.Add(lines4);

            // ASCII ART 텍스트 파일명 저장
            for (int i = 1; i <= 20; i++)
            {
                asciiText.Add(i + ".txt");
            }

            // 4개의 패를 선택
            for (int i = 0; i < 4; i++)
            {
                // 이전에 사용한 ASCII ART 파일명이 리스트에 있을경우(중복이 발생할 경우) 다시 선택
                string selected;
                do
                {
                    // asciiText에 접근하는데 사용할 0 ~ 19 사이의 랜덤 인덱스 생성
                    int randNum = rand.Next(0, asciiText.Count - 1);
                    selected = asciiText[randNum];
                } while (selectedArt.Contains(selected));

                // 중복이 발생하지 않은 ASCII ART 파일명이 선택되면 해당 파일을 읽어와서 각 패에 할당
                lines[i] = ReadAllLinesFromFile(selected);
                // 중복 선택되지 않도록 선택된 ASCII ART 파일명은 asciiText 리스트에서 제거
                asciiText.Remove(selected);
                selectedArt.Add(selected);
            }

            // currentIndex 초기화
            for(int i = 0;i < currentIndex.Length; i++)
            {
                currentIndex[i] = 0;
            }

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

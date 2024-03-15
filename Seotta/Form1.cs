using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace Seotta
{
    public partial class Form1 : Form
    {
        private Timer timer1;
        private Timer timer2;
        private Timer[] timer;

        private string[] lines1;
        private string[] lines2;
        private string[] lines3;
        private string[] lines4;

        // ASCII ART를 담는 string 배열을 원소로 가지는 리스트 생성
        private List<string[]> lines = new List<string[]>();
        
        private int currentIndex1 = 0;
        private int currentIndex2 = 0;
        private int currentIndex3 = 0;
        private int currentIndex4 = 0;

        private List<string> asciiText = new List<string>();

        // 파일 입력으로 string 배열에 순차적으로 데이터 삽입
        // split()

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

            SelectPae();

            // 타이머 시작
            timer1.Start();

            // 자동으로 다음 컨트롤로 포커스 이동, 패1 textBox에서 
            this.SelectNextControl(this.ActiveControl, true, true, true, true);
        }

        private string[] ReadAllLinesFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllLines(fileName);
            }
            else
            {
                Console.WriteLine($"File not found: {fileName}");
                return new string[0];
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (currentIndex1 < lines[0].Length && currentIndex3 < lines[3].Length)
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
            lines.Add(lines1);
            lines.Add(lines2);
            lines.Add(lines3);
            lines.Add(lines4);

            for (int i = 1; i <= 20; i++)
            {
                asciiText.Add(i + ".txt");
            }

            Random rand = new Random();

            
            for(int i = 0; i < 4; i++)
            {
                int randNum = rand.Next(0, asciiText.Count - 1);
                lines[i] = ReadAllLinesFromFile(asciiText[randNum]);
                asciiText.RemoveAt(randNum);
            }
            
        }
    }
}

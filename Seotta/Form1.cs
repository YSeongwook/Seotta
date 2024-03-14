using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private int currentIndex1 = 0;
        private int currentIndex2 = 0;
        private int currentIndex3 = 0;
        private int currentIndex4 = 0;

        public Form1()
        {
            InitializeComponent();

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

            // 텍스트 파일에서 줄 읽어오기
            lines1 = ReadAllLinesFromFile("1.txt");
            lines2 = ReadAllLinesFromFile("2.txt");
            lines3 = ReadAllLinesFromFile("3.txt");
            lines4 = ReadAllLinesFromFile("4.txt");

            // 타이머 시작
            timer1.Start();
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
            if (currentIndex1 < lines1.Length)
            {
                textBox1.AppendText(lines1[currentIndex1] + Environment.NewLine);
                textBox3.AppendText(lines3[currentIndex3] + Environment.NewLine);
                currentIndex1++;
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
            if (currentIndex2 < lines2.Length)
            {
                textBox2.AppendText(lines2[currentIndex2] + Environment.NewLine);
                textBox4.AppendText(lines4[currentIndex4] + Environment.NewLine);
                currentIndex2++;
                currentIndex4++;
            }
            else
            {
                // 타이머2 중지
                timer2.Stop();
            }
        }
    }
}

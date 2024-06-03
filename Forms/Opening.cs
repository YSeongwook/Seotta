using System;
using System.IO;
using System.Windows.Forms;

namespace Seotta
{
    public partial class Opening : Form
    {
        Timer timer1;

        int index = 0;
        string[] asciiArt;

        public Opening()
        {
            InitializeComponent();

            this.startBtn.Click += new EventHandler(this.startBtn_Click);
            this.exitBtn.Click += new EventHandler(this.exitBtn_Click);
            this.KeyDown += new KeyEventHandler(Opening_KeyDown);

            // FormClosed 이벤트 핸들러 등록
            this.FormClosed += Opening_FormClosed;

            // 타이머1 설정
            timer1 = new Timer();
            timer1.Interval = 1; // 1밀리초마다 변경
            timer1.Tick += Timer1_Tick;

            asciiArt = ReadAllLinesFromFile("Main3.txt");

            timer1.Start();
        }

        private void Opening_KeyDown(object sender, KeyEventArgs e)
        {
            // Escape 키를 누르면 폼 종료
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void Opening_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 타이머 중지
            timer1.Stop();

            Application.Exit(); // 폼이 종료되도 백그라운드에서 돌아갈 수 있기에 this.Close() => Application.Exit()로 수정함
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

        // Ascii Art 한줄 씩 출력
        private void DisplayLines(TextBox textBox)
        {
            if (index < asciiArt.Length)
            {
                // ASCII ART를 한줄 씩 TextBox에 추가
                textBox.AppendText(asciiArt[index] + Environment.NewLine);
                index++;
            }
            else
            {
                if (index == 0)
                {
                    timer1.Stop();
                }
            }
        }

        // ASCII ART를 한 번에 출력
        private void DisplayAllLines(TextBox textBox)
        {
            if (index < asciiArt.Length)
            {
                textBox.Text = string.Join(Environment.NewLine, asciiArt);

                // 인덱스를 마지막으로 이동
                index = asciiArt.Length;

                // 타이머 중지
                timer1.Stop();
            }
        }

        // Ascii Art 텍스트 파일 읽기
        public string[] ReadAllLinesFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllLines(fileName);
            }
            else
            { 
                return new string[0];
            }
        }

        // PrintResult(), cpu 패도 모두 출력하고 비교하여 결과 산출, 1밀리초마다 이벤트 발생
        private void Timer1_Tick(object sender, EventArgs e)
        {
            // DisplayLines(openingAsciiArt);
            DisplayAllLines(openingAsciiArt);
        }

        // startBtn 클릭 이벤트 핸들러
        private void startBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            // Form1을 생성하고 보여줍니다.
            Form1 form1 = new Form1();
            form1.Show();

            // 현재 폼을 숨김
            this.Hide();
        }

        // exitBtn 클릭 이벤트 핸들러
        private void exitBtn_Click(object sender, EventArgs e)
        {
            // 현재 폼을 종료
            Application.Exit();
        }
    }
}
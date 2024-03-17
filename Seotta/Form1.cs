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
      Game game;

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
            game = new Game(this, pae1, pae2, pae3, pae4, gameProgress);
            game.StartGame();

            this.Focus();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                game.ResetPae();     // 패 초기화
                game.SelectPae();    // 패 선택
            }
        }
    }
}

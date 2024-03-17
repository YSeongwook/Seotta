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

            // KeyPreview를 true로 설정하여 폼이 키 이벤트를 먼저 받도록 함
            this.KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(this, pae1, pae2, pae3, pae4, gameProgress);
            game.StartGame();

            gameProgress.AppendText(this.Focus().ToString());   // 현재 false임
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            gameProgress.KeyDown += new KeyEventHandler(gameProgress_KeyDown);  // 이벤트 핸들러 만들어서 등록하니 엔터키 입력 됌
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 엔터 키의 기본 동작을 막음
                game.ResetPae();     // 패 초기화
                game.SelectPae();    // 패 선택
            }
        }

        // 
        private void gameProgress_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter 키를 눌렀을 때
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 엔터 키의 기본 동작을 막음
                game.ResetPae();     // 패 초기화
                game.SelectPae();    // 패 선택
            }
        }
    }
}

using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Seotta
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cpuLabel = new System.Windows.Forms.Label();
            this.playerLabel = new System.Windows.Forms.Label();
            this.pae1 = new System.Windows.Forms.TextBox();
            this.pae2 = new System.Windows.Forms.TextBox();
            this.pae3 = new System.Windows.Forms.TextBox();
            this.pae4 = new System.Windows.Forms.TextBox();
            this.gameProgress = new System.Windows.Forms.TextBox();
            this.betBtn7 = new System.Windows.Forms.Button();
            this.betBtn8 = new System.Windows.Forms.Button();
            this.betBtn4 = new System.Windows.Forms.Button();
            this.betBtn5 = new System.Windows.Forms.Button();
            this.betBtn1 = new System.Windows.Forms.Button();
            this.betBtn0 = new System.Windows.Forms.Button();
            this.jokboHelpBtn = new System.Windows.Forms.Button();
            this.betHelpBtn = new System.Windows.Forms.Button();
            this.jokboHelper = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // cpuLabel
            this.cpuLabel.AutoSize = true;
            this.cpuLabel.Font = new System.Drawing.Font("Consolas", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuLabel.ForeColor = System.Drawing.Color.White;
            this.cpuLabel.Location = new System.Drawing.Point(370, 24);
            this.cpuLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cpuLabel.Name = "cpuLabel";
            this.cpuLabel.Size = new System.Drawing.Size(140, 75);
            this.cpuLabel.TabIndex = 5;
            this.cpuLabel.Text = "컴퓨터";
            this.cpuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // playerLabel
            this.playerLabel.AutoSize = true;
            this.playerLabel.Font = new System.Drawing.Font("Consolas", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerLabel.ForeColor = System.Drawing.Color.White;
            this.playerLabel.Location = new System.Drawing.Point(1270, 24);
            this.playerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(140, 75);
            this.playerLabel.TabIndex = 5;
            this.playerLabel.Text = "플레이어";
            this.playerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // pae1
            this.pae1.BackColor = System.Drawing.Color.Black;
            this.pae1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pae1.Font = new System.Drawing.Font("Consolas", 4F);
            this.pae1.ForeColor = System.Drawing.Color.White;
            this.pae1.Location = new System.Drawing.Point(12, 120);
            this.pae1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pae1.Multiline = true;
            this.pae1.Name = "pae1";
            this.pae1.ReadOnly = true;
            this.pae1.Size = new System.Drawing.Size(442, 665);
            this.pae1.TabIndex = 5;
            this.pae1.TabStop = false;

            // pae2
            this.pae2.BackColor = System.Drawing.Color.Black;
            this.pae2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pae2.Font = new System.Drawing.Font("Consolas", 4F);
            this.pae2.ForeColor = System.Drawing.Color.White;
            this.pae2.Location = new System.Drawing.Point(475, 120);
            this.pae2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pae2.Multiline = true;
            this.pae2.Name = "pae2";
            this.pae2.ReadOnly = true;
            this.pae2.Size = new System.Drawing.Size(442, 665);
            this.pae2.TabIndex = 2;
            this.pae2.TabStop = false;

            // pae3
            this.pae3.BackColor = System.Drawing.Color.Black;
            this.pae3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pae3.Font = new System.Drawing.Font("Consolas", 4F);
            this.pae3.ForeColor = System.Drawing.Color.White;
            this.pae3.Location = new System.Drawing.Point(938, 120);
            this.pae3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pae3.Multiline = true;
            this.pae3.Name = "pae3";
            this.pae3.ReadOnly = true;
            this.pae3.Size = new System.Drawing.Size(442, 665);
            this.pae3.TabIndex = 3;
            this.pae3.TabStop = false;

            // pae4
            this.pae4.BackColor = System.Drawing.Color.Black;
            this.pae4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pae4.Font = new System.Drawing.Font("Consolas", 4F);
            this.pae4.ForeColor = System.Drawing.Color.White;
            this.pae4.Location = new System.Drawing.Point(1400, 120);
            this.pae4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pae4.Multiline = true;
            this.pae4.Name = "pae4";
            this.pae4.ReadOnly = true;
            this.pae4.Size = new System.Drawing.Size(442, 665);
            this.pae4.TabIndex = 4;
            this.pae4.TabStop = false;

            // gameProgress
            this.gameProgress.BackColor = System.Drawing.Color.Black;
            this.gameProgress.Font = new System.Drawing.Font("Consolas", 24F);
            this.gameProgress.ForeColor = System.Drawing.Color.White;
            this.gameProgress.Location = new System.Drawing.Point(12, 794);
            this.gameProgress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gameProgress.Multiline = true;
            this.gameProgress.Name = "gameProgress";
            this.gameProgress.ReadOnly = true;
            this.gameProgress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gameProgress.Size = new System.Drawing.Size(1816, 452);
            this.gameProgress.TabIndex = 0;
            this.gameProgress.TabStop = false;

            // betBtn7
            this.betBtn7.Font = new System.Drawing.Font("Consolas", 20F, FontStyle.Bold);
            this.betBtn7.ForeColor = System.Drawing.Color.White;
            this.betBtn7.Location = new System.Drawing.Point(1850, 792);
            this.betBtn7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.betBtn7.Name = "betBtn7";
            this.betBtn7.Size = new System.Drawing.Size(250, 96);
            this.betBtn7.TabIndex = 5;
            this.betBtn7.TabStop = false;
            this.betBtn7.Text = "7|   콜  ";

            // betBtn8
            this.betBtn8.Font = new System.Drawing.Font("Consolas", 20F, FontStyle.Bold);
            this.betBtn8.ForeColor = System.Drawing.Color.White;
            this.betBtn8.Location = new System.Drawing.Point(2112, 792);
            this.betBtn8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.betBtn8.Name = "betBtn8";
            this.betBtn8.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.betBtn8.Size = new System.Drawing.Size(250, 96);
            this.betBtn8.TabIndex = 6;
            this.betBtn8.TabStop = false;
            this.betBtn8.Text = "8|  다이 ";

            // betBtn4
            this.betBtn4.Font = new System.Drawing.Font("Consolas", 20F, FontStyle.Bold);
            this.betBtn4.ForeColor = System.Drawing.Color.White;
            this.betBtn4.Location = new System.Drawing.Point(1850, 912);
            this.betBtn4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.betBtn4.Name = "betBtn4";
            this.betBtn4.Size = new System.Drawing.Size(250, 96);
            this.betBtn4.TabIndex = 7;
            this.betBtn4.TabStop = false;
            this.betBtn4.Text = "4|   삥  ";

            // betBtn5
            this.betBtn5.Font = new System.Drawing.Font("Consolas", 20F, FontStyle.Bold);
            this.betBtn5.ForeColor = System.Drawing.Color.White;
            this.betBtn5.Location = new System.Drawing.Point(2112, 912);
            this.betBtn5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.betBtn5.Name = "betBtn5";
            this.betBtn5.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.betBtn5.Size = new System.Drawing.Size(250, 96);
            this.betBtn5.TabIndex = 8;
            this.betBtn5.TabStop = false;
            this.betBtn5.Text = "5|  체크 ";
            // 
            // betBtn1
            // 
            this.betBtn1.Font = new System.Drawing.Font("Consolas", 20F, FontStyle.Bold);
            this.betBtn1.ForeColor = System.Drawing.Color.White;
            this.betBtn1.Location = new System.Drawing.Point(1850, 1032);
            this.betBtn1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.betBtn1.Name = "betBtn1";
            this.betBtn1.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.betBtn1.Size = new System.Drawing.Size(250, 96);
            this.betBtn1.TabIndex = 9;
            this.betBtn1.TabStop = false;
            this.betBtn1.Text = "1|  따당 ";

            // betBtn0
            this.betBtn0.Font = new System.Drawing.Font("Consolas", 20F, FontStyle.Bold);
            this.betBtn0.ForeColor = System.Drawing.Color.White;
            this.betBtn0.Location = new System.Drawing.Point(1850, 1152);
            this.betBtn0.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.betBtn0.Name = "betBtn0";
            this.betBtn0.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.betBtn0.Size = new System.Drawing.Size(250, 96);
            this.betBtn0.TabIndex = 10;
            this.betBtn0.TabStop = false;
            this.betBtn0.Text = "0|  하프 ";

            // jokboHelpBtn
            this.jokboHelpBtn.Font = new System.Drawing.Font("Consolas", 15F, FontStyle.Bold);
            this.jokboHelpBtn.ForeColor = System.Drawing.Color.White;
            this.jokboHelpBtn.Location = new System.Drawing.Point(2016, 24);
            this.jokboHelpBtn.Name = "jokboHelpBtn";
            this.jokboHelpBtn.Size = new System.Drawing.Size(168, 58);
            this.jokboHelpBtn.TabIndex = 11;
            this.jokboHelpBtn.TabStop = false;
            this.jokboHelpBtn.Text = "족보 도움말";
            this.jokboHelpBtn.Click += new System.EventHandler(this.JokboHelpBtn_BtnClick);

            // betHelpBtn
            this.betHelpBtn.Font = new System.Drawing.Font("Consolas", 15F, FontStyle.Bold);
            this.betHelpBtn.ForeColor = System.Drawing.Color.White;
            this.betHelpBtn.Location = new System.Drawing.Point(2191, 24);
            this.betHelpBtn.Name = "betHelpBtn";
            this.betHelpBtn.Size = new System.Drawing.Size(168, 58);
            this.betHelpBtn.TabIndex = 12;
            this.betHelpBtn.TabStop = false;
            this.betHelpBtn.TextAlign = ContentAlignment.MiddleCenter;
            this.betHelpBtn.Text = "베팅 도움말";
            this.betHelpBtn.Click += new System.EventHandler(this.BetHelpBtn_BtnClick);

            // jokboHelper
            this.jokboHelper.BackColor = System.Drawing.Color.Black;
            this.jokboHelper.Font = new System.Drawing.Font("Consolas", 14F);
            this.jokboHelper.ForeColor = System.Drawing.Color.White;
            this.jokboHelper.Location = new System.Drawing.Point(1840, 470);
            this.jokboHelper.Multiline = true;
            this.jokboHelper.Name = "jokboHelper";
            this.jokboHelper.ReadOnly = true;
            this.jokboHelper.Size = new System.Drawing.Size(518, 220);
            this.jokboHelper.TabIndex = 0;
            this.jokboHelper.TabStop = false;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2400, 1296);
            this.Controls.Add(this.gameProgress);
            this.Controls.Add(this.pae4);
            this.Controls.Add(this.pae3);
            this.Controls.Add(this.pae2);
            this.Controls.Add(this.pae1);
            this.Controls.Add(this.playerLabel);
            this.Controls.Add(this.cpuLabel);
            this.Controls.Add(this.betBtn7);
            this.Controls.Add(this.betBtn8);
            this.Controls.Add(this.betBtn4);
            this.Controls.Add(this.betBtn5);
            this.Controls.Add(this.betBtn1);
            this.Controls.Add(this.betBtn0);
            this.Controls.Add(this.jokboHelpBtn);
            this.Controls.Add(this.betHelpBtn);
            this.Controls.Add(this.jokboHelper);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;    // 폼을 화면에 가운데에 생성
            this.BackColor = Color.Black;                           // 폼의 배경색을 검정색으로 설정
            this.KeyPreview = true;                                 // KeyPreview를 true로 설정하여 폼이 키 이벤트를 먼저 받도록 함
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // 패 위치 구분 레이블
        private System.Windows.Forms.Label cpuLabel;
        private System.Windows.Forms.Label playerLabel;

        // 패 텍스트 박스
        private System.Windows.Forms.TextBox pae1;
        private System.Windows.Forms.TextBox pae2;
        private System.Windows.Forms.TextBox pae3;
        private System.Windows.Forms.TextBox pae4;

        // 게임 안내 텍스트 박스
        private System.Windows.Forms.TextBox gameProgress;

        // 베팅 버튼
        private System.Windows.Forms.Button betBtn7;
        private System.Windows.Forms.Button betBtn8;
        private System.Windows.Forms.Button betBtn4;
        private System.Windows.Forms.Button betBtn5;
        private System.Windows.Forms.Button betBtn1;
        private System.Windows.Forms.Button betBtn0;

        // 족보, 베팅 도움말 버튼
        private System.Windows.Forms.Button jokboHelpBtn;
        private System.Windows.Forms.Button betHelpBtn;

        // 족보 도우미 텍스트 박스
        private System.Windows.Forms.TextBox jokboHelper;
    }
}

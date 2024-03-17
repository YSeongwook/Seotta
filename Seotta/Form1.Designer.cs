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
            this.labelTitle = new System.Windows.Forms.Label(); // 게임 제목
            this.pae1 = new System.Windows.Forms.TextBox();     // 화투패
            this.pae2 = new System.Windows.Forms.TextBox();
            this.pae3 = new System.Windows.Forms.TextBox();
            this.pae4 = new System.Windows.Forms.TextBox();
            this.gameProgress = new System.Windows.Forms.TextBox();  // 게임 진행 안내문
            this.SuspendLayout();

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new Font("Consolas", 32, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = Color.White;
            this.labelTitle.Location = new Point(688, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new Size(124, 240);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "섰다";
            this.labelTitle.TextAlign = ContentAlignment.MiddleCenter;

            // pae1
            this.pae1.BackColor = Color.Black;
            this.pae1.Font = new Font("Consolas", 4);
            this.pae1.ForeColor = Color.White;
            this.pae1.Location = new Point(10, 100);
            this.pae1.Multiline = true;
            this.pae1.Name = "pae1";
            this.pae2.TabIndex = 1;
            this.pae1.Size = new Size(354, 554);
            this.pae1.BorderStyle = BorderStyle.None;
            this.pae1.ReadOnly = true;  // TextBox를 읽기 전용으로 설정
            this.pae1.TabStop = false;

            // pae2
            this.pae2.BackColor = Color.Black;
            this.pae2.Font = new Font("Consolas", 4);
            this.pae2.ForeColor = Color.White;
            this.pae2.Location = new Point(380, 100);
            this.pae2.Multiline = true;
            this.pae2.Name = "pae2";
            this.pae2.Size = new Size(354, 554);
            this.pae2.TabIndex = 2;
            this.pae2.BorderStyle = BorderStyle.None;
            this.pae2.ReadOnly = true;
            this.pae2.TabStop = false;

            // pae3
            this.pae3.BackColor = Color.Black;
            this.pae3.Font = new Font("Consolas", 4);
            this.pae3.ForeColor = Color.White;
            this.pae3.Location = new Point(750, 100);
            this.pae3.Multiline = true;
            this.pae3.Name = "pae3";
            this.pae3.Size = new Size(354, 554);
            this.pae3.TabIndex = 3;
            this.pae3.BorderStyle = BorderStyle.None;
            this.pae3.ReadOnly = true;
            this.pae3.TabStop = false;

            // pae4
            this.pae4.BackColor = Color.Black;
            this.pae4.Font = new Font("Consolas", 4);
            this.pae4.ForeColor = Color.White;
            this.pae4.Location = new Point(1120, 100);
            this.pae4.Multiline = true;
            this.pae4.Name = "pae4";
            this.pae4.Size = new Size(354, 554);
            this.pae4.TabIndex = 4;
            this.pae4.BorderStyle = BorderStyle.None;
            this.pae4.ReadOnly = true;
            this.pae4.TabStop = false;

            // gameProgress
            this.gameProgress.BackColor = Color.Black;
            this.gameProgress.Font = new Font("Consolas", 24F);
            this.gameProgress.ForeColor = Color.White;
            this.gameProgress.Location = new Point(10, 664);
            this.gameProgress.Multiline = true;
            this.gameProgress.Name = "gameProgress";
            this.gameProgress.Size = new Size(1454, 375);
            this.gameProgress.TabIndex = 0; // TabIndex를 0으로 설정해 폼이 생성되면 포커스가 자동으로 할당
            this.gameProgress.TabStop = false;
            this.gameProgress.ReadOnly = true;
            this.gameProgress.ScrollBars = ScrollBars.Vertical;

            // bettingButton
            this.betBtn7 = new System.Windows.Forms.Button();
            this.betBtn7.Location = new System.Drawing.Point(1480, 660); // 버튼 위치 설정
            this.betBtn7.Name = "betBtn7";
            this.betBtn7.Size = new System.Drawing.Size(200, 80); // 버튼 크기 설정
            this.betBtn7.Text = "7|   콜  "; // 버튼 텍스트 설정
            this.betBtn7.TextAlign = ContentAlignment.MiddleCenter;
            this.betBtn7.Font = new Font("Consolas", 20);
            this.betBtn7.ForeColor = Color.White;
            this.Controls.Add(this.betBtn7); // 폼에 버튼 추가
            
            this.betBtn8 = new System.Windows.Forms.Button();
            this.betBtn8.Location = new System.Drawing.Point(1700, 660); // 버튼 위치 설정
            this.betBtn8.Name = "betBtn8";
            this.betBtn8.Size = new System.Drawing.Size(200, 80); // 버튼 크기 설정
            this.betBtn8.Text = "8|  다이 "; // 버튼 텍스트 설정
            this.betBtn8.TextAlign = ContentAlignment.MiddleCenter;
            this.betBtn8.Font = new Font("Consolas", 20);
            this.betBtn8.ForeColor = Color.White;
            this.betBtn8.Padding = new Padding(0, 0, 10, 0);
            this.Controls.Add(this.betBtn8); // 폼에 버튼 추가

            this.betBtn4 = new System.Windows.Forms.Button();
            this.betBtn4.Location = new System.Drawing.Point(1480, 760); // 버튼 위치 설정
            this.betBtn4.Name = "betBtn4";
            this.betBtn4.Size = new System.Drawing.Size(200, 80); // 버튼 크기 설정
            this.betBtn4.Text = "4|   삥  "; // 버튼 텍스트 설정
            this.betBtn4.TextAlign = ContentAlignment.MiddleCenter;
            this.betBtn4.Font = new Font("Consolas", 20);
            this.betBtn4.ForeColor = Color.White;
            this.Controls.Add(this.betBtn4); // 폼에 버튼 추가

            this.betBtn5 = new System.Windows.Forms.Button();
            this.betBtn5.Location = new System.Drawing.Point(1700, 760); // 버튼 위치 설정
            this.betBtn5.Name = "betBtn5";
            this.betBtn5.Size = new System.Drawing.Size(200, 80); // 버튼 크기 설정
            this.betBtn5.Text = "5|  체크 "; // 버튼 텍스트 설정
            this.betBtn5.TextAlign = ContentAlignment.MiddleCenter;
            this.betBtn5.Font = new Font("Consolas", 20);
            this.betBtn5.ForeColor = Color.White;
            this.betBtn5.Padding = new Padding(0, 0, 10, 0);
            this.Controls.Add(this.betBtn5); // 폼에 버튼 추가

            this.betBtn1 = new System.Windows.Forms.Button();
            this.betBtn1.Location = new System.Drawing.Point(1480, 860); // 버튼 위치 설정
            this.betBtn1.Name = "betBtn1";
            this.betBtn1.Size = new System.Drawing.Size(200, 80); // 버튼 크기 설정
            this.betBtn1.Text = "1|  따당 "; // 버튼 텍스트 설정
            this.betBtn1.TextAlign = ContentAlignment.MiddleCenter;
            this.betBtn1.Font = new Font("Consolas", 20);
            this.betBtn1.ForeColor = Color.White;
            this.betBtn1.Padding = new Padding(0, 0, 10, 0);
            this.Controls.Add(this.betBtn1); // 폼에 버튼 추가

            this.betBtn0 = new System.Windows.Forms.Button();
            this.betBtn0.Location = new System.Drawing.Point(1480, 960); // 버튼 위치 설정
            this.betBtn0.Name = "betBtn0";
            this.betBtn0.Size = new System.Drawing.Size(200, 80); // 버튼 크기 설정
            this.betBtn0.Text = "0|  하프 "; // 버튼 텍스트 설정
            this.betBtn0.TextAlign = ContentAlignment.MiddleCenter;
            this.betBtn0.Font = new Font("Consolas", 20);
            this.betBtn0.ForeColor = Color.White;
            this.betBtn0.Padding = new Padding(0, 0, 10, 0);
            this.Controls.Add(this.betBtn0); // 폼에 버튼 추가

            // Form1
            this.AutoScaleDimensions = new SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(1920, 1080);
            this.Controls.Add(this.gameProgress);
            this.Controls.Add(this.pae4);
            this.Controls.Add(this.pae3);
            this.Controls.Add(this.pae2);
            this.Controls.Add(this.pae1);
            this.Controls.Add(this.labelTitle);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox pae1;
        private System.Windows.Forms.TextBox pae2;
        private System.Windows.Forms.TextBox pae3;
        private System.Windows.Forms.TextBox pae4;
        private System.Windows.Forms.TextBox gameProgress;

        // 베팅 버튼
        private System.Windows.Forms.Button betBtn7;
        private System.Windows.Forms.Button betBtn8;
        private System.Windows.Forms.Button betBtn4;
        private System.Windows.Forms.Button betBtn5;
        private System.Windows.Forms.Button betBtn1;
        private System.Windows.Forms.Button betBtn0;
        private System.Windows.Forms.Button betBtn7Call;
    }
}

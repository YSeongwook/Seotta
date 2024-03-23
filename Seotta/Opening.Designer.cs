using System.Drawing;

namespace Seotta
{
    partial class Opening
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
            this.openingAsciiArt = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // openingAsciiArt
            this.openingAsciiArt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.openingAsciiArt.BackColor = Color.Black;
            this.openingAsciiArt.Font = new Font("Consolas", 1F);
            this.openingAsciiArt.ForeColor = Color.White;
            this.openingAsciiArt.Location = new System.Drawing.Point(5, 10);
            this.openingAsciiArt.Name = "OpeningAsciiArt";
            this.openingAsciiArt.Text = "";
            this.openingAsciiArt.Size = new Size(1000, 580); // 텍스트 박스 크기 조정
            this.openingAsciiArt.TabIndex = 1;
            this.openingAsciiArt.Multiline = true;
            this.openingAsciiArt.ReadOnly = true;

            // startBtn
            this.startBtn.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold);
            this.startBtn.ForeColor = System.Drawing.Color.White;
            this.startBtn.Location = new System.Drawing.Point(240, 590); // 시작 버튼 위치 조정
            this.startBtn.Margin = new System.Windows.Forms.Padding(4);
            this.startBtn.Name = "게임 시작";
            this.startBtn.Size = new System.Drawing.Size(160, 60);
            this.startBtn.TabStop = false;
            this.startBtn.Text = "게임 시작";

            // exitBtn
            this.exitBtn.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold);
            this.exitBtn.ForeColor = System.Drawing.Color.White;
            this.exitBtn.Location = new System.Drawing.Point(600, 590); // 종료 버튼 위치 조정
            this.exitBtn.Margin = new System.Windows.Forms.Padding(4);
            this.exitBtn.Name = "게임 종료";
            this.exitBtn.Size = new System.Drawing.Size(160, 60);
            this.exitBtn.TabStop = false;
            this.exitBtn.Text = "게임 종료";

            // Opening
            this.AutoScaleDimensions = new SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = Color.Black;
            this.ClientSize = new Size(1000, 660);
            this.Controls.Add(this.openingAsciiArt);
            this.Controls.Add(this.startBtn); // 시작 버튼 추가
            this.Controls.Add(this.exitBtn); // 종료 버튼 추가
            this.KeyPreview = true;
            this.Name = "Opening";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "섰다";
            this.Icon = new Icon("seotta1.ico");
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox openingAsciiArt;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button exitBtn;
    }
}

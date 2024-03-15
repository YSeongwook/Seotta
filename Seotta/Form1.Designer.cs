using System.Windows.Forms;
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
            this.labelTitle.Font = new System.Drawing.Font("Consolas", 32, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(690, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(124, 240);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "섰다";

            // pae1
            this.pae1.BackColor = System.Drawing.Color.Black;
            this.pae1.Font = new System.Drawing.Font("Consolas", 4);
            this.pae1.ForeColor = System.Drawing.Color.White;
            this.pae1.Location = new System.Drawing.Point(10, 100);
            this.pae1.Multiline = true;
            this.pae1.Name = "pae1";
            this.pae2.TabIndex = 1;
            this.pae1.Size = new System.Drawing.Size(354, 554);
            this.pae1.BorderStyle = BorderStyle.None;
            this.pae1.ReadOnly = true;  // TextBox를 읽기 전용으로 설정
            this.pae1.TabStop = false;

            // pae2
            this.pae2.BackColor = System.Drawing.Color.Black;
            this.pae2.Font = new System.Drawing.Font("Consolas", 4);
            this.pae2.ForeColor = System.Drawing.Color.White;
            this.pae2.Location = new System.Drawing.Point(380, 100);
            this.pae2.Multiline = true;
            this.pae2.Name = "pae2";
            this.pae2.Size = new System.Drawing.Size(354, 554);
            this.pae2.TabIndex = 2;
            this.pae2.BorderStyle = BorderStyle.None;
            this.pae2.ReadOnly = true;  // TextBox를 읽기 전용으로 설정
            this.pae2.TabStop = false;

            // pae3
            this.pae3.BackColor = System.Drawing.Color.Black;
            this.pae3.Font = new System.Drawing.Font("Consolas", 4);
            this.pae3.ForeColor = System.Drawing.Color.White;
            this.pae3.Location = new System.Drawing.Point(750, 100);
            this.pae3.Multiline = true;
            this.pae3.Name = "pae3";
            this.pae3.Size = new System.Drawing.Size(354, 554);
            this.pae3.TabIndex = 3;
            this.pae3.BorderStyle = BorderStyle.None;
            this.pae3.ReadOnly = true;  // TextBox를 읽기 전용으로 설정
            this.pae3.TabStop = false;

            // pae4
            this.pae4.BackColor = System.Drawing.Color.Black;
            this.pae4.Font = new System.Drawing.Font("Consolas", 4);
            this.pae4.ForeColor = System.Drawing.Color.White;
            this.pae4.Location = new System.Drawing.Point(1120, 100);
            this.pae4.Multiline = true;
            this.pae4.Name = "pae4";
            this.pae4.Size = new System.Drawing.Size(354, 554);
            this.pae4.TabIndex = 4;
            this.pae4.BorderStyle = BorderStyle.None;
            this.pae4.ReadOnly = true;  // TextBox를 읽기 전용으로 설정
            this.pae4.TabStop = false;

            // gameProgress
            this.gameProgress.BackColor = System.Drawing.Color.Black;
            this.gameProgress.Font = new System.Drawing.Font("Consolas", 24F);
            this.gameProgress.ForeColor = System.Drawing.Color.White;
            this.gameProgress.Location = new System.Drawing.Point(10, 664);
            this.gameProgress.Multiline = true;
            this.gameProgress.Name = "gameProgress";
            this.gameProgress.Size = new System.Drawing.Size(1454, 360);
            this.gameProgress.TabIndex = 0; // TabIndex를 0으로 설정해 폼이 생성되면 포커스가 자동으로 할당
            this.gameProgress.TabStop = false;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
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
    }
}

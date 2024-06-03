using System.Drawing;

namespace Seotta
{
    partial class HelpForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.contentTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Consolas", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(560, 10);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "Title";
            this.titleLabel.Size = new System.Drawing.Size(240, 50);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contentTextBox
            // 
            this.contentTextBox.BackColor = System.Drawing.Color.Black;
            this.contentTextBox.Font = new System.Drawing.Font("Consolas", 22F);
            this.contentTextBox.ForeColor = System.Drawing.Color.White;
            this.contentTextBox.Location = new System.Drawing.Point(10, 80);
            this.contentTextBox.Multiline = true;
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.Text = "contentTextBox";
            this.contentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentTextBox.ReadOnly = true;
            this.contentTextBox.Size = new System.Drawing.Size(1390, 720);
            this.contentTextBox.TabIndex = 0;
            this.contentTextBox.TabStop = false;
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1420, 800);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.contentTextBox);
            this.KeyPreview = true;
            this.Name = "HelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "도움말";
            this.Icon = new Icon("seotta1.ico");
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox contentTextBox;
    }
}
    
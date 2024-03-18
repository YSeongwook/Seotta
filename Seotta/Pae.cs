using System.Windows.Forms;
using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;

namespace Seotta
{
    public class Pae
    {
        public string PaeNum { get; set; }
        public string PaeName { get; set; }
        public int PaeLevel { get; set; }
        public string SpecialPae;

        public Pae(string paeNum, string paeName)
        {
            this.PaeNum = paeNum;
            this.PaeName = paeName;
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

        // 출력은 PaeNum으로 "{pae.PaeNum}.txt"을 이용해 출력
        // 족보 계산은 PaeNum, PaeName으로 계산
    }
}

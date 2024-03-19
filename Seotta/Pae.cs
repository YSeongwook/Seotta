using System.Windows.Forms;
using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;

namespace Seotta
{
    public class Pae
    {
        // 월 변수 ex) 1, 2, 3, etc..
        public string PaeMonth { get; set; }
        // 이름 변수 ex) 1광, 1띠, etc..
        public string PaeName { get; set; }
        public int PaeLevel { get; set; }

        public string AsciiArtFileName { get; set; }

        public string specialPae;

        public string[] AsciiArt { get; }

        public string[] BackAsciiArt { get; }

        public Pae(string paeMonth, string paeName, string asciiArtTextFileName)
        {
            this.PaeMonth = paeMonth;
            this.PaeName = paeName;
            this.AsciiArtFileName = asciiArtTextFileName;
            this.AsciiArt = ReadAsciiArtFromFile(asciiArtTextFileName);
            this.BackAsciiArt = ReadAsciiArtFromFile("back_of_pae.txt");

        }

        public string[] ReadAsciiArtFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllLines(fileName);
            }
            else
            {
                WriteLine($"{fileName} 파일을 불러올 수 없습니다.");

                return new string[0];
            }
        }

        // 족보 계산은 PaeNum, PaeName으로 계산


    }
}

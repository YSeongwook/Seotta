using static System.Console;
using System.IO;

namespace Seotta
{
    public class Pae
    {
        public string PaeMonth { get; set; }// 월 변수 ex) 1, 2, 3, etc..
        public string PaeName { get; set; } // 이름 변수 ex) 1광, 1띠, etc..
        public int PaeLevel { get; set; }
        public string AsciiArtFileName { get; set; }
        public string[] AsciiArt { get; }
        public string[] BackAsciiArt { get; }

        public string specialPae;

        public Pae(string paeMonth, string paeName, string asciiArtTextFileName)
        {
            this.PaeMonth = paeMonth;
            this.PaeName = paeName;
            this.AsciiArtFileName = asciiArtTextFileName;
            this.AsciiArt = ReadAsciiArtFromFile(asciiArtTextFileName);     // 패의 앞면 Ascii Art를 읽는다.
            this.BackAsciiArt = ReadAsciiArtFromFile("back_of_pae.txt");    // 패의 뒷면 Ascii Art를 읽는다.
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
    }
}

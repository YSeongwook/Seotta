using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seotta
{
    class JokboHelper
    {
        Form1 form;
        Game game;

        public JokboHelper(Form1 form, Game game)
        {
            this.form = form;
            this.game = game;
        }

        public void DisplayJokboHelper(TextBox textBox, Pae[] pae, string name)
        {
            string jokbo = ComparePaeName(pae);    // 첫번째 패가 두번째 패보다 낮은 월이라면

            if (jokbo.Equals("3광8광"))
            {
                jokbo = "38광땡";
            }
            else if (jokbo.Equals("4열끗7열끗"))
            {
                jokbo = "암행어사";
            }
            else if (CountOccurrences(jokbo, "광")) // jokbo가 38광땡이 아니고, 광이 2개 이상 들어있다면(광땡이라면)
            {
                jokbo = ComparePaeMonth(pae);

                jokbo += "광땡";
            }
            else if (CountOccurrences(jokbo, "10")) // jokbo에 10이 2개 이상 들어있다면(장땡이라면)
            {
                jokbo = "장땡";
            }
            else if (jokbo.Equals("4열끗9열끗"))   // 멍텅구리 구사
            {
                jokbo = "멍텅구리구사";
            }
            else if (jokbo.Equals("3광7열끗"))    // 땡잡이
            {
                jokbo = "땡잡이";
            }
            else if (jokbo[0] == jokbo[2])    // 같은 월인 경우 ex) 1광1띠
            {
                jokbo = jokbo[0] + "땡";
            }
            else
            {
                jokbo = ComparePaeMonth(pae);

                switch (jokbo)
                {
                    case "49":
                        jokbo = "구사";
                        break;
                    case "12":
                        jokbo = "알리";
                        break;
                    case "14":
                        jokbo = "독사";
                        break;
                    case "19":
                        jokbo = "구삥";
                        break;
                    case "110":
                        jokbo = "장삥";
                        break;
                    case "104":
                        jokbo = "장사";
                        break;
                    case "46":
                        jokbo = "세륙";
                        break;
                    default:
                        int kkut = (Int32.Parse(pae[0].PaeMonth) + Int32.Parse(pae[1].PaeMonth)) % 10;
                        switch (kkut)
                        {
                            case 9:
                                jokbo = "갑오(9끗)";
                                break;
                            case 8:
                                jokbo = "8끗";
                                break;
                            case 7:
                                jokbo = "7끗";
                                break;
                            case 6:
                                jokbo = "6끗";
                                break;
                            case 5:
                                jokbo = "5끗";
                                break;
                            case 4:
                                jokbo = "4끗";
                                break;
                            case 3:
                                jokbo = "3끗";
                                break;
                            case 2:
                                jokbo = "2끗";
                                break;
                            case 1:
                                jokbo = "1끗";
                                break;
                            case 0:
                                jokbo = "망통(0끗)";
                                break;
                        }
                        break;
                }
            }
            textBox.Text = JokboDescription.GetDescription(jokbo);

            // 만약 둘이 비교해야한다면 아래 조건문 만나기 전에 값 넘겨줘서 비교해야함

            if (name == "컴퓨터")
            {
                game.CpuJokbo = jokbo;
            }
            else
            {
                game.PlayerJokbo = jokbo;
            }

            // 이부분에서 jokbo에 패종류만 들어가게 해야함
            if (jokbo.Contains("38광땡"))
            {
                jokbo = "1.38광땡";
            }
            else if (jokbo.Contains("광땡"))
            {
                jokbo = "2.광땡";
            }
            else if (jokbo.Contains("암행어사"))
            {
                jokbo = "* 암행어사";
            }
            else if (jokbo.Contains("땡잡이"))
            {
                jokbo = "* 땡잡이";
            }
            else if (jokbo.Contains("땡"))
            {
                jokbo = "3.땡";
            }
            else if (jokbo.Contains("구사"))
            {
                jokbo = "* 구사";
            }
            else if (jokbo.Contains("알리"))
            {
                jokbo = "4.알리";
            }
            else if (jokbo.Contains("독사"))
            {
                jokbo = "5.독사";
            }
            else if (jokbo.Contains("구삥"))
            {
                jokbo = "6.구삥";
            }
            else if (jokbo.Contains("장삥"))
            {
                jokbo = "7.장삥";
            }
            else if (jokbo.Contains("장사"))
            {
                jokbo = "8.장사";
            }
            else if (jokbo.Contains("세륙"))
            {
                jokbo = "9.세륙";
            }
            else if (jokbo.Contains("갑오"))
            {
                jokbo = "10.갑오";
            }
            else if (jokbo.Contains("끗"))
            {
                jokbo = "11.끗,망통";
            }

            form.HighlightJokboButton(game.GetJokboPanel(), jokbo);
        }

        // 첫번째 패가 두번째 패보다 낮은 월이라면
        public string ComparePaeName(Pae[] pae)
        {
            if (string.Compare(pae[0].PaeName, pae[1].PaeName) < 0)
            {
                return pae[0].PaeName + pae[1].PaeName;
            }
            else
            {
                return pae[1].PaeName + pae[0].PaeName;
            }
        }

        // 첫번째 패가 두번째 패보다 낮은 월이라면
        public string ComparePaeMonth(Pae[] pae)
        {
            if (string.Compare(pae[0].PaeMonth, pae[1].PaeMonth) < 0)
            {
                return pae[0].PaeMonth + pae[1].PaeMonth;
            }
            else
            {
                return pae[1].PaeMonth + pae[0].PaeMonth;
            }
        }

        // 문자열에 어떤 문자가 몇개 들어있는지 반환
        public bool CountOccurrences(string text, string pattern)
        {
            int count = 0;
            int index = 0;
            while ((index = text.IndexOf(pattern, index)) != -1)
            {
                index += pattern.Length;
                count++;
            }

            // 같은 문자가 2개이상 들어있다면
            if (count >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

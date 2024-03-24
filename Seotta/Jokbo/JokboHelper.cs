using System;
using System.Windows.Forms;
using System.Xml.Linq;

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

            if (name == "컴퓨터")
            {
                game.CpuJokbo = jokbo;
            }
            else
            {
                // 족보 도우미 텍스트 박스에 족보 설명 출력
                textBox.Text = jokbo + "\r\n" + JokboDescription.GetDescription(jokbo);

                game.PlayerJokbo = jokbo;
                jokbo = GroupingJokbo(jokbo);
                form.HighlightJokboButton(game.GetJokboPanel(), jokbo);
            }
        }

        // 족보 그룹화
        public string GroupingJokbo(string jokbo)
        {
            if (jokbo.Contains("38광땡")) return "1.38광땡";
            if (jokbo.Contains("광땡")) return "2.광땡";
            if (jokbo.Contains("암행어사")) return "* 암행어사";
            if (jokbo.Contains("땡잡이")) return "* 땡잡이";
            if (jokbo.Contains("땡")) return "3.땡";
            if (jokbo.Contains("구사")) return "* 구사";
            if (jokbo.Contains("알리")) return "4.알리";
            if (jokbo.Contains("독사")) return "5.독사";
            if (jokbo.Contains("구삥")) return "6.구삥";
            if (jokbo.Contains("장삥")) return "7.장삥";
            if (jokbo.Contains("장사")) return "8.장사";
            if (jokbo.Contains("세륙")) return "9.세륙";
            if (jokbo.Contains("갑오")) return "10.갑오";
            if (jokbo.Contains("끗")) return "11.끗,망통";

            return jokbo; // 만약 해당하는 문자열이 없으면 원래 jokbo 반환
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

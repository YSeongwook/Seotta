using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

// 족보 도우미에 출력할 설명
public class JokboDescription
{
    private Dictionary<string, string> descriptions;

    public JokboDescription(string filePath)
    {
        LoadDescriptionsFromJson(filePath);
    }

    public void LoadDescriptionsFromJson(string filePath)
    {
        // JSON 파일에서 문자열을 읽어옴
        string jsonText = File.ReadAllText(filePath);

        // JSON 문자열을 딕셔너리로 변환
        this.descriptions = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);
    }

    public string GetDescription(string name)
    {
        bool isKkut = name.Contains("끗") && !name.Equals("갑오(9끗)") && !name.Equals("망통(0끗)");
        bool isGwangTaeng = name.Equals("13광땡") || name.Equals("18광땡");
        bool isTaeng = name.Contains("땡") && !name.Equals("38광땡") && !name.Equals("13광땡") && !name.Equals("18광땡") && !name.Equals("장땡") && !name.Equals("땡잡이");

        // 갑오, 망통이 아닌 끗인 경우
        if (isKkut)
        {
            name = "끗";
        } // 광땡인 경우
        else if(isGwangTaeng)
        {
            name = "광땡";
        } // 땡인 경우 
        else if(isTaeng)
        {
            name = "땡";
        }

        return descriptions[name];
    }
}
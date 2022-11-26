using System.Text.RegularExpressions;

namespace Server.Helpers
{
    public class StringHelper
    {
        public static string StringFormatParseFirst(string template, string str) => StringHelper.StringFormatParse(template, str).FirstOrDefault<string>();

        public static List<string> StringFormatParse(string template, string str)
        {
        Match match = new Regex("^" + Regex.Replace(template, "\\{[0-9]+\\}", "(.*?)") + "$").Match(str);
        List<string> stringList = new List<string>();
        for (int groupnum = 1; groupnum < match.Groups.Count; ++groupnum)
            stringList.Add(match.Groups[groupnum].Value);
        return stringList;
        }

        public string CutToLength(string str, int len) => str != null && str.Length > len ? str.Remove(len) : str;

        public string RemoveCertSpace(string str) => new Regex("\\s*-\\s*").Replace(str, "-").Replace(" ", "_");

        public string RemoveDashs(string str) => str == null ? (string) null : new Regex("-").Replace(str, "");

        public string RemoveSpaces(string str) => str == null ? (string) null : new Regex("\\s*").Replace(str, "");

        public static string GetTruncatedProgramVersion(string programVersion)
        {
        if (string.IsNullOrEmpty(programVersion))
            return programVersion;
        Regex regex1 = new Regex("^.{0,}\\s[0-9]{1,}\\.[0-9]{1,}\\.[0-9]{1,}\\.[0-9]{1,}$");
        Regex regex2 = new Regex("^.{0,}\\s[0-9]{1,}\\.[0-9]{1,}\\.[0-9]{1,}\\.[0-9]{1,}\\.[0-9]{1,}$");
        Regex regex3 = new Regex("^.{0,}\\s[0-9]{1,}\\.[0-9]{1,}");
        string input = programVersion;
        return regex1.IsMatch(input) || regex2.IsMatch(programVersion) ? regex3.Match(programVersion).Value : programVersion;
        }
    }
}
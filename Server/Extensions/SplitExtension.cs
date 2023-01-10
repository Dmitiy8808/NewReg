namespace Server.Extensions
{
    public static class SplitExtension
    {
        public static IEnumerable<string> SplitEx(this string str, int n)
        {
            if (String.IsNullOrEmpty(str) || n < 1) {
                throw new ArgumentException();
            }
    
            return Enumerable.Range(0, str.Length / n)
                            .Select(i => str.Substring(i * n, n));
        }
    }
}
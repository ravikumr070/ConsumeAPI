namespace Application.Extensions
{
    public static class StringExtension
    {
        public static string GetFirst(this string source, int length)
        {
            return length >= source.Length ? source.ToUpper() : source.Substring(0, length).ToUpper();
        }
        public static string GetLast(this string source, int length)
        {
            return length >= source.Length ? source.ToUpper() : source.Substring(source.Length - length).ToUpper();
        }
        public static string GetUptoFirstSpace(this string source)
        {
            return source.Contains(" ") ? source.Split(" ")[0].ToUpper() : source.ToUpper();
        }
    }
}

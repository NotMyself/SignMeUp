namespace SignMeUp.Core.Extensions
{
    public static class StringExtensions
    {
        public static string FormatWith(this string @format, params string[] args)
        {
            return string.Format(@format, args);
        }

    }
}

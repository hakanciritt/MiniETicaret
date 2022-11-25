namespace ETicaret.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        {
            name?.Replace(@"\", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("/", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("=", "")
                .Replace("?", "")
                .Replace("-", "")
                .Replace("@", "")
                .Replace(",", "")
                .Replace(";", "")
                .Replace(":", "")
                .Replace(".", "-")
                .Replace("İ", "i")
                .Replace("ı", "i")
                .Replace("ş", "s")
                .Replace("Ş", "S")
                .Replace("ğ", "g")
                .Replace("G", "G")
                .Replace("ü", "u")
                .Replace("Ü", "U")
                .Replace("ö", "o")
                .Replace("Ö", "O")
                .Replace("ç", "c")
                .Replace("Ç", "C");

            return name;
        }
    }
}

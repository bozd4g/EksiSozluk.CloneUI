namespace EksiSozluk.CloneUI.iOS.Helper
{
    public static class FontHelper
    {
        public static string GetFontName(string fontFamily)
        {
            switch (fontFamily)
            {
                case "fonts/SourceSansPro-L.ttf":
                    return "SourceSansPro-Light";
                case "fonts/SourceSansPro-R.ttf":
                    return "SourceSansPro-Regular";
                case "fonts/SourceSansPro-SB.ttf":
                    return "SourceSansPro-Semibold";
                case "fonts/SourceSansPro-B.ttf":
                    return "SourceSansPro-Bold";
                default:
                    return "SourceSansPro-Regular";
            }
        }
    }
}
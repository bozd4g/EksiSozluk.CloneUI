using UIKit;

namespace EksiSozluk.CloneUI.iOS.Helper
{
    public static class FontHelper
    {
        public static string GetFontName(string fontFamily)
        {
            if (fontFamily.Contains("SourceSansPro-L"))
                return "SourceSansPro-Light";
            else if(fontFamily.Contains("SourceSansPro-R"))
                return "SourceSansPro-Regular";
            else if(fontFamily.Contains("SourceSansPro-SB"))
                return "SourceSansPro-SemiBold";
            else if (fontFamily.Contains("SourceSansPro-B"))
                return "SourceSansPro-Bold";
            else
                return "SourceSansPro-Regular";
        }
         
    }
}
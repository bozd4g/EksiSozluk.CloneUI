using EksiSozluk.CloneUI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageRenderer))]

namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class NavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;

            var att = new UITextAttributes
            {
                TextColor = UIColor.White,
                Font = UIFont.FromName("SourceSansPro-R", 32)
            };
            UINavigationBar.Appearance.SetTitleTextAttributes(att);
            UINavigationBar.Appearance.ShadowImage = new UIImage();
        }
    }
}
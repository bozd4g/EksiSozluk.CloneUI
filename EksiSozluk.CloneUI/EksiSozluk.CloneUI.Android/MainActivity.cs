using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.CrossPlatformTintedImage.Android;
using Plugin.HtmlLabel.Android;

namespace EksiSozluk.CloneUI.Droid
{
    [Activity(Label = "EksiSozluk.CloneUI", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public new static Context ApplicationContext;

        protected override void OnCreate(Bundle bundle)
        {
            ApplicationContext = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            HtmlLabelRenderer.Initialize();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            TintedImageRenderer.Init();

            LoadApplication(new App());
        }
    }
}


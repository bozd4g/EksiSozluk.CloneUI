using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using Plugin.CrossPlatformTintedImage.Android;

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

            global::Xamarin.Forms.Forms.Init(this, bundle);
            TintedImageRenderer.Init();
            CarouselViewRenderer.Init();

            LoadApplication(new App());
        }
    }
}


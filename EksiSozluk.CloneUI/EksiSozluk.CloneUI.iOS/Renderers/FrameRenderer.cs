using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FrameRenderer = EksiSozluk.CloneUI.iOS.Renderers.FrameRenderer;

[assembly: ExportRenderer(typeof(Frame), typeof(FrameRenderer))]
namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class FrameRenderer : global::Xamarin.Forms.Platform.iOS.FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.50f;
        }
    }
}
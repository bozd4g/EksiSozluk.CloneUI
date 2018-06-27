using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircleView), typeof(CircleViewRenderer))]
namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class CircleViewRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            Layer.MasksToBounds = true;
            Layer.CornerRadius = (float)((CircleView)Element).CornerRadius;
        }
    }
}
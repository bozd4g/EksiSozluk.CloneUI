using System;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]
namespace EksiSozluk.CloneUI.iOS.Renderers
{
    public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, UISegmentedControl>
    {
        UISegmentedControl nativeControl;

        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                nativeControl = new UISegmentedControl();
                for (var i = 0; i < Element.Children.Count; i++)
                {
                    nativeControl.InsertSegment(Element.Children[i].Text, i, false);
                }

                nativeControl.Layer.CornerRadius = 15.0f;
                nativeControl.Layer.BorderColor = Element.TintColor.ToCGColor();
                nativeControl.Layer.BorderWidth = 1.0f;
                nativeControl.Layer.MasksToBounds = true;

                nativeControl.Enabled = Element.IsEnabled;
                nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Element.DisabledColor.ToUIColor();
                SetSelectedTextColor();

                nativeControl.SelectedSegment = Element.SelectedSegment;

                SetNativeControl(nativeControl);
            }

            if (e.OldElement != null)
            {
                if (nativeControl != null)
                    nativeControl.ValueChanged -= NativeControl_ValueChanged;
            }

            if (e.NewElement != null)
            {
                nativeControl.ValueChanged += NativeControl_ValueChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (nativeControl == null || Element == null) return;
            switch (e.PropertyName)
            {
                case "Renderer":
                    Element?.SendValueChanged();
                    break;
                case "SelectedSegment":
                    nativeControl.SelectedSegment = Element.SelectedSegment;
                    Element.SendValueChanged();
                    break;
                case "TintColor":
                    nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Element.DisabledColor.ToUIColor();
                    break;
                case "IsEnabled":
                    nativeControl.Enabled = Element.IsEnabled;
                    nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Element.DisabledColor.ToUIColor();
                    break;
                case "SelectedTextColor":
                    SetSelectedTextColor();
                    break;

            }

        }

        void SetSelectedTextColor()
        {
            var attr = new UITextAttributes();
            attr.TextColor = Element.SelectedTextColor.ToUIColor();
            nativeControl.SetTitleTextAttributes(attr, UIControlState.Selected);
        }

        void NativeControl_ValueChanged(object sender, EventArgs e)
        {
            Element.SelectedSegment = (int)nativeControl.SelectedSegment;
        }

        protected override void Dispose(bool disposing)
        {
            if (nativeControl != null)
            {
                nativeControl.ValueChanged -= NativeControl_ValueChanged;
                nativeControl.Dispose();
                nativeControl = null;
            }

            try
            {
                base.Dispose(disposing);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public static void Init()
        {
            var temp = DateTime.Now;
        }
    }
}
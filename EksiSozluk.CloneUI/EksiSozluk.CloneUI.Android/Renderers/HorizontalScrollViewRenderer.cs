using System;
using System.ComponentModel;
using EksiSozluk.CloneUI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HorizontalScrollView = EksiSozluk.CloneUI.Custom.HorizontalScrollView;

[assembly: ExportRenderer(typeof(HorizontalScrollView), typeof(HorizontalScrollViewRenderer))]

namespace EksiSozluk.CloneUI.Droid.Renderers
{
    public class HorizontalScrollViewRenderer : ScrollViewRenderer
    {
        public HorizontalScrollViewRenderer() : base(MainActivity.ApplicationContext)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as HorizontalScrollView;
            element?.Render();

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (ChildCount <= 0)
                return;

            GetChildAt(0).HorizontalScrollBarEnabled = false;
            GetChildAt(0).VerticalScrollBarEnabled = false;
        }
    }
}
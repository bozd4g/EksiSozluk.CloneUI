using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]
namespace EksiSozluk.CloneUI.Droid.Renderers
{
    public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, RadioGroup>
    {
        RadioGroup nativeControl;
        RadioButton _rb;

        public SegmentedControlRenderer() : base(MainActivity.ApplicationContext)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property with
                // the SetNativeControl method

                var layoutInflater = LayoutInflater.From(MainActivity.ApplicationContext);
                var view = layoutInflater.Inflate(Resource.Layout.RadioGroup, null);
                nativeControl = (RadioGroup)layoutInflater.Inflate(Resource.Layout.RadioGroup, null);

                for (var i = 0; i < Element.Children.Count; i++)
                {
                    var o = Element.Children[i];
                    var rb = (RadioButton)layoutInflater.Inflate(Resource.Layout.RadioButton, null);

                    Typeface typeface = Typeface.CreateFromAsset(this.Context.Assets, "fonts/SourceSansPro-R.ttf");
                    rb.SetTypeface(typeface, TypefaceStyle.Normal);
                    rb.SetTextSize(ComplexUnitType.Dip, 13f);

                    rb.LayoutParameters = new RadioGroup.LayoutParams(0, LayoutParams.WrapContent, 1f);
                    rb.Text = o.Text;

                    if (i == 0)
                        rb.SetBackgroundResource(Resource.Drawable.segmented_control_first_background);
                    else if (i == Element.Children.Count - 1)
                        rb.SetBackgroundResource(Resource.Drawable.segmented_control_last_background);

                    ConfigureRadioButton(i, rb);

                    nativeControl.AddView(rb);
                }

                var option = (RadioButton)nativeControl.GetChildAt(Element.SelectedSegment);

                if (option != null)
                    option.Checked = true;

                SetNativeControl(nativeControl);
            }

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources

                if (nativeControl != null)
                    nativeControl.CheckedChange -= NativeControl_ValueChanged;
            }

            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers

                nativeControl.CheckedChange += NativeControl_ValueChanged;
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
                    var option = (RadioButton)nativeControl.GetChildAt(Element.SelectedSegment);

                    if (option != null)
                        option.Checked = true;

                    if (Element.SelectedSegment < 0)
                    {
                        var layoutInflater = LayoutInflater.From(MainActivity.ApplicationContext);

                        nativeControl = (RadioGroup)layoutInflater.Inflate(Resource.Layout.RadioGroup, null);

                        for (var i = 0; i < Element.Children.Count; i++)
                        {
                            var o = Element.Children[i];
                            var rb = (RadioButton)layoutInflater.Inflate(Resource.Layout.RadioButton, null);

                            rb.LayoutParameters = new RadioGroup.LayoutParams(0, LayoutParams.WrapContent, 1f);
                            rb.Text = o.Text;

                            if (i == 0)
                                rb.SetBackgroundResource(Resource.Drawable.segmented_control_first_background);
                            else if (i == Element.Children.Count - 1)
                                rb.SetBackgroundResource(Resource.Drawable.segmented_control_last_background);

                            ConfigureRadioButton(i, rb);

                            nativeControl.AddView(rb);
                        }

                        nativeControl.CheckedChange += NativeControl_ValueChanged;

                        SetNativeControl(nativeControl);
                    }

                    Element.SendValueChanged();
                    break;
                case "TintColor":
                    OnPropertyChanged();
                    break;
                case "IsEnabled":
                    OnPropertyChanged();
                    break;
                case "SelectedTextColor":
                    var v = (RadioButton)nativeControl.GetChildAt(Element.SelectedSegment);
                    v.SetTextColor(Element.SelectedTextColor.ToAndroid());
                    break;
            }
        }

        void OnPropertyChanged()
        {
            if (nativeControl != null && Element != null)
            {
                for (var i = 0; i < Element.Children.Count; i++)
                {
                    var rb = (RadioButton)nativeControl.GetChildAt(i);

                    ConfigureRadioButton(i, rb);
                }
            }
        }

        void ConfigureRadioButton(int i, RadioButton rb)
        {
            if (i == Element.SelectedSegment)
            {
                rb.SetTextColor(Element.SelectedTextColor.ToAndroid());
                _rb = rb;
            }
            else
            {
                var textColor = Element.IsEnabled ? Element.TextColor.ToAndroid() : Element.DisabledColor.ToAndroid();
                rb.SetTextColor(textColor);
            }

            var gradientDrawable = (StateListDrawable)rb.Background;
            var drawableContainerState = (DrawableContainer.DrawableContainerState)gradientDrawable.GetConstantState();
            var children = drawableContainerState.GetChildren();

            // Doesnt works on API < 18
            var selectedShape = children[0] is GradientDrawable drawable ? drawable : (GradientDrawable)((InsetDrawable)children[0]).Drawable;
            var unselectedShape = children[1] is GradientDrawable drawable1 ? drawable1 : (GradientDrawable)((InsetDrawable)children[1]).Drawable;

            var color = Element.IsEnabled ? Element.TintColor.ToAndroid() : Element.DisabledColor.ToAndroid();

            selectedShape.SetStroke(3, color);
            selectedShape.SetColor(color);
            unselectedShape.SetStroke(3, color);

            rb.Enabled = Element.IsEnabled;
        }

        void NativeControl_ValueChanged(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            var rg = (RadioGroup)sender;
            if (rg.CheckedRadioButtonId != -1)
            {
                var id = rg.CheckedRadioButtonId;
                var radioButton = rg.FindViewById(id);
                var radioId = rg.IndexOfChild(radioButton);

                var rb = (RadioButton)rg.GetChildAt(radioId);

                var color = Element.IsEnabled ? Element.UnselectedTextColor.ToAndroid() : Element.DisabledColor.ToAndroid();
                _rb?.SetTextColor(color);
                rb.SetTextColor(Element.SelectedTextColor.ToAndroid());
                _rb = rb;

                Element.SelectedSegment = radioId;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (nativeControl != null)
            {
                nativeControl.CheckedChange -= NativeControl_ValueChanged;
                nativeControl.Dispose();
                nativeControl = null;
                _rb = null;
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
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var temp = DateTime.Now;
        }
    }
}
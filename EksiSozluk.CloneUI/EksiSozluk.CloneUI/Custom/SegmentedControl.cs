using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Custom
{
    public class SegmentedControl : View, IViewContainer<SegmentedControlOption>
    {
        public IList<SegmentedControlOption> Children { get; set; }

        public SegmentedControl()
        {
            Children = new List<SegmentedControlOption>();
        }

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(
            nameof(TintColor),
            typeof(Color),
            typeof(SegmentedControl),
            Color.Blue);

        public Color TintColor
        {
            get => (Color) GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(SegmentedControl),
            Color.Blue);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(
            nameof(DisabledColor),
            typeof(Color),
            typeof(SegmentedControl),
            Color.Gray);

        public Color DisabledColor
        {
            get => (Color) GetValue(DisabledColorProperty);
            set => SetValue(DisabledColorProperty, value);
        }

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(SegmentedControl),
            Color.White);

        public Color SelectedTextColor
        {
            get => (Color) GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }

        public static readonly BindableProperty UnselectedTextColorProperty = BindableProperty.Create(
            nameof(UnselectedTextColor),
            typeof(Color),
            typeof(SegmentedControl),
            Color.White);

        public Color UnselectedTextColor
        {
            get => (Color)GetValue(UnselectedTextColorProperty);
            set => SetValue(UnselectedTextColorProperty, value);
        }

        public static readonly BindableProperty SelectedSegmentProperty = BindableProperty.Create(
            nameof(SelectedSegment),
            typeof(int),
            typeof(SegmentedControl),
            0);

        public int SelectedSegment
        {
            get => (int) GetValue(SelectedSegmentProperty);
            set => SetValue(SelectedSegmentProperty, value);
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendValueChanged()
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs {NewValue = this.SelectedSegment});
        }
    }

    public class SegmentedControlOption : View
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(SegmentedControlOption),
            string.Empty);

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }

    public class ValueChangedEventArgs : EventArgs
    {
        public int NewValue { get; set; }
    }
}
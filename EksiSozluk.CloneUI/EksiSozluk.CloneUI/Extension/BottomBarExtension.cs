﻿using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Extension
{
    // Just for Android
    public static class BottomBarExtension
    {
        #region TabColorProperty

        public static readonly BindableProperty TabColorProperty = BindableProperty.CreateAttached(
            propertyName: "TabColor",
            returnType: typeof(Color?),
            declaringType: typeof(Page),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay,
            validateValue: null,
            propertyChanged: null);

        public static void SetTabColor(this Page page, Color? color)
        {
            page.SetValue(TabColorProperty, color);
        }

        public static Color? GetTabColor(this Page page)
        {
            return (Color?) page.GetValue(TabColorProperty);
        }

        #endregion
    }
}
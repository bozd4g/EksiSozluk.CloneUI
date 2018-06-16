using System;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Custom
{
    public class CustomPicker : Picker
    {
        public EventHandler HasItemEvent;

        public CustomPicker()
        {
            HasItemEvent = (sender, args) => { SelectedIndex = 0; };
        }
    }
}

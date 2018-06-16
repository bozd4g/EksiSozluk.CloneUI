using System.Windows.Input;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Custom
{
    public class ItemTappedAttached
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(ListView),
                defaultValue: null,
                defaultBindingMode: BindingMode.OneWay,
                validateValue: null,
                propertyChanged: OnItemTappedChanged);

        public static ICommand GetItemTapped(BindableObject bindable)
        {
            return (ICommand) bindable.GetValue(CommandProperty);
        }

        public static void SetItemTapped(BindableObject bindable, ICommand value)
        {
            bindable.SetValue(CommandProperty, value);
        }

        public static void OnItemTappedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ListView control)
                control.ItemTapped += OnItemTapped;
        }

        private static void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView control)
            {
                control.SelectedItem = null;

                var command = GetItemTapped(control);
                if (command != null && command.CanExecute(e.Item))
                    command.Execute(e.Item);
            }
        }
    }
}
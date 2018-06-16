using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.Droid.Utils
{
    public class PageController : IPageController
    {
        private readonly ReflectedProxy<Page> _proxy;

        public static IPageController Create(Page page)
        {
            return new PageController(page);
        }

        PageController(Page page)
        {
            _proxy = new ReflectedProxy<Page>(page);
        }

        public Rectangle ContainerArea
        {
            get => _proxy.GetPropertyValue<Rectangle>();
            set => _proxy.SetPropertyValue(value);
        }

        public bool IgnoresContainerArea
        {
            get => _proxy.GetPropertyValue<bool>();
            set => _proxy.SetPropertyValue(value);
        }

        public ObservableCollection<Element> InternalChildren
        {
            get => _proxy.GetPropertyValue<ObservableCollection<Element>>();
            set => _proxy.SetPropertyValue(value);
        }

        public void SendAppearing()
        {
            _proxy.Call();
        }

        public void SendDisappearing()
        {
            _proxy.Call();
        }
    }
}
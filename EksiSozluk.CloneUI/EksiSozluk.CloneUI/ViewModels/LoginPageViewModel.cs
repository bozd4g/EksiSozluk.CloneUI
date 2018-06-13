using System.Windows.Input;
using EksiSozluk.CloneUI.Extension.Notify;
using EksiSozluk.CloneUI.Helpers;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.ViewModels
{
    public class LoginPageViewModel : Base
    {
        public LoginPageViewModel()
        {
        }

        public ICommand SignupCommand { get; set; }

        private async void OnSignup()
        {
        }
    }
}

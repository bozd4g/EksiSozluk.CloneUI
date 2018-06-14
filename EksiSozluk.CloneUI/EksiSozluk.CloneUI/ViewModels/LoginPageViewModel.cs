using System.Windows.Input;
using EksiSozluk.CloneUI.Extension.Notify;
using EksiSozluk.CloneUI.Helpers;
using EksiSozluk.CloneUI.Views;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.ViewModels
{
    public class LoginPageViewModel : Base
    {
        public LoginPageViewModel()
        {
            SignupCommand = new Command(OnSignup);
        }

        public ICommand SignupCommand { get; set; }

        private async void OnSignup()
        {
            await NavigationHelper.PushModalAsync(new NavigationPage(new SignupPage()));
        }
    }
}

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
            LoginCommand = new Command(OnLogin);
            SignupCommand = new Command(OnSignup);
        }

        public ICommand LoginCommand { get; set; }
        public ICommand SignupCommand { get; set; }

        private void OnLogin()
        {
            Application.Current.MainPage = new HomePage();
        }
        private async void OnSignup()
        {
            await NavigationHelper.PushModalAsync(new NavigationPage(new SignupPage()));
        }
    }
}

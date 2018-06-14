using System.Windows.Input;
using EksiSozluk.CloneUI.Extension.Notify;
using EksiSozluk.CloneUI.Helpers;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.ViewModels
{
    public class SignupPageViewModel : Base
    {
        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value; 
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public SignupPageViewModel()
        {
            CheckCommand = new Command(OnChecked);
            LoginCommand = new Command(OnLogin);
        }

        public ICommand CheckCommand { get; set; }
        public ICommand LoginCommand { get; set; }


        private void OnChecked()
        {
            IsChecked = !IsChecked;
        }

        private async void OnLogin()
        {
            await NavigationHelper.PopModalAsync();
        }

    }
}

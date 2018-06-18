using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EksiSozluk.CloneUI.Extension.Notify;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.ViewModels
{
    public class NotificationsPageViewModel : Base
    {
        #region Encapsulation

        private IList<NotificationsBinding> list;
        public IList<NotificationsBinding> List
        {
            get => list ?? (list = new ObservableCollection<NotificationsBinding>());
            set
            {
                list = value; 
                OnPropertyChanged(nameof(List));
            }
        }

        #endregion

        public NotificationsPageViewModel()
        {
            BindData();

            ItemSelectedCommand = new Command(OnItemSelected);
        }

        void BindData()
        {
            List.Add(new NotificationsBinding
            {
                Id = 0,
                Title = "olan biten",
                NotifyCount = 1,
                HasNotify = true
            });
            List.Add(new NotificationsBinding
            {
                Id = 1,
                Title = "sözlükte kan aranıyor duyuruları",
                HasNotify = false
            });
        }

        #region Commands

        public ICommand ItemSelectedCommand { get; set; }

        #endregion

        void OnItemSelected(object e)
        {

        }

    }

    public class NotificationsBinding
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool HasNotify { get; set; }
        public int NotifyCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EksiSozluk.CloneUI.Extension.Notify;

namespace EksiSozluk.CloneUI.ViewModels
{
    public class MessagesPageViewModel : Base
    {
        #region Encapsulation

        private IList<MessagesBinding> list;
        public IList<MessagesBinding> List
        {
            get => list ?? (list = new ObservableCollection<MessagesBinding>());
            set
            {
                list = value;
                OnPropertyChanged(nameof(List));
            }
        }

        #endregion

        public MessagesPageViewModel()
        {
            BindData();
        }

        void BindData()
        {
            List.Add(new MessagesBinding
            {
                Id = 0,
                User = "ekşisözlük",
                Content = "10. entry'nizi girdiğinizi görünce dayanamadık, sizi onay sırasına aldık. profilinizden sıranızı görebilirsiniz. hadi bakalım.",
                ProfileText = "ek",
                Date = DateTime.Now
            });
        }

    }

    public class MessagesBinding
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Content { get; set; }
        public string ProfileText { get; set; }
        public DateTime Date { get; set; }
    }
}

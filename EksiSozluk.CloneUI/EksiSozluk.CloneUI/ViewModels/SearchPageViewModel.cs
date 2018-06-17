using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EksiSozluk.CloneUI.Extension.Notify;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.ViewModels
{
    public class SearchPageViewModel : Base
    {
        #region Encapsulation

        private IList<SearchBinding> list;
        public IList<SearchBinding> List
        {
            get => list ?? (list = new ObservableCollection<SearchBinding>());
            set
            {
                list = value;
                OnPropertyChanged(nameof(List));
            }
        }

        #endregion

        public SearchPageViewModel()
        {
            BindData();

            ItemSelectedCommand = new Command(OnItemSelected);
        }

        void BindData()
        {
            string[] tags =
            {
                "spor", "ilişkiler", "siyaset", "seyahat", "otomotiv", "tv", "anket", "bilim", "edebiyat", "eğitim",
                "ekonomi", "ekşi-sözlük", "haber", "havacılık", "magazin", "moda", "motosiklet", "müzik", "oyun",
                "programlama", "sağlık", "sanat", "sinema", "spoiler", "tarih", "teknoloji", "yeme-içme"
            };

            for (int i = 0; i < tags.Length; i++)
            {
                List.Add(new SearchBinding
                {
                    Id = i,
                    Name = "#" + tags[i]
                });
            }
        }

        #region Commands

        public ICommand ItemSelectedCommand { get; set; }

        #endregion

        void OnItemSelected(object e)
        {
            if(!(e is SearchBinding binding))
                return;
        }
    }

    public class SearchBinding
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
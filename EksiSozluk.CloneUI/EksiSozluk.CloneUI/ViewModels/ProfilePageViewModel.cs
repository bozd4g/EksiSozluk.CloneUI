using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EksiSozluk.CloneUI.Extension.Notify;

namespace EksiSozluk.CloneUI.ViewModels
{
    public class ProfilePageViewModel : Base
    {
        #region Encapsulation

        private IList<ProfileBinding> list;
        public IList<ProfileBinding> List
        {
            get => list ?? (list = new ObservableCollection<ProfileBinding>());
            set
            {
                list = value; 
                OnPropertyChanged(nameof(List));
            }
        }

        private string profileName;
        public string ProfileName
        {
            get => profileName;
            set
            {
                profileName = value; 
                OnPropertyChanged(nameof(ProfileName));
            }
        }

        private string profileShortName;
        public string ProfileShortName
        {
            get => profileShortName;
            set
            {
                profileShortName = value;
                OnPropertyChanged(nameof(ProfileShortName));
            }
        }

        private int entryCount;
        public int EntryCount
        {
            get => entryCount;
            set
            {
                entryCount = value;
                OnPropertyChanged(nameof(EntryCount));
            }
        }

        private int followCount;
        public int FollowCount
        {
            get => followCount;
            set
            {
                followCount = value; 
                OnPropertyChanged(nameof(FollowCount));
            }
        }

        private int followerCount;
        public int FollowerCount
        {
            get => followerCount;
            set
            {
                followerCount = value;
                OnPropertyChanged(nameof(FollowerCount));
            }
        }

        private string orderText;
        public string OrderText
        {
            get => orderText;
            set
            {
                orderText = value; 
                OnPropertyChanged(nameof(OrderText));
            }
        }

        #endregion

        public ProfilePageViewModel()
        {
            ProfileName = "langah";
            ProfileShortName = "la";
            OrderText = $"çaylak onay listesinde <b>{33791}.</b> sıradasınız";

            BindData();
        }

        void BindData()
        {
            List.Add(new ProfileBinding
            {
                Id = 0,
                Title = "ilişkisini instagramda yaşayan çift",
                Entry = "insanın \"allah sizi alsın\" diyesi gelen çifttir",
                FavoriteCount = 1,
                Date = DateTime.Now
            });
            List.Add(new ProfileBinding
            {
                Id = 1,
                Title = "it might hurt a bit",
                Entry = "biraz farklılıklar olsa da <font color=\"#81c14b\">amazon</font>'da satılan kitabın ismidir",
                FavoriteCount = 0,
                Date = DateTime.Now
            });
            List.Add(new ProfileBinding
            {
                Id = 2,
                Title = "komik çocuk",
                Entry = "komik olmak için zeki olmak gerektiğine inandığım ve kendisine de bu sıfatla baktığım çocuk tipidir",
                FavoriteCount = 0,
                Date = DateTime.Now
            });
            List.Add(new ProfileBinding
            {
                Id = 3,
                Title = "gelen uçağa el sallamak",
                Entry = "eylemin sonrasında \"napıyorsun <font color=\"#81c14b\">andaval</font>\" diye kendisini sorgulamasını istediğim durumdur. zira gidişat hiç iyi değil",
                FavoriteCount = 0,
                Date = DateTime.Now
            });

            foreach (var profileBinding in List)
                profileBinding.FavoriteVisible = profileBinding.FavoriteCount != 0;
        }

    }

    public class ProfileBinding
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Entry { get; set; }
        public int FavoriteCount { get; set; }
        public bool FavoriteVisible { get; set; }
        public DateTime Date { get; set; }
    }
}
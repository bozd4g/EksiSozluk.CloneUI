using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EksiSozluk.CloneUI.Custom;
using EksiSozluk.CloneUI.Extension.Notify;
using Xamarin.Forms;

namespace EksiSozluk.CloneUI.ViewModels
{
    public class HeadlinesPageViewModel : Base
    {
        #region Encapsulation

        private IList<HeadlinesMenuBinding> menu;
        public IList<HeadlinesMenuBinding> Menu
        {
            get => menu ?? (menu = new List<HeadlinesMenuBinding>());
            set
            {
                menu = value;
                OnPropertyChanged(nameof(Menu));
            }
        }

        private IList<ListViewBinding> allData;
        public IList<ListViewBinding> AllData
        {
            get => allData ?? (allData = new ObservableCollection<ListViewBinding>());
            set
            {
                allData = value;
                OnPropertyChanged(nameof(AllData));
            }
        }

        private int carouselPosition;
        public int CarouselPosition
        {
            get => carouselPosition;
            set
            {
                carouselPosition = value; 
                OnPropertyChanged(nameof(CarouselPosition));
            }
        }

        #endregion

        private int lastSelectedHeadlineIndex = 0;
        private HorizontalScrollView horizontalScrollView;

        public HeadlinesPageViewModel(Binding horizontalScrollViewBinding)
        {
            GetBinding(horizontalScrollViewBinding);
            BindMenu();

            HeadlineSelectedCommand = new Command(OnHeadlineSelected);
            ItemSelectedCommand = new Command(OnItemSelected);
        }

        // for scrollTo process
        async void GetBinding(Binding binding)
        {
            await Task.Delay(1000);
            horizontalScrollView = binding.Source as HorizontalScrollView;
        }

        void BindMenu()
        {
            Menu.Add(new HeadlinesMenuBinding
            {
                Id = 0,
                Name = "bugün",
                IsSelected = true
            });
            Menu.Add(new HeadlinesMenuBinding
            {
                Id = 1,
                Name = "gündem",
                IsSelected = false
            });
            Menu.Add(new HeadlinesMenuBinding
            {
                Id = 2,
                Name = "takip",
                IsSelected = false
            });
            Menu.Add(new HeadlinesMenuBinding
            {
                Id = 3,
                Name = "tarihte bugün",
                IsSelected = false
            });
            Menu.Add(new HeadlinesMenuBinding
            {
                Id = 4,
                Name = "video",
                IsSelected = false
            });
            Menu.Add(new HeadlinesMenuBinding
            {
                Id = 5,
                Name = "son",
                IsSelected = false
            });
            Menu.Add(new HeadlinesMenuBinding
            {
                Id = 6,
                Name = "kenar",
                IsSelected = false
            });
            Menu.Add(new HeadlinesMenuBinding
            {
                Id = 7,
                Name = "çaylaklar",
                IsSelected = false
            });

            foreach (var unused in Menu)
                AllData.Add(new ListViewBinding());


            for (int i = 0; i < Menu.Count; i++)
            {
                var binding = new ListViewBinding
                {
                    IsFollowSide = i == 2,
                    IsTodayInHistorySide = i == 3,
                    List = new ObservableCollection<EntriesBinding>()
                };

                if(i == 0)
                    BindData(0);

                if (i == 3)
                {
                    binding.YearsList = new ObservableCollection<string>();
                    for (int j = 1999; j < 2017; j++)
                        binding.YearsList.Add(j.ToString());
                }

                AllData[i] = binding;
            }
        }

        async void BindData(int index, int delayMiliseconds = 1000)
        {
            AllData[index].IsLoading = true;
            await Task.Delay(delayMiliseconds);

            for (int i = 0; i < 25; i++)
            {
                AllData[index].List.Add(new EntriesBinding
                {
                    Title = "bir zamanlar benim de api'm vardı. " + i,
                    EntryCount = index * 10,
                });
            }

            AllData[index].IsLoading = false;
        }

        #region Commands

        public ICommand HeadlineSelectedCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }

        #endregion

        async void OnHeadlineSelected(object e)
        {
            // That's required because I need list element for scrollTo process.
            if(!(e is object[] array))
                return;

            if(!(array[0] is HeadlinesMenuBinding headline) || headline.Id == lastSelectedHeadlineIndex)
                return;

            // When menu item has been selected.
            headline.IsSelected = true;

            // Last menu will be unselected.
            Menu[lastSelectedHeadlineIndex].IsSelected = false;

            // Setting current menu id for access last selected menu item.
            lastSelectedHeadlineIndex = headline.Id;

            // Setting carousel side with menu as synchronous.
            CarouselPosition = headline.Id;

            await horizontalScrollView.ScrollToAsync(array[1] as Element, ScrollToPosition.Start, false);

            if (AllData[CarouselPosition].List.Count == 0)
                BindData(CarouselPosition);
        }

        void OnItemSelected(object e)
        {
            if(!(e is EntriesBinding entry))
                return;


        }
    }

    public class HeadlinesMenuBinding : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

    }

    public class ListViewBinding : Base
    {
        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public bool IsFollowSide { get; set; }
        public bool IsTodayInHistorySide { get; set; }
        public IList<string> YearsList { get; set; }
        public IList<EntriesBinding> List { get; set; }

        public ListViewBinding()
        {
            List = new ObservableCollection<EntriesBinding>();
        }

    }

    public class EntriesBinding
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int EntryCount { get; set; }
    }
}

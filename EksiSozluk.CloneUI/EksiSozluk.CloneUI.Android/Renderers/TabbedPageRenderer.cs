using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using EksiSozluk.CloneUI.Droid.Utils;
using EksiSozluk.CloneUI.Extension;
using EksiSozluk.CloneUI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using TabbedPageRenderer = EksiSozluk.CloneUI.Droid.Renderers.TabbedPageRenderer;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(HomePage), typeof(TabbedPageRenderer))]
namespace EksiSozluk.CloneUI.Droid.Renderers
{
    public class TabbedPageRenderer : VisualElementRenderer<HomePage>, IOnTabClickListener
    {
        bool _disposed;
        BottomNavigationBar.BottomBar _bottomBar;
        FrameLayout _frameLayout;
        Utils.IPageController _pageController;

        public TabbedPageRenderer() : base(MainActivity.ApplicationContext)
        {
            AutoPackage = false;
        }

        #region IOnTabClickListener

        public void OnTabSelected(int position)
        {
            SwitchContent(Element.Children[position]);
            var bottomBarPage = Element as HomePage;
            bottomBarPage.CurrentPage = Element.Children[position];
        }

        public void OnTabReSelected(int position)
        {
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                RemoveAllViews();

                foreach (Page pageToRemove in Element.Children)
                {
                    IVisualElementRenderer pageRenderer = Platform.GetRenderer(pageToRemove);

                    if (pageRenderer != null)
                    {
                        pageRenderer.ViewGroup?.RemoveFromParent();
                        pageRenderer.Dispose();
                    }

                    // pageToRemove.ClearValue (Platform.RendererProperty);
                }

                if (_bottomBar != null)
                {
                    _bottomBar.SetOnTabClickListener(null);
                    _bottomBar.Dispose();
                    _bottomBar = null;
                }

                if (_frameLayout != null)
                {
                    _frameLayout.Dispose();
                    _frameLayout = null;
                }

                /*if (Element != null) {
					PageController.InternalChildren.CollectionChanged -= OnChildrenCollectionChanged;
				}*/
            }

            base.Dispose(disposing);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            _pageController.SendAppearing();
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            _pageController.SendDisappearing();
        }


        protected override void OnElementChanged(ElementChangedEventArgs<HomePage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                HomePage bottomBarPage = e.NewElement;

                if (_bottomBar == null)
                {
                    _pageController = PageController.Create(bottomBarPage);

                    _frameLayout = new FrameLayout(MainActivity.ApplicationContext)
                    {
                        LayoutParameters = new FrameLayout.LayoutParams(LayoutParams.MatchParent,
                            LayoutParams.MatchParent, GravityFlags.Fill)
                    };
                    AddView(_frameLayout, 0);

                    _bottomBar = BottomNavigationBar.BottomBar.Attach(_frameLayout, null);
                    _bottomBar.NoTopOffset();
                    _bottomBar.NoTabletGoodness();

                    if (bottomBarPage.FixedMode)
                        _bottomBar.UseFixedMode();

                    switch (bottomBarPage.BarTheme)
                    {
                        case BarThemeTypes.Light:
                            break;
                        case BarThemeTypes.DarkWithAlpha:
                            _bottomBar.UseDarkThemeWithAlpha(true);
                            break;
                        case BarThemeTypes.DarkWithoutAlpha:
                            _bottomBar.UseDarkThemeWithAlpha(false);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    _bottomBar.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                    _bottomBar.SetOnTabClickListener(this);

                    UpdateTabs();
                    UpdateBarBackgroundColor();
                    UpdateBarTextColor();
                }

                if (bottomBarPage.CurrentPage != null)
                {
                    SwitchContent(bottomBarPage.CurrentPage);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(TabbedPage.CurrentPage))
            {
                SwitchContent(Element.CurrentPage);
            }
            else if (e.PropertyName == NavigationPage.BarBackgroundColorProperty.PropertyName)
            {
                UpdateBarBackgroundColor();
            }
            else if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName)
            {
                UpdateBarTextColor();
            }
        }

        protected virtual void SwitchContent(Page view)
        {
            Context.HideKeyboard(this);

            _frameLayout.RemoveAllViews();

            if (view == null)
            {
                return;
            }

            if (Platform.GetRenderer(view) == null)
            {
                Platform.SetRenderer(view, Platform.CreateRenderer(view));
            }

            _frameLayout.AddView(Platform.GetRenderer(view).ViewGroup);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            int width = r - l;
            int height = b - t;

            var context = Context;

            _bottomBar.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpecFactory.MakeMeasureSpec(height, MeasureSpecMode.AtMost));
            int tabsHeight = Math.Min(height, Math.Max(_bottomBar.MeasuredHeight, _bottomBar.MinimumHeight));

            if (width > 0 && height > 0)
            {
                _pageController.ContainerArea = new Rectangle(0, 0, context.FromPixels(width),
                    context.FromPixels(_frameLayout.Height));
                ObservableCollection<Element> internalChildren = _pageController.InternalChildren;

                for (var i = 0; i < internalChildren.Count; i++)
                {
                    if (!(internalChildren[i] is VisualElement child))
                    {
                        continue;
                    }

                    IVisualElementRenderer renderer = Platform.GetRenderer(child);
                    if (renderer is NavigationPageRenderer navigationRenderer)
                    {
                        // navigationRenderer.ContainerPadding = tabsHeight;
                    }
                }

                _bottomBar.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                    MeasureSpecFactory.MakeMeasureSpec(tabsHeight, MeasureSpecMode.Exactly));
                _bottomBar.Layout(0, 0, width, tabsHeight);
            }

            base.OnLayout(changed, l, t, r, b);
        }

        void UpdateBarBackgroundColor()
        {
            if (_disposed || _bottomBar == null)
            {
                return;
            }

            _bottomBar.SetBackgroundColor(Element.BarBackgroundColor.ToAndroid());
        }

        void UpdateBarTextColor()
        {
            if (_disposed || _bottomBar == null)
            {
                return;
            }

            _bottomBar.SetActiveTabColor(Element.BarTextColor.ToAndroid());
        }

        void UpdateTabs()
        {
            SetTabItems();
            SetTabColors();

            View view = _bottomBar.Parent as View;
            ViewGroup mItemContainer = (ViewGroup) view?.FindViewById(BottomNavigationBar.Resource.Id.bb_bottom_bar_item_container);
            for (int i = 0; i < mItemContainer?.ChildCount; i++)
            {
                View viewItem = mItemContainer.GetChildAt(i);
                viewItem.SetBackgroundColor(Color.FromHex("#EEEEEE").ToAndroid());

                if (viewItem.FindViewById(BottomNavigationBar.Resource.Id.bb_bottom_bar_title) is TextView titleTab)
                    titleTab.Visibility = ViewStates.Gone;

                var icon = viewItem.FindViewById(BottomNavigationBar.Resource.Id.bb_bottom_bar_icon);
                icon.SetY(25);
            }
        }

        void SetTabItems()
        {
            BottomBarTab[] tabs = Element.Children.Select(page =>
            {
                var tabIconId = ResourceManagerEx.IdFromTitle(page.Icon, ResourceManager.DrawableClass);
                return new BottomBarTab(tabIconId, null);
            }).ToArray();


            _bottomBar.SetItems(tabs);
            _bottomBar.MakeBadgeForTabAt(3, Element.BarTextColor.ToAndroid(), 1);
        }

        void SetTabColors()
        {
            for (int i = 0; i < Element.Children.Count; ++i)
            {
                Page page = Element.Children[i];
                Color? tabColor = page.GetTabColor() ?? Color.Black;
                _bottomBar.MapColorForTab(i, tabColor.Value.ToAndroid());
            }
        }
    }
}
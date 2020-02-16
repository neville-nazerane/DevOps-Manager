using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevOpsManager.MobileApp.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteTab : Grid
    {

        public const string AllKey = "all";
        public const string TabKey = "tab";

        public static readonly BindableProperty IsFavoriteSelectedProperty = BindableProperty.Create(nameof(IsFavoriteSelected),
                                                                                                     typeof(bool),
                                                                                                     typeof(FavoriteTab),
                                                                                                     propertyChanged: IsFavoriteSelectedChanged);
        private bool _animatingSlide;

        private Style UnselectedStyle => Resources["unselected"] as Style;

        private Style SelectedStyle => Resources["selected"] as Style;

        public bool IsFavoriteSelected
        {
            get => (bool)GetValue(IsFavoriteSelectedProperty); 
            set
            {
                SetValue(IsFavoriteSelectedProperty, value);
                UpdateUI();
            }
        }

        public FavoriteTab()
        {
            _animatingSlide = false;
            InitializeComponent();
        }

        private void UpdateUI()
        {
            if (IsFavoriteSelected)
            {
                allTab.Style = UnselectedStyle;
                favTab.Style = SelectedStyle;
            }
            else
            {
                favTab.Style = UnselectedStyle;
                allTab.Style = SelectedStyle;
            }
        }

        private async Task ChangeAsync(bool selected)
        {
            IsFavoriteSelected = selected;
            _animatingSlide = true;
            if (selected) // first tab
                await slide.TranslateTo(slide.Width, slide.Y);
            else // second tab
                await slide.TranslateTo(0, slide.Y);
            UpdateUI();
            _animatingSlide = false;
        }

        private void Slider_SizeChanged(object sender, EventArgs e)
        {
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            slide.WidthRequest = slider.Width / 2;
        }

        private static void IsFavoriteSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((FavoriteTab)bindable).IsFavoriteSelected = (bool)newValue;
        }

        private async void FavTapped(object sender, EventArgs e)
        {
            if (_animatingSlide) return;
            await ChangeAsync(true);
        }

        private async void AllTapped(object sender, EventArgs e)
        {
            if (_animatingSlide) return;
            await ChangeAsync(false);
        }

        private async void Swapped(object sender, EventArgs e)
        {
            if (_animatingSlide) return;
            await ChangeAsync(!IsFavoriteSelected);
        }

    }
}
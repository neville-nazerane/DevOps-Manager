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
        private readonly TapGestureRecognizer _allTap;
        private readonly TapGestureRecognizer _favTap;

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
            _allTap = MakeRecognizer(false);
            _favTap = MakeRecognizer(true);
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

        private TapGestureRecognizer MakeRecognizer(bool selected)
        {
            return new TapGestureRecognizer
            {
                Command = new Command(async () => await ChangeAsync(selected), () => !_animatingSlide)
            };
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

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent is null)
            {
                allTab.GestureRecognizers.Remove(_allTap);
                favTab.GestureRecognizers.Remove(_favTap);
                slider.SizeChanged -= Slider_SizeChanged;
            }
            else
            {
                allTab.GestureRecognizers.Add(_allTap);
                favTab.GestureRecognizers.Add(_favTap);
                slider.SizeChanged += Slider_SizeChanged;
            }
        }

        private void Slider_SizeChanged(object sender, EventArgs e)
        {
            slide.WidthRequest = slider.Width / 2;
        }

        private static void IsFavoriteSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((FavoriteTab)bindable).IsFavoriteSelected = (bool)newValue;
        }

    }
}
using DevOpsManager.MobileApp.Helpers;
using DevOpsManager.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.Components
{
    class StarredView : Image
    {

        public static readonly BindableProperty IsStarredProperty = BindableProperty.Create(nameof(IsStarred), typeof(bool), typeof(StarredView), propertyChanged: OnStarChanged, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty IdentifierProperty = BindableProperty.Create(nameof(Identifier), typeof(string), typeof(StarredView));

        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(Command<StarredContext>), typeof(StarredView));

        private readonly TapGestureRecognizer _tapGesture;

        public bool IsStarred
        {
            get => (bool) GetValue(IsStarredProperty); 
            set
            {
                SetValue(IsStarredProperty, value);
                if (value) Source = "star_on.png".GetSharedImage();
                else Source = "star_off.png".GetSharedImage();
            }
        }

        public string Identifier { get => (string)GetValue(IdentifierProperty); set => SetValue(IdentifierProperty, value); }

        public Command<StarredContext> TappedCommand { get => (Command<StarredContext>)GetValue(TappedCommandProperty); set => SetValue(TappedCommandProperty, value);   }


        public StarredView()
        {
            _tapGesture = new TapGestureRecognizer {
                Command = new Command(OnTapped)
            };
            IsStarred = false;
        }

        private void InternalSetStarred(bool isStarred)
        {
            if (isStarred) Source = "star_on.png".GetSharedImage();
            else Source = "star_off.png".GetSharedImage();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent is null)
            {
                GestureRecognizers.Remove(_tapGesture);
            }
            else
            {
                GestureRecognizers.Add(_tapGesture);
            }
        }

        private void OnTapped()
        {
            IsStarred = !IsStarred;
            var context = new StarredContext { 
                Identifier = Identifier,
                IsStarred = IsStarred
            };
            if (TappedCommand?.CanExecute(context) == true)
            {
                TappedCommand.Execute(context);
            }
        }

        private static void OnStarChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((StarredView)bindable).IsStarred = (bool)newValue;
        }

    }
}

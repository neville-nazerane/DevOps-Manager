using DevOpsManager.MobileApp.Components.Models;
using DevOpsManager.MobileApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.Components
{
    class StarredView : Image
    {

        public static readonly BindableProperty IsStarredProperty = BindableProperty.Create(nameof(IsStarred), typeof(bool), typeof(StarredView), propertyChanged: OnStarChanged);

        public static readonly BindableProperty IdentifierProperty = BindableProperty.Create(nameof(Identifier), typeof(string), typeof(StarredView));

        public static readonly BindableProperty SwitchedCommandProperty = BindableProperty.Create(nameof(SwitchedCommand), typeof(Command<StarredContext>), typeof(StarredView));

        private readonly TapGestureRecognizer _tapGesture;
        private bool _isStarred;
        public bool IsStarred
        {
            get => _isStarred; 
            set
            {
                _isStarred = value;
                if (value) Source = "star_on.png".GetSharedImage();
                else Source = "star_off.png".GetSharedImage();
            }
        }

        public string Identifier { get => (string)GetValue(IdentifierProperty); set => SetValue(IdentifierProperty, value); }

        public Command<StarredContext> SwitchedCommand { get => (Command<StarredContext>)GetValue(SwitchedCommandProperty); set => SetValue(SwitchedCommandProperty, value);   }


        public StarredView()
        {
            _tapGesture = new TapGestureRecognizer {
                Command = new Command(OnTapped)
            };
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
            var context = new StarredContext { 
                Identifier = Identifier,
                IsStarred = IsStarred
            };
            if (SwitchedCommand?.CanExecute(context) == true)
            {
                SwitchedCommand.Execute(context);
            }
        }

        private static void OnStarChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((StarredView)bindable).IsStarred = (bool)newValue;
        }

    }
}

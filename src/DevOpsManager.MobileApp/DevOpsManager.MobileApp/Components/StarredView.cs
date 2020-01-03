using DevOpsManager.MobileApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.Components
{
    class StarredView : Image
    {

        public static readonly BindableProperty IsStarredProperty = BindableProperty.Create(nameof(IsStarred), typeof(bool), typeof(ScrollView), propertyChanged: OnStarChanged);
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

        private static void OnStarChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((StarredView)bindable).IsStarred = (bool)newValue;
        }

    }
}

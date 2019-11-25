using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    interface IPageControl<TViewModel>
    {
        Page Page { get; }
        TViewModel ViewModel { get; }
    }
}

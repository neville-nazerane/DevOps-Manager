using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    public static class InjectionControl
    {

        internal static IServiceProvider _provider;
        internal static IServiceCollection _services;

        internal static Action<Page> _navigationAction;
        internal static Func<Page, Task> _asyncNavigationFunc;

        public static T Resolve<T>() => _provider.GetService<T>();

        public static Page ResolvePage<TViewModel>()
        {
            var scope = _provider.CreateScope();
            var provider = scope.ServiceProvider;
            var control = provider.GetService<IPageControl<TViewModel>>();
            return control.Page;
        }

        public static Page Navigate<TViewModel>()
        {
            Page page = ResolvePage<TViewModel>();
            _navigationAction?.Invoke(page);
            return page;
        }

        public static async Task<Page> NavigateAsync<TViewModel>()
        {
            Page page = ResolvePage<TViewModel>();
            if (_asyncNavigationFunc != null)
                await _asyncNavigationFunc(page);
            return page;
        }

    }
}

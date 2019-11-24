using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.FluentInjector
{
    public class InjectionBuilder
    {
        private readonly Application _app;
        private readonly ServiceCollection _services;
        private Assembly _pageAssembly;
        private Assembly _viewModelAssembly;
        private IServiceProvider _provider;

        internal InjectionBuilder(Application app)
        {
            _app = app;
            _services = new ServiceCollection();
            _pageAssembly = _app.GetType().Assembly;
            _viewModelAssembly = _app.GetType().Assembly;
        }

        public InjectionBuilder(object app)
        {
            _services = new ServiceCollection();
            _pageAssembly = app.GetType().Assembly;
            _viewModelAssembly = app.GetType().Assembly;
        }

        #region adding services

        public InjectionBuilder AddSingleton<TService>(TService service)
            where TService : class
        {
            _services.AddSingleton(service);
            return this;
        }

        public InjectionBuilder AddSingleton<TService>()
            where TService : class
        {
            _services.AddSingleton<TService>();
            return this;
        }

        public InjectionBuilder AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>();
            return this;
        }

        public InjectionBuilder AddTransient<TService>()
            where TService : class
        {
            _services.AddTransient<TService>();
            return this;
        }

        public InjectionBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddTransient<TService, TImplementation>();
            return this;
        }

    //    public InjectionBuilder AddScoped<TService>()
    //where TService : class
    //    {
    //        _services.AddScoped<TService>();
    //        return this;
    //    }

    //    public InjectionBuilder AddScoped<TService, TImplementation>()
    //        where TService : class
    //        where TImplementation : class, TService
    //    {
    //        _services.AddScoped<TService, TImplementation>();
    //        return this;
    //    } 
        #endregion

        public InjectionBuilder SetPageAssembly(Assembly assembly)
        {
            _pageAssembly = assembly;
            return this;
        }
        public InjectionBuilder SetViewModelAssembly(Assembly assembly)
        {
            _viewModelAssembly = assembly;
            return this;
        }

        public string Build()
        {

            var types = _pageAssembly.GetTypes();
            var filtered = types.Where(t => typeof(Page).IsAssignableFrom(t));
            // fetching pages
            foreach (var page in _pageAssembly.GetTypes().Where(t => typeof(Page).IsAssignableFrom(t)))
            {
                _services.AddScoped(page);
            }


            // fetching view models
            var mods = _viewModelAssembly.GetTypes();
            int allCount = mods.Count();
           var viewModels = _viewModelAssembly.GetTypes()
                                   .Where(t => t.Name.EndsWith("viewmodel", StringComparison.InvariantCultureIgnoreCase)
                                               || t.Name.EndsWith("pagemodel", StringComparison.InvariantCultureIgnoreCase));
            int count = viewModels.Count();
            foreach (var vm in viewModels)
            {

            }
            _provider = _services.BuildServiceProvider();
            return $"All were {allCount} but found {count} view models which are: {string.Join(",", viewModels.Select(v => v.Name))}";
        }

    }
}

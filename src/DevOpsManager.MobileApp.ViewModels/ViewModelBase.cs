using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FluentInjector;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.ViewModels
{
    public class ViewModelBase : InjectorViewModelBase
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading; 
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public Command BuildCommand(Func<Task> func)
        {
            return new Command(async () =>
            {
                IsBusy = true;
                await func();
                IsBusy = false;
            });
        }

        public Command<T> BuildCommand<T>(Func<T, Task> func)
        {
            return new Command<T>(async obj =>
            {
                IsLoading = true;
                IsBusy = true;
                await func(obj);
                IsLoading = false;
                IsBusy = false;
            });
        }

        public override async Task<Page> NavigateAsync<TViewModel>()
        {
            var vm = await base.NavigateAsync<TViewModel>();
            if (vm.BindingContext is ViewModelBase viewModel)
            {
                viewModel.IsLoading = true;
                await viewModel.InitAsync();
                viewModel.IsLoading = false;
            }

            return vm;
        }

        public virtual Task InitAsync() => Task.CompletedTask;

    }
}

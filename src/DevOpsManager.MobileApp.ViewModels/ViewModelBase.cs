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

        public Command BuildCommand(Func<Task> func)
        {
            return new Command(async () => {
                IsBusy = true;
                await func();
                IsBusy = false;
            });
        }

        public Command<T> BuildCommand<T>(Func<T, Task> func)
        {
            return new Command<T>(async obj => {
                IsBusy = true;
                await func(obj);
                IsBusy = false;
            });
        }

        public override async Task<Page> NavigateAsync<TViewModel>()
        {
            var vm = await base.NavigateAsync<TViewModel>();
            if (vm.BindingContext is ViewModelBase viewModel) await viewModel.InitAsync();
            return vm;
        }

        public virtual Task InitAsync() => Task.CompletedTask;

    }
}

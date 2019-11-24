using DevOpsManager.MobileApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.FluentInjector;

namespace Simple
{
    class Program
    {
        static void Main(string[] args)
        {

            new InjectionBuilder(1)
                    .SetViewModelAssembly(typeof(MonViewModel).Assembly);
            var s = new Simpler();

            s.SetAssembly(typeof(MonViewModel).Assembly);

            Console.WriteLine("Hello World!");
        }
    }
}

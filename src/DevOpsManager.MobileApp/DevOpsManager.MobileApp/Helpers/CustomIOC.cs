using FreshMvvm;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DevOpsManager.MobileApp.Helpers
{
    class CustomIOC : IFreshIOC
    {

        private readonly ServiceCollection _services;

        public CustomIOC()
        {
            _services = new ServiceCollection();
        }

        public IRegisterOptions Register<RegisterType>(RegisterType instance) where RegisterType : class
        {
            throw new NotImplementedException();
        }

        public IRegisterOptions Register<RegisterType>(RegisterType instance, string name) where RegisterType : class
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type resolveType)
        {
            throw new NotImplementedException();
        }

        public ResolveType Resolve<ResolveType>() where ResolveType : class
        {
            return null;
        }

        public ResolveType Resolve<ResolveType>(string name) where ResolveType : class
        {
            throw new NotImplementedException();
        }

        public void Unregister<RegisterType>()
        {
            throw new NotImplementedException();
        }

        public void Unregister<RegisterType>(string name)
        {
            throw new NotImplementedException();
        }

        IRegisterOptions IFreshIOC.Register<RegisterType, RegisterImplementation>()
        {
            throw new NotImplementedException();
        }

        class RegisterOptions : IRegisterOptions
        {
            public IRegisterOptions AsMultiInstance()
            {
                throw new NotImplementedException();
            }

            public IRegisterOptions AsSingleton()
            {
                throw new NotImplementedException();
            }

            public IRegisterOptions UsingConstructor<RegisterType>(Expression<Func<RegisterType>> constructor)
            {
                throw new NotImplementedException();
            }

            public IRegisterOptions WithStrongReference()
            {
                throw new NotImplementedException();
            }

            public IRegisterOptions WithWeakReference()
            {
                throw new NotImplementedException();
            }
        }

    }
}

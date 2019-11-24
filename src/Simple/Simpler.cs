using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Simple
{
    class Simpler
    {

        private Assembly _assembly;

        public Simpler() => _assembly = GetType().Assembly;

        public void SetAssembly(Assembly value)
        {
            _assembly = value;
        }
    }
}

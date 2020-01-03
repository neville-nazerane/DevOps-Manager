using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.Helpers
{
    static class ImageExtensions
    {

        static Assembly currentAssembly = typeof(ImageExtensions).Assembly;


        public static ImageSource GetSharedImage(this string Source) 
            => ImageSource.FromResource($"DevOpsManager.MobileApp.Images.{Source}", currentAssembly);

    }
}

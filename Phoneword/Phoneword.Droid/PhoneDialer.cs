using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Phoneword.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(PhoneDialer))]

namespace Phoneword.Droid
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            throw new NotImplementedException();
        }
    }
}
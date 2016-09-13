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
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PhoneDialer))]

namespace Phoneword.Droid
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            Context context = Forms.Context;
            if (context == null)
                return false;

            try
            {
                Intent phone = new Intent(Intent.ActionCall,
                              Android.Net.Uri.Parse(string.Format("tel:{0}", number)));
                context.StartActivity(phone);

                return true;

            } catch (Exception e)
            {
                throw e;
            }
        }
    }
}
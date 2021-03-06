﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Phoneword
{
    public class App : Application
    {
        public static List<string> PhoneNumbers { get; set; }

        public App()
        {
            PhoneNumbers = new List<string>();

            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

﻿using System;
using Planner.Data;
using Planner.Model;
using Planner.ViewModels;
using Xamarin.Forms;
using Planner.Views;

namespace Planner
{
    public class App : Application
    {
        public App()
        {
            Initialize();
            //App.Database.DeleteAllPlan();
            MainPage = GetMainPage();
        }

        private void Initialize()
        {
            RegisterTypes();
        }

        static void RegisterTypes()
        {
            // This can be replaced by any number of MVVM tools. It is done this way merely because this 
            // is not intended to be a demo of those tools.
            ViewFactory.Register<MainPage, MainPageViewModel>();
            ViewFactory.Register<PlanPage, PlanViewModel>();
            ViewFactory.Register<WeeklyPlanPage, WeeklyPlanViewModel>();
            ViewFactory.Register<DailyPlanPage, DailyPlanViewModel>();
        }

        public static Page GetMainPage()
        {
            RegisterTypes();
            var mainNav = new NavigationPage(ViewFactory.CreatePage<MainPageViewModel>());
            //mainNav.BarBackgroundColor = Color.FromHex("0863BB");
            //mainNav.BackgroundColor = Color.FromHex("1976d2");

            MessagingCenter.Subscribe<MainPageViewModel, Plan>(mainNav, "WeeklyPlanSelected", (sender, model) => {
                var planvm = new PlanViewModel(model);
                mainNav.Navigation.PushAsync(ViewFactory.CreatePage(planvm));
            });

            return mainNav;
        }

        static PlanDatabase database;
        public static PlanDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PlanDatabase();
                }
                return database;
            }
        }
    }
}

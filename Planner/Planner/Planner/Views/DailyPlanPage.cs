﻿using Planner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planner.Views
{
    public class DailyPlanPage : ContentPage
    {
        public DailyPlanPage()
        {
            Padding = new Thickness(10);

            Title = " Default title";
            this.SetBinding(TitleProperty, "MainText");

            NavigationPage.SetHasNavigationBar(this, true);

            var DailyPlans = new ListView();
            DailyPlans.RowHeight = 40;
            //DailyPlans.SetBinding(ListView.ItemsSourceProperty, "Plans");
            DailyPlans.SetBinding(ListView.ItemsSourceProperty, "PlansGrouped");
            DailyPlans.HasUnevenRows = true;
            DailyPlans.GroupShortNameBinding = new Binding("Key");
            DailyPlans.IsGroupingEnabled = true;
            DailyPlans.GroupDisplayBinding = new Binding("Key");
            DailyPlans.SetBinding(ListView.SelectedItemProperty, new Binding("SelectedPlan", BindingMode.TwoWay));
            DailyPlans.ItemTemplate = new DataTemplate(typeof(DailyPlanCell));

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                //BackgroundColor = Color.Aqua,
                Children = { DailyPlans }
            };


            var toolBarItem = new ToolbarItem("New plan", "add", () => {
                var tool = new Plan(PlanEnumeration.PlanType.Plan);
                MessagingCenter.Send(this, "CreatePlan", tool);
            }, 0, 0);
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                toolBarItem = new ToolbarItem("New plan", "add", () => {
                    var tool = new Plan(PlanEnumeration.PlanType.Plan);
                    MessagingCenter.Send(this, "CreatePlan", tool);
                }, 0, 0);
            }
            ToolbarItems.Add(toolBarItem);
        }
    }
}

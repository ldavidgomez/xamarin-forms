using Planner.Model;
using Planner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Planner.Views
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Padding = new Thickness(10);

            Title = " Default title";
            this.SetBinding(TitleProperty, "MainText");

            NavigationPage.SetHasNavigationBar(this, true);

            var lastUpdateWeeklyPlansLV = new ListView();
            lastUpdateWeeklyPlansLV.RowHeight = 40;
            lastUpdateWeeklyPlansLV.SetBinding(ListView.ItemsSourceProperty, "LastUpdateWeeklyPlans");
            lastUpdateWeeklyPlansLV.SetBinding(ListView.SelectedItemProperty, new Binding("SelectedWeeklyPlan", BindingMode.TwoWay));
            lastUpdateWeeklyPlansLV.ItemTemplate = new DataTemplate(typeof(WeeklyPlanCell));

            var deleteAllButton = new Button { Text = "Delete database" };
            deleteAllButton.SetBinding(Button.CommandProperty, "DeleteDatabaseCommand");

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { lastUpdateWeeklyPlansLV, deleteAllButton }
            };

            var toolBarItem = new ToolbarItem("+", null, () => {
                var tool = new Plan(PlanEnumeration.PlanType.Weekly);
                MessagingCenter.Send(this, "WeeklyPlanCreate", tool);
            }, 0, 0);
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                toolBarItem = new ToolbarItem("+", "plus", () => {
                    var tool = new Plan(PlanEnumeration.PlanType.Weekly);
                    MessagingCenter.Send(this, "WeeklyPlanCreate", tool);
                }, 0, 0);
            }
            ToolbarItems.Add(toolBarItem);

        }
    }
}

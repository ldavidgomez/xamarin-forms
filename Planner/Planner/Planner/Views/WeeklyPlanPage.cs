using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planner.Utilities;
using Xamarin.Forms;

namespace Planner.Views
{
    public class WeeklyPlanPage : ContentPage
    {
        public WeeklyPlanPage()
        {
            Padding = new Thickness(10);

            Title = " Default title";
            this.SetBinding(TitleProperty, "MainText");

            NavigationPage.SetHasNavigationBar(this, true);

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;

            DatePicker datePicker = new DatePicker();
            datePicker.SetBinding(DatePicker.MinimumDateProperty, "MinimunDate");
            datePicker.SetBinding(DatePicker.DateProperty, "Date");

            var createWeeklyPlanButton = new Button { Text = "Create" };
            createWeeklyPlanButton.SetBinding(Button.CommandProperty, "AddPlanCommand");


            var DailyPlans = new ListView();
            DailyPlans.RowHeight = 40;
            DailyPlans.SetBinding(ListView.ItemsSourceProperty, "DailyPlans");
            DailyPlans.SetBinding(ListView.SelectedItemProperty, new Binding("SelectedDailyPlan", BindingMode.TwoWay));
            DailyPlans.ItemTemplate = new DataTemplate(typeof(DailyPlanCell));

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { datePicker, createWeeklyPlanButton, DailyPlans }
            };
        }
    }
}

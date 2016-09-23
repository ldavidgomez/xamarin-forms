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
            datePicker.SetBinding(DatePicker.MinimumDateProperty, "minimunDate");
            datePicker.SetBinding(DatePicker.DateProperty, "date");

            var speakButton = new Button { Text = "Create" };
            speakButton.SetBinding(Button.CommandProperty, "AddPlanCommand");


            var lastUpdateWeeklyPlansLV = new ListView();
            lastUpdateWeeklyPlansLV.RowHeight = 40;
            lastUpdateWeeklyPlansLV.SetBinding(ListView.ItemsSourceProperty, "WeeklyPlans");
            lastUpdateWeeklyPlansLV.SetBinding(ListView.SelectedItemProperty, new Binding("SelectedDayPlan", BindingMode.TwoWay));
            lastUpdateWeeklyPlansLV.ItemTemplate = new DataTemplate(typeof(DailyPlanCell));

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { datePicker, speakButton, lastUpdateWeeklyPlansLV }
            };
        }
    }
}

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

            Title = "Weekly Plan!";
            this.SetBinding(TitleProperty, "MainText");

            NavigationPage.SetHasNavigationBar(this, true);

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;

            Image dateImage = new Image
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.End,
                Source = FileImageSource.FromFile("calendar"),
                HeightRequest = 30,
                Margin = new Thickness(10, 0),  
                //BackgroundColor = Color.Blue,
            };


            DatePicker datePicker = new DatePicker()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                //BackgroundColor = Color.Navy,
            };
            datePicker.SetBinding(DatePicker.MinimumDateProperty, "MinimunDate");
            datePicker.SetBinding(DatePicker.DateProperty, "Date");

            var dateContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { datePicker, dateImage, }
            };

            var DailyPlans = new ListView();
            DailyPlans.VerticalOptions = LayoutOptions.StartAndExpand;
            //DailyPlans.SetBinding(ListView.ItemsSourceProperty, "DailyPlans");
            DailyPlans.SetBinding(ListView.ItemsSourceProperty, "DailyPlansGrouped");
            DailyPlans.HasUnevenRows = true;
            DailyPlans.GroupShortNameBinding = new Binding("Key");
            DailyPlans.IsGroupingEnabled = true;
            //DailyPlans.GroupDisplayBinding = new Binding("Key");
            DailyPlans.SetBinding(ListView.SelectedItemProperty, new Binding("SelectedDailyPlan", BindingMode.TwoWay));
            DailyPlans.ItemTemplate = new DataTemplate(typeof(PlanCell));

            //DailyPlans.BackgroundColor = Color.Olive;
            

            if (Device.OS != TargetPlatform.WinPhone)
                DailyPlans.GroupHeaderTemplate = new DataTemplate(typeof(HeaderCell));


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation =StackOrientation.Vertical,
                Padding = 5,
                //BackgroundColor = Color.Green,
                Children = { dateContainer, DailyPlans }
            };
        }
    }
}

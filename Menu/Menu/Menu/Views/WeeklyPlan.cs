using Menu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Menu
{
    public class WeeklyPlan : StackLayout
    {
        ListView listView;

        public WeeklyPlan(DateTime week)
        {
            Label lastUpdateLabel = new Label
            {
                Text = "Last updates",
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            listView.ItemsSource = App.Database.GetWeeklyPlan(week);
            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "description");

            //listView.ItemSelected += (sender, e) => {
            //    var session = e.SelectedItem;
            //    var sessionPage = new SessionPage();
            //    //todoPage.BindingContext = todoItem;
            //    Navigation.PushAsync(sessionPage);
            //};

            HorizontalOptions = LayoutOptions.FillAndExpand;
            Orientation = StackOrientation.Vertical;
            Spacing = 15;
            HeightRequest = 200;
            Children.Add(lastUpdateLabel);
            Children.Add(listView);
        }
    }
}

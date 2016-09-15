using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Menu
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.Title = "Planner";
            this.Padding = new Thickness(20, Device.OnPlatform(40, 20, 20), 20, 20);

            StackLayout panel = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15
            };

            Label header = new Label
            {
                Text = "Last updates",
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            panel.Children.Add(header);

            StackLayout lastUpdatePanel = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15,
                HeightRequest = 200
            };

            var tableItems = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                tableItems.Add(i + " row");
            }

            ListView lastUptdates = new ListView
            {
                SeparatorVisibility = SeparatorVisibility.Default,
                SeparatorColor = Color.White,
                HeightRequest = 200,
                ItemsSource = tableItems
            };

            lastUpdatePanel.Children.Add(lastUptdates);
            panel.Children.Add(lastUpdatePanel);

            //Button button1 = new Button
            //{
            //    Text = " New Menu ",
            //    BorderWidth = 1
            //};
            //button1.Clicked += async (sender, args) =>
            //    await Navigation.PushAsync(new CreateMenuPage());

            //panel.Children.Add(button1);

            //Button button2 = new Button
            //{
            //    Text = " Menu History ",
            //    BorderWidth = 1
            //};
            //button2.Clicked += async (sender, args) =>
            //    await Navigation.PushAsync(new HistoryMenuPage());

            //panel.Children.Add(button2);

            // Build the page.
            this.Content = panel;
        }
    }
}

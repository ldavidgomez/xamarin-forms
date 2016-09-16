using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Menu
{    
    public class WeeklyPlanPage : ContentPage
    {
        public WeeklyPlanPage()
            : this(DateTime.Now)
        { }

        public WeeklyPlanPage(DateTime date)
        {
            this.Title = "New Weekly Plan";

            Label header = new Label
            {
                Text = "Choose week",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;

            DatePicker dataPicker = new DatePicker {
                MinimumDate = Utilities.StartOfWeek(date, dfi.FirstDayOfWeek),
                Date = date         
            };

            Label label = new Label
            {
                Text = "Create menu",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Build the page.
            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Children =
                {
                    header,
                    dataPicker,
                    label
                    
                }
            };
        }
    }
}

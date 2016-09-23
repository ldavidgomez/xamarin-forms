using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planner.Views
{
    public class DailyPlanCell : ViewCell
    {
        public DailyPlanCell()
        {
            var startDateLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            startDateLabel.SetBinding(Label.TextProperty, "description");

            var categoryLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            categoryLabel.SetBinding(Label.TextProperty, "category");

            var endDateLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            endDateLabel.SetBinding(Label.TextProperty, "startDate");

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { startDateLabel, categoryLabel, endDateLabel }
            };
            View = layout;
        }
    }
}

using System;
using Xamarin.Forms;

namespace Planner.Views
{
    public class WeeklyPlanCell : ViewCell
    {
        public WeeklyPlanCell()
        {
            var startDateLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            startDateLabel.SetBinding(Label.TextProperty, "startDate");

            var endDateLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            endDateLabel.SetBinding(Label.TextProperty, "endDate");

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { startDateLabel, endDateLabel }
            };
            View = layout;
        }

        protected override void OnBindingContextChanged()
        {
            // Fixme : this is happening because the View.Parent is getting 
            // set after the Cell gets the binding context set on it. Then it is inheriting
            // the parents binding context.
            View.BindingContext = BindingContext;
            base.OnBindingContextChanged();
        }
    }
}


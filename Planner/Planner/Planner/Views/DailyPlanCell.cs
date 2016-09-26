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
            var descriptionLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            descriptionLabel.SetBinding(Label.TextProperty, "Description");

            var categoryLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            categoryLabel.SetBinding(Label.TextProperty, "Category");

            var startDateLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            startDateLabel.SetBinding(Label.TextProperty, "StartDate");

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { descriptionLabel, categoryLabel, startDateLabel }
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

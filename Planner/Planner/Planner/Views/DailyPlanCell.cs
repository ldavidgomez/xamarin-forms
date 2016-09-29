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
            var categoryLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            categoryLabel.SetBinding(Label.TextProperty, "Category");

            var descriptionLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = 40,
                Margin = new Thickness(20, 0, 0, 0)
            };
            descriptionLabel.SetBinding(Label.TextProperty, "Description");

            //var Plans = new ListView();
            //Plans.RowHeight = 40;
            //Plans.SetBinding(ListView.ItemsSourceProperty, "Plans");
            //Plans.SetBinding(ListView.SelectedItemProperty, new Binding("SelectedPlan", BindingMode.TwoWay));
            //Plans.ItemTemplate = new DataTemplate(typeof(PlanCell));

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { descriptionLabel, categoryLabel }
                //Children = { descriptionLabel, categoryLabel, startDateLabel }
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

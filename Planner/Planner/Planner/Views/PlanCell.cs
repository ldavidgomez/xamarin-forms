﻿using System;
using Xamarin.Forms;

namespace Planner.Views
{
    public class PlanCell : ViewCell
    {
        public PlanCell()
        {
            var descriptionLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            descriptionLabel.SetBinding(Label.TextProperty, "description");

            var dateLabel = new Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            dateLabel.SetBinding(Label.TextProperty, "date");

            //var tick = new Image
            //{
            //    Source = FileImageSource.FromFile("check"),
            //};
            //tick.SetBinding(Image.IsVisibleProperty, "Done");

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { descriptionLabel, dateLabel }
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

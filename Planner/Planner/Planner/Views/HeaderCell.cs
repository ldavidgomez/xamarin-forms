using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planner.Views
{
    public class HeaderCell : ViewCell
    {
        public HeaderCell()
        {
            this.Height = 35;
            var title = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, this),
                FontAttributes = FontAttributes.Bold,
                //TextColor = Color.White,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            title.SetBinding(Label.TextProperty, "Key");

            //var plus = new Label
            //{
            //    FontSize = Device.GetNamedSize(NamedSize.Large, this),
            //    FontAttributes = FontAttributes.Bold,
            //    TextColor = Color.White,
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    HorizontalOptions = LayoutOptions.End,
            //    Text = "+"
            //};

            var plus = new Image
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.End,
                Source = FileImageSource.FromFile("add"),
                HeightRequest = 30
            };
            //title.SetBinding(new Binding("SelectedPlan", BindingMode.TwoWay));

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                MessagingCenter.Send(this, "AddPlan", title.Text);
            };

            plus.GestureRecognizers.Add(tapGestureRecognizer);


            View = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 25,
                //BackgroundColor = Color.FromRgb(52, 152, 218),
                Padding = new Thickness(15, 0, 15, 0),
                Orientation = StackOrientation.Horizontal,
                Children = { title, plus }
            };
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

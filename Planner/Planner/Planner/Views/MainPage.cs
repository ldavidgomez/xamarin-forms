using Planner.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Planner.Views
{
    public class MainPage_View : ContentPage
    {
        public MainPage_View()
        {
            BindingContext = new MainPageViewModel(this);

            Padding = new Thickness(10);

            // see https://forums.xamarin.com/discussion/45111/has-anybody-managed-to-get-a-toolbar-working-on-winrt-windows-using-xf
            if (Device.OS == TargetPlatform.Windows)
                Padding = new Xamarin.Forms.Thickness(Padding.Left, this.Padding.Top, this.Padding.Right, 95);

            //ForceLayout();

            Title = " Default title";

            this.SetBinding(TitleProperty, "MainText");

            DataTemplate lastUpdate_ItemTemplate = new DataTemplate(() => {
                var grid = new Grid();

                var descriptionLabel = new Label { FontAttributes = FontAttributes.Bold };
                var dateLabel = new Label();
                //var locationLabel = new Label { HorizontalTextAlignment = TextAlignment.End };

                descriptionLabel.SetBinding(Label.TextProperty, "description");
                dateLabel.SetBinding(Label.TextProperty, "date");

                grid.Children.Add(descriptionLabel);
                grid.Children.Add(dateLabel, 1, 0);
                //grid.Children.Add(locationLabel, 2, 0);

                return new ViewCell { View = grid };
            });

            ListView lastUpdate_ListView = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ItemTemplate = lastUpdate_ItemTemplate
            };

            lastUpdate_ListView.SetBinding(ListView.ItemsSourceProperty, new Binding("LastUpdatePlans"));

            StackLayout mainPanel = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,

                Children = { lastUpdate_ListView }
            };

            

            Content = mainPanel;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            ToolbarItem addPlan_ItemBar = new ToolbarItem { Icon = "plus_square_black.png", ClassId = "About", Order = ToolbarItemOrder.Primary, BindingContext = BindingContext };
            //addPlan_ItemBar.SetBinding(Planner.CommandProperty, new Binding("ShowAboutPageCommand"));

            ToolbarItems.Add(addPlan_ItemBar);
        }
    }
}

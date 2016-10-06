using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planner.Views
{
    public class PlanPage : ContentPage
    {

        public PlanPage()
        {
            Padding = new Thickness(10);

            Title = " Default title";
            this.SetBinding(TitleProperty, "MainText");

            NavigationPage.SetHasNavigationBar(this, true);

            //var nameLabel = new Label { Text = "Name" };
            var descriptionEntry = new Entry {
				Text = "Description",
				Keyboard = Keyboard.Text
			};
            descriptionEntry.SetBinding(Entry.TextProperty, "Description");

            var categoryEntry = new Entry {
				Text = "Category",
				Keyboard = Keyboard.Text				
			};
            categoryEntry.SetBinding(Entry.TextProperty, "Category");
			

			var suggestLV = new ListView();
			suggestLV.RowHeight = 40;
			suggestLV.SetBinding(ListView.ItemsSourceProperty, "Categories");

			var relative = new RelativeLayout
			{
				Parent = categoryEntry
				
			};
			relative.Children.Add(suggestLV, Constraint.RelativeToParent((parent) => { return (parent.Width) - 100; }));

			//relative.SetBinding(RelativeLayout.IsVisibleProperty, "CategoriesVisibility");
			//suggestLV.SetBinding(ListView.IsVisibleProperty, "CategoriesVisibility");

			string dateFormat = "dd/MM/yy";
			var datePicker = new DatePicker();
            datePicker.SetBinding(DatePicker.DateProperty, "StartDate");
			datePicker.Format = dateFormat;
			datePicker.SetBinding(DatePicker.MinimumDateProperty, "MinimunDate");


			var saveButton = new Button { Text = "Save" };
            saveButton.SetBinding(Button.CommandProperty, "SaveCommand");
            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.SetBinding(Button.CommandProperty, "CancelCommand");
            cancelButton.SetBinding(Button.IsVisibleProperty, "CanCancel");
            var deleteButton = new Button { Text = "Delete" };
            deleteButton.SetBinding(Button.CommandProperty, "DeleteCommand");
            deleteButton.SetBinding(Button.IsVisibleProperty, "CanDelete");

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children = {
					descriptionEntry, categoryEntry, suggestLV, datePicker,
                    saveButton, cancelButton, deleteButton
				}
            };
        }
        
    }
}

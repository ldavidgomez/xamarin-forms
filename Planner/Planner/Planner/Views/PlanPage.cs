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
            var descriptionEntry = new Entry { Text = "<Description>" };
            descriptionEntry.SetBinding(Entry.TextProperty, "Description");

            //var notesLabel = new Label { Text = "Notes" };
            var categoryEntry = new Entry { Text = "<Category>" };
            categoryEntry.SetBinding(Entry.TextProperty, "Category");

            //var doneLabel = new Label { Text = "Done" };
            var dateEntry = new DatePicker();
            //var dateEntry = new Entry { Text = "Date" };
            dateEntry.SetBinding(DatePicker.DateProperty, "StartDate");


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
                Children = {descriptionEntry, categoryEntry,
                    dateEntry,
                    saveButton, cancelButton, deleteButton}
            };
        }
        
    }
}

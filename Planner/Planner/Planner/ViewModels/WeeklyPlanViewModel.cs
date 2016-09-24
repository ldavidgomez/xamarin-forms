using Planner.Model;
using Planner.Utilities;
using Planner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planner.ViewModels
{
    class WeeklyPlanViewModel : BaseViewModel
    {
        Plan plan;
        ICommand _addPlanCommand;
        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;

        public Plan Plan { get { return plan; } }

        private string _mainText;
        public string MainText
        {
            get { return _mainText; }
            private set { _mainText = value; }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            private set { _date = value; }
        }

        private DateTime _minimunDate;
        public DateTime MinimunDate
        {
            get { return _minimunDate; }
            private set { _minimunDate = value; }
        }

        ObservableCollection<PlanViewModel> _weeklyPlans = new ObservableCollection<PlanViewModel>();
        public ObservableCollection<PlanViewModel> WeeklyPlans
        {
            get { return _weeklyPlans; }
            set
            {
                if (_weeklyPlans == value)
                    return;
                _weeklyPlans = value;
                OnPropertyChanged();
            }
        }

        public WeeklyPlanViewModel(Plan plan)
        {
            MainText = "Weekly Plan!";
            this.plan = plan;
            this.Date = plan.startDate;
            this.MinimunDate = DateTimeUtils.StartOfWeek(plan.startDate, dfi.FirstDayOfWeek);

            AddPlanCommand = new Command(AddPlan);

            var weeklyPlans = App.Database.GetWeeklyPlans(plan.startDate);

            foreach (var t in weeklyPlans)
            {
                _weeklyPlans.Add(new PlanViewModel(t));
            }

            MessagingCenter.Subscribe<WeeklyPlanPage, Plan>(this, "PlanAdd", (sender, viewModel) => {
                var planvm = new PlanViewModel(viewModel);
                Navigation.Push(ViewFactory.CreatePage(planvm));
            });

            MessagingCenter.Subscribe<PlanViewModel, Plan>(this, "PlanSaved", (sender, model) => {
                App.Database.SavePlan(model);
                Reload();
            });
        }

        void Reload()
        {
            var all = App.Database.GetLastUpdates(5);

            // HACK: this kinda breaks iOS "NSInternalInconsistencyException". Works fine in Android.
            //			Contents.Clear ();
            //			foreach (var t in all) {
            //				Contents.Add (new TodoItemCellViewModel (t));
            //			}

            // HACK: this works in iOS.
            var x = new ObservableCollection<PlanViewModel>();
            foreach (var t in all)
            {
                x.Add(new PlanViewModel(t));
            }
            WeeklyPlans = x;
        }

        public void AddPlan()
        {
            var todo = new Plan();
            var todovm = new PlanViewModel(todo);
            Navigation.Push(ViewFactory.CreatePage(todovm));
        }

        //public void Save()
        //{
        //    MessagingCenter.Send(this, "TodoSaved", plan);
        //    Navigation.Pop();
        //}
        //public void Delete()
        //{
        //    MessagingCenter.Send(this, "TodoDeleted", plan);
        //    Navigation.Pop();
        //}

        public ICommand AddPlanCommand
        {
            get { return _addPlanCommand; }
            set
            {
                if (_addPlanCommand == value)
                    return;
                _addPlanCommand = value;
                OnPropertyChanged();
            }
        }

        object selectedItem;
        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem == value)
                    return;
                // something was selected
                selectedItem = value;

                OnPropertyChanged();

                if (selectedItem != null)
                {

                    var todovm = new PlanViewModel(((PlanViewModel)selectedItem).Plan);

                    Navigation.Push(ViewFactory.CreatePage(todovm));

                    selectedItem = null;
                }
            }
        }
    }
}

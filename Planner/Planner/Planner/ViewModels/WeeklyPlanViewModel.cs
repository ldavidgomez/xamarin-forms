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
        ICommand _createWeeklyPlanCommand, _addPlanCommand;
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
            private set
            {
                if (_date == value)
                    return;
                _date = value;
                OnPropertyChanged();
                
            }
        }

        private DateTime _minimunDate;
        public DateTime MinimunDate
        {
            get { return _minimunDate; }
            private set { _minimunDate = value; }
        }

        ObservableCollection<DailyPlanCellViewModel> _dailyPlans = new ObservableCollection<DailyPlanCellViewModel>();
        public ObservableCollection<DailyPlanCellViewModel> DailyPlans
        {
            get { return _dailyPlans; }
            set
            {
                if (_dailyPlans == value)
                    return;
                _dailyPlans = value;
                OnPropertyChanged();
            }
        }

        public WeeklyPlanViewModel(Plan plan)
        {

            AddPlanCommand = new Command(AddPlan);
            CreateWeeklyPlanCommand = new Command(CreateWeeklyPlan);

            MainText = "Weekly Plan!";
            InitializePlan(plan);
            InitializeDailyPlan();

            MessagingCenter.Subscribe<WeeklyPlanPage, Plan>(this, "WeeklyPlanCreated", (sender, viewModel) =>
            {
                Reload();
            });

            MessagingCenter.Subscribe<WeeklyPlanPage, Plan>(this, "PlanAdded", (sender, viewModel) =>
            {
                var planvm = new PlanViewModel(viewModel);
                Navigation.Push(ViewFactory.CreatePage(planvm));
            });

            MessagingCenter.Subscribe<PlanViewModel, Plan>(this, "PlanSaved", (sender, model) =>
            {
                App.Database.SavePlan(model);
                Reload();
            });
        }

        private void InitializePlan(Plan plan)
        {
            Plan existsPlan = App.Database.GetWeeklyPlan(plan.startDate, plan.endDate);

            if (existsPlan != null)
            {
                this.plan = existsPlan;
                return;
            }                

            var startDate = DateTimeUtils.StartOfWeek(plan.startDate, dfi.FirstDayOfWeek);

            this.plan = new Plan(startDate.ToString("dd/MM/yy"), startDate, string.Empty, PlanEnumeration.PlanType.Weekly);
            this.Date = startDate;
            this.MinimunDate = startDate;

            App.Database.SavePlan(this.plan);
        }

        private void InitializeDailyPlan()
        {
            var weeklyPlans = App.Database.GetDailyPlans(plan);

            foreach (var t in weeklyPlans)
            {
                DailyPlans.Add(new DailyPlanCellViewModel(t));
            }

            if (DailyPlans.Count == 0)

            for (int i = 0; i < 7; i++)
            {
                Plan plan = new Plan(this.plan.startDate.AddDays(i).ToString("dd/MM/yy"), this.plan.startDate.AddDays(i), string.Empty, PlanEnumeration.PlanType.Daily);
                DailyPlans.Add(new DailyPlanCellViewModel(plan));
                App.Database.SavePlan(plan);
            }

            Reload();
        }

        void Reload()
        {
            var all = App.Database.GetDailyPlans(this.plan);

            // HACK: this kinda breaks iOS "NSInternalInconsistencyException". Works fine in Android.
            //			Contents.Clear ();
            //			foreach (var t in all) {
            //				Contents.Add (new TodoItemCellViewModel (t));
            //			}

            // HACK: this works in iOS.
            var x = new ObservableCollection<DailyPlanCellViewModel>();
            foreach (var t in all)
            {
                x.Add(new DailyPlanCellViewModel(t));
            }
            DailyPlans = x;
        }

        public void AddPlan()
        {
            var todo = new Plan(PlanEnumeration.PlanType.Daily);
            var todovm = new PlanViewModel(todo);
            Navigation.Push(ViewFactory.CreatePage(todovm));
        }


        public void CreateWeeklyPlan()
        {
            Reload();
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

        public ICommand CreateWeeklyPlanCommand
        {
            get { return _createWeeklyPlanCommand; }
            set
            {
                if (_createWeeklyPlanCommand == value)
                    return;
                _createWeeklyPlanCommand = value;
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

                    var todovm = new PlanViewModel(((DailyPlanCellViewModel)selectedItem).Plan);

                    Navigation.Push(ViewFactory.CreatePage(todovm));

                    selectedItem = null;
                }
            }
        }
    }
}

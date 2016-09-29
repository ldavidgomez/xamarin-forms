﻿using Planner.Model;
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

        public ObservableCollection<Grouping<string, DailyPlanCellViewModel>> DailyPlansGrouped { get; set; }

        public WeeklyPlanViewModel(Plan plan)
        {

            AddPlanCommand = new Command(AddPlan);
            CreateWeeklyPlanCommand = new Command(CreateWeeklyPlan);

            MainText = "Weekly Plan!";
            InitializePlan(plan);
            InitializeDailyPlan();

            MessagingCenter.Subscribe<WeeklyPlanPage, Plan>(this, "WeeklyPlanCreated", (sender, viewModel) =>
            {
                //Reload();
                InitializeDailyPlan();
            });

            MessagingCenter.Subscribe<WeeklyPlanPage, Plan>(this, "PlanAdded", (sender, viewModel) =>
            {
                var planvm = new PlanViewModel(viewModel);
                Navigation.Push(ViewFactory.CreatePage(planvm));
            });

            //MessagingCenter.Subscribe<PlanViewModel, Plan>(this, "PlanSaved", (sender, model) =>
            //{
            //    Reload();
            //});
        }

        private void InitializePlan(Plan plan)
        {
            Plan existsPlan = App.Database.GetWeeklyPlan(plan.startDate, plan.endDate);

            if (existsPlan != null)
            {
                this.plan = existsPlan;
                this.Date = DateTime.Parse(this.plan.startDate);
                return;
            }                

            var startDate = DateTimeUtils.StartOfWeek(DateTime.Parse(plan.startDate), dfi.FirstDayOfWeek);

            this.plan = new Plan(startDate.ToString("dd/MM/yy"), startDate, string.Empty, PlanEnumeration.PlanType.Weekly);
            this.Date = startDate;
            this.MinimunDate = startDate;

            App.Database.SavePlan(this.plan);
            MessagingCenter.Send(this, "WeeklyPlanCreated", plan);
        }

        //private void InitializeDailyPlan()
        //{
        //    var weeklyPlans = App.Database.GetDailyPlans(plan);

        //    foreach (var t in weeklyPlans)
        //    {
        //        DailyPlans.Add(new DailyPlanCellViewModel(t));
        //    }

        //    if (DailyPlans.Count == 0)
        //    {
        //        for (int i = 0; i < 7; i++)
        //        {
        //            Plan plan = new Plan(DateTime.Parse(this.plan.startDate).AddDays(i).ToString(), DateTime.Parse(this.plan.startDate).AddDays(i), string.Empty, PlanEnumeration.PlanType.Daily);
        //            DailyPlans.Add(new DailyPlanCellViewModel(plan));
        //            App.Database.SavePlan(plan);
        //            MessagingCenter.Send(this, "DailyPlanCreated", plan);
        //        }
        //    }
        //    //else
        //    //{
        //    //    for (int i = 0; i < DailyPlans.Count; i++)
        //    //    {
        //    //        var plans = App.Database.GetPlans(DailyPlans[i].Plan);

        //    //        foreach (Plan item in plans)
        //    //        {
        //    //            DailyPlans[i].Plan.AddPlan(item);
        //    //        }
        //    //    }
        //    //}

        //    if (DailyPlans.Count > 0)
        //    {
        //        var sorted = from plan in DailyPlans
        //                     orderby DateTime.Parse(plan.StartDate).ToString("yyyyMMdd")
        //                     group plan by plan.StartDate into dailyPlanGroup
        //                     select new Grouping<string, DailyPlanCellViewModel>(dailyPlanGroup.Key, dailyPlanGroup);

        //        DailyPlansGrouped = new ObservableCollection<Grouping<string, DailyPlanCellViewModel>>(sorted);
        //    }

        //    //Reload();
        //}

        private void InitializeDailyPlan()
        {
            var weeklyPlans = App.Database.GetDailyPlansFromWeek(plan);

            foreach (var t in weeklyPlans)
            {
                DailyPlans.Add(new DailyPlanCellViewModel(t));
            }

            if (DailyPlans.Count == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    Plan plan = new Plan(DateTime.Parse(this.plan.startDate).AddDays(i).ToString(), DateTime.Parse(this.plan.startDate).AddDays(i), string.Empty, PlanEnumeration.PlanType.Daily);
                    DailyPlans.Add(new DailyPlanCellViewModel(plan));
                    App.Database.SavePlan(plan);
                    MessagingCenter.Send(this, "DailyPlanCreated", plan);
                }
            }

            if (DailyPlans.Count > 0)
            {
                var sorted = from plan in DailyPlans
                             orderby DateTime.Parse(plan.StartDate).ToString("yyyyMMdd")
                             group plan by plan.StartDate into dailyPlanGroup
                             select new Grouping<string, DailyPlanCellViewModel>(dailyPlanGroup.Key, dailyPlanGroup);

                DailyPlansGrouped = new ObservableCollection<Grouping<string, DailyPlanCellViewModel>>(sorted);
            }

            //Reload();
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
            //Reload();
            InitializeDailyPlan();
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

        object selectedDailyPlan;
        public object SelectedDailyPlan
        {
            get { return selectedDailyPlan; }
            set
            {
                if (selectedDailyPlan == value)
                    return;
                // something was selected
                selectedDailyPlan = value;

                OnPropertyChanged();

                if (selectedDailyPlan != null)
                {

                    var todovm = new DailyPlanViewModel(((DailyPlanCellViewModel)selectedDailyPlan).Plan);

                    Navigation.Push(ViewFactory.CreatePage(todovm));

                    selectedDailyPlan = null;
                    OnPropertyChanged();
                }
            }
        }
    }
}

using Planner.Model;
using Planner.Services;
using Planner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planner.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        ICommand _deleteDatabaseCommand;

        private static IPlanService PlanService { get; } = DependencyService.Get<IPlanService>();

        private string _mainText;
        public string MainText
        {
            get { return _mainText; }
            private set { _mainText = value; }
        }

        ObservableCollection<WeeklyPlanCellViewModel> _lastUpdateWeeklyPlans = new ObservableCollection<WeeklyPlanCellViewModel>();
        public ObservableCollection<WeeklyPlanCellViewModel> LastUpdateWeeklyPlans
        {
            get { return _lastUpdateWeeklyPlans; }
            set
            {
                if (_lastUpdateWeeklyPlans == value)
                    return;
                _lastUpdateWeeklyPlans = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<WeeklyPlanCellViewModel> _all = new ObservableCollection<WeeklyPlanCellViewModel>();
        public ObservableCollection<WeeklyPlanCellViewModel> All
        {
            get { return _all; }
            set
            {
                if (_all == value)
                    return;
                _all = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            _deleteDatabaseCommand = new Command(DeleteDatabase);

            MainText = "Planner!";

            var lastUpdateWeeklyPlans = App.Database.GetLastUpdatesWeeklyPlans(5);

            foreach (var t in lastUpdateWeeklyPlans)
            {
                LastUpdateWeeklyPlans.Add(new WeeklyPlanCellViewModel(t));
            }

            var all = App.Database.GetAll();

            foreach (var t in all)
            {
                All.Add(new WeeklyPlanCellViewModel(t));
            }

            MessagingCenter.Subscribe<MainPage, Plan>(this, "SelectedDayPlan", (sender, viewModel) => {
                var planvm = new WeeklyPlanViewModel(viewModel);
                Navigation.Push(ViewFactory.CreatePage(planvm));
            });

            MessagingCenter.Subscribe<MainPage, Plan>(this, "WeeklyPlanCreate", (sender, viewModel) => {
                var planvm = new WeeklyPlanViewModel(viewModel);
                Navigation.Push(ViewFactory.CreatePage(planvm));
            });
            
        }

        private void DeleteDatabase()
        {
            App.Database.DeleteAllPlan();
            Reload();
        }

        void Reload()
        {
            var all = App.Database.GetLastUpdatesWeeklyPlans(5);

            // HACK: this kinda breaks iOS "NSInternalInconsistencyException". Works fine in Android.
            //			Contents.Clear ();
            //			foreach (var t in all) {
            //				Contents.Add (new TodoItemCellViewModel (t));
            //			}

            // HACK: this works in iOS.
            var x = new ObservableCollection<WeeklyPlanCellViewModel>();
            foreach (var t in all)
            {
                x.Add(new WeeklyPlanCellViewModel(t));
            }
            LastUpdateWeeklyPlans = x;
        }

        object selectedWeeklyPlan;
        public object SelectedWeeklyPlan
        {
            get { return selectedWeeklyPlan; }
            set
            {
                if (selectedWeeklyPlan == value)
                    return;
                // something was selected
                selectedWeeklyPlan = value as WeeklyPlanCellViewModel;

                OnPropertyChanged();

                if (selectedWeeklyPlan != null)
                {

                    var todovm = new WeeklyPlanViewModel(((WeeklyPlanCellViewModel)selectedWeeklyPlan).Plan);

                    Navigation.Push(ViewFactory.CreatePage(todovm));

                    selectedWeeklyPlan = null;
                }
            }
        }

        public ICommand DeleteDatabaseCommand
        {
            get { return _deleteDatabaseCommand; }
            set
            {
                if (_deleteDatabaseCommand == value)
                    return;
                _deleteDatabaseCommand = value;
                OnPropertyChanged();
            }
        }
    }
}

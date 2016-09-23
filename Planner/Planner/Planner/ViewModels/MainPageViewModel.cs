using Planner.Model;
using Planner.Services;
using Planner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planner.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        private static IPlanService PlanService { get; } = DependencyService.Get<IPlanService>();

        private string _mainText;
        public string MainText
        {
            get { return _mainText; }
            private set { _mainText = value; }
        }

        ObservableCollection<WeeklyPlanViewModel> _lastUpdateWeeklyPlans = new ObservableCollection<WeeklyPlanViewModel>();
        public ObservableCollection<WeeklyPlanViewModel> LastUpdateWeeklyPlans
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

        public MainPageViewModel()
        {
            MainText = "Planner!";

            var lastUpdateWeeklyPlans = PlanService.GetLastUpdates(5);

            foreach (var t in lastUpdateWeeklyPlans)
            {
                _lastUpdateWeeklyPlans.Add(new WeeklyPlanViewModel(t));
            }

            MessagingCenter.Subscribe<MainPage, Plan>(this, "WeeklyPlannAdd", (sender, viewModel) => {
                var plan = new Plan();
                var planvm = new WeeklyPlanViewModel(viewModel);
                Navigation.Push(ViewFactory.CreatePage(planvm));
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
            var x = new ObservableCollection<WeeklyPlanViewModel>();
            foreach (var t in all)
            {
                x.Add(new WeeklyPlanViewModel(t));
            }
            LastUpdateWeeklyPlans = x;
        }

        object selectedPlan;
        public object SelectedPlan
        {
            get { return selectedPlan; }
            set
            {
                if (selectedPlan == value)
                    return;
                // something was selected
                selectedPlan = value;

                OnPropertyChanged();

                if (selectedPlan != null)
                {

                    var todovm = new PlanViewModel(((PlanViewModel)selectedPlan).Plan);

                    Navigation.Push(ViewFactory.CreatePage(todovm));

                    selectedPlan = null;
                }
            }
        }
    }
}

using Planner.Model;
using Planner.Utilities;
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
    class DailyPlanViewModel : BaseViewModel
    {
        Plan plan;
        ICommand _createPlanCommand;

        public Plan Plan { get { return plan; } }

        private string _mainText;
        public string MainText
        {
            get { return _mainText; }
            private set { _mainText = value; }
        }

        public string StartDate
        {
            get { return plan.startDate; }
        }

        public string Day1
        {
            get { return plan.startDate; }
        }

        ObservableCollection<PlanCellViewModel> _plans = new ObservableCollection<PlanCellViewModel>();
        public ObservableCollection<PlanCellViewModel> Plans
        {
            get { return _plans; }
            set
            {
                if (_plans == value)
                    return;
                _plans = value;
                OnPropertyChanged();
            }
        }

        //ObservableCollection<PlanCellViewModel> _plansGrouped = new ObservableCollection<PlanCellViewModel>();
        public ObservableCollection<Grouping<string, PlanCellViewModel>> PlansGrouped { get; set; }

        public DailyPlanViewModel(Plan plan)
        {
            CreatePlanCommand = new Command(CreatePlan);
            //CreateWeeklyPlanCommand = new Command(CreateWeeklyPlan);

            MainText = DateTime.Parse(plan.startDate).ToString("dd/MM/yyyy");
            this.plan = plan;
            GetPlans();

            MessagingCenter.Subscribe<PlanViewModel, Plan>(this, "PlanSaved", (sender, viewModel) =>
            {
                App.Database.SavePlan(viewModel);
                GetPlans();
            });

            MessagingCenter.Subscribe<DailyPlanPage, Plan>(this, "CreatePlan", (sender, viewModel) => {
                viewModel.startDate = StartDate;
                viewModel.endDate = StartDate;
                var planvm = new PlanViewModel(viewModel);
                Navigation.Push(ViewFactory.CreatePage(planvm));
            });

        }

        void GetPlans()
        {
            var all = App.Database.GetPlans(this.plan);

            // HACK: this kinda breaks iOS "NSInternalInconsistencyException". Works fine in Android.
            //			Contents.Clear ();
            //			foreach (var t in all) {
            //				Contents.Add (new TodoItemCellViewModel (t));
            //			}

            // HACK: this works in iOS.
            var x = new ObservableCollection<PlanCellViewModel>();
            foreach (var t in all)
            {
                x.Add(new PlanCellViewModel(t));
            }
            Plans = x;

            if (Plans.Count > 0)
            {
                var sorted = from plan in Plans
                             orderby plan.StartDate
                             group plan by plan.Category into planGroup
                             select new Grouping<string, PlanCellViewModel>(planGroup.Key, planGroup);

                PlansGrouped = new ObservableCollection<Grouping<string, PlanCellViewModel>>(sorted);
            }
        }

        public void CreatePlan()
        {
            //var todo = new Plan(PlanEnumeration.PlanType.Item);
            //var todovm = new ItemViewModel(todo);
            //Navigation.Push(ViewFactory.CreatePage(todovm));
        }

        public ICommand CreatePlanCommand
        {
            get { return _createPlanCommand; }
            set
            {
                if (_createPlanCommand == value)
                    return;
                _createPlanCommand = value;
                OnPropertyChanged();
            }
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

                    var todovm = new PlanViewModel(((PlanCellViewModel)selectedPlan).Plan);

                    Navigation.Push(ViewFactory.CreatePage(todovm));

                    selectedPlan = null;
                    OnPropertyChanged();
                }
            }
        }
    }
}

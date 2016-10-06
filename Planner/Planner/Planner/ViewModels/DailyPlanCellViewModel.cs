using Planner.Model;
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
    class DailyPlanCellViewModel : BaseViewModel
    {
        Plan plan;
        string dateFormat = "dd/MM/yy";

        public Plan Plan { get { return plan; } }

        public Guid Id { get { return plan.id; } }

        public PlanEnumeration.PlanType type { get { return plan.type; } }

        public string Description { get { return plan.description; } }

        public string Category { get { return plan.category; } }

        public string StartDate { get { return DateTime.Parse(plan.startDate).ToString(dateFormat); } }

        public string EndDate { get { return DateTime.Parse(plan.endDate).ToString(dateFormat); } }

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

        public DailyPlanCellViewModel(Plan plan)
        {
            this.plan = plan;
            InitializePlans();
        }

        private void InitializePlans()
        {
            var plans = App.Database.GetPlans(plan);

            foreach (var t in plans)
            {
                Plans.Add(new PlanCellViewModel(t));
            }

            Reload();
        }

        void Reload()
        {
            var all = App.Database.GetPlans(plan);

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
        }

        public override bool Equals(object obj)
        {
            DailyPlanCellViewModel plan = (DailyPlanCellViewModel)obj;

            if (plan.Id.Equals(this.Id))
                return true;

            return false;
        }

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}

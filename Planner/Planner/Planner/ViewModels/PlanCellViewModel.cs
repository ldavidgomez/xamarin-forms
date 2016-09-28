using Planner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.ViewModels
{
    class PlanCellViewModel : BaseViewModel
    {
        Plan plan;
        string dateFormat = "dd/MM/yy";
        const string dateFormatToPersist = "yyyy-MM-dd HH:mm:ss";

        public Plan Plan { get { return plan; } }

        public string Description { get { return plan.description; } }

        public string Category { get { return plan.category; } }

        public string StartDate { get { return DateTime.Parse(plan.startDate).ToString(dateFormatToPersist); } }

        ObservableCollection<PlanViewModel> _plans = new ObservableCollection<PlanViewModel>();
        public ObservableCollection<PlanViewModel> Plans
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

        public PlanCellViewModel(Plan plan)
        {
            this.plan = plan;
            GetPlans();
        }

        private void GetPlans()
        {
            var all = App.Database.GetPlans(plan);

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
            Plans = x;
        }
    }
}

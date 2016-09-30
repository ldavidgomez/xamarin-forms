using Planner.Model;
using Planner.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planner.ViewModels
{
    class HeaderCellViewModel : BaseViewModel
    {
        Plan plan;
        string dateFormat = "dd/MM/yy";
        const string dateFormatToPersist = "yyyy-MM-dd HH:mm:ss";

        public Plan Plan { get { return plan; } }

        public string Description { get { return plan.description; } }

        public string Category { get { return plan.category; } }

        public string StartDate { get { return DateTime.Parse(plan.startDate).ToString(dateFormatToPersist); } }

        public HeaderCellViewModel(Plan plan)
        {
            this.plan = plan;
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

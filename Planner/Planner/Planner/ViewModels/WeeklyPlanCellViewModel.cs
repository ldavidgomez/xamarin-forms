using Planner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.ViewModels
{
    class WeeklyPlanCellViewModel : BaseViewModel
    {
        Plan plan;
        string dateFormat = "dd/MM/yy";
        string separatorChar = "-";

        public Plan Plan { get { return plan; } }

        public string StartDate { get { return DateTime.Parse(plan.startDate).Date.ToString(dateFormat); } }

        public string SeparatorChar { get { return separatorChar; } }

        public string EndDate { get { return DateTime.Parse(plan.endDate).ToString(dateFormat); } }

        public string PlanType { get { return plan.type.ToString(); } }
        public WeeklyPlanCellViewModel(Plan plan)
        {
            this.plan = plan;
        }
    }
}

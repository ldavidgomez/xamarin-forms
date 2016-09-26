using Planner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.ViewModels
{
    class DailyPlanCellViewModel : BaseViewModel
    {
        Plan plan;
        string dateFormat = "dd/MM/yy";
        string separatorChar = "-";

        public Plan Plan { get { return plan; } }

        public string Description { get { return plan.description; } }

        public string Category { get { return plan.category; } }

        public string StartDate { get { return plan.startDate.ToString(dateFormat); } }

        public DailyPlanCellViewModel(Plan plan)
        {
            this.plan = plan;
        }
    }
}

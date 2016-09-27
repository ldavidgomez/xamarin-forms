using Planner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.ViewModels
{
    class PlanCellViewModel
    {
        Plan plan;
        string dateFormat = "dd/MM/yy";
        string separatorChar = "-";

        public Plan Plan { get { return plan; } }

        public string Description { get { return plan.description; } }

        public string Category { get { return plan.category; } }

        public string EndDate { get { return DateTime.Parse(plan.endDate).ToString(dateFormat); } }

        public PlanCellViewModel(Plan plan)
        {
            this.plan = plan;
        }
    }
}

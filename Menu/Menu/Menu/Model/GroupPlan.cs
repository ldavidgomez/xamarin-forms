using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace Menu.Model
{
    public class GroupPlan : IPlan
    {
        [PrimaryKey]
        Guid id { get; set; }
        public string description { get; set; }
        List<IPlan> planList;

        public int length {
            get
            {
                if (planList.Count > 0)
                    return planList.Count;
                
                return 0;
            }
        }

        public GroupPlan(string description)
        {
            this.id = Guid.NewGuid();
            this.description = description;
            this.planList = new List<IPlan>();
        }

        public string GetDescription()
        {
            return this.description;
        }

        public void AddPlan(IPlan plan)
        {
            if(planList.IndexOf(plan) < 0)
                this.planList.Add(plan);
        }

        public IPlan getPlan(IPlan plan)
        {

            int searchedPlanIndex = planList.IndexOf(plan);

            if (searchedPlanIndex >= 0)
                return planList[searchedPlanIndex];

            return null;
        }

        public bool Exists(IPlan plan)
        {
            if (planList.IndexOf(plan) >= 0)
                return true;

            return false;
        }
    }
}

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
        public string category { get; set; }
        public PlanEnumeration.PlanType type { get; set; }
        public DateTime date { get; set; }
        public DateTime lastUpdate { get; set; }
        List<IPlan> planList;

        public int length {
            get
            {
                if (planList.Count > 0)
                    return planList.Count;
                
                return 0;
            }
        }

        public GroupPlan()
        { }

        public GroupPlan(string description, DateTime date, string category, PlanEnumeration.PlanType type)
        {
            this.id = Guid.NewGuid();
            this.description = description;
            this.category = category;
            this.type = type;
            this.date = date;
            this.planList = new List<IPlan>();
            this.lastUpdate = DateTime.Now;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public DateTime GetDate()
        {
            return this.date;
        }

        public string GetCategory()
        {
            return this.category;
        }

        public PlanEnumeration.PlanType GetPlanType()
        {
            return this.type;
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

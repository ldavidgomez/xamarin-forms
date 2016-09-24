using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;

namespace Planner.Model
{
    public class Plan
    {
        [PrimaryKey]
        public Guid id { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public PlanEnumeration.PlanType type { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime lastUpdate { get; set; }
        List<Plan> planList;

        public int length {
            get
            {
                if (planList.Count > 0)
                    return planList.Count;
                
                return 0;
            }
        }

        public Plan()
        { }

        public Plan(PlanEnumeration.PlanType type)
            : this("Description", DateTime.Now, "Category", type)
        { }

        public Plan(string description, DateTime startDate, string category, PlanEnumeration.PlanType type)
        {
            this.id = Guid.NewGuid();
            this.description = description;
            this.category = category;
            this.type = type;
            this.startDate = startDate.Date;
            setEndDay();

            this.planList = new List<Plan>();
            this.lastUpdate = DateTime.Now;
        }

        private void setEndDay()
        {
            switch (this.type)
            {
                case PlanEnumeration.PlanType.Item:
                case PlanEnumeration.PlanType.Plan:
                case PlanEnumeration.PlanType.Daily:
                    this.endDate = this.startDate;
                    break;
                case PlanEnumeration.PlanType.Weekly:
                    this.endDate = this.startDate.AddDays(7);
                    break;
                default:
                    break;
            }
        }

        public string GetDescription()
        {
            return this.description;
        }

        public DateTime GetDate()
        {
            return this.startDate;
        }

        public string GetCategory()
        {
            return this.category;
        }

        public PlanEnumeration.PlanType GetPlanType()
        {
            return this.type;
        }

        public void AddPlan(Plan plan)
        {
            if(planList.IndexOf(plan) < 0)
                this.planList.Add(plan);
        }

        public Plan getPlan(Plan plan)
        {

            int searchedPlanIndex = planList.IndexOf(plan);

            if (searchedPlanIndex >= 0)
                return planList[searchedPlanIndex];

            return null;
        }

        public bool Exists(Plan plan)
        {
            if (planList.IndexOf(plan) >= 0)
                return true;

            return false;
        }
    }
}

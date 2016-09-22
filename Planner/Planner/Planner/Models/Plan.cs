using System;
using System.Collections.Generic;

using SQLite;
using SQLite.Net.Attributes;

namespace Planner.Model
{
    public class Plan
    {
        [PrimaryKey]
        public Guid id { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public PlanEnumeration.PlanType type { get; set; }
        public DateTime date { get; set; }
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

        public Plan(string description, DateTime date, string category, PlanEnumeration.PlanType type)
        {
            this.id = Guid.NewGuid();
            this.description = description;
            this.category = category;
            this.type = type;
            this.date = date;

            this.planList = new List<Plan>();
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

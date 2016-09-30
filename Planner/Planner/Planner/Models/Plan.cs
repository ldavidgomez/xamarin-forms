using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using Planner.Utilities;

namespace Planner.Model
{
    public class Plan
    {
        const string dateFormatToPersist = "yyyy-MM-dd HH:mm:ss";

        [PrimaryKey]
        public Guid id { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public PlanEnumeration.PlanType type { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string lastUpdate { get; set; }
        List<Plan> planList = new List<Plan>();

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
            : this(string.Empty, DateTime.Now, string.Empty, type)
        { }

        public Plan(string description, DateTime startDate, string category, PlanEnumeration.PlanType type)
        {
            this.id = Guid.NewGuid();
            this.description = description;
            this.category = category;
            this.type = type;
            this.startDate = setStartDay(startDate);
            this.endDate = setEndDay();

            this.planList = new List<Plan>();
            this.lastUpdate = DateTime.Now.ToString(dateFormatToPersist);
        }

        private string setStartDay(DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            var startDate = DateTimeUtils.StartOfWeek(DateTime.Now, dfi.FirstDayOfWeek);

            switch (this.type)
            {
                case PlanEnumeration.PlanType.Item:
                case PlanEnumeration.PlanType.Plan:
                case PlanEnumeration.PlanType.Daily:
                    return date.Date.ToString(dateFormatToPersist);
                case PlanEnumeration.PlanType.Weekly:
                    return startDate.ToString(dateFormatToPersist);
                default:
                    throw new NotSupportedException("Not recognized Plan type");
            }
        }

        private string setEndDay()
        {
            switch (this.type)
            {
                case PlanEnumeration.PlanType.Item:
                case PlanEnumeration.PlanType.Plan:
                case PlanEnumeration.PlanType.Daily:
                    return DateTime.Parse(this.startDate).ToString(dateFormatToPersist); ;
                case PlanEnumeration.PlanType.Weekly:
                    return DateTime.Parse(this.startDate).AddDays(6).ToString(dateFormatToPersist);
                default:
                    throw new NotSupportedException("Not recognized Plan type");
            }
        }

        public string GetDescription()
        {
            return this.description;
        }

        public string GetDate()
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

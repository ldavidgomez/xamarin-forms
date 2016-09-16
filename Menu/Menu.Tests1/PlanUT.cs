using Menu.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Tests1
{
    [TestFixture]
    public class PlanUT
    {
        [Test]
        public void CreatePlan()
        {
            string description = "New GroupPlan";
            DateTime date = DateTime.Now;
            string category = "category";
            PlanEnumeration.PlanType type = PlanEnumeration.PlanType.Weekly;
            Plan groupPlan = new Plan(description, date, category, type);

            Assert.AreEqual(groupPlan.GetDescription(), description);
            Assert.AreEqual(groupPlan.GetDate(), date);
            Assert.AreEqual(groupPlan.GetCategory(), category);
            Assert.AreEqual(groupPlan.GetPlanType(), type);
        }       
        
        [Test]
        public void AddPlan()
        {
            Plan weeklyPlan = new Plan("MainPlan", DateTime.Now, "category1", PlanEnumeration.PlanType.Weekly);
            Plan dailyPlan = new Plan("SubPlan", DateTime.Now, "subcategory", PlanEnumeration.PlanType.Daily);

            weeklyPlan.AddPlan(dailyPlan);

            Assert.IsTrue(weeklyPlan.Exists(dailyPlan));
        } 

        [Test]
        public void GetPlan()
        {
            Plan weeklyPlan = new Plan("MainPlan", DateTime.Now, "category1", PlanEnumeration.PlanType.Weekly);
            Plan dailyPlan = new Plan("SubPlan", DateTime.Now, "subcategory", PlanEnumeration.PlanType.Daily);

            weeklyPlan.AddPlan(dailyPlan);

            Plan searchedPlan = (Plan) weeklyPlan.getPlan(dailyPlan);
            Assert.AreSame(dailyPlan, searchedPlan);
        }
    }
}

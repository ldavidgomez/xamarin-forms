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
    public class GroupPlanUT
    {
        [Test]
        public void CreatePlan()
        {
            string description = "New GroupPlan";
            DateTime date = DateTime.Now;
            string category = "category";
            PlanEnumeration.PlanType type = PlanEnumeration.PlanType.Weekly;
            GroupPlan groupPlan = new GroupPlan(description, date, category, type);

            Assert.AreEqual(groupPlan.GetDescription(), description);
            Assert.AreEqual(groupPlan.GetDate(), date);
            Assert.AreEqual(groupPlan.GetCategory(), category);
            Assert.AreEqual(groupPlan.GetPlanType(), type);
        }       
        
        [Test]
        public void AddPlan()
        {
            GroupPlan mainPlan = new GroupPlan("MainPlan", DateTime.Now, "category1", PlanEnumeration.PlanType.Weekly);
            GroupPlan subPlan = new GroupPlan("SubPlan", DateTime.Now, "subcategory", PlanEnumeration.PlanType.Daily);

            mainPlan.AddPlan(subPlan);

            Assert.IsTrue(mainPlan.Exists(subPlan));
        } 

        [Test]
        public void GetPlan()
        {
            GroupPlan mainPlan = new GroupPlan("MainPlan", DateTime.Now, "category1", PlanEnumeration.PlanType.Weekly);
            GroupPlan subPlan = new GroupPlan("SubPlan", DateTime.Now, "subcategory", PlanEnumeration.PlanType.Daily);

            mainPlan.AddPlan(subPlan);

            GroupPlan searchedPlan = (GroupPlan) mainPlan.getPlan(subPlan);
            Assert.AreSame(subPlan, searchedPlan);
        }
    }
}

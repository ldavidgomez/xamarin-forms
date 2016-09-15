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
        public void CreateGroupPlan()
        {
            string description = "New GroupPlan";
            GroupPlan groupPlan = new GroupPlan(description);

            Assert.AreEqual(groupPlan.GetDescription(), description);
        }       
        
        [Test]
        public void AddPlan()
        {
            GroupPlan mainPlan = new GroupPlan("MainPlan");
            GroupPlan subPlan = new GroupPlan("SubPlan");

            mainPlan.AddPlan(subPlan);

            Assert.IsTrue(mainPlan.Exists(subPlan));
        } 

        [Test]
        public void GetPlan()
        {
            GroupPlan mainPlan = new GroupPlan("MainPlan");
            GroupPlan subPlan = new GroupPlan("SubPlan");

            mainPlan.AddPlan(subPlan);

            GroupPlan searchedPlan = (GroupPlan) mainPlan.getPlan(subPlan);
            Assert.AreSame(subPlan, searchedPlan);
        }
    }
}

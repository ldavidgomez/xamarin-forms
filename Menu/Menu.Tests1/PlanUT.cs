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
            string planText = "New plan";
            Plan plan = new Plan(planText);

            Assert.AreEqual(plan.GetDescription(), planText);
        }        
    }
}

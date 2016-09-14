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
    public class DailyMenuUT
    {
        [Test]
        public void createDailyMenu()
        {
            DailyMenu dailyMenu = new DailyMenu();

            Assert.AreNotEqual(dailyMenu, null);
        }       
        
        [Test]
        public void addMealtime()
        {
            DailyMenu dailyMenu = new DailyMenu();
            Mealtime mealTime = new Mealtime();

            dailyMenu.addMealtime(mealTime);

            Assert.IsTrue(dailyMenu.exists(mealTime));
        } 
    }
}

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
    public class MealtimeUT
    {
        [Test]
        public void createMealtime()
        {
            Mealtime mealTime = new Mealtime();

            Assert.AreNotEqual(mealTime, null);
        }    
        
        [Test]
        public void addItem()
        {
            string itemText = "New item";
            Item item = new Item(itemText);

            Mealtime mealTime = new Mealtime();
            mealTime.addItem(item);

            Assert.IsTrue(mealTime.exists(item));
        }

        [Test]
        public void addItem_ExistsNotAllowed()
        {
            string itemText = "New item";
            Item item = new Item(itemText);

            Mealtime mealTime = new Mealtime();
            mealTime.addItem(item);

            var count = mealTime.length;

            mealTime.addItem(item);

            Assert.AreEqual(mealTime.length, count);
        }
    }
}

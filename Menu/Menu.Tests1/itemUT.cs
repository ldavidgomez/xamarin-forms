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
    public class ItemUT
    {
        [Test]
        public void createItem()
        {
            string itemText = "New item";
            Item item = new Item(itemText);

            Assert.AreEqual(item.GetText(), itemText);
        }        
    }
}

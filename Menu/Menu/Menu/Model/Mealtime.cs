using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Model
{
    public class Mealtime
    {
        List<Item> items;

        public int length {
            get
            {
                if (items.Count > 0)
                    return items.Count;
                
                return 0;
            }
        }

        public Mealtime()
        {
            this.items = new List<Item>();
        }

        public void addItem(Item item)
        {
            if(items.IndexOf(item) < 0)
                this.items.Add(item);
        }

        public bool exists(Item item)
        {
            if (items.IndexOf(item) >= 0)
                return true;

            return false;
        }
    }
}

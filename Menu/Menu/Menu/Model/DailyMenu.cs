using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Model
{
    public class DailyMenu
    {
        Guid id;
        List<Mealtime> mealtimes;
        DateTime date;

        public int length
        {
            get
            {
                if (mealtimes.Count > 0)
                    return mealtimes.Count;

                return 0;
            }
        }

        public DailyMenu()
        {
            id = Guid.NewGuid();
            mealtimes = new List<Mealtime>();
        }

        public void addMealtime(Mealtime mealtime)
        {
            this.mealtimes.Add(mealtime);
        }

        public bool exists(Mealtime mealtime)
        {
            if (this.mealtimes.IndexOf(mealtime) >= 0)
                return true;

            return false;
        }
    }
}

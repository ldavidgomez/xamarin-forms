using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Model
{
    class WeeklyMenu
    {
        Guid id;
        List<DailyMenu> dailyMenus;

        public WeeklyMenu()
        {
            id = Guid.NewGuid();
            dailyMenus = new List<Model.DailyMenu>();
        }
    }
}

using Menu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Data
{
    public interface IPlanData
    {
        //IPlan GetPlan(Guid id);
        IList<Plan> GetLastUpdates(int limit);
        IList<Plan> GetWeeklyPlan(DateTime week);

    }
}

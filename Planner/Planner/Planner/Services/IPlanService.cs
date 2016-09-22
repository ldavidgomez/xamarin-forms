using Planner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Services
{
    public interface IPlanService
    {
        IList<Plan> GetLastUpdates();
        IList<Plan> GetCategories();
    }
}

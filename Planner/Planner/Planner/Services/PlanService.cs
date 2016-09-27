using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Planner.Model;
using Xamarin.Forms;
using Planner.Services;
using SQLite.Net;

[assembly: Dependency(typeof(PlanService))]
namespace Planner.Services
{
    public class PlanService : IPlanService
    {
        //private static readonly AsyncLocal Locker = new AsyncLock();

        //private SQLiteAsyncConnection Database { get; } = DependencyService.Get<ISQLite>().GetAsyncConnection();
        private SQLiteConnection Database { get; } = DependencyService.Get<ISQLite>().GetConnection();

        //TODO fix datasource dependency

        public IList<Plan> GetLastUpdates(int limit)
        {
            //using (await Locker.LockAsync())
            //{
                return Database.Table<Plan>().Where(x => x.id != Guid.Empty).Take(limit).ToList<Plan>();
            //}
        }

        public IList<Plan> GetWeeklyPlans(string date)
        {
            //return Database.Table<Plan>().Where(x => x.startDate <= date && x.endDate >= date.AddDays(7)).Where(x => x.id != Guid.Empty).Take(5).ToList<Plan>();
            return null;
        }

        public IList<Plan> GetCategories()
        {
            return Database.Table<Plan>().Where(x => x.id != Guid.Empty).ToList<Plan>();
            //throw new NotImplementedException();
        }
    }
}

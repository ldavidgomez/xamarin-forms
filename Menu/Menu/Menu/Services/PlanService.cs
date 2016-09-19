using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Menu.Model;
using Xamarin.Forms;
using Menu.Services;
using SQLite;

[assembly: Dependency(typeof(PlanService))]
namespace Menu.Services
{
    public class PlanService : IPlanService
    {
        //private static readonly AsyncLocal Locker = new AsyncLock();

        //private SQLiteAsyncConnection Database { get; } = DependencyService.Get<ISQLite>().GetAsyncConnection();
        private SQLiteConnection Database { get; } = DependencyService.Get<ISQLite>().GetConnection();

        public IList<Plan> GetLastUpdates()
        {
            //using (await Locker.LockAsync())
            //{
                return Database.Table<Plan>().Where(x => x.id != Guid.Empty).Take(5).ToList<Plan>();
            //}
        }

        public IList<Plan> GetCategories()
        {
            return Database.Table<Plan>().Where(x => x.id != Guid.Empty).ToList<Plan>();
            //throw new NotImplementedException();
        }
    }
}

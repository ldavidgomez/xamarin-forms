using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using SQLite;
using Menu.Model;

namespace Menu.Data
{

    public class PlanDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public PlanDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            database.CreateTable<Plan>();
            database.CreateTable<GroupPlan>();
        }

        public Plan GetPlan(Guid id)
        {
            lock (locker)
            {
                return database.Table<Plan>().FirstOrDefault(x => x.id == id);
            }
        }

        public IList<GroupPlan> GetLastUpdates()
        {
            lock (locker)
            {
                var groupPlan = database.Query<GroupPlan>("SELECT * FROM GroupPlan ORDER BY lastUpdate DESC LIMIT 5");
                return groupPlan.ToList();
            }
        }

    }
}

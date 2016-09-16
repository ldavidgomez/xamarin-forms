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
    public class PlanDatabase : IPlanData
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
            Initialize();
        }

        private void Initialize()
        {
            GetConnection();
            CreateTables();
        }

        private void CreateTables()
        {
            // create the tables
            database.CreateTable<Plan>();
            database.CreateTable<GroupPlan>();
        }

        private void GetConnection()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
        }

        public IPlan GetPlan(Guid id)
        {
            lock (locker)
            {
                return database.Table<Plan>().FirstOrDefault(x => x.id == id);
            }
        }

        public IList<GroupPlan> GetLastUpdates(int limit)
        {
            lock (locker)
            {
                var groupPlan = database.Query<GroupPlan>("SELECT * FROM GroupPlan ORDER BY lastUpdate DESC LIMIT ?", limit);
                return groupPlan.ToList();
            }
        }

        public IList<GroupPlan> GetWeeklyPlan(DateTime week)
        {
            lock (locker)
            {
                var groupPlan = database.Query<GroupPlan>("SELECT * FROM GroupPlan ORDER BY lastUpdate DESC LIMIT ?", week);
                return groupPlan.ToList();
            }
        }

    }
}

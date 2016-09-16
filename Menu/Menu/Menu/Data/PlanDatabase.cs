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
            //database.CreateTable<Plan>();
            database.CreateTable<Plan>();
        }

        private void GetConnection()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
        }

        //public IPlan GetPlan(Guid id)
        //{
        //    lock (locker)
        //    {
        //        return database.Table<Plan>().FirstOrDefault(x => x.id == id);
        //    }
        //}

        public IList<Plan> GetLastUpdates(int limit)
        {
            lock (locker)
            {
                var groupPlan = database.Query<Plan>("SELECT * FROM GroupPlan ORDER BY lastUpdate DESC LIMIT ?", limit);
                return groupPlan.ToList();
            }
        }

        public IList<Plan> GetWeeklyPlan(DateTime week)
        {
            lock (locker)
            {
                var groupPlan = database.Query<Plan>("SELECT * FROM GroupPlan ORDER BY lastUpdate DESC LIMIT ?", week);
                return groupPlan.ToList();
            }
        }

        public int SavePlan(Plan plan)
        {
            lock (locker)
            {
                if (plan.id != 0)
                {
                    database.Update(plan);
                    return plan.id;
                }
                else
                {
                    return database.Insert(plan);
                }
            }
        }

        public int DeletePlan(int id)
        {
            lock (locker)
            {
                return database.Delete<Plan>(id);
            }
        }

    }
}

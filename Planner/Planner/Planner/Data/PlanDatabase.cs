using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using SQLite.Net;
using Planner.Model;

namespace Planner.Data
{
    public class PlanDatabase : IPlanData
    {
        static object locker = new object();

        SQLiteConnection SyncConnection;
        //SQLiteAsyncConnection AsyncConnection;

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

        private void GetConnection()
        {
            SyncConnection = DependencyService.Get<ISQLite>().GetConnection();
        }

        //private void GetAsyncConnection()
        //{
        //    AsyncConnection = DependencyService.Get<ISQLite>().GetAsyncConnection();
        //}

        private void CreateTables()
        {
            // create the tables
            SyncConnection.CreateTable<Plan>();
        }

        //public IPlan GetPlan(Guid id)
        //{
        //    lock (locker)
        //    {
        //        return database.Table<Plan>().FirstOrDefault(x => x.id == id);
        //    }
        //}

        public IList<Plan> GetLastUpdatesWeeklyPlans(int limit)
        {
            lock (locker)
            {
                var lastUpdateWeeklyPlans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? ORDER BY [lastUpdate] DESC LIMIT ?", new object[] { PlanEnumeration.PlanType.Weekly, limit } );
                return lastUpdateWeeklyPlans.ToList();
            }
        }

        public IList<Plan> GetAll()
        {
            lock (locker)
            {
                var all = SyncConnection.Query<Plan>("SELECT * FROM [Plan] ORDER BY [lastUpdate] DESC");
                return all.ToList();
            }
        }

        public IList<Plan> GetDailyPlans(Plan plan)
        {
            lock (locker)
            {
                var dailyPlans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? AND [startDate] >= ? AND [endDate] <= ? ", new object[] { PlanEnumeration.PlanType.Daily, DateTime.Parse(plan.startDate).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Parse(plan.endDate).ToString("yyyy-MM-dd HH:mm:ss") });
                //var dailyPlans = GetPlans(plan, PlanEnumeration.PlanType.Daily);
                return dailyPlans.ToList();
            }
        }

        public IList<Plan> GetDailyPlansFromWeek(Plan plan)
        {
            lock (locker)
            {
                var dailyPlans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE ([type] = ? OR [type] = ?) AND [startDate] >= ? AND [endDate] <= ? ", new object[] { PlanEnumeration.PlanType.Daily, PlanEnumeration.PlanType.Plan, DateTime.Parse(plan.startDate).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Parse(plan.endDate).ToString("yyyy-MM-dd HH:mm:ss") });
                //var dailyPlans = GetPlans(plan, PlanEnumeration.PlanType.Daily);
                return dailyPlans.ToList();
            }
        }

        public IList<Plan> GetPlans(Plan plan)
        {
            lock (locker)
            {
                var plans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? AND [startDate] = ? AND [endDate] = ? ", new object[] { PlanEnumeration.PlanType.Plan, DateTime.Parse(plan.startDate).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Parse(plan.endDate).ToString("yyyy-MM-dd HH:mm:ss") });
                //var items = GetPlans(plan, PlanEnumeration.PlanType.Item);
                return plans.ToList();
            }
        }

        IList<Plan> GetPlans(Plan plan, PlanEnumeration.PlanType planType)
        {
            lock (locker)
            {
                var plans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? AND [startDate] = ? AND [endDate] = ? ", new object[] { planType, DateTime.Parse(plan.startDate).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Parse(plan.endDate).ToString("yyyy-MM-dd HH:mm:ss") });
                return plans.ToList();
            }
        }

        public Plan GetWeeklyPlan(string startDate, string endDate)
        {
            lock (locker)
            {
                //var dailyPlans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? AND [startDate] <= ?  AND [endDate] >= ? ORDER BY [startDate] DESC", new object[] { PlanEnumeration.PlanType.Daily, plan.startDate, plan.endDate });
                var plans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? AND [startDate] = ? AND [endDate] = ? ", new object[] { PlanEnumeration.PlanType.Weekly, DateTime.Parse(startDate).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Parse(endDate).ToString("yyyy-MM-dd HH:mm:ss") });

                if (plans.Count > 1)
                    throw new RankException("Can't exists more than one week with same dates");

                if (plans.Count == 1)
                    return plans[0];

                return null;
            }
        }

        //public IList<Plan> GetWeeklyPlans(int limit)
        //{
        //    lock (locker)
        //    {
        //        var plan = SyncConnection.Query<Plan>("SELECT * FROM Plan ORDER BY lastUpdate DESC LIMIT ?", limit);
        //        return plan.ToList();
        //    }
        //}

        public int SavePlan(Plan plan)
        {
            lock (locker)
            {
                //if (plan.id != Guid.Empty)
                //{
                //    return database.Update(plan);
                //}
                //else
                //{
                //    return database.Insert(plan);
                //}

                var affectedRows = SyncConnection.Update(plan);
                if (affectedRows == 0)
                    return SyncConnection.Insert(plan);

                return affectedRows;
            }
        }

        public int DeletePlan(Guid id)
        {
            lock (locker)
            {
                return SyncConnection.Delete<Plan>(id);
            }
        }

        public int DeleteAllPlan()
        {
            lock (locker)
            {
                return SyncConnection.DeleteAll<Plan>();
            }
        }

    }
}

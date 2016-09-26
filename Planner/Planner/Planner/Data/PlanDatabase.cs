﻿using System;
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

        public IList<Plan> GetDailyPlans(Plan plan)
        {
            lock (locker)
            {
                //var dailyPlans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? AND [startDate] <= ?  AND [endDate] >= ? ORDER BY [startDate] DESC", new object[] { PlanEnumeration.PlanType.Daily, plan.startDate, plan.endDate });
                var dailyPlans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? ", new object[] { PlanEnumeration.PlanType.Daily});
                return dailyPlans.ToList();
            }
        }

        public Plan GetWeeklyPlan(DateTime startDate, DateTime endDate)
        {
            lock (locker)
            {
                //var dailyPlans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [type] = ? AND [startDate] <= ?  AND [endDate] >= ? ORDER BY [startDate] DESC", new object[] { PlanEnumeration.PlanType.Daily, plan.startDate, plan.endDate });
                var plans = SyncConnection.Query<Plan>("SELECT * FROM [Plan] WHERE [startDate] = ? AND [endDate] = ? ", new object[] { startDate, endDate });

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

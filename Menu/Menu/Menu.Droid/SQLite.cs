using System;
using Menu;
using Xamarin.Forms;
using System.IO;
using Menu.Droid;

[assembly: Dependency(typeof(SQLite_Android))]

namespace Menu
{
    public class SQLite_Android : ISQLite
    {

        public SQLite_Android()
        {
        }

        #region ISQLite implementation
        public global::SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "PlanSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.PlanSQLite);  // RESOURCE NAME ###

                // create a write stream
                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                // write to the stream
                ReadWriteStream(s, writeStream);
            }

            var conn = new global::SQLite.SQLiteConnection(path);

            // Return the database connection 
            return conn;
        }

        //public global::SQLite.SQLiteAsyncConnection GetAsyncConnection()
        //{
        //    var sqliteFilename = "PlanSQLite.db3";
        //    string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
        //    var path = Path.Combine(documentsPath, sqliteFilename);

        //    var platForm = new SQLitePlatformAndroid();

        //    var connectionFactory = new Func<SQLiteConnectionWithLock>(
        //        () =>
        //        {
        //            if (_conn == null)
        //            {
        //                _conn =
        //                    new SQLiteConnectionWithLock(platForm,
        //                        new SQLiteConnectionString(path, storeDateTimeAsTicks: true));
        //            }
        //            return _conn;
        //        });

        //    return new SQLiteAsyncConnection(connectionFactory);
        //}
        #endregion

        /// <summary>
        /// helper method to get the database out of /raw/ and into the user filesystem
        /// </summary>
        void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}
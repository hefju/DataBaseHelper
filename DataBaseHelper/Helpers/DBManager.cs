using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper.Helpers
{
    /// <summary>
    /// 数据库管理,提供多个数据库操作
    /// </summary>
   static class DBManager
    {
       static Dictionary<string, IDBHelper> dblist = new Dictionary<string, IDBHelper>();
        static SqlHelper DefaultMssql;
        static SQLiteHelper DefaultSqlite;
        static OleDbHelper DefaultAccess;

         static DBManager()
        {
            DefaultMssql = new SqlHelper("Server=192.168.100.200;Database=JXC;User Id=guest;Password=133;");
            DefaultSqlite = new SQLiteHelper("Data Source=test.db;Version=3;");
            DefaultAccess = new OleDbHelper("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=test.mdb;User Id=admin;Jet OLEDB:Database Password=1;");
        }

        public static SqlHelper GetDefaultMssql()
        {
            return DefaultMssql;
        }
        public static SQLiteHelper GetDefaultSqlite()
        {
            return DefaultSqlite;
        }
        public static OleDbHelper GetDefaultAccess()
        {
            return DefaultAccess;
        }
        public static void AddDbhelper(DbType type, string name, string connectstring)
        {
            switch (type)
            {
                case DbType.SqlHelper:
                    dblist[name] = new SqlHelper(connectstring);
                    break;
                case DbType.SQLiteHelper:
                    dblist[name] = new SQLiteHelper(connectstring);
                    break;
                case DbType.OleDbHelper:
                    dblist[name] = new OleDbHelper(connectstring);
                    break;
                default:
                    break;
            }
        }


        public static IDBHelper GetDbhelper(string name)
        {
            if (dblist.ContainsKey(name))
            {
                return dblist[name];
            }
            else
                return null;
        }



    }
    public enum DbType{
        SqlHelper,SQLiteHelper,OleDbHelper
    }
}

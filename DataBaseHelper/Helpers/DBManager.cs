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
    class DBManager
    {
        Dictionary<string, IDBHelper> dblist = new Dictionary<string, IDBHelper>();
        SqlHelper DefaultMssql;
        SQLiteHelper DefaultSqlite;
        OleDbHelper DefaultAccess;



    }
}

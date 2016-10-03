using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper.Helpers
{
    interface IDBHelper
    {
        int ExecuteNonQuery2(string sql);

        int ExecuteNonQuery(string sql);

        object ExecuteScalar(string sql);
        DataTable GetDataTable(string sql);
    }
}

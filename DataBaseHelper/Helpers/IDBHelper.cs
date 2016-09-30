using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper.Helpers
{
    interface IDBHelper
    {
        int ExecuteNonQuery(string sql);
    }
}

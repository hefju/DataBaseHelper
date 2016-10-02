using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper.Helpers
{
    class SqlCreater 
    {
        public int Save(DataTable dt,IDBHelper dbhelper)
        {
            if (dt == null)
                return 0;//没有修改也返回1表示保存成功.
            var dtChange = dt.GetChanges();//可能传入没有修改的DataTable
            if (dtChange == null)
                return 1;//没有修改也返回1表示保存成功.

            int count=0;
           // SqlHelper db=SqlHelper.getInstance();
            foreach (DataRow dr in dtChange.Rows)
            {
                string sql = "";
                switch (dr.RowState)
                {
                    case DataRowState.Added:
                        sql = GenerateSqlAdded(dr, dtChange);
                        break;
                    case DataRowState.Deleted:
                        break;
                    case DataRowState.Detached:
                        break;
                    case DataRowState.Modified:
                        sql = GenerateSqlModified(dr, dtChange);
                        break;
                    case DataRowState.Unchanged:
                        break;
                    default:
                        break;
                }
                if (sql != "")
                {
                    count += dbhelper.ExecuteNonQuery2(sql);
                    Console.WriteLine(sql);//测试输出语句
                }
            }
            dt.AcceptChanges();
            return count;
        }

        //
        private string GenerateSqlAdded(DataRow dr, DataTable dtChange)
        {
            var sql = "Insert into " +dtChange.TableName + "(";

            List<string> columns = new List<string>();//列名
            List<string> values = new List<string>();//插入值

            List<DataColumn> fileds = new List<DataColumn>();//字段集合
            foreach (DataColumn dc in dtChange.Columns)
            {
                fileds.Add(dc);
            }
            for (int i = 0; i < fileds.Count; i++)
            {
                var f = fileds[i];
                var colname = f.ColumnName.ToLower();
                if (colname == "id")
                    continue;
                columns.Add(f.ColumnName);
                values.Add("'" + dr[i].ToString() + "'");
            }

            sql += string.Join(",", columns) + ")";
            sql += " VALUES (" + string.Join(",", values) + ")";

            return sql;
        }

        private string GenerateSqlModified(DataRow dr, DataTable dtChange)
        {
            var sql = "UPDATE " +dtChange.TableName + " set ";
            List<string> fileds = new List<string>();

            var changedCols = new List<DataColumn>();//修改过的字段
            foreach (DataColumn dc in dtChange.Columns)
            {
                if (!dr[dc, DataRowVersion.Original].Equals(dr[dc, DataRowVersion.Current])) /* skipped Proposed as indicated by a commenter */
                    changedCols.Add(dc);
            }

            for (int i = 0; i < changedCols.Count; i++)
            {
                var f = changedCols[i];
                fileds.Add(f.ColumnName + "='" + dr[f.ColumnName].ToString() + "'");
            }
            sql += string.Join(",", fileds);
            sql += " where id=" + dr["id"].ToString();

            return sql;
        }



      





        private void Update(DataTable dt2)
        {
            string constr = "Data Source=I53470\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=mytestdb";// "server=localhost\\sqlserver2008;initial catalog=test;uid=sa;pwd=123456;";
            SqlConnection conn = new SqlConnection(constr);
            //设置select查询命令，SqlCommandBuilder要求至少有select命令
            SqlCommand selectCMD = new SqlCommand("select  * from Person", conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selectCMD);
            //上面的语句中使用select 0，不是为了查询出数据，而是要查询出表结构以向DataTable中填充表结构
            sda.Fill(dt);
            //先更新第1，2条数据的SName和SAge
            dt.Rows[0]["SName"] = "AAA";
            dt.Rows[0]["SAge"] = 33;
            dt.Rows[1]["SName"] = "BBB";
            dt.Rows[1]["SAge"] = 444;
            //然后使用RemoveAt删除第3，4条数据
            dt.Rows.RemoveAt(2);
            dt.Rows.RemoveAt(3);
            //使用Delete删除
            //dt.Rows[2].Delete();
            //dt.Rows[3].Delete();
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            //执行更新
            sda.Update(dt.GetChanges());
            //使DataTable保存更新
            dt.AcceptChanges();
        }
    }
}

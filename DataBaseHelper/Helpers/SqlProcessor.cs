using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper.Helpers
{
    class SqlProcessor
    {

        public void Save(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr.RowState)
                {
                    case DataRowState.Added:
                        break;
                    case DataRowState.Deleted:
                        break;
                    case DataRowState.Detached:
                        break;
                    case DataRowState.Modified:
                        break;
                    case DataRowState.Unchanged:
                        break;
                    default:
                        break;
                }
       
            }
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

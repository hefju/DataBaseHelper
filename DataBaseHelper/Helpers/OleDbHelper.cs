using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper.Helpers
{
    class OleDbHelper : IDBHelper
    {
        private static OleDbHelper singleton;
         private OleDbHelper() { }
         public static OleDbHelper getInstance()
        {
            if (singleton == null)
            {
                singleton = new OleDbHelper();
                singleton.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=test.mdb;User Id=admin;Jet OLEDB:Database Password=1;";//"Data Source=I53470\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=mytestdb"
            }
            return singleton;
        }

        #region 通用方法
         private OleDbConnection con;
        private String GetOleDbConnection()
        {
            return ConnectionString;
        }
        public string ConnectionString { get; set; }


        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command.
        /// </summary>
        /// <param name="command">the SqlCommand to be prepared</param>
        /// <param name="connection">a valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">a valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        private  void PrepareCommand(OleDbCommand command, OleDbConnection connection, OleDbTransaction transaction, CommandType commandType, string commandText, OleDbParameter[] commandParameters)
        {
            //if the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //associate the connection with the command
            command.Connection = connection;

            //set the command text (stored procedure name or OleDb statement)
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            //set the command type
            command.CommandType = commandType;

            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }

        /// <summary>
        /// This method is used to attach array's of OleDbParameters to an OleDbCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">an array of OleDbParameters tho be added to command</param>
        private  void AttachParameters(OleDbCommand command, OleDbParameter[] commandParameters)
        {
            foreach (OleDbParameter p in commandParameters)
            {
                //check for derived output value with no value assigned
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }

        #endregion



        #region GetData
        public  DataTable GetTable(string sql, params OleDbParameter[] param)//根据指定的SQL语句,返回DataTable
        {
            DataTable dt = new DataTable();
            try
            {
                String ConnStr = GetOleDbConnection();
                using (OleDbConnection conn = new OleDbConnection(ConnStr))
                {
                    OleDbCommand cmd = new OleDbCommand();
                    PrepareCommand(cmd, conn, (OleDbTransaction)null, CommandType.Text, sql, param);

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                    da.Fill(dt);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }

        public  int ExecSQL(string sql, params  OleDbParameter[] param)//执行SQL语句
        {
            int affect = 0;
            try
            {
                String ConnStr = GetOleDbConnection();
                using (OleDbConnection conn = new OleDbConnection(ConnStr))
                {
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    cmd.Parameters.AddRange(param);
                    conn.Open();
                    affect = cmd.ExecuteNonQuery();
                    conn.Close();
                    return affect;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return affect;
        }
        //public int ExecuteNonQuery(string sql)
        //{
        //    return ExecSQL(sql);
        //}
        public int ExecuteNonQuery2(string sql)
        {
            return ExecSQL(sql, new  OleDbParameter[0]);
        }
        /// <summary>
        /// 新增记录返回ID
        /// </summary>
        /// <param name="sql">插入语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="param">参数列表</param>
        /// <returns></returns>
        public  int InsertObject(string sql, string tableName, params  OleDbParameter[] param)//插入对象并返回ID
        {
            int rowID = 0;
            using (OleDbConnection connection = new OleDbConnection(GetOleDbConnection()))
            {
                connection.Open();
                using (OleDbTransaction trans = connection.BeginTransaction())
                {
                    OleDbCommand cmd = new OleDbCommand();
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = trans;
                        cmd.CommandText = sql;// "INSERT INTO...";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "select max(id) from " + tableName;// " SELECT @@INDENTITY as newID";

                        rowID = (Int32)cmd.ExecuteScalar();
                        trans.Commit();
                        connection.Close();
                    }
                    catch (Exception E)
                    {
                        trans.Rollback();
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
            return rowID;
        }


        public  object GetObject(string sql, params  OleDbParameter[] param)//返回执行结果的第一行第一列
        {
            try
            {
                string ConnStr = GetOleDbConnection();
                using (OleDbConnection conn = new OleDbConnection(ConnStr))
                {
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    PrepareCommand(cmd, conn, (OleDbTransaction)null, CommandType.Text, sql, param);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return null;
        }
        #endregion


        #region 执行sql字符串
        /// <summary>   
        /// 执行不带参数的SQL语句   
        /// </summary>   
        /// <param name="Sqlstr"></param>   
        /// <returns></returns>   
        public  int ExecuteSql(String Sqlstr)
        {
            String ConnStr = GetOleDbConnection();
            using (OleDbConnection conn = new OleDbConnection(ConnStr))
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return 1;
            }
        }
        public  object ExecuteSqlScalar(String Sqlstr)
        {
            String ConnStr = GetOleDbConnection();
            using (OleDbConnection conn = new OleDbConnection(ConnStr))
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;


                conn.Open();

                object obj;
                obj = cmd.ExecuteScalar();
                conn.Close();
                return obj;
            }
        }
        /// <summary>   
        /// 执行带参数的SQL语句   
        /// </summary>   
        /// <param name="Sqlstr">SQL语句</param>   
        /// <param name="param">参数对象数组</param>   
        /// <returns></returns>   
        public  int ExecuteSql(String Sqlstr, OleDbParameter[] param)
        {
            String ConnStr = GetOleDbConnection();
            using (OleDbConnection conn = new OleDbConnection(ConnStr))
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                cmd.Parameters.AddRange(param);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return 1;
            }
        }
        /// <summary>   
        /// 返回DataReader   
        /// </summary>   
        /// <param name="Sqlstr"></param>   
        /// <returns></returns>   
        public  OleDbDataReader ExecuteReader(String Sqlstr)
        {
            String ConnStr = GetOleDbConnection();
            OleDbConnection conn = new OleDbConnection(ConnStr);//返回DataReader时,是不可以用using()的   
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                conn.Open();
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);//关闭关联的Connection   
            }
            catch //(Exception ex)   
            {
                return null;
            }
        }
        /// <summary>   
        /// 执行SQL语句并返回数据表   
        /// </summary>   
        /// <param name="Sqlstr">SQL语句</param>   
        /// <returns></returns>   
        public  DataTable ExecuteDt(String Sqlstr)
        {
            String ConnStr = GetOleDbConnection();
            using (OleDbConnection conn = new OleDbConnection(ConnStr))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(Sqlstr, conn);
                DataTable dt = new DataTable();
                conn.Open();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
        }
        /// <summary>   
        /// 执行SQL语句并返回DataSet   
        /// </summary>   
        /// <param name="Sqlstr">SQL语句</param>   
        /// <returns></returns>   
        public  DataSet ExecuteDs(String Sqlstr)
        {
            String ConnStr = GetOleDbConnection();
            using (OleDbConnection conn = new OleDbConnection(ConnStr))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(Sqlstr, conn);
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds);
                conn.Close();
                return ds;
            }
        }
        #endregion
        #region 操作存储过程
        /// <summary>   
        /// 运行存储过程(已重载)   
        /// </summary>   
        /// <param name="procName">存储过程的名字</param>   
        /// <returns>存储过程的返回值</returns>   
        public int RunProc(string procName)
        {
            OleDbCommand cmd = CreateCommand(procName, null);
            cmd.ExecuteNonQuery();
            this.Close();
            return (int)cmd.Parameters["ReturnValue"].Value;
        }
        /// <summary>   
        /// 运行存储过程(已重载)   
        /// </summary>   
        /// <param name="procName">存储过程的名字</param>   
        /// <param name="prams">存储过程的输入参数列表</param>   
        /// <returns>存储过程的返回值</returns>   
        public int RunProc(string procName, OleDbParameter[] prams)
        {
            OleDbCommand cmd = CreateCommand(procName, prams);
            cmd.ExecuteNonQuery();
            this.Close();
            return (int)cmd.Parameters[0].Value;
        }
        /// <summary>   
        /// 运行存储过程(已重载)   
        /// </summary>   
        /// <param name="procName">存储过程的名字</param>   
        /// <param name="dataReader">结果集</param>   
        public void RunProc(string procName, out OleDbDataReader dataReader)
        {
            OleDbCommand cmd = CreateCommand(procName, null);
            dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        /// <summary>   
        /// 运行存储过程(已重载)   
        /// </summary>   
        /// <param name="procName">存储过程的名字</param>   
        /// <param name="prams">存储过程的输入参数列表</param>   
        /// <param name="dataReader">结果集</param>   
        public void RunProc(string procName, OleDbParameter[] prams, out OleDbDataReader dataReader)
        {
            OleDbCommand cmd = CreateCommand(procName, prams);
            dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        /// <summary>   
        /// 创建Command对象用于访问存储过程   
        /// </summary>   
        /// <param name="procName">存储过程的名字</param>   
        /// <param name="prams">存储过程的输入参数列表</param>   
        /// <returns>Command对象</returns>   
        private OleDbCommand CreateCommand(string procName, OleDbParameter[] prams)
        {
            // 确定连接是打开的   
            Open();
            //command = new OleDbCommand( sprocName, new OleDbConnection( ConfigManager.DALConnectionString ) );   
            OleDbCommand cmd = new OleDbCommand(procName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // 添加存储过程的输入参数列表   
            if (prams != null)
            {
                foreach (OleDbParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }
            // 返回Command对象   
            return cmd;
        }
        /// <summary>   
        /// 创建输入参数   
        /// </summary>   
        /// <param name="ParamName">参数名</param>   
        /// <param name="DbType">参数类型</param>   
        /// <param name="Size">参数大小</param>   
        /// <param name="Value">参数值</param>   
        /// <returns>新参数对象</returns>   
        public OleDbParameter MakeInParam(string ParamName, OleDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary>   
        /// 创建输出参数   
        /// </summary>   
        /// <param name="ParamName">参数名</param>   
        /// <param name="DbType">参数类型</param>   
        /// <param name="Size">参数大小</param>   
        /// <returns>新参数对象</returns>   
        public OleDbParameter MakeOutParam(string ParamName, OleDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }
        /// <summary>   
        /// 创建存储过程参数   
        /// </summary>   
        /// <param name="ParamName">参数名</param>   
        /// <param name="DbType">参数类型</param>   
        /// <param name="Size">参数大小</param>   
        /// <param name="Direction">参数的方向(输入/输出)</param>   
        /// <param name="Value">参数值</param>   
        /// <returns>新参数对象</returns>   
        public OleDbParameter MakeParam(string ParamName, OleDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            OleDbParameter param;
            if (Size > 0)
            {
                param = new OleDbParameter(ParamName, DbType, Size);
            }
            else
            {
                param = new OleDbParameter(ParamName, DbType);
            }
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
            {
                param.Value = Value;
            }
            return param;
        }
        #endregion
        #region 数据库连接和关闭
        /// <summary>   
        /// 打开连接池   
        /// </summary>   
        private void Open()
        {
            // 打开连接池   
            if (con == null)
            {
                //这里不仅需要using System.Configuration;还要在引用目录里添加   
                con = new OleDbConnection(GetOleDbConnection());
                con.Open();
            }
        }
        /// <summary>   
        /// 关闭连接池   
        /// </summary>   
        public void Close()
        {
            if (con != null)
                con.Close();
        }
        /// <summary>   
        /// 释放连接池   
        /// </summary>   
        public void Dispose()
        {
            // 确定连接已关闭   
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }
        #endregion



    
    }
}

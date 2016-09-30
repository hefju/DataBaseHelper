using DataBaseHelper.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseHelper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //http://www.111cn.net/database/mssqlserver/45135.htm

        }

        private void label3_Click(object sender, EventArgs e)
        {
            FrmAbout frm = new FrmAbout();
            frm.Show();
        }
        //参考文章  SqlDataAdapter.Update批量数据更新
        //http://www.cnblogs.com/moss_tan_jun/archive/2011/11/26/2263992.html
        //.NET进阶系列之四：深入DataTable
        //http://www.cnblogs.com/morvenhuang/archive/2008/11/17/1335271.html
        //使用 DataAdapter 执行数据源批量更新
        //http://www.voidcn.com/blog/xyd_linux/article/p-3424596.html
//建表语句
// DROP TABLE Person
//CREATE TABLE Person 
//(
//ID int primary key identity,
//LastName varchar(50),
//FirstName varchar(50),
//Address2 varchar(50),
//Age int
//) 

        private void button1_Click(object sender, EventArgs e)
        {
            //I53470\SQLEXPRESS
            SqlHelper db = SqlHelper.getInstance();//"Data Source=I53470\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=mytestdb"
            //var sql = "select Id, Username, Pwd, Age, RegisterDate, Address1 from Person";
            //var dt = db.ExecuteDataTable(sql);
            //dgv.DataSource = dt;
            //dgv.Columns["Id"].Visible = false;
            //dgv.Columns["Username"].HeaderText = "用户名";
            var sql = "SELECT top 10 * FROM  Person";
            var dt = db.ExecuteDataTable(sql);
            dt.TableName = "Person";
            dgv.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;
          //  dt.TableName = "Person";
            SqlProcessor sp = new SqlProcessor();
          int count=  sp.Save(dt);
          MessageBox.Show("保存成功:"+count.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbHelper db = OleDbHelper.getInstance();
            var sql = "SELECT  * FROM  skwtemp";
            var dt = db.GetTable(sql);
            dt.TableName = "skwtemp";
            dgv.DataSource = dt;

        }
    }
}

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
    /// <summary>
    /// 这套数据库操作程序不能做成类库, 因为不同的数据库, 引用了不同的dll, 实际程序开发的时候, 可能就用到其中一两种数据库,没有必要把5种数据的dll都打包在一起.
    /// 
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //WinUI.SetButtonImage(button12, "btnblue");
            //WinUI.SetButtonImage(pictureBox1, "btnblue");

            #region 使用label作为贴图按钮. pictureBox1也可以,但无法输入文字,如果再加一个label还不如直接用label.button12做贴图按钮在窗口非激活状态下有一个黑框.
            List<Image> imgs = new List<Image>();
            imgs.Add(Image.FromFile( @"images\btnblue0.png"));
            imgs.Add(Image.FromFile( @"images\btnblue1.png"));
            imgs.Add(Image.FromFile( @"images\btnblue2.png"));
            WinUI.SetLabelImage(label4, imgs);
            WinUI.SetLabelImage(label5, imgs);

            imgs = new List<Image>();
            imgs.Add(Image.FromFile(@"images\btngray0.png"));
            imgs.Add(Image.FromFile(@"images\btngray1.png"));
            imgs.Add(Image.FromFile(@"images\btngray2.png"));
            WinUI.SetLabelImage(label7, imgs);
            WinUI.SetLabelImage(label6, imgs);

            imgs = new List<Image>();
            imgs.Add(Image.FromFile(@"images\btngreen0.png"));
            imgs.Add(Image.FromFile(@"images\btngreen1.png"));
            imgs.Add(Image.FromFile(@"images\btngreen2.png"));
            WinUI.SetLabelImage(label11, imgs);
            WinUI.SetLabelImage(label10, imgs);

            imgs = new List<Image>();
            imgs.Add(Image.FromFile(@"images\btnred0.png"));
            imgs.Add(Image.FromFile(@"images\btnred1.png"));
            imgs.Add(Image.FromFile(@"images\btnred2.png"));
            WinUI.SetLabelImage(label8, imgs);
            WinUI.SetLabelImage(label9, imgs);

            imgs = new List<Image>();
            imgs.Add(Image.FromFile(@"images\btnyellow0.png"));
            imgs.Add(Image.FromFile(@"images\btnyellow1.png"));
            imgs.Add(Image.FromFile(@"images\btnyellow2.png"));
            WinUI.SetLabelImage(label13, imgs);
            WinUI.SetLabelImage(label12, imgs);
 
            #endregion
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //http://www.111cn.net/database/mssqlserver/45135.htm
            //label4
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
          int count=  sp.Save(dt,SqlHelper.getInstance());
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

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;
            SqlProcessor sp = new SqlProcessor();
            int count = sp.Save(dt, OleDbHelper.getInstance());
            MessageBox.Show("保存成功:" + count.ToString());
 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SQLiteHelper db = SQLiteHelper.getInstance();
            var sql = "SELECT  * FROM  PageProfile";
            var dt = db.GetTable(sql);
            dt.TableName = "PageProfile";
            dgv.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;
            SqlProcessor sp = new SqlProcessor();
            int count = sp.Save(dt, SQLiteHelper.getInstance());
            MessageBox.Show("保存成功:" + count.ToString());
        }

        private void label4_Click(object sender, EventArgs e)
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

        private void label5_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;
            //  dt.TableName = "Person";
            SqlProcessor sp = new SqlProcessor();
            int count = sp.Save(dt, SqlHelper.getInstance());
            MessageBox.Show("保存成功:" + count.ToString());
        }

        private void label7_Click(object sender, EventArgs e)
        {
            OleDbHelper db = OleDbHelper.getInstance();
            var sql = "SELECT  * FROM  skwtemp";
            var dt = db.GetTable(sql);
            dt.TableName = "skwtemp";
            dgv.DataSource = dt;

        }

        private void label6_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;
            SqlProcessor sp = new SqlProcessor();
            int count = sp.Save(dt, OleDbHelper.getInstance());
            MessageBox.Show("保存成功:" + count.ToString());
        }

        private void label11_Click(object sender, EventArgs e)
        {
            SQLiteHelper db = SQLiteHelper.getInstance();
            var sql = "SELECT  * FROM  PageProfile";
            var dt = db.GetTable(sql);
            dt.TableName = "PageProfile";
            dgv.DataSource = dt;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;
            SqlProcessor sp = new SqlProcessor();
            int count = sp.Save(dt, SQLiteHelper.getInstance());
            MessageBox.Show("保存成功:" + count.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}

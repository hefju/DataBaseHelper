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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlHelper helper = new SqlHelper("Server=ASUS-PC;Database=testdb;Trusted_Connection=sspi;");
           
        private void button1_Click(object sender, EventArgs e)
        {

            var sql = string.Format("select COUNT(*) from Users where UserName = '{1}' and  Password = '{0}'  ", textBox1.Text,
                textBox2.Text);
           var obj= helper.ExecuteScalar(sql);
           showresult(obj);
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
            var sql = string.Format("select COUNT(*) from Users where UserName = '{1}' and Password = '{0}' ", textBox1.Text,
                textBox3.Text);
            var obj = helper.ExecuteScalar(sql);
            showresult(obj);
        }
        private void showresult(object obj)
        {
             richTextBox1.Clear();
             string result = "";
             if (obj == null)
                 result = "null";
             else
                 result = obj.ToString();
             richTextBox1.AppendText(result);
        }

        DataTable dtMain;
        private void button4_Click(object sender, EventArgs e)
        {
            var sql = "select * from  Users";
            dtMain = helper.GetDataTable(sql);
            dtMain.TableName = "Users";
            dgv.DataSource = dtMain;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCreater sc = new SqlCreater();
           int count=  sc.Save(dtMain, helper);
           var msg = "保存成功.";
           if (count < 1)
               msg = "保存失败.";
           showresult(msg);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //dtMain.Clear();
            //for (int i = 1; i < 6; i++)
            //{
            //    var dr = dtMain.NewRow();
            //    dr["Id"] = Guid.NewGuid().ToString();
            //    dr["UserId"] = i;
            //    dr["UserName"] = "name" + i.ToString();
            //    dr["Password"] = "pwd" + i.ToString();
            //    dtMain.Rows.Add(dr);
            //}
            //button3_Click(null, null);
        }
    }
}

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
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
            this.Text = "关于";
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            richTextBox1.AppendText(
    @"数据库存储和检索一直是我需要做的工作, 大部分的需求都是把数据存储下来, 提供查询, 统计结果. " + Environment.NewLine +
    "电脑的资料收集,快速统计和查询是普通excel不能轻易做到的. 正是这样, 我写的程序才得以在日常工作中发挥作用." + Environment.NewLine +
    "在获取需求到编写程序过程中, 最令人感到繁琐的是sql语句Insert,Update. Select可以用*号偷懒," + Environment.NewLine +
    " Delete只要确定主键或者范围就基本上不会改动." + Environment.NewLine +
    "字段的变更是最常遇到的, 修改字段名, 增删字段都会造成Insert,Update重写." + Environment.NewLine +
    "如果字段有50个, 那么在中间删减个字段, 是比较痛苦的." + Environment.NewLine +
    "这个工具是希望系统能自动生成Insert,Update语言, 减少人为干预, 能够根据数据去sql语句.");
        }
    }
}

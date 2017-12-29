using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class SignForm : Form
    {
        public bool yes = false;
        public string name;
        public SignForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "MP10.ssk";
            label1.Text = "用户名";
            label2.Text = "密码";
            button1.Text = "登录";
            passw.PasswordChar='*';
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = uname.Text;
            string strconn = "Data Source = (local);Initial Catalog = DriverSchool;";
            strconn += "UID = " + uname.Text + ";Pwd = " + passw.Text +";";
            try
            {
                using (SqlConnection conn = new SqlConnection(strconn))
                {
                    conn.Open();
                    MessageBox.Show("登陆成功！");
                    yes = true;
                    Close();
                }
            }
            catch (Exception aaa) {
                MessageBox.Show("登录失败，不存在该管理员。");
            }
        }
    }
}

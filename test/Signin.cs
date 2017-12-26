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
            textBox2.PasswordChar='*';
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            string strconn = "Data Source = TheOldOfTang;Initial Catalog = DriverSchool;Integrated Security = SSPI";
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                conn.Open();
                string namew = textBox1.Text;
                string number = textBox2.Text;
                string tem;
                string order = "select password from managers ";
                order += "where MID = @MID";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = order;
                cmd.Parameters.Add(new SqlParameter("@MID", namew));
                cmd.Connection = conn;
                tem = (string)cmd.ExecuteScalar();
                if (tem == number)
                {
                    MessageBox.Show("登陆成功！");
                    yes = true;
                    Close();
                }
                else
                    MessageBox.Show("查无此人，请检查密码或用户名");
            }

        }
    }
}

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
        public int power = 0;
        public string name;
        public SignForm()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        /*  
         *  编写时间：2017-12-30,15:53
         *  编写人：唐胜洋
         *  功能：管理员登录响应
         * */
        private void button1_Click(object sender, EventArgs e)
        {
            string strconn = "Data Source = (local);Initial Catalog = DriverSchool;Integrated Security = SSPI";
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                conn.Open();
                string namew = uname.Text;
                string number = passw.Text;
                string tem;
                string order = "select PASSWORD as r1,POWER as r2 from manager ";
                order += "where MNAME = @MID";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = order;
                cmd.Parameters.Add(new SqlParameter("@MID", namew));
                cmd.Connection = conn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (!dr.Read())//SqlDataReader对象读取后要读数据必须先read();
                {
                    MessageBox.Show("无搜索结果.");
                    return;
                }
                string ttt = dr["r1"].ToString();
                if (ttt == number)
                {
                    MessageBox.Show("登陆成功！");
                    yes = true;
                    power = Convert.ToInt32(dr["r2"].ToString());
                    Close();
                }
                else
                    MessageBox.Show("查无此人，请检查密码或用户名");
            }
        }
    }
}

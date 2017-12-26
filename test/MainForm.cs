using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class MainForm : Form
    {
        SignForm f1;
        const int panellocax = 7;
        const int panellocay = 14;
        /*
         * 加载时，所有的panel都把左上角固定在(7,14);
         */
        #region 初始化参数设置
        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm(SignForm f)
        {
            f1 = f;
            InitializeComponent();
        }

        private void hide() {

        }


        /*
         *根据管理员的权限来设置可见。 
         */
        private void enable(int p) {
            if (p == 1) //后勤
            {
                menu.Items[4].Enabled = false;
                menu.Items[5].Enabled = false;
            }
            else if (p == 2)    //运营
            {
            }
            else if (p == 3)    //财务
            {
                menu.Items[4].Enabled = false;
                menu.Items[1].Enabled = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = f1.name;
            timer1.Enabled = true;
            hide();
            /*
            I_sex.Items.Add("男");
            I_sex.Items.Add("女");
            I_缴费.Items.Add("已缴费");
            I_缴费.Items.Add("未缴费");
            updatepanel.Location = new System.Drawing.Point(panellocax, panellocay);
            querypanel.Location = new System.Drawing.Point(panellocax, panellocay);
            pipeipanel.Location = new System.Drawing.Point(panellocax, panellocay);
            */
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void Form2_Close(object sender, EventArgs e) {
            timer1.Enabled = false;
        }
        #endregion

        #region 菜单响应相关
        /*
        private void 信息更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hide();
            updatepanel.Show();
        }

        private void 信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hide();
            querypanel.Show();
        }
        */
        #endregion

        #region 信息更新
        

        #endregion


        #region 查询响应函数

        #endregion

        
    }
}

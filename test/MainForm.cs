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
        string sqlconnect = "Data Sourcce = (local);DataBase = DriverSchool;";
        /*
         * 加载时，所有的panel都把左上角固定在(7,14);
         */
        #region 初始化参数设置
        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        public MainForm(SignForm f)
        {
            f1 = f;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        

        /*  
        *  编写时间：2017-12-30,15:54
        *  编写人：唐胜洋
        *  功能：根据管理员权限来设置可见,并且设置数据库连接角色。
        *  
        *  注意：如果编写代码有功能区分，记得在这里设置可见和不可见
        * */
        private void enable(int p) {
            if (p == 1)         //后勤
            {
                menu.Items[4].Enabled = false;
                menu.Items[5].Enabled = false;
                sqlconnect += "UID = n1;Pwd = 111";
            }
            else if (p == 2)    //运营
            {
                sqlconnect += "UID = n2;Pwd = 111";
            }
            else if (p == 3)    //财务
            {
                menu.Items[3].Enabled = false;
                menu.Items[1].Enabled = false;
                sqlconnect += "UID = n3;Pwd = 111";
            }
            else
            {
                sqlconnect += "UID = tsy;Pwd = 111";
            }
        }
        

 /*  
 *  编写时间：2017-12-30,14:54
 *  编写人：唐胜洋
 *  功能：主窗口加载函数
 *  最新更新时间:2017-12-30,15:54
 *  更新功能：注释
 * */
        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = f1.name;
            timer1.Enabled = true;
            enable(f1.power);
            /*
            I_sex.Items.Add("男");
            I_sex.Items.Add("女");
            I_缴费.Items.Add("已缴费");
            I_缴费.Items.Add("未缴费");
            updatepanel.Location = new System.Drawing.Point(panellocax, panellocay);
            querypanel.Location = new System.Drawing.Point(panellocax, panellocay);
            pipeipanel.Location = new System.Drawing.Point(panellocax, panellocay);
            一开始把所有panel统一设置左上角即可。编辑的时候放在不挡路的地方。
            */
        }
        /*  
        *  编写时间：2017-12-30,15:54
        *  编写人：唐胜洋
        *  功能：设置主窗口右下角时钟。
        * */
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

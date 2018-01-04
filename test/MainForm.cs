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
        private SignForm f1;
        const int panellocax = 7;
        const int panellocay = 14;
        const int rightbond = 586;
        private string sqlconnect;//这些东西可以直接用。
        private DataSet myset;
        private SqlDataAdapter da;
        private SqlCommandBuilder myCbd;
        private SqlCommand cmd;
        private SqlConnection conn;
        private BindingSource bing;
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
        *  编写时间：2018-1-1,20:15
        *  编写人：唐胜洋
        *  功能：初始化业务模块的内容
        *  更新：1-2，内容：支出模块初始化。
        * */
        private void bussiness_init() {
            for (int i = 2009; i <= 2018; i++) {
                b_inc_year.Items.Add(i);
                b_exp_year.Items.Add(i);
                b_exa_year.Items.Add(i);
            }
            for (int i = 1; i < 13; i++)
            {
                b_inc_mon.Items.Add(i);
                b_exp_mon.Items.Add(i);
                b_exa_mon.Items.Add(i);
            }
            for (int i = 1; i < 31; i++)
            {
                b_inc_day.Items.Add(i);
                b_exp_day.Items.Add(i);
                b_exa_day.Items.Add(i);
            }
            b_inc_year.SelectedItem = System.DateTime.Now.Year;
            b_inc_mon.SelectedItem = System.DateTime.Now.Month;
            b_inc_day.SelectedItem = System.DateTime.Now.Day;
            
            b_exp_year.SelectedItem = System.DateTime.Now.Year;
            b_exp_mon.SelectedItem = System.DateTime.Now.Month;
            b_exp_day.SelectedItem = System.DateTime.Now.Day;
            
            b_exa_year.SelectedItem = System.DateTime.Now.Year;
            b_exa_mon.SelectedItem = System.DateTime.Now.Month;
            b_exa_day.SelectedItem = System.DateTime.Now.Day;

            /*
             * 从数据库中读取下拉菜单
             * 
             */
            try
            {
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select IKNO from i_kind ";
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        b_inc_kind.Items.Add(dr.GetString(0));
                    }
                    cmd.CommandText = "select EKNO from e_kind ";
                    dr.Close();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        b_exp_kind.Items.Add(dr.GetString(0));
                    }
                    cmd.CommandText = "select SJNAME from subject ";
                    dr.Close();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        b_exa_kind.Items.Add(dr[0]);
                        b_sc_kind.Items.Add(dr[0]);
                    }

                    conn.Close();

                }
            }
            catch(Exception em)
            {
                MessageBox.Show("初始化查询模块出错！");
                this.Close();
            }
        }

        private void statics_init()
        {
            sta_s_sex.Items.Add("男");
            sta_s_sex.Items.Add("女");
            sta_s_sex.Items.Add("");
            sta_s_pro.Items.Add("1");
            sta_s_pro.Items.Add("2");
            sta_s_pro.Items.Add("3");
            sta_s_pro.Items.Add("4");
            sta_s_pro.Items.Add("5");
            sta_s_pro.Items.Add("");
            sta_s_pay.Items.Add(1);
            sta_s_pay.Items.Add(0);
            sta_s_pay.Items.Add("");
            sta_s_stat.Items.Add("在学");
            sta_s_stat.Items.Add("所有");
            sta_s_stat.Items.Add("毕业");
            sta_pic.Image = Image.FromFile("hhhh.png");
            sta_c_sex.Items.Add("男");
            sta_c_sex.Items.Add("女");
            for (int i = 2009; i <= 2018; i++) {
                sta_exa_y1.Items.Add(i);
                sta_exa_y2.Items.Add(i);
                sta_inc_y1.Items.Add(i);
                sta_inc_y2.Items.Add(i);
                sta_exp_y1.Items.Add(i);
                sta_exp_y2.Items.Add(i);
            }
            for (int i = 1; i < 13; i++) {
                sta_exa_m1.Items.Add(i);
                sta_exa_m2.Items.Add(i);
                sta_inc_m1.Items.Add(i);
                sta_inc_m2.Items.Add(i);
                sta_exp_m2.Items.Add(i);
                sta_exp_m1.Items.Add(i);
            }
            for (int i = 1; i < 31; i++)
            {
                sta_exa_d1.Items.Add(i);
                sta_exa_d2.Items.Add(i);
                sta_inc_d2.Items.Add(i);
                sta_inc_d1.Items.Add(i);
                sta_exp_d1.Items.Add(i);
                sta_exp_d2.Items.Add(i);
            }
            try
            {
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select LIC from car ";
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        sta_c_lic.Items.Add(dr.GetString(0));
                    }
                    dr.Close();
                    cmd.CommandText = "select IKNO from i_kind ";
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        sta_inc_kind.Items.Add(dr.GetString(0));
                    }
                    dr.Close();
                    cmd.CommandText = "select SJNAME from subject ";
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        sta_exa_sub.Items.Add(dr.GetString(0));
                    }
                    dr.Close();
                    cmd.CommandText = "select MNAME from manager ";
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        sta_inc_man.Items.Add(dr.GetString(0));
                        sta_exp_man.Items.Add(dr.GetString(0));
                    }
                    dr.Close();
                    cmd.CommandText = "select EKNO from expenditure ";
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        sta_exp_kind.Items.Add(dr.GetString(0));
                    }
                    conn.Close();

                }
            }
            catch(Exception em)
            {
                MessageBox.Show("初始化统计模块出错！");
                this.Close();
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
            L_mana.Text = f1.name;
            timer1.Enabled = true;
            enable(f1.power);
            switch (f1.power)
            {
                case 1:
                    sqlconnect = "Data Source = .;Database = DriverSchool;UID = n1;Pwd = 111";
                    break;
                case 2:
                    sqlconnect = "Data Source = .;Database = DriverSchool;UID = n2;Pwd = 111";
                    break;
                case 3:
                    sqlconnect = "Data Source = .;Database = DriverSchool;UID = n3;Pwd = 111";
                    break;
                case 4:
                    sqlconnect = "Data Source = .;Database = DriverSchool;UID = tsy;Pwd = 111";
                    break;
            }
            业务panel.Location = new System.Drawing.Point(panellocax, panellocay);
            统计panel.Location = new System.Drawing.Point(panellocax, panellocay);
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
            bussiness_init();
            statics_init();
        }
        /*  
        *  编写时间：2017-12-30,15:54
        *  编写人：唐胜洋
        *  功能：设置主窗口右下角时钟。
        * */
        private void timer1_Tick(object sender, EventArgs e)
        {
            sys_time.Text = DateTime.Now.ToString();
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
        /*  
        *  编写时间：2017-12-30,15:54
        *  编写人：唐胜洋
        *  功能：菜单响应，某菜单打开，其他菜单关闭，这个你们写一个功能就要在这里加一个，而且自己的也要。
        * */
        private void 联系管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            业务panel.Show();
            统计panel.Show();
        }


        private void 统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            统计panel.Show();
            业务panel.Hide();
        }

        #endregion

        #region 信息更新


        #endregion

        #region 查询响应函数

        #endregion


        #region 业务
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-1  20:28
         * 功能：下面这两个函数实现输入姓名自动找到学号、输入学号找到姓名
         */
        private void b_inc_sno_TextChanged(object sender, EventArgs e)
        {
            if(b_inc_sno.Text.Length == 8)
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string tem = "select SNAME from student where SNO = '" + b_inc_sno.Text + "'";
                cmd = new SqlCommand(tem, conn);
                b_inc_sna.Text = (string)cmd.ExecuteScalar();
                conn.Close();
            }
        }
        private void b_inc_sna_TextChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string tem = "select SNO from student where SNAME = '" + b_inc_sna.Text + "'";
                cmd = new SqlCommand(tem, conn);
                b_inc_sno.Text = (string)cmd.ExecuteScalar();
                conn.Close();
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-1  20:28
         * 功能：下面这个函数实现根据选择的种类来显示介绍
         */
        private void b_inc_kind_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string text;
                string tem = "select IINTRO from i_kind where IKNO = '" + b_inc_kind.Text + "'";
                cmd = new SqlCommand(tem, conn);
                text = (string)cmd.ExecuteScalar();
                b_inc_note.Text = text;
                conn.Close();
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  9:28
         * 功能：清除income输入数据
         */
        private void income_clear()
        {
            b_inc_sna.Text = "";
            b_inc_sno.Text = "";
            b_inc_money.Text = "";
            b_inc_kind.Text = "";
            b_inc_ino.Text = "";
        }

        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：收入界面点击函数
         */
        private void page_income_Click(object sender, EventArgs e)
        {
            income_clear();
            string strcmd = "select * from income";
            conn = new SqlConnection(sqlconnect);
            cmd = new SqlCommand(strcmd, conn);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            myCbd = new SqlCommandBuilder(da);
            myset = new DataSet();
            bing = new BindingSource();
            try
            {
                da.Fill(myset,"income");
                DataTable dt = myset.Tables["income"];
                dt.PrimaryKey = new DataColumn[] { dt.Columns["INO"] };
                bing.DataSource = myset;
                bing.DataMember = "income";
                bing.Filter = "";
                b_dataview.DataSource = bing;
                b_dataview.Width = Math.Min(b_dataview.Columns.Count * 100+50, rightbond);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("操作数据库失败");
            }

        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：提交响应
         */
        private void b_inc_sub_Click(object sender, EventArgs e)
        {
            try
            {
                da.DeleteCommand = myCbd.GetDeleteCommand();
                int cont = da.Update(myset.Tables["income"]);
                myset.Tables["income"].AcceptChanges();
                MessageBox.Show("成功更新了"+cont+"行数据.");
                myset.Clear();
                da.Fill(myset, "income");
                b_dataview.Refresh();
            }
            catch (Exception em)
            {
                myset.Tables["income"].RejectChanges();
                MessageBox.Show("提交失败");
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：删除响应
         */
        private void b_inc_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除所选一行数据？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int k = b_dataview.SelectedRows.Count;
                for(int i = k; i > 0; i--)
                {
                    string INO = b_dataview.SelectedRows[i - 1].Cells["INO"].Value.ToString();
                    DataRow tdr = myset.Tables["income"].Rows.Find(INO);
                    tdr.Delete();
                }
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：增加记录的响应
         */
        private void b_inc_add_Click(object sender, EventArgs e)
        {
            if (b_inc_sno.Text.Length < 1 || b_inc_kind.Text.Length < 1 || b_inc_money.Text.Length < 1 || b_inc_ino.Text.Length < 1)
            {
                MessageBox.Show("请完善信息!");
                return;
            }
            else if(!Regex.IsMatch(b_inc_sno.Text, @"^XY[0-9]{6}\s+$") && !Regex.IsMatch(b_inc_sno.Text, @"^XY[0-9]{6}$"))
            {
                MessageBox.Show("学号格式错误!");
                return;
            }
            else if (!Regex.IsMatch(b_inc_money.Text, @"[1-9]\d*"))
            {
                MessageBox.Show("请输入数字!");
                return;
            }
            else if (!Regex.IsMatch(b_inc_ino.Text, @"IN[0-9]{8}"))
            {
                MessageBox.Show("账单号格式错误!");
                return;
            }
            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "exec INSERT_INCOME @sno,@mon,@date,@ope,@ik,@ino";
                    cmd.Parameters.Add(new SqlParameter("@sno", b_inc_sno.Text));
                    cmd.Parameters.Add(new SqlParameter("@mon", b_inc_money.Text));
                    cmd.Parameters.Add(new SqlParameter("@date", b_inc_year.Text + "-" + b_inc_mon.Text + "-" + b_inc_day.Text));
                    cmd.Parameters.Add(new SqlParameter("@ope", f1.name));
                    cmd.Parameters.Add(new SqlParameter("@ik", b_inc_kind.Text));
                    cmd.Parameters.Add(new SqlParameter("@ino", b_inc_ino.Text));
                    int t = cmd.ExecuteNonQuery();
                    if (t <= 0)
                        throw new Exception();
                    MessageBox.Show("插入成功！");
                    conn.Close();
                }
                catch(Exception em)
                {
                    MessageBox.Show("插入失败，请检查输入。");
                }
            }
        }



        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  9:28
         * 功能：清除expend输入数据
         */
        private void expen_clear()
        {
            b_exp_lic.Text = "";
            b_exp_money.Text = "";
            b_exp_kind.Text = "";
            b_exp_bno.Text = "";
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：点击支出的响应
         */
        private void page_expend_Click(object sender, EventArgs e)
        {
            string strcmd = "select * from expenditure";
            conn = new SqlConnection(sqlconnect);
            cmd = new SqlCommand(strcmd, conn);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            myCbd = new SqlCommandBuilder(da);
            myset = new DataSet();
            bing = new BindingSource();
            try
            {
                da.Fill(myset, "expenditure");
                DataTable dt = myset.Tables["expenditure"];
                dt.PrimaryKey = new DataColumn[] { dt.Columns["BNO"] };
                bing.DataSource = myset;
                bing.DataMember = "expenditure";
                bing.Filter = "";
                b_dataview.DataSource = bing;
                b_dataview.Width = Math.Min(b_dataview.Columns.Count * 100+50, rightbond);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("操作数据库失败");
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：支出种类改变的时候的的响应
         */
        private void b_exp_kind_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string text;
                string tem = "select EINTRO from e_kind where EKNO = '" + b_exp_kind.Text + "'";
                cmd = new SqlCommand(tem, conn);
                text = (string)cmd.ExecuteScalar();
                b_exp_note.Text = text;
                conn.Close();
            }
        }

        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：点击提交的响应
         */
        private void b_exp_sub_Click(object sender, EventArgs e)
        {
            try
            {
                da.DeleteCommand = myCbd.GetDeleteCommand();
                int cont = da.Update(myset.Tables["expenditure"]);
                myset.Tables["expenditure"].AcceptChanges();
                MessageBox.Show("成功更新了" + cont + "行数据.");
                myset.Clear();
                da.Fill(myset, "expenditure");
                b_dataview.Refresh();
            }
            catch (Exception em)
            {
                myset.Tables["expenditure"].RejectChanges();
                MessageBox.Show("提交失败");
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：点击删除的响应
         */
        private void b_exp_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除所选一行数据？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int k = b_dataview.SelectedRows.Count;
                for (int i = k; i > 0; i--)
                {
                    string ENO = b_dataview.SelectedRows[i - 1].Cells["BNO"].Value.ToString();
                    DataRow tdr = myset.Tables["expenditure"].Rows.Find(ENO);
                    tdr.Delete();
                }
            }
        }

        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：出账模块点击增加的响应
         */
        private void b_exp_add_Click(object sender, EventArgs e)
        {
            if (b_exp_lic.Text.Length < 1 || b_exp_kind.Text.Length < 1 || b_exp_money.Text.Length < 1 || b_exp_bno.Text.Length < 1)
            {
                MessageBox.Show("请完善信息!");
                return;
            }
            else if (!Regex.IsMatch(b_exp_lic.Text, @"^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$"))
            {
                MessageBox.Show("车牌号格式错误!");
                return;
            }
            else if (!Regex.IsMatch(b_exp_money.Text, @"[1-9]\d*"))
            {
                MessageBox.Show("请输入数字!");
                return;
            }
            else if (!Regex.IsMatch(b_exp_bno.Text, @"EX[0-9]{8}"))
            {
                MessageBox.Show("账单号格式错误!");
                return;
            }
            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "exec INSERT_EXPENDITURE @lic,@mon,@date,@ope,@ek,@bno";
                    cmd.Parameters.Add(new SqlParameter("@lic", b_exp_lic.Text));
                    cmd.Parameters.Add(new SqlParameter("@mon", b_exp_money.Text));
                    cmd.Parameters.Add(new SqlParameter("@date", b_exp_year.Text + "-" + b_exp_mon.Text + "-" + b_exp_day.Text));
                    cmd.Parameters.Add(new SqlParameter("@ope", f1.name));
                    cmd.Parameters.Add(new SqlParameter("@ek", b_exp_kind.Text));
                    cmd.Parameters.Add(new SqlParameter("@bno", b_exp_bno.Text));
                    int t = cmd.ExecuteNonQuery();
                    if (t <= 0)
                        throw new Exception();
                    MessageBox.Show("插入成功！");
                    conn.Close();
                }
                catch (Exception em)
                {
                    MessageBox.Show("插入失败，请检查输入。");
                }
            }
        }


        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  9:28
         * 功能：清除exam输入数据
         */
        private void exam_clear()
        {
            b_exa_sna.Text = "";
            b_exa_sno.Text = "";
            b_exa_gra.Text = "";
            b_exa_kind.Text = "";
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：点击考试的响应
         */
        private void page_exam_Click(object sender, EventArgs e)
        {
            string strcmd = "select * from exam";
            conn = new SqlConnection(sqlconnect);
            cmd = new SqlCommand(strcmd, conn);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            myCbd = new SqlCommandBuilder(da);
            myset = new DataSet();
            bing = new BindingSource();
            try
            {
                da.Fill(myset, "exam");
                DataTable dt = myset.Tables["exam"];
                dt.PrimaryKey = new DataColumn[] { dt.Columns["SNO"],dt.Columns["SJNAME"] };
                bing.DataSource = myset;
                bing.DataMember = "exam";
                bing.Filter = "";
                b_dataview.DataSource = bing;
                b_dataview.Width = Math.Min(b_dataview.Columns.Count * 100+50, rightbond);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("操作数据库失败");
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  20:28
         * 功能：下面这两个函数实现输入姓名自动找到学号、输入学号找到姓名
         */
        private void b_exa_sno_TextChanged(object sender, EventArgs e)
        {
            if (b_exa_sno.Text.Length == 8)
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    string tem = "select SNAME from student where SNO = '" + b_exa_sno.Text + "'";
                    cmd = new SqlCommand(tem, conn);
                    b_exa_sna.Text = (string)cmd.ExecuteScalar();
                    conn.Close();
                }
        }
        private void b_exa_sna_TextChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string tem = "select SNO from student where SNAME = '" + b_exa_sna.Text + "'";
                cmd = new SqlCommand(tem, conn);
                b_exa_sno.Text = (string)cmd.ExecuteScalar();
                conn.Close();
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  20:48
         * 功能：科目选择变化之后介绍的变化
         */
        private void b_exa_kind_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string text;
                string tem = "select SINTRO from subject where SJNAME = '" + b_exa_kind.Text + "'";
                cmd = new SqlCommand(tem, conn);
                text = (string)cmd.ExecuteScalar();
                b_exa_note.Text = text;
                conn.Close();
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  20:48
         * 功能：增加一条考试记录
         */
        private void b_exa_add_Click(object sender, EventArgs e)
        {
            if (b_exa_sno.Text.Length < 1 || b_exa_kind.Text.Length < 1 || b_exa_gra.Text.Length < 1)
            {
                MessageBox.Show("请完善信息!");
                return;
            }
            else if (!Regex.IsMatch(b_exa_sno.Text, @"^XY[0-9]{6}\s+$") && !Regex.IsMatch(b_exa_sno.Text, @"^XY[0-9]{6}$"))
            {
                MessageBox.Show("学号格式错误!");
                return;
            }
            else if (!Regex.IsMatch(b_exa_gra.Text, @"[1-9]\d*"))
            {
                MessageBox.Show("请输入数字!");
                return;
            }
            else if(Convert.ToInt32(b_exa_gra.Text)<0 || Convert.ToInt32(b_exa_gra.Text) >=100 )
            {
                MessageBox.Show("输入数字不合法!");
                return;
            }
            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "exec INSERT_EXAM @sno,@date,@ek,@grade";
                    cmd.Parameters.Add(new SqlParameter("@sno", b_exa_sno.Text));
                    cmd.Parameters.Add(new SqlParameter("@grade", Convert.ToInt32(b_exa_gra.Text)));
                    cmd.Parameters.Add(new SqlParameter("@date", b_exa_year.Text + "-" + b_exa_mon.Text + "-" + b_exa_day.Text));
                    cmd.Parameters.Add(new SqlParameter("@ek", Convert.ToInt16(b_exa_kind.Text)));
                    int t = cmd.ExecuteNonQuery();
                    if (t <= 0)
                        throw new Exception();
                    MessageBox.Show("插入成功！");
                    conn.Close();
                }
                catch (Exception em)
                {
                    MessageBox.Show("插入失败，请检查输入。");
                }
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  20:48
         * 功能：删除考试数据的响应
         */
        private void b_exa_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除所选行数据？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int k = b_dataview.SelectedRows.Count;
                for (int i = k; i > 0; i--)
                {
                    string[] aaa = { b_dataview.SelectedRows[i - 1].Cells["SNO"].Value.ToString(),
                        b_dataview.SelectedRows[i - 1].Cells["SJNAME"].Value.ToString()};
                    DataRow tdr = myset.Tables["exam"].Rows.Find(aaa);
                    tdr.Delete();
                }
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  20:48
         * 功能：提交考试记录删除
         */
        private void b_exa_sub_Click(object sender, EventArgs e)
        {
            try
            {
                da.DeleteCommand = myCbd.GetDeleteCommand();
                int cont = da.Update(myset.Tables["exam"]);
                myset.Tables["exam"].AcceptChanges();
                MessageBox.Show("成功更新了" + cont + "行数据.");
                myset.Clear();
                da.Fill(myset, "exam");
                b_dataview.Refresh();
            }
            catch (Exception em)
            {
                myset.Tables["exam"].RejectChanges();
                MessageBox.Show("提交失败");
            }
        }

      



        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：点击学员教练的响应
         */
        private void page_sc_Click(object sender, EventArgs e)
        {
            string strcmd = "select * from sc";
            conn = new SqlConnection(sqlconnect);
            cmd = new SqlCommand(strcmd, conn);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            myCbd = new SqlCommandBuilder(da);
            myset = new DataSet();
            bing = new BindingSource();
            try
            {
                da.Fill(myset, "sc");
                DataTable dt = myset.Tables["sc"];
                dt.PrimaryKey = new DataColumn[] { dt.Columns["SNO"],dt.Columns["CNO"], dt.Columns["SJNAME"] };
                bing.DataSource = myset;
                bing.DataMember = "sc";
                bing.Filter = "";
                b_dataview.DataSource = bing;
                b_dataview.Width = Math.Min(b_dataview.Columns.Count * 100+50,rightbond);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("操作数据库失败");
            }
        }
        
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  9:38
         * 功能：下面4个函数是根据名字找号码
         */
        private void b_sc_sna_TextChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string tem = "select SNO from student where SNAME = '" + b_sc_sna.Text + "'";
                cmd = new SqlCommand(tem, conn);
                b_sc_sno.Text = (string)cmd.ExecuteScalar();
                conn.Close();
            }
        }

        private void b_sc_sno_TextChanged(object sender, EventArgs e)
        {
            if (b_sc_sno.Text.Length == 8)
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    string tem = "select SNAME from student where SNO = '" + b_sc_sno.Text + "'";
                    cmd = new SqlCommand(tem, conn);
                    b_sc_sna.Text = (string)cmd.ExecuteScalar();
                    conn.Close();
                }
        }

        private void b_sc_cno_TextChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string tem = "select CNAME from coach where CNO = '" + b_sc_cno.Text + "'";
                cmd = new SqlCommand(tem, conn);
                b_sc_cna.Text = (string)cmd.ExecuteScalar();
                if (Regex.IsMatch(b_sc_cno.Text, @"^JL[0-9]{6}\s+$") || Regex.IsMatch(b_sc_cno.Text, @"^JL[0-9]{6}$"))
                {
                    try
                    {
                        cmd = new SqlCommand("select dbo.GET_COACH_TEACH_NUMBER_NOW('" + b_sc_cno.Text + "')",conn);
                        int temm= (int)cmd.ExecuteScalar();
                        L_sc_now.Text = temm.ToString();
                    }
                    catch (Exception em)
                    {

                    }
                }
                conn.Close();
            }
        }

        private void b_sc_cna_TextChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string tem = "select CNO from coach where CNAME = '" + b_sc_cna.Text + "'";
                cmd = new SqlCommand(tem, conn);
                b_sc_cno.Text = (string)cmd.ExecuteScalar();
                conn.Close();
            }
        }

        private void b_sc_kind_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string text;
                string tem = "select SINTRO from subject where SJNAME = '" + b_sc_kind.Text + "'";
                cmd = new SqlCommand(tem, conn);
                text = (string)cmd.ExecuteScalar();
                b_sc_note.Text = text;
                conn.Close();
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  20:48
         * 功能：增加一条练车匹配记录
         */
        private void b_sc_add_Click(object sender, EventArgs e)
        {
            if (b_sc_sno.Text.Length < 1 || b_sc_kind.Text.Length < 1 || b_sc_cno.Text.Length < 1)
            {
                MessageBox.Show("请完善信息!");
                return;
            }
            else if (!Regex.IsMatch(b_sc_sno.Text, @"^XY[0-9]{6}\s+$") && !Regex.IsMatch(b_sc_sno.Text, @"^XY[0-9]{6}$"))
            {
                MessageBox.Show("学号格式错误!");
                return;
            }
            else if (!Regex.IsMatch(b_sc_cno.Text, @"^JL[0-9]{6}\s+$") && !Regex.IsMatch(b_sc_cno.Text, @"^JL[0-9]{6}$"))
            {
                MessageBox.Show("工号格式错误!");
                return;
            }
            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "exec INSERT_C_S @sno,@ek,@cno";
                    cmd.Parameters.Add(new SqlParameter("@sno", b_sc_sno.Text));
                    cmd.Parameters.Add(new SqlParameter("@cno", b_sc_cno.Text));
                    cmd.Parameters.Add(new SqlParameter("@ek", Convert.ToInt16(b_sc_kind.Text)));
                    int t = cmd.ExecuteNonQuery();
                    if (t <= 0)
                        throw new Exception();
                    MessageBox.Show("插入成功！");
                    conn.Close();
                }
                catch (Exception em)
                {
                    MessageBox.Show("插入失败，请检查输入。");
                }
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  10:00
         * 功能：点击删除的响应
         */
        private void b_sc_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除所选行数据？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int k = b_dataview.SelectedRows.Count;
                for (int i = k; i > 0; i--)
                {
                    object[] aaa = { b_dataview.SelectedRows[i - 1].Cells["SNO"].Value.ToString(),
                        b_dataview.SelectedRows[i - 1].Cells["CNO"].Value.ToString(),
                        Convert.ToInt32(b_dataview.SelectedRows[i - 1].Cells["SJNAME"].Value.ToString()),
                    };
                    DataRow tdr = myset.Tables["sc"].Rows.Find(aaa);
                    tdr.Delete();
                }
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  10:00
         * 功能：点击提交的响应
         */
        private void b_sc_sub_Click(object sender, EventArgs e)
        {
            try
            {
                da.DeleteCommand = myCbd.GetDeleteCommand();
                int cont = da.Update(myset.Tables["sc"]);
                myset.Tables["sc"].AcceptChanges();
                MessageBox.Show("成功更新了" + cont + "行数据.");
                myset.Clear();
                da.Fill(myset, "sc");
                b_dataview.Refresh();
            }
            catch (Exception em)
            {
                myset.Tables["sc"].RejectChanges();
                MessageBox.Show("提交失败");
            }
        }



        /**
         * 编写人：唐胜洋
         * 时间：2018-1-2  16:28
         * 功能：点击教练车辆的响应
         */
        private void page_cc_Click(object sender, EventArgs e)
        {
            string strcmd = "select * from cc";
            conn = new SqlConnection(sqlconnect);
            cmd = new SqlCommand(strcmd, conn);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            myCbd = new SqlCommandBuilder(da);
            myset = new DataSet();
            bing = new BindingSource();
            try
            {
                da.Fill(myset, "cc");
                DataTable dt = myset.Tables["cc"];
                dt.PrimaryKey = new DataColumn[] { dt.Columns["CNO"], dt.Columns["LIC"] };
                bing.DataSource = myset;
                bing.DataMember = "cc";
                bing.Filter = "";
                b_dataview.DataSource = bing;
                b_dataview.Width = Math.Min(b_dataview.Columns.Count * 100 + 50, rightbond);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("操作数据库失败");
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  12:28
         * 功能：下面两个函数实现教练名字和工号自动对应
         */
        private void b_cc_cno_TextChanged(object sender, EventArgs e)
        {
            if (b_cc_cno.Text.Length == 8)
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    string tem = "select CNAME from coach where CNO = '" + b_cc_cno.Text + "'";
                    cmd = new SqlCommand(tem, conn);
                    b_cc_cna.Text = (string)cmd.ExecuteScalar();
                }
        }

        private void b_cc_cna_TextChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sqlconnect))
            {
                conn.Open();
                string tem = "select CNO from coach where CNAME = '" + b_cc_cna.Text + "'";
                cmd = new SqlCommand(tem, conn);
                b_cc_cno.Text = (string)cmd.ExecuteScalar();
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  12:28
         * 功能：点击增加一条教练车辆匹配响应
         */
        private void b_cc_add_Click(object sender, EventArgs e)
        {
            if (b_cc_cno.Text.Length < 1 || b_cc_lic.Text.Length < 1)
            {
                MessageBox.Show("请完善信息!");
                return;
            }
            else if (!Regex.IsMatch(b_cc_cno.Text, @"^JL[0-9]{6}\s+$") && !Regex.IsMatch(b_cc_cno.Text, @"^JL[0-9]{6}$"))
            {
                MessageBox.Show("工号格式错误!");
                return;
            }
            else if (!Regex.IsMatch(b_cc_lic.Text, @"^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$"))
            {
                MessageBox.Show("车牌号格式错误!");
                return;
            }
            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "exec INSERT_C_C @LIC,@CNO";
                    cmd.Parameters.Add(new SqlParameter("@CNO", b_cc_cno.Text));
                    cmd.Parameters.Add(new SqlParameter("@LIC", b_cc_lic.Text));
                    int t = cmd.ExecuteNonQuery();
                    if (t <= 0)
                        throw new Exception();
                    MessageBox.Show("插入成功！");
                    conn.Close();
                }
                catch (Exception em)
                {
                    MessageBox.Show("插入失败，请检查输入。");
                }
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  12:28
         * 功能：点击教练车辆删除的响应
         */
        private void b_cc_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除所选行数据？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int k = b_dataview.SelectedRows.Count;
                for (int i = k; i > 0; i--)
                {
                    string[] aaa = { b_dataview.SelectedRows[i - 1].Cells["CNO"].Value.ToString(),
                        b_dataview.SelectedRows[i - 1].Cells["LIC"].Value.ToString()};
                    DataRow tdr = myset.Tables["cc"].Rows.Find(aaa);
                    tdr.Delete();
                }
            }
        }
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-3  12:28
         * 功能：点击教练车辆提交的响应
         */
        private void b_cc_sub_Click(object sender, EventArgs e)
        {
            try
            {
                da.DeleteCommand = myCbd.GetDeleteCommand();
                int cont = da.Update(myset.Tables["cc"]);
                myset.Tables["cc"].AcceptChanges();
                MessageBox.Show("成功更新了" + cont + "行数据.");
                myset.Clear();
                da.Fill(myset, "cc");
                b_dataview.Refresh();
            }
            catch (Exception em)
            {
                myset.Tables["cc"].RejectChanges();
                MessageBox.Show("提交失败");
            }
        }


        #endregion

        #region 统计

        /*
         * 下面五个函数对应着统计学员模块的五个删除
         * 编写人：唐胜洋
         * 编写时间：2018-1-3 15:22
         * */
        private void sta_s_cno_clear_Click(object sender, EventArgs e)
        {
            sta_s_cno.Text = "";
        }

        private void sta_s_sex_clear_Click(object sender, EventArgs e)
        {
            sta_s_sex.Text = "";
        }

        private void sta_s_pay_clear_Click(object sender, EventArgs e)
        {
            sta_s_pay.Text = "";
        }

        private void sta_s_stat_clear_Click(object sender, EventArgs e)
        {
            sta_s_stat.Text = "";
        }

        private void sta_s_pro_clear_Click(object sender, EventArgs e)
        {
            sta_s_pro.Text = "";
        }

        private void L_sta_s_cna_clear_Click(object sender, EventArgs e)
        {
            sta_s_cna.Text = "";
        }

        /*
         * 编写人：唐胜洋
         * 编写时间：2018-1-3 18:52
         * 功能：统计学生模块的开始查找
         **/
        private void sta_s_go_Click(object sender, EventArgs e)
        {
            sta_see.Items.Clear();
            sta_see.Columns.Clear();
            sta_see.Columns.Add("学号"); sta_see.Columns.Add("姓名");
            sta_see.Columns.Add("电话"); sta_see.Columns.Add("入学");
            sta_see.Columns.Add("进度"); sta_see.Columns.Add("及格率");
            sta_see.Columns.Add("性别");
            sta_see.View = View.Details;
            if (sta_s_cno.Text.Length > 0 && !Regex.IsMatch(sta_s_cno.Text, @"^JL[0-9]{6}\s+$"))
            {
                MessageBox.Show("教练编号输入不正确.");
                return;
            }
            if (sta_s_stat.Text.Length < 1)
            {
                MessageBox.Show("状态不能为空.");
                return;
            }
            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string from = "select * , dbo.GET_PASS_RATE(SNO) AS PASS from ";
                    string sql = " where SNO = SNO ";
                    if (sta_s_cno.Text.Length > 0)
                    {
                        sql += "and IS_S(SNO,@cno) >= 1 ";
                        cmd.Parameters.Add(new SqlParameter("@cno", sta_s_cno.Text));
                    }
                    if (sta_s_cna.Text.Length > 0)
                    {
                        sql += "and CON = dbo.SNA_SNO(@cna) ";
                        cmd.Parameters.Add(new SqlParameter("@cna", sta_s_cna.Text));
                    }
                    if (sta_s_pay.Text.Length > 0)
                    {
                        sql += "and PAY = @pay ";
                        cmd.Parameters.Add(new SqlParameter("@pay", sta_s_pay.Text));
                    }
                    if (sta_s_sex.Text.Length > 0)
                    {
                        sql += "and SEX = @sex ";
                        cmd.Parameters.Add(new SqlParameter("@sex", sta_s_sex.Text));
                    }
                    if (sta_s_pro.Text.Length > 0)
                    {
                        sql += "and progress = @pro ";
                        cmd.Parameters.Add(new SqlParameter("@pro", sta_s_pro.Text));
                    }

                    if (sta_s_stat.Text == "在学") { from += " s_atschool "; }
                    else if (sta_s_stat.Text == "毕业") { from += " s_not_atschool "; }
                    else { from += "student "; }
                    cmd.CommandText = from + sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    sta_see.BeginUpdate();
                    while (dr.Read())
                    {
                        ListViewItem tem = new ListViewItem();
                        tem.Text = (string)dr["SNO"];
                        tem.SubItems.Add((string)dr["SNAME"]);
                        tem.SubItems.Add((string)dr["PHONE"]);
                        tem.SubItems.Add(dr["PAY"].ToString());
                        tem.SubItems.Add(dr["PROGRESS"].ToString());
                        tem.SubItems.Add(dr["PASS"].ToString());
                        tem.SubItems.Add(dr["SEX"].ToString());
                        sta_see.Items.Add(tem);
                    }
                    sta_see.EndUpdate();
                }
                catch(Exception em)
                {
                    MessageBox.Show("出错啦");
                }
            }
            
        }

        /*
         * 下面三个函数对应着统计教练模块的五个删除
         * 编写人：唐胜洋
         * 编写时间：2018-1-3 18:52
         * */
        private void sta_c_sno_clear_Click(object sender, EventArgs e)
        {
            sta_c_sno.Text = "";
        }

        private void sta_c_clear_Click(object sender, EventArgs e)
        {
            sta_c_sex.Text = "";
        }

        private void sta_c_lic_clear_Click(object sender, EventArgs e)
        {
            sta_c_lic.Text = "";
        }
        /*
         * 编写人：唐胜洋
         * 编写时间：2018-1-3 15:22
         * 功能：开始找教练
         **/
        private void sta_c_go_Click(object sender, EventArgs e)
        {
            sta_see.Items.Clear();
            sta_see.Columns.Clear();
            sta_see.Columns.Add("工号"); sta_see.Columns.Add("姓名");
            sta_see.Columns.Add("性别"); sta_see.Columns.Add("电话");
            sta_see.Columns.Add("及格率");
            sta_see.View = View.Details;
            if (sta_c_sno.Text.Length > 0 && !Regex.IsMatch(sta_c_sno.Text, @"^XY[0-9]{6}\s+$") && !Regex.IsMatch(sta_c_sno.Text, @"^XY[0-9]{6}$"))
            {
                MessageBox.Show("学员编号输入不正确.");
                return;
            }
            else if (sta_c_lic.Text.Length>0 && !Regex.IsMatch(sta_c_lic.Text, @"^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$"))
            {
                MessageBox.Show("车牌号输入不正确.");
                return;
            }
            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string from = "select * , dbo.GET_C_PASS_RATE(CNO) AS PASS from coach";
                    string sql = " where CNO = CNO ";
                    if (sta_c_sno.Text.Length > 0)
                    {
                        sql += "and dbo.IS_S(@sno,CNO) >= 1 ";
                        cmd.Parameters.Add(new SqlParameter("@sno", sta_c_sno.Text));
                    }
                    if (sta_c_lic.Text.Length > 0)
                    {
                        sql += "and dbo.IS_C(@lic,CNO) >= 1 ";
                        cmd.Parameters.Add(new SqlParameter("@lic", sta_c_lic.Text));
                    }
                    if (sta_c_sex.Text.Length > 0)
                    {
                        sql += "and SEX = @sex ";
                        cmd.Parameters.Add(new SqlParameter("@sex", sta_c_sex.Text));
                    }
                    cmd.CommandText = from + sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    sta_see.BeginUpdate();
                    while (dr.Read())
                    {
                        ListViewItem tem = new ListViewItem();
                        tem.Text = (string)dr["CNO"];
                        tem.SubItems.Add((string)dr["CNAME"]);
                        tem.SubItems.Add(dr["SEX"].ToString());
                        tem.SubItems.Add((string)dr["PHONE"]);
                        tem.SubItems.Add(dr["PASS"].ToString());
                        sta_see.Items.Add(tem);
                    }
                    sta_see.EndUpdate();
                }
                catch (Exception em)
                {
                    MessageBox.Show("出错啦");
                }
            }
        }
        /*
         * 下面四个函数对应着统计考试模块的五个删除
         * 编写人：唐胜洋
         * 编写时间：2018-1-3 19:52
         * */
        private void sta_exa_sno_clear_Click(object sender, EventArgs e)
        {
            sta_exa_sno.Text = "";
        }

        private void sta_exa_sub_clear_Click(object sender, EventArgs e)
        {
            sta_exa_sub.Text = "";
        }

        private void sta_exa_gra_clear_Click(object sender, EventArgs e)
        {
            sta_exa_gra1.Text = "";
            sta_exa_gra2.Text = "";
        }

        private void sta_exa_time_clear_Click(object sender, EventArgs e)
        {
            sta_exa_y1.Text = ""; sta_exa_y2.Text = "";
            sta_exa_m1.Text = ""; sta_exa_m2.Text = "";
            sta_exa_d1.Text = ""; sta_exa_d2.Text = "";
        }
        /*
         * 编写人：唐胜洋
         * 编写时间：2018-1-3 19:55
         * 功能:统计考试信息
         **/
        private void sta_exa_go_Click(object sender, EventArgs e)
        {
            sta_see.Items.Clear();
            sta_see.Columns.Clear();
            sta_see.Columns.Add("学号"); sta_see.Columns.Add("姓名");
            sta_see.Columns.Add("科目"); sta_see.Columns.Add("成绩");
            sta_see.Columns.Add("时间");
            sta_see.View = View.Details;
            if (sta_exa_sno.Text.Length > 0 && !Regex.IsMatch(sta_c_sno.Text, @"^XY[0-9]{6}\s+$") && !Regex.IsMatch(sta_c_sno.Text, @"^XY[0-9]{6}$"))
            {
                MessageBox.Show("学员编号输入不正确.");
                return;
            }
            else if (sta_exa_gra1.Text.Length > 0 && !Regex.IsMatch(sta_exa_gra2.Text, @"^(\d{1,2}|100)$"))
            {
                MessageBox.Show("分数输入不正确.");
                return;
            }
            else if (sta_exa_gra2.Text.Length > 0 && !Regex.IsMatch(sta_exa_gra2.Text, @"^(\d{1,2}|100)$"))
            {
                MessageBox.Show("分数输入不正确.");
                return;
            }
            else if( (sta_exa_gra2.Text.Length ^ sta_exa_gra1.Text.Length) == 1)
            {
                MessageBox.Show("请输入分数完整范围.");
                return;
            }
            else if( (sta_exa_y1.Text.Length|sta_exa_y2.Text.Length | sta_exa_m2.Text.Length | sta_exa_m2.Text.Length 
                | sta_exa_d1.Text.Length| sta_exa_d2.Text.Length)==1 && (sta_exa_y1.Text.Length 
                & sta_exa_y2.Text.Length & sta_exa_m2.Text.Length & sta_exa_m2.Text.Length 
                & sta_exa_d1.Text.Length & sta_exa_d2.Text.Length) == 0)
            {
                MessageBox.Show("请输入日期完整范围.");
                return;
            }
            
            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string from = "select *  from exam_static ";
                    string sql = " where SNO = SNO ";
                    if (sta_exa_sno.Text.Length > 0)
                    {
                        sql += "and SNO = @sno ";
                        cmd.Parameters.Add(new SqlParameter("@sno", sta_exa_sno.Text));
                    }
                    if (sta_exa_sub.Text.Length > 0)
                    {
                        sql += "and SJNAME = @sub ";
                        cmd.Parameters.Add(new SqlParameter("@sub", sta_exa_sub.Text));
                    }
                    if (sta_exa_gra1.Text.Length > 0)
                    {
                        sql += " and GRADE >= @g1 and GRADE <= @g2 ";
                        cmd.Parameters.Add(new SqlParameter("@g1", Convert.ToInt32(sta_exa_gra1.Text)));
                        cmd.Parameters.Add(new SqlParameter("@g2", Convert.ToInt32(sta_exa_gra2.Text)));
                    }
                    if (sta_exa_y1.Text.Length > 0)
                    {
                        string date1,date2;
                        date1 = sta_exa_y1.Text + "-" + sta_exa_m1.Text + "-" + sta_exa_d1.Text;
                        date2 = sta_exa_y2.Text + "-" + sta_exa_m2.Text + "-" + sta_exa_d2.Text;
                        sql += "and EDATE >= @d1 and EDATE <= @d2";
                        cmd.Parameters.Add(new SqlParameter("@d1", date1));
                        cmd.Parameters.Add(new SqlParameter("@d2", date2));
                    }
                    cmd.CommandText = from + sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    sta_see.BeginUpdate();
                    while (dr.Read())
                    {
                        ListViewItem tem = new ListViewItem();
                        tem.Text = (string)dr["SNO"];
                        tem.SubItems.Add((string)dr["SNAME"]);
                        tem.SubItems.Add(dr["SJNAME"].ToString());
                        tem.SubItems.Add(dr["GRADE"].ToString());
                        tem.SubItems.Add(dr["EDATE"].ToString());
                        sta_see.Items.Add(tem);
                    }
                    sta_see.EndUpdate();
                }
                catch (Exception em)
                {
                    MessageBox.Show("出错啦");
                }
            }
        }


        /*
         * 编写人:唐胜洋
         * 时间：2018-1-3 20:54
         * 功能：针对选中的种类发送提示信息
         * 
         * */
        private void sta_inc_kind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sta_inc_kind.Text.Length == 0)
                return;
            try
            {
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select IINTRO from i_kind where IKNO = '" + sta_inc_kind.Text +"'";
                    L_sta_inc_tip.Text = (string)cmd.ExecuteScalar();
                }

            }
            catch (Exception em) {
                MessageBox.Show("获取提示失败");
            }
        }

        /*
         * 编写人:唐胜洋
         * 时间：2018-1-3 20:54
         * 功能：下面五个函数都是清除输入的
         * 
         * */
        private void sta_inc_sno_clear_Click(object sender, EventArgs e)
        {
            sta_inc_sno.Text = "";
        }

        private void sta_inc_man_clear_Click(object sender, EventArgs e)
        {
            sta_inc_man.Text = "";
        }

        private void sta_inc_y1_cash_clear_Click(object sender, EventArgs e)
        {
            sta_inc_m1.Text = "";
            sta_inc_m2.Text = "";
        }

        private void sta_inc_time_clear_Click(object sender, EventArgs e)
        {
            sta_inc_y1.Text = ""; sta_inc_m1.Text = ""; sta_inc_d1.Text = "";
            sta_inc_y2.Text = ""; sta_inc_m2.Text = ""; sta_inc_d2.Text = "";
        }

        private void sta_inc_kind_clear_Click(object sender, EventArgs e)
        {
            sta_inc_kind.Text = "";
            L_sta_inc_tip.Text = "注:";
        }
        /*
         * 编写人:唐胜洋
         * 时间：2018-1-3 20:54
         * 功能：查询入账
         * 
         * */
        private void sta_inc_go_Click(object sender, EventArgs e)
        {
            sta_see.Items.Clear();
            sta_see.Columns.Clear();
            sta_see.Columns.Add("账单号"); sta_see.Columns.Add("学号");
            sta_see.Columns.Add("种类"); sta_see.Columns.Add("金额");
            sta_see.Columns.Add("时间"); sta_see.Columns.Add("操作员");
            sta_see.View = View.Details;
            if (sta_inc_sno.Text.Length > 0 && !Regex.IsMatch(sta_inc_sno.Text, @"^XY[0-9]{6}\s+$") && !Regex.IsMatch(sta_inc_sno.Text, @"^XY[0-9]{6}$"))
            {
                MessageBox.Show("学员编号输入不正确.");
                return;
            }
            else if (sta_inc_c1.Text.Length > 0 && !Regex.IsMatch(sta_inc_c2.Text, @"[1-9]\d*"))
            {
                MessageBox.Show("金额输入不正确.");
                return;
            }
            else if (sta_inc_c2.Text.Length > 0 && !Regex.IsMatch(sta_inc_c2.Text, @"^(\d{1,2}|100)$"))
            {
                MessageBox.Show("金额输入不正确.");
                return;
            }
            else if ((sta_inc_c2.Text.Length ^ sta_inc_c1.Text.Length) == 1)
            {
                MessageBox.Show("请输入分数完整范围.");
                return;
            }
            else if ((sta_inc_y1.Text.Length | sta_inc_y2.Text.Length | sta_inc_m2.Text.Length | sta_inc_m2.Text.Length
                | sta_inc_d1.Text.Length | sta_inc_d2.Text.Length) == 1 && (sta_inc_y1.Text.Length
                & sta_inc_y2.Text.Length & sta_inc_m2.Text.Length & sta_inc_m2.Text.Length
                & sta_inc_d1.Text.Length & sta_inc_d2.Text.Length) == 0)
            {
                MessageBox.Show("请输入日期完整范围.");
                return;
            }

            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string from = "select *  from income ";
                    string sql = " where IKNO = IKNO ";
                    if (sta_inc_sno.Text.Length > 0)
                    {
                        sql += "and SNO = @sno ";
                        cmd.Parameters.Add(new SqlParameter("@sno", sta_inc_sno.Text));
                    }
                    if (sta_inc_man.Text.Length > 0)
                    {
                        sql += "and OPERATOR = @man ";
                        cmd.Parameters.Add(new SqlParameter("@man", sta_inc_man.Text));
                    }
                    if (sta_inc_kind.Text.Length > 0)
                    {
                        sql += "and IKNO = @kind ";
                        cmd.Parameters.Add(new SqlParameter("@kind", sta_inc_kind.Text));
                    }
                    if (sta_inc_c1.Text.Length > 0)
                    {
                        sql += " and CASH >= @c1 and CASH <= @c2 ";
                        cmd.Parameters.Add(new SqlParameter("@c1", Convert.ToInt32(sta_inc_c1.Text)));
                        cmd.Parameters.Add(new SqlParameter("@c2", Convert.ToInt32(sta_inc_c2.Text)));
                    }
                    if (sta_inc_y1.Text.Length > 0)
                    {
                        string date1, date2;
                        date1 = sta_inc_y1.Text + "-" + sta_inc_m1.Text + "-" + sta_inc_d1.Text;
                        date2 = sta_inc_y2.Text + "-" + sta_inc_m2.Text + "-" + sta_inc_d2.Text;
                        sql += "and EDATE >= @d1 and EDATE <= @d2";
                        cmd.Parameters.Add(new SqlParameter("@d1", date1));
                        cmd.Parameters.Add(new SqlParameter("@d2", date2));
                    }
                    cmd.CommandText = from + sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    sta_see.BeginUpdate();
                    while (dr.Read())
                    {
                        ListViewItem tem = new ListViewItem();
                        tem.Text = (string)dr["INO"];
                        tem.SubItems.Add((string)dr["SNO"]);
                        tem.SubItems.Add(dr["IKNO"].ToString());
                        tem.SubItems.Add(dr["CASH"].ToString());
                        tem.SubItems.Add(dr["EDATE"].ToString());
                        tem.SubItems.Add(dr["OPERATOR"].ToString());
                        sta_see.Items.Add(tem);
                    }
                    sta_see.EndUpdate();
                }
                catch (Exception em)
                {
                    MessageBox.Show("出错啦");
                }
            }
        }


        /*
        * 编写人:唐胜洋
        * 时间：2018-1-3 22:54
        * 功能：下面五个函数都是清除支出
        * 
        * */
        private void sta_exp_lic_cle_Click(object sender, EventArgs e)
        {
            sta_exp_lic.Text = "";
        }

        private void sta_exp_kind_cle_Click(object sender, EventArgs e)
        {
            sta_exp_kind.Text = "";
            L_sta_exp_tip.Text = "注:";
        }

        private void sta_exp_cash_cle_Click(object sender, EventArgs e)
        {
            sta_exp_c1.Text = "";
            sta_exp_c2.Text = "";
        }

        private void sta_exp_date_cle_Click(object sender, EventArgs e)
        {
            sta_exp_y1.Text = ""; sta_exp_m1.Text = ""; sta_exp_d1.Text = "";
            sta_exp_y2.Text = ""; sta_exp_m2.Text = ""; sta_exp_d2.Text = "";
        }

        private void sta_exp_man_cle_Click(object sender, EventArgs e)
        {
            sta_exp_man.Text = "";
        }

        /*
        * 编写人:唐胜洋
        * 时间：2018-1-3 22:54
        * 功能：对不同的种类显示注释
        * */

        private void sta_exp_kind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sta_exp_kind.Text.Length == 0)
                return;
            try
            {
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select EINTRO from e_kind where EKNO = '" + sta_exp_kind.Text + "'";
                    L_sta_exp_tip.Text = (string)cmd.ExecuteScalar();
                }

            }
            catch (Exception em)
            {
                MessageBox.Show("获取提示失败");
            }
        }

        /*
        * 编写人:唐胜洋
        * 时间：2018-1-3 22:54
        * 功能：支出统计响应
        * 
        * */
        private void sta_exp_go_Click(object sender, EventArgs e)
        {
            sta_see.Items.Clear();
            sta_see.Columns.Clear();
            sta_see.Columns.Add("账单号"); sta_see.Columns.Add("车牌");
            sta_see.Columns.Add("种类"); sta_see.Columns.Add("金额");
            sta_see.Columns.Add("时间"); sta_see.Columns.Add("操作员");
            sta_see.View = View.Details;
          
            if(sta_exp_lic.Text.Length > 0 && !Regex.IsMatch(sta_exp_lic.Text, @"^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$"))
            {
                MessageBox.Show("车牌号输入不正确.");
                return;
            }
            else if (sta_exp_c1.Text.Length > 0 && !Regex.IsMatch(sta_exp_c2.Text, @"[1-9]\d*"))
            {
                MessageBox.Show("金额输入不正确.");
                return;
            }
            else if (sta_exp_c2.Text.Length > 0 && !Regex.IsMatch(sta_exp_c2.Text, @"^(\d{1,2}|100)$"))
            {
                MessageBox.Show("金额输入不正确.");
                return;
            }
            else if ((sta_exp_c2.Text.Length ^ sta_exp_c1.Text.Length) == 1)
            {
                MessageBox.Show("请输入分数完整范围.");
                return;
            }
            else if ((sta_exp_y1.Text.Length | sta_exp_y2.Text.Length | sta_exp_m2.Text.Length | sta_exp_m2.Text.Length
                | sta_exp_d1.Text.Length | sta_exp_d2.Text.Length) == 1 && (sta_exp_y1.Text.Length
                & sta_exp_y2.Text.Length & sta_exp_m2.Text.Length & sta_exp_m2.Text.Length
                & sta_exp_d1.Text.Length & sta_exp_d2.Text.Length) == 0)
            {
                MessageBox.Show("请输入日期完整范围.");
                return;
            }

            using (conn = new SqlConnection(sqlconnect))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string from = "select *  from expenditure ";
                    string sql = " where BNO = BNO ";
                    if (sta_exp_lic.Text.Length > 0)
                    {
                        sql += "and LIC = @lic ";
                        cmd.Parameters.Add(new SqlParameter("@lic", sta_exp_lic.Text));
                    }
                    if (sta_exp_man.Text.Length > 0)
                    {
                        sql += "and OPERATOR = @man ";
                        cmd.Parameters.Add(new SqlParameter("@man", sta_exp_man.Text));
                    }
                    if (sta_exp_kind.Text.Length > 0)
                    {
                        sql += "and EKNO = @kind ";
                        cmd.Parameters.Add(new SqlParameter("@kind", sta_exp_kind.Text));
                    }
                    if (sta_exp_c1.Text.Length > 0)
                    {
                        sql += " and CASH >= @c1 and CASH <= @c2 ";
                        cmd.Parameters.Add(new SqlParameter("@c1", Convert.ToInt32(sta_exp_c1.Text)));
                        cmd.Parameters.Add(new SqlParameter("@c2", Convert.ToInt32(sta_exp_c2.Text)));
                    }
                    if (sta_exp_y1.Text.Length > 0)
                    {
                        string date1, date2;
                        date1 = sta_exp_y1.Text + "-" + sta_exp_m1.Text + "-" + sta_exp_d1.Text;
                        date2 = sta_exp_y2.Text + "-" + sta_exp_m2.Text + "-" + sta_exp_d2.Text;
                        sql += "and EDATE >= @d1 and EDATE <= @d2";
                        cmd.Parameters.Add(new SqlParameter("@d1", date1));
                        cmd.Parameters.Add(new SqlParameter("@d2", date2));
                    }
                    cmd.CommandText = from + sql;
                    SqlDataReader dr = cmd.ExecuteReader();
                    sta_see.BeginUpdate();
                    while (dr.Read())
                    {
                        ListViewItem tem = new ListViewItem();
                        tem.Text = (string)dr["BNO"];
                        tem.SubItems.Add((string)dr["LIC"]);
                        tem.SubItems.Add(dr["EKNO"].ToString());
                        tem.SubItems.Add(dr["CASH"].ToString());
                        tem.SubItems.Add(dr["EDATE"].ToString());
                        tem.SubItems.Add(dr["OPERATOR"].ToString());
                        sta_see.Items.Add(tem);
                    }
                    sta_see.EndUpdate();
                }
                catch (Exception em)
                {
                    MessageBox.Show("出错啦");
                }
            }
        }


        #endregion

    }
}

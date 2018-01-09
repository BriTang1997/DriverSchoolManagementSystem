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
        const int panellocax = 12;
        const int panellocay = 28;
        const int rightbond = 586;
        private static string sqlconnect;//这些东西可以直接用。
        private DataSet myset;
        private SqlDataAdapter da;
        private SqlCommandBuilder myCbd;
        private SqlCommand cmd;
        private SqlConnection conn;
        private BindingSource bing;
        /*
         * 加载时，所有的panel都把左上角固定在(7,28);
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
            add_student_sex_comboBox.SelectedIndex = 0;
            add_student_pay_comboBox.SelectedIndex = 0;
            add_coach_sex_comboBox.SelectedIndex = 0;
            linkLabel1.BackColor = Color.Transparent;
            linkLabel2.BackColor = Color.Transparent;
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
                menu.Items[3].Enabled = false;
                sqlconnect += "UID = n1;Pwd = 111";
            }
            else if (p == 2)    //运营
            {
                sqlconnect += "UID = n2;Pwd = 111";
                menu.Items[5].Enabled = false;
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
        private void bussiness_kind_init()
        {
            try
            {
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select IKNO from i_kind ";
                    SqlDataReader dr = cmd.ExecuteReader();
                    b_inc_kind.Items.Clear();
                    while (dr.Read())
                    {
                        b_inc_kind.Items.Add(dr.GetString(0));
                    }
                    cmd.CommandText = "select EKNO from e_kind ";
                    dr.Close();
                    dr = cmd.ExecuteReader();
                    b_exp_kind.Items.Clear();
                    while (dr.Read())
                    {
                        b_exp_kind.Items.Add(dr.GetString(0));
                    }
                    cmd.CommandText = "select SJNAME from subject ";
                    dr.Close();
                    dr = cmd.ExecuteReader();
                    b_exa_kind.Items.Clear();
                    b_sc_kind.Items.Clear();
                    while (dr.Read())
                    {
                        b_exa_kind.Items.Add(dr[0].ToString());
                        b_sc_kind.Items.Add(dr[0].ToString());
                    }

                    conn.Close();

                }
            }
            catch (Exception em)
            {
                MessageBox.Show("初始化业务模块出错！");
                this.Close();
            }
        }

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


        }
        /*  
        *  编写时间：2018-1-8,20:15
        *  编写人：唐胜洋
        *  功能：初始化统计模块的内容
        * */
        private void sta_kind_init()
        {
            try
            {
                using (conn = new SqlConnection(sqlconnect))
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select LIC from car ";
                    SqlDataReader dr = cmd.ExecuteReader();
                    sta_c_lic.Items.Clear();
                    while (dr.Read())
                    {
                        sta_c_lic.Items.Add(dr.GetString(0));
                    }
                    dr.Close();
                    cmd.CommandText = "select IKNO from i_kind ";
                    dr = cmd.ExecuteReader();
                    sta_inc_kind.Items.Clear();
                    while (dr.Read())
                    {
                        sta_inc_kind.Items.Add(dr.GetString(0));
                    }
                    dr.Close();
                    cmd.CommandText = "select SJNAME from subject ";
                    dr = cmd.ExecuteReader();
                    sta_exa_sub.Items.Clear();
                    while (dr.Read())
                    {
                        sta_exa_sub.Items.Add(dr[0].ToString());
                    }
                    dr.Close();
                    cmd.CommandText = "select MNAME from manager ";
                    dr = cmd.ExecuteReader();
                    sta_inc_man.Items.Clear();
                    sta_exp_man.Items.Clear();
                    while (dr.Read())
                    {
                        sta_inc_man.Items.Add(dr.GetString(0));
                        sta_exp_man.Items.Add(dr.GetString(0));
                    }
                    dr.Close();
                    cmd.CommandText = "select EKNO from e_kind ";
                    dr = cmd.ExecuteReader();
                    sta_exp_kind.Items.Clear();
                    while (dr.Read())
                    {
                        sta_exp_kind.Items.Add(dr[0].ToString());
                    }
                    conn.Close();

                }
            }
            catch (Exception em)
            {
                MessageBox.Show("初始化统计模块出错！");
                this.Close();
            }
        }
        /*  
        *  编写时间：2018-1-2,20:15
        *  编写人：唐胜洋
        *  功能：初始化统计模块的内容
        * */
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
        }

        /*
         * 编写时间：2018年1月3日16:20:48
         * 编写人：罗礼鹏
         * 功能：填充下拉框的内容
         */
        private void init_xiala()
        {
            SqlConnection con = new SqlConnection(sqlconnect);
            string str_coach = "select cno from coach";
            string str_student = "select sno from student";
            string str_car = "select lic from car";
            string str_test = "select SJNAME from subject";
            string str_expense = "select ekno from e_kind";
            string str_pay = "select ikno from i_kind";
            SqlCommand sqlcom_coach = new SqlCommand(str_coach, con);
            SqlCommand sqlcom_student = new SqlCommand(str_student, con);
            SqlCommand sqlcom_car = new SqlCommand(str_car, con);
            SqlCommand sqlcom_test = new SqlCommand(str_test, con);
            SqlCommand sqlcom_expense = new SqlCommand(str_expense, con);
            SqlCommand sqlcom_pay = new SqlCommand(str_pay, con);
            DataSet ds = new DataSet();
            SqlDataAdapter da_coach = new SqlDataAdapter(sqlcom_coach);
            SqlDataAdapter da_student = new SqlDataAdapter(sqlcom_student);
            SqlDataAdapter da_car = new SqlDataAdapter(sqlcom_car);
            SqlDataAdapter da_test = new SqlDataAdapter(sqlcom_test);
            SqlDataAdapter da_expense = new SqlDataAdapter(sqlcom_expense);
            SqlDataAdapter da_pay = new SqlDataAdapter(sqlcom_pay);
            da_coach.Fill(ds, "coach");
            da_student.Fill(ds, "student");
            da_car.Fill(ds, "car");
            da_test.Fill(ds, "test");
            da_expense.Fill(ds, "expense");
            da_pay.Fill(ds, "pay");
            int j_coach = ds.Tables["coach"].Rows.Count;
            int j_student = ds.Tables["student"].Rows.Count;
            int j_car = ds.Tables["car"].Rows.Count;
            int j_test = ds.Tables["test"].Rows.Count;
            int j_expense = ds.Tables["expense"].Rows.Count;
            int j_pay = ds.Tables["pay"].Rows.Count;
            for (int i = 0; i < j_coach; i++)
            {
                up_coach_cno_comboBox.Items.Add(ds.Tables["coach"].Rows[i][0].ToString());
                delete_coach_cno_comboBox.Items.Add(ds.Tables["coach"].Rows[i][0].ToString());
            }
            for (int i = 0; i < j_student; i++)
            {
                up_student_sno_comboBox.Items.Add(ds.Tables["student"].Rows[i][0].ToString());
                delete_student_sno_comboBox.Items.Add(ds.Tables["student"].Rows[i][0].ToString());
            }
            for (int i = 0; i < j_car; i++)
            {
                up_car_cp_comboBox.Items.Add(ds.Tables["car"].Rows[i][0].ToString());
                delete_car_cp_comboBox.Items.Add(ds.Tables["car"].Rows[i][0].ToString());
            }
            for (int i = 0; i < j_test; i++)
            {
                up_test_tno_comboBox.Items.Add(ds.Tables["test"].Rows[i][0].ToString());
                delete_test_tno_comboBox.Items.Add(ds.Tables["test"].Rows[i][0].ToString());
                up_student_jd_comboBox.Items.Add(ds.Tables["test"].Rows[i][0].ToString());
                add_student_jd_comboBox.Items.Add(ds.Tables["test"].Rows[i][0].ToString());

            }
            for (int i = 0; i < j_expense; i++)
            {
                up_expense_eno_comboBox.Items.Add(ds.Tables["expense"].Rows[i][0].ToString());
                delete_expense_eno_comboBox.Items.Add(ds.Tables["expense"].Rows[i][0].ToString());
            }
            for (int i = 0; i < j_pay; i++)
            {
                up_pay_pno_comboBox.Items.Add(ds.Tables["pay"].Rows[i][0].ToString());
                delete_pay_pno_comboBox.Items.Add(ds.Tables["pay"].Rows[i][0].ToString());
            }
            con.Close();
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
            业务panel.Location = new System.Drawing.Point(panellocax, panellocay);
            统计panel.Location = new System.Drawing.Point(panellocax, panellocay);
            查询panel.Location = new System.Drawing.Point(panellocax, panellocay);
            生成报表panel.Location = new System.Drawing.Point(panellocax, panellocay);
            信息更新_panel.Location = new System.Drawing.Point(panellocax, panellocay);
            开始_panel.Location = new System.Drawing.Point(0, panellocay);
            L_mana.Text = f1.name;
            timer1.Enabled = true;
            label6.BackColor = Color.Transparent;
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
            init_xiala();
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

        private void 开始ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            信息更新_panel.Hide();
            业务panel.Hide();
            统计panel.Hide();
            查询panel.Hide();
            开始_panel.Show();
            生成报表panel.Hide();
        }
        /*
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
            统计panel.Hide();
            查询panel.Hide();
            生成报表panel.Hide();
            信息更新_panel.Hide();
            开始_panel.Hide();
            bussiness_kind_init();
        }


        private void 统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            统计panel.Show();
            业务panel.Hide();
            查询panel.Hide();
            生成报表panel.Hide();
            信息更新_panel.Hide();
            开始_panel.Hide();
            sta_kind_init();
        }
        private void 信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(sqlconnect);
            统计panel.Hide();
            业务panel.Hide();
            查询panel.Show();
            生成报表panel.Hide();
            信息更新_panel.Hide();
            开始_panel.Hide();
        }
        private void 打印报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(sqlconnect);
            统计panel.Hide();
            业务panel.Hide();
            查询panel.Hide();
            生成报表panel.Show();
            信息更新_panel.Hide();
            开始_panel.Hide();
        }

        private void 信息更新ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            信息更新_panel.Show();
            业务panel.Hide();
            统计panel.Hide();
            查询panel.Hide();
            开始_panel.Hide();
            生成报表panel.Hide();
            label6.BackColor = Color.Transparent;
        }

        private void up_test_tabPage_Click(object sender, EventArgs e)
        {

        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            信息更新_panel.Hide();
            业务panel.Hide();
            统计panel.Hide();
            查询panel.Hide();
            开始_panel.Show();
            生成报表panel.Hide();
        }
        #endregion

    #region 信息更新
        /*
         * 编写时间：2018年1月1日14:11:16
         * 编写人：罗礼鹏
         * 功能：判断新增对象是查询数据库中是否已经存在了。
         */
        public bool havingsameno(string no, int mark)
        {

            using (SqlConnection conn1 = new SqlConnection(sqlconnect))
            {
                conn1.Open();
                int count = 0;
                string tempno = no;
                string strsql = null;
                if (mark == 1)
                {
                    strsql = "select count(*) from coach ";
                    strsql += "where cno='" + no + "'";
                }
                else if (mark == 2)
                {
                    strsql = "select count(*) from car ";
                    strsql += "where lic='" + no + "'";
                }
                else if (mark == 4)
                {
                    strsql = "select count(*) from subject ";
                    strsql += "where sjname='" + no + "'";
                }
                else if (mark == 5)
                {
                    strsql = "select count(*) from e_kind ";
                    strsql += "where ekno='" + no + "'";
                }
                else if (mark == 6)
                {
                    strsql = "select count(*) from i_kind ";
                    strsql += "where ikno='" + no + "'";
                }
                else if (mark == 3)
                {
                    strsql = "select count(*) from student ";
                    strsql += "where sno='" + no + "'";
                }
                SqlCommand cmd = new SqlCommand(strsql, conn1);  //构造sql命令
                count = (int)cmd.ExecuteScalar();//执行该命令，返回数据表中第一行，第一列的结果，即汇总结果,当只需读取一个统计数据时，用此方法
                conn1.Close();
                if (count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        /*
         * 编写时间：2018年1月1日15:20:16
         * 编写人：罗礼鹏
         * 功能：根据编号查询所有信息并显示在文本框里
         */
        public void show_attribute(string no, int mark)
        {
            if (mark == 0)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select brand,color,mile,buytime from car where lic='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        up_car_cs_textBox.Text = reader["brand"].ToString();
                        up_car_ys_textBox.Text = reader["color"].ToString();
                        up_car_lc_textBox.Text = reader["mile"].ToString();
                        DateTime dt = DateTime.Parse(reader["buytime"].ToString());
                        up_car_dateTimePicker.Value = dt;
                    }
                    sqlcon.Close();
                }
            }
            else if (mark == 1)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select sname,phone,pay,sex,progress from student where sno='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        up_student_name_textBox.Text = reader["sname"].ToString();
                        up_student_phone_textBox.Text = reader["phone"].ToString();
                        up_student_pay_comboBox.Text = reader["pay"].ToString();
                        up_student_sex_comboBox.Text = reader["sex"].ToString();
                        up_student_jd_comboBox.Text = reader["progress"].ToString();

                    }
                    sqlcon.Close();
                }

            }
            else if (mark == 2)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select cname,sex,phone from coach where cno='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        up_coach_name_textBox.Text = reader["cname"].ToString();
                        up_coach_sex_comboBox.Text = reader["sex"].ToString();
                        up_coach_phone_textBox.Text = reader["phone"].ToString();
                    }
                    sqlcon.Close();
                }
            }
            else if (mark == 3)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select sjname,place,sintro from subject where sjname='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        up_test_name_textBox.Text = reader["place"].ToString();
                        up_test_sm_textBox.Text = reader["sintro"].ToString();
                    }
                    sqlcon.Close();
                }
            }
            else if (mark == 4)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select ename,eintro from e_kind where ekno='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        up_expense_name_textBox.Text = reader["ename"].ToString();
                        up_expense_sm_textBox.Text = reader["eintro"].ToString();
                    }
                    sqlcon.Close();
                }
            }
            else if (mark == 5)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select iname,iintro from i_kind where ikno='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        up_pay_name_textBox.Text = reader["iname"].ToString();
                        up_pay_sm_textBox.Text = reader["iintro"].ToString();
                    }
                    sqlcon.Close();
                }
            }
        }
        /*
         * 编写时间：2018年1月2日14:20:56
         * 编写人：罗礼鹏
         * 功能：根据删除编号对应显示相关信息反馈给用户。
 */
        public void show_delete_attribute(string no, int mark)
        {
            if (mark == 1)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select sname from student where sno='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        delete_student_name_textBox.Text = reader["sname"].ToString();
                    }
                    sqlcon.Close();
                }

            }
            else if (mark == 2)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select cname from coach where cno='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        delete_coach_name_textBox.Text = reader["cname"].ToString();
                    }
                    sqlcon.Close();
                }
            }
            else if (mark == 3)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select sjname,place,sintro from subject where sjname='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        delete_test_name_textBox.Text = reader["place"].ToString();
                        delete_test_sm_textBox.Text = reader["sintro"].ToString();
                    }
                    sqlcon.Close();
                }
            }
            else if (mark == 4)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select ename,eintro from e_kind where ekno='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        delete_expense_name_textBox.Text = reader["ename"].ToString();
                        delete_expense_sm_textBox.Text = reader["eintro"].ToString();
                    }
                    sqlcon.Close();
                }
            }
            else if (mark == 5)
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlconnect))
                {
                    sqlcon.Open();
                    String strsql = "select iname,iintro from i_kind where ikno='" + no + "'";
                    SqlCommand command = new SqlCommand(strsql, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        delete_pay_name_textBox.Text = reader["iname"].ToString();
                        delete_pay_sm_textBox.Text = reader["iintro"].ToString();
                    }
                    sqlcon.Close();
                }
            }
        }
        /*
         * 编写时间：2018年1月3日13:16:28
         * 编写人：罗礼鹏
         * 功能：新增和删除对象时更新下拉框里的内容
         */
        private void up_xiala(int a)
        {
            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();
            if (a == 0)
            {
                delete_car_cp_comboBox.Items.Clear();
                string str_car = "select lic from car";
                SqlCommand sqlcom_car = new SqlCommand(str_car, con);
                DataSet ds = new DataSet();
                SqlDataAdapter da_car = new SqlDataAdapter(sqlcom_car);
                da_car.Fill(ds, "car");
                int j_car = ds.Tables["car"].Rows.Count;
                up_car_cp_comboBox.Items.Clear();
                for (int i = 0; i < j_car; i++)
                {
                    up_car_cp_comboBox.Items.Add(ds.Tables["car"].Rows[i][0].ToString());
                    delete_car_cp_comboBox.Items.Add(ds.Tables["car"].Rows[i][0].ToString());
                }
                ds.Clear();
            }
            else if (a == 1)
            {
                delete_student_sno_comboBox.Items.Clear();
                string str_student = "select sno from student";
                SqlCommand sqlcom_student = new SqlCommand(str_student, con);
                DataSet ds = new DataSet();
                SqlDataAdapter da_student = new SqlDataAdapter(sqlcom_student);
                da_student.Fill(ds, "student");
                int j_student = ds.Tables["student"].Rows.Count;
                up_student_sno_comboBox.Items.Clear();
                for (int i = 0; i < j_student; i++)
                {
                    up_student_sno_comboBox.Items.Add(ds.Tables["student"].Rows[i][0].ToString());
                    delete_student_sno_comboBox.Items.Add(ds.Tables["student"].Rows[i][0].ToString());
                }
                ds.Clear();
            }
            else if (a == 2)
            {
                delete_coach_cno_comboBox.Items.Clear();
                string str_coach = "select cno from coach";
                SqlCommand sqlcom_coach = new SqlCommand(str_coach, con);
                DataSet ds = new DataSet();
                SqlDataAdapter da_coach = new SqlDataAdapter(sqlcom_coach);
                da_coach.Fill(ds, "coach");
                int j_coach = ds.Tables["coach"].Rows.Count;
                up_coach_cno_comboBox.Items.Clear();
                for (int i = 0; i < j_coach; i++)
                {
                    up_coach_cno_comboBox.Items.Add(ds.Tables["coach"].Rows[i][0].ToString());
                    delete_coach_cno_comboBox.Items.Add(ds.Tables["coach"].Rows[i][0].ToString());
                }
                ds.Clear();
            }
            else if (a == 3)
            {
                delete_test_tno_comboBox.Items.Clear();
                string str_test = "select sjname from subject";
                SqlCommand sqlcom_test = new SqlCommand(str_test, con);
                DataSet ds = new DataSet();
                SqlDataAdapter da_test = new SqlDataAdapter(sqlcom_test);
                da_test.Fill(ds, "test");
                int j_student = ds.Tables["test"].Rows.Count;
                up_test_tno_comboBox.Items.Clear();
                for (int i = 0; i < j_student; i++)
                {
                    up_test_tno_comboBox.Items.Add(ds.Tables["test"].Rows[i][0].ToString());
                    delete_test_tno_comboBox.Items.Add(ds.Tables["test"].Rows[i][0].ToString());
                }
                ds.Clear();
            }
            else if (a == 4)
            {
                delete_expense_eno_comboBox.Items.Clear();
                string str_expense = "select ekno from e_kind";
                SqlCommand sqlcom_expense = new SqlCommand(str_expense, con);
                DataSet ds = new DataSet();
                SqlDataAdapter da_expense = new SqlDataAdapter(sqlcom_expense);
                da_expense.Fill(ds, "expense");
                int j_expense = ds.Tables["expense"].Rows.Count;
                up_expense_eno_comboBox.Items.Clear();
                for (int i = 0; i < j_expense; i++)
                {
                    up_expense_eno_comboBox.Items.Add(ds.Tables["expense"].Rows[i][0].ToString());
                    delete_expense_eno_comboBox.Items.Add(ds.Tables["expense"].Rows[i][0].ToString());
                }
                ds.Clear();
            }
            else if (a == 5)
            {
                delete_pay_pno_comboBox.Items.Clear();
                string str_pay = "select ikno from i_kind";
                SqlCommand sqlcom_pay = new SqlCommand(str_pay, con);
                DataSet ds = new DataSet();
                SqlDataAdapter da_pay = new SqlDataAdapter(sqlcom_pay);
                da_pay.Fill(ds, "pay");
                int j_pay = ds.Tables["pay"].Rows.Count;
                up_pay_pno_comboBox.Items.Clear();
                for (int i = 0; i < j_pay; i++)
                {
                    up_pay_pno_comboBox.Items.Add(ds.Tables["pay"].Rows[i][0].ToString());
                    delete_pay_pno_comboBox.Items.Add(ds.Tables["pay"].Rows[i][0].ToString());
                }
                ds.Clear();
            }
        }

        private void add_car_button_Click(object sender, EventArgs e)
        {

            if (add_car_cp_textbox.Text.Trim() == "")
            {
                MessageBox.Show("添加的车牌号不能为空");
                add_car_cp_textbox.Focus();
                return;
            }

            if (add_car_cp_textbox.Text.Trim().Length != 7)
            {
                MessageBox.Show("添加的车牌号必须为7位");
                add_car_cp_textbox.Focus();
                return;
            }
            if (havingsameno(add_car_cp_textbox.Text.Trim(), 2))
            {
                MessageBox.Show("添加的车牌号已经存在");
                add_car_cp_textbox.Focus();
                return;
            }

            if (add_car_cs_textbox.Text.Trim() == "")
            {
                MessageBox.Show("添加的厂商不能为空");
                add_car_cs_textbox.Focus();
                return;
            }

            if (add_car_ys_textbox.Text.Trim() == "")
            {
                MessageBox.Show("添加的颜色不能为空");
                add_car_ys_textbox.Focus();
                return;
            }
            if (add_car_lc_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的里程数不能为空");
                add_car_lc_textBox.Focus();
                return;
            }
            float mile;
            if (!float.TryParse(add_car_lc_textBox.Text.Trim(), out mile))
            {
                MessageBox.Show("添加的里程数只能是数字");
                add_car_lc_textBox.Focus();
                return;
            }
            try
            {
                int count = 0;
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                string strsql = "insert into car(lic,brand,color,mile,buytime) values('";
                strsql += add_car_cp_textbox.Text.Trim() + "','" + add_car_cs_textbox.Text.Trim() + "','" + add_car_ys_textbox.Text.Trim() + "','" + add_car_lc_textBox.Text.Trim() + "','" + add_car_dateTimePicker.Text + "')";
                SqlCommand cmd = new SqlCommand(strsql, con);  //构造sql命令
                count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                con.Close();
                add_car_cp_textbox.Text = "";
                add_car_cs_textbox.Text = "";
                add_car_ys_textbox.Text = "";
                add_car_lc_textBox.Text = "";
                add_car_dateTimePicker.Value = DateTime.Now;
                up_xiala(0);
                MessageBox.Show("成功插入数据" + count + "行");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }
        }
        private void up_test_hq_button_Click(object sender, EventArgs e)
        {
            String s = up_test_tno_comboBox.Text.Trim();
            show_attribute(s, 3);
        }
        private void delete_expense_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "delete e_kind where ekno='" + delete_expense_eno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                up_xiala(4);
                delete_expense_eno_comboBox.Text = null;
                delete_expense_name_textBox.Text = null;
                delete_expense_sm_textBox.Text = null;
                MessageBox.Show("删除支出类型成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "删除支出类型失败");
            }
        }
        private void add_student_button_Click(object sender, EventArgs e)
        {
            if (add_student_sno_textbox.Text.Trim() == "")
            {
                MessageBox.Show("添加的学员号不能为空");
                add_car_cp_textbox.Focus();
                return;
            }

            if (add_student_sno_textbox.Text.Trim().Length != 8)
            {
                MessageBox.Show("添加的学员号必须为8位");
                add_student_sno_textbox.Focus();
                return;
            }
            if (havingsameno(add_student_sno_textbox.Text.Trim(), 3))
            {
                MessageBox.Show("添加的学员已经存在");
                add_student_sno_textbox.Focus();
                return;
            }

            if (add_student_phone_textbox.Text.Trim() == "")
            {
                MessageBox.Show("电话号码不能为空");
                add_student_phone_textbox.Focus();
                return;
            }

            float phone;
            if (!float.TryParse(add_student_phone_textbox.Text.Trim(), out phone))
            {
                MessageBox.Show("添加的电话只能是数字");
                add_student_phone_textbox.Focus();
                return;
            }

            try
            {
                int count = 0;
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                string strsql = "insert into student(sno,sname,phone,pay,sex,progress) values('";
                strsql += add_student_sno_textbox.Text.Trim() + "','" + add_student_name_textbox.Text.Trim() + "','" + add_student_phone_textbox.Text.Trim() + "','" + add_student_pay_comboBox.Text.Trim() + "','" + add_student_sex_comboBox.Text.Trim() + "','" + add_student_jd_comboBox.Text.Trim() + "')";
                SqlCommand cmd = new SqlCommand(strsql, con);  //构造sql命令
                count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                con.Close();
                add_student_sno_textbox.Text = "";
                add_student_name_textbox.Text = "";
                add_student_phone_textbox.Text = "";
                add_student_pay_comboBox.Text = "";
                add_student_sex_comboBox.Text = "";
                add_student_jd_comboBox.Text = "";
                up_xiala(1);
                MessageBox.Show("成功插入数据" + count + "行");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }
        }

        private void add_coach_button_Click(object sender, EventArgs e)
        {

            if (add_coach_cno_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的教练编号不能为空");
                add_coach_cno_textBox.Focus();
                return;
            }

            if (add_coach_cno_textBox.Text.Trim().Length != 8)
            {
                MessageBox.Show("添加的教练必须为8位");
                add_coach_cno_textBox.Focus();
                return;
            }
            if (havingsameno(add_coach_cno_textBox.Text.Trim(), 1))
            {
                MessageBox.Show("添加的教练号已经存在");
                add_coach_cno_textBox.Focus();
                return;
            }

            if (add_coach_cno_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的教练名字不能为空");
                add_coach_cno_textBox.Focus();
                return;
            }

            if (add_coach_cno_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的电话不能为空");
                add_coach_cno_textBox.Focus();
                return;
            }
            float phone;
            if (!float.TryParse(add_coach_phone_textBox.Text.Trim(), out phone))
            {
                MessageBox.Show("添加的电话只能是数字");
                add_coach_phone_textBox.Focus();
                return;
            }

            try
            {
                int count = 0;
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                string strsql = "insert into coach(cno,cname,sex,phone) values('";
                strsql += add_coach_cno_textBox.Text.Trim() + "','" + add_coach_name_textBox.Text.Trim() + "','" + add_coach_sex_comboBox.Text.Trim() + "','" + add_coach_phone_textBox.Text.Trim() + "')";
                SqlCommand cmd = new SqlCommand(strsql, con);  //构造sql命令
                count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                con.Close();
                add_coach_cno_textBox.Text = null;
                add_coach_name_textBox.Text = null;
                add_coach_sex_comboBox.Text = null;
                add_coach_phone_textBox.Text = null;
                up_xiala(2);
                MessageBox.Show("成功插入数据" + count + "行");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }
        }

        private void add_test_button_Click(object sender, EventArgs e)
        {

            if (add_test_tno_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的考试类别编号不能为空");
                add_test_tno_textBox.Focus();
                return;
            }
            if (havingsameno(add_test_tno_textBox.Text.Trim(), 4))
            {
                MessageBox.Show("添加的考试类别已经存在");
                add_test_tno_textBox.Focus();
                return;
            }
            try
            {
                int count = 0;
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                string strsql = "insert into subject(sjname,place,sintro) values('";
                strsql += add_test_tno_textBox.Text.Trim() + "','" + add_test_name_textBox.Text.Trim() + "','" + add_test_sm_textBox.Text.Trim() + "')";
                SqlCommand cmd = new SqlCommand(strsql, con);  //构造sql命令
                count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                con.Close();
                add_test_tno_textBox.Text = "";
                add_test_name_textBox.Text = null;
                add_test_sm_textBox.Text = null;
                up_xiala(3);
                MessageBox.Show("成功插入数据" + count + "行");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }
        }

        private void add_expense_button_Click(object sender, EventArgs e)
        {
            if (add_expense_eno_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的支出种类编号不能为空");
                add_expense_eno_textBox.Focus();
                return;
            }
            if (havingsameno(add_expense_eno_textBox.Text.Trim(), 5))
            {
                MessageBox.Show("添加的支出种类已经存在");
                add_expense_eno_textBox.Focus();
                return;
            }
            if (add_expense_name_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的支出种类名字不能为空");
                add_expense_eno_textBox.Focus();
                return;
            }
            try
            {
                int count = 0;
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                string strsql = "insert into e_kind(ekno,ename,eintro) values('";
                strsql += add_expense_eno_textBox.Text.Trim() + "','" + add_expense_name_textBox.Text.Trim() + "','" + add_test_sm_textBox.Text.Trim() + "')";
                SqlCommand cmd = new SqlCommand(strsql, con);  //构造sql命令
                count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                con.Close();
                add_expense_eno_textBox.Text = "";
                add_expense_name_textBox.Text = null;
                add_expense_sm_textBox.Text = null;
                up_xiala(4);
                MessageBox.Show("成功插入数据" + count + "行");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }
        }

        private void add_pay_button_Click(object sender, EventArgs e)
        {
            if (add_pay_pno_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的缴费种类编号不能为空");
                add_pay_pno_textBox.Focus();
                return;
            }
            if (havingsameno(add_pay_pno_textBox.Text.Trim(), 6))
            {
                MessageBox.Show("添加的缴费种类已经存在");
                add_pay_pno_textBox.Focus();
                return;
            }
            if (add_pay_name_textBox.Text.Trim() == "")
            {
                MessageBox.Show("添加的缴费种类名字不能为空");
                add_pay_name_textBox.Focus();
                return;
            }
            try
            {
                int count = 0;
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                string strsql = "insert into i_kind(ikno,iname,iintro) values('";
                strsql += add_pay_pno_textBox.Text.Trim() + "','" + add_pay_name_textBox.Text.Trim() + "','" + add_pay_sm_textBox.Text.Trim() + "')";
                SqlCommand cmd = new SqlCommand(strsql, con);  //构造sql命令
                count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                con.Close();
                add_pay_pno_textBox.Text = "";
                add_pay_name_textBox.Text = "";
                add_pay_sm_textBox.Text = "";
                up_xiala(5);
                MessageBox.Show("成功插入数据" + count + "行");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }
        }

        private void add_student_sex_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void up_student_tabPage_Click(object sender, EventArgs e)
        {

        }

        private void up_car_hq_button_Click(object sender, EventArgs e)
        {
            String s = up_car_cp_comboBox.Text.Trim();
            show_attribute(s, 0);
        }

        private void up_car_xg_button_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "update car set brand='" + up_car_cs_textBox.Text.Trim() + "',color='" + up_car_ys_textBox.Text.Trim() + "',mile='" + up_car_lc_textBox.Text.Trim() + "',buytime='" + up_car_dateTimePicker.Value + "'where lic='" + up_car_cp_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                count = (int)sqlcom.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("成功修改教练数据");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }

        }

        private void up_student_hq_button_Click(object sender, EventArgs e)
        {
            String s = up_student_sno_comboBox.Text.Trim();
            show_attribute(s, 1);
        }

        private void up_coach_hq_button_Click(object sender, EventArgs e)
        {
            String s = up_coach_cno_comboBox.Text.Trim();
            show_attribute(s, 2);
        }

        private void up_expense_hq_button_Click(object sender, EventArgs e)
        {
            String s = up_expense_eno_comboBox.Text.Trim();
            show_attribute(s, 4);
        }

        private void up_pay_hq_button_Click(object sender, EventArgs e)
        {
            String s = up_pay_pno_comboBox.Text.Trim();
            show_attribute(s, 5);
        }

        private void up_car_qx_button_Click(object sender, EventArgs e)
        {
            up_car_cp_comboBox.Text = "";
            up_car_cs_textBox.Text = "";
            up_car_ys_textBox.Text = "";
            up_car_lc_textBox.Text = "";
            up_car_dateTimePicker.Value = DateTime.Now;
        }

        private void up_student_qx_button_Click(object sender, EventArgs e)
        {
            up_student_sno_comboBox.Text = "";
            up_student_name_textBox.Text = "";
            up_student_phone_textBox.Text = "";
            up_student_pay_comboBox.Text = "";
            up_student_sex_comboBox.Text = "";
            up_student_jd_comboBox.Text = "";
        }

        private void up_coach_qx_button_Click(object sender, EventArgs e)
        {
            up_coach_cno_comboBox.Text = "";
            up_coach_name_textBox.Text = null;
            up_coach_sex_comboBox.Text = null;
            up_coach_phone_textBox.Text = null;
        }

        private void up_test_qx_button_Click(object sender, EventArgs e)
        {
            up_test_tno_comboBox.Text = "";
            up_test_name_textBox.Text = null;
            up_test_sm_textBox.Text = null;
        }

        private void up_expense_qx_button_Click(object sender, EventArgs e)
        {
            up_expense_eno_comboBox.Text = "";
            up_expense_name_textBox.Text = null;
            up_expense_sm_textBox.Text = null;
        }

        private void up_pay_qx_button_Click(object sender, EventArgs e)
        {
            up_pay_pno_comboBox.Text = "";
            up_pay_name_textBox.Text = "";
            up_pay_sm_textBox.Text = "";
        }

        private void up_student_xg_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "update student set sname='" + up_student_name_textBox.Text.Trim() + "',phone='" + up_student_phone_textBox.Text.Trim() + "',sex='" + up_student_sex_comboBox.Text.Trim() + "',pay='" + up_student_pay_comboBox.Text.Trim() + "',progress='" + up_student_jd_comboBox.Text.Trim() + "' where sno='" + up_student_sno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("成功修改学员数据");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }
        }

        private void up_coach_xg_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "update coach set cname='" + up_coach_name_textBox.Text.Trim() + "',phone='" + up_coach_phone_textBox.Text.Trim() + "',sex='" + up_coach_sex_comboBox.Text.Trim() + "' where cno='" + up_coach_cno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("成功修改教练数据");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

            }
        }

        private void up_test_xg_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "update subject set place='" + up_test_name_textBox.Text.Trim() + "',sintro='" + up_test_sm_textBox.Text.Trim() + "' where sjname='" + up_test_tno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("成功修改科目数据");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }

        private void up_expense_xg_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "update e_kind set ename='" + up_expense_name_textBox.Text.Trim() + "',eintro='" + up_expense_sm_textBox.Text.Trim() + "' where ekno='" + up_expense_eno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("成功修改支出数据");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }

        private void up_pay_xg_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "update i_kind set iname='" + up_pay_name_textBox.Text.Trim() + "',iintro='" + up_pay_sm_textBox.Text.Trim() + "' where ikno='" + up_pay_pno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("成功修改缴费数据");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }

        private void delete_car_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "delete car where lic='" + delete_car_cp_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, conn);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                conn.Close();
                delete_car_cp_comboBox.Text = null;
                delete_car_cp_comboBox.Items.Clear();
                conn.Open();
                delete_car_cp_comboBox.Text = null;
                up_xiala(0);
                MessageBox.Show("删除车辆成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "删除车辆成功");
            }

        }

        private void delete_student_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "delete student where sno='" + delete_student_sno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                up_xiala(1);
                delete_student_sno_comboBox.Text = null;
                delete_student_name_textBox.Text = null;
                MessageBox.Show("删除学员成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "删除学员失败");
            }
        }

        private void delete_student_sno_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String s = delete_student_sno_comboBox.Text.Trim();
            show_delete_attribute(s, 1);
        }

        private void delete_coach_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "delete coach where cno='" + delete_coach_cno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                up_xiala(2);
                delete_coach_cno_comboBox.Text = null;
                delete_coach_name_textBox.Text = null;
                MessageBox.Show("删除教练成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "删除教练失败");
            }
        }

        private void delete_test__button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "delete subject where sjname='" + delete_test_tno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                up_xiala(3);
                delete_test_tno_comboBox.Text = null;
                delete_test_name_textBox.Text = null;
                delete_test_sm_textBox.Text = null;
                MessageBox.Show("删除科目成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "删除科目失败");
            }
        }
        private void delete_pay_button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();
                String sql = "delete i_kind where ikno='" + delete_pay_pno_comboBox.Text.Trim() + "'";
                SqlCommand sqlcom = new SqlCommand(sql, con);
                int count = 0;
                count = (int)sqlcom.ExecuteNonQuery();
                con.Close();
                up_xiala(5);
                delete_pay_pno_comboBox.Text = null;
                delete_pay_name_textBox.Text = null;
                delete_pay_sm_textBox.Text = null;
                MessageBox.Show("删除缴费类型成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "删除缴费类型失败");
            }
        }
        private void delete_test_tno_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String s = delete_test_tno_comboBox.Text.Trim();
            show_delete_attribute(s, 3);
        }
        private void delete_coach_cno_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String s = delete_coach_cno_comboBox.Text.Trim();
            show_delete_attribute(s, 2);
        }

        private void delete_expense_eno_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String s = delete_expense_eno_comboBox.Text.Trim();
            show_delete_attribute(s, 4);
        }
        private void delete_pay_pno_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String s = delete_pay_pno_comboBox.Text.Trim();
            show_delete_attribute(s, 5);
        }
        #endregion

    #region 查询响应函数
        int search_buss_mark = 0; //定义标志 1表示查询sc 2表示查询cc  3表示查询exam
        /*  
        *  编写时间：2018年1月2日15:00:22
        *  编写人：叶聿宽
        *  功能：隐藏查询学员教练匹配条件。
        * */
        private void hide_sear_sc()
        {
            cb_sear_sc_cname.Hide();
            cb_sear_sc_sname.Hide();
            chb_sear_buss_sc_cname.Hide();
            chb_sear_buss_sc_sname.Hide();
        }
        /*编写时间：2018年1月2日15:03:50
        *  编写人：叶聿宽
        *  功能：显示查询学员教练匹配条件。*/
        private void show_sear_sc()
        {
            cb_sear_sc_cname.Show();
            cb_sear_sc_sname.Show();
            chb_sear_buss_sc_cname.Show();
            chb_sear_buss_sc_sname.Show();
        }
        /*编写时间：2018年1月2日21:30:03
        *  编写人：叶聿宽
        *  功能：显示查询车辆教练匹配条件。*/
        private void show_sear_cc()
        {
            cb_sear_cc_cname.Show();
            cb_sear_cc_lic.Show();
            chb_sear_buss_cc_cname.Show();
            chb_sear_buss_cc_lic.Show();
        }
        /*编写时间：2018年1月2日21:30:03
        *  编写人：叶聿宽
        *  功能：隐藏查询车辆教练匹配条件。*/
        private void hide_sear_cc()
        {
            cb_sear_cc_cname.Hide();
            cb_sear_cc_lic.Hide();
            chb_sear_buss_cc_cname.Hide();
            chb_sear_buss_cc_lic.Hide();
        }
        /*编写时间：2018年1月2日21:32:44
        *  编写人：叶聿宽
        *  功能：显示查询成绩条件。*/
        private void show_sear_exam()
        {
            cb_sear_exam_sname.Show();
            cb_sear_exam_subject.Show();
        }
        /*编写时间：2018年1月2日21:34:21
        *  编写人：叶聿宽
        *  功能：显示查询成绩条件。*/
        private void hide_sear_exam()
        {
            cb_sear_exam_subject.Hide();
            cb_sear_exam_sname.Hide();
        }
        private void b_sear_sc_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(sqlconnect);
            l_sear_buss1.Text = "教练名";
            l_sear_buss2.Text = "学员名";
            l_sear_buss1.Show();
            l_sear_buss2.Show();
            show_sear_sc();
            hide_sear_cc();
            hide_sear_exam();
            search_buss_mark = 1;
            lv_sear_buss.Clear();
            lv_sear_buss.Columns.Add("教练名");
            lv_sear_buss.Columns.Add("学员名");
            try
            {
                string strsql = "select * from cname_sname ";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["cname"].ToString();
                    lt.SubItems.Add(dr["sname"].ToString());
                    lv_sear_buss.Items.Add(lt);
                }
                dr.Close();
                //生成教练姓名下拉单
                string str1 = "select distinct cname from cname_sname";
                DataSet myset1 = new DataSet();//建立数据集对象
                SqlDataAdapter da = new SqlDataAdapter(str1, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                da.Fill(myset1, "cname");//将查询数据填充到数据集的表中
                cb_sear_sc_cname.DataSource = myset1.Tables["cname"];
                cb_sear_sc_cname.DisplayMember = "cname";
                //生成学员姓名下拉单
                string str2 = "select distinct sname from cname_sname";
                DataSet myset2 = new DataSet();//建立数据集对象
                SqlDataAdapter da1 = new SqlDataAdapter(str2, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                da1.Fill(myset2, "sname");//将查询数据填充到数据集的表中
                cb_sear_sc_sname.DataSource = myset2.Tables["sname"];//将combobox与数据源绑定
                cb_sear_sc_sname.DisplayMember = "sname";

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        private void b_sear_buss_Click(object sender, EventArgs e)
        {
            if (search_buss_mark == 1)
            {
                lv_sear_buss.Clear();
                lv_sear_buss.Columns.Add("教练名");
                lv_sear_buss.Columns.Add("学员名");
                try
                {
                    string strsql = "select *  from cname_sname ";
                    //根据查询条件构造sql语句
                    if (chb_sear_buss_sc_cname.Checked == true && chb_sear_buss_sc_sname.Checked == false)
                        strsql = strsql + "where cname ='" + cb_sear_sc_cname.Text + "'";
                    if (chb_sear_buss_sc_cname.Checked == true && chb_sear_buss_sc_sname.Checked == true)
                        strsql = strsql + "where cname ='" + cb_sear_sc_cname.Text + "' and sname='" + cb_sear_sc_sname.Text + "'";
                    if (chb_sear_buss_sc_cname.Checked == false && chb_sear_buss_sc_sname.Checked == true)
                        strsql = strsql + "where sname ='" + cb_sear_sc_sname.Text + "'";
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    DataSet myset = new DataSet();//建立数据集对象
                    SqlDataAdapter da = new SqlDataAdapter(strsql, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                    SqlDataReader dr = dbquery.ExecuteReader();
                    while (dr.Read())
                    {
                        ListViewItem lt = new ListViewItem();
                        //将数据库数据转变成ListView类型的一行数据
                        lt.Text = dr["cname"].ToString();
                        lt.SubItems.Add(dr["sname"].ToString());
                        lv_sear_buss.Items.Add(lt);
                    }
                    dr.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                }
            }
            if (search_buss_mark == 2)
            {
                lv_sear_buss.Clear();
                lv_sear_buss.Columns.Add("教练名");
                lv_sear_buss.Columns.Add("车牌号");
                try
                {
                    string strsql = "select *  from cname_lic ";
                    //根据查询条件构造sql语句
                    if (chb_sear_buss_cc_cname.Checked == true && chb_sear_buss_cc_lic.Checked == false)
                        strsql = strsql + "where cname ='" + cb_sear_cc_cname.Text + "'";
                    if (chb_sear_buss_cc_cname.Checked == true && chb_sear_buss_cc_lic.Checked == true)
                        strsql = strsql + "where cname ='" + cb_sear_cc_cname.Text + "' and lic='" + cb_sear_cc_lic.Text + "'";
                    if (chb_sear_buss_cc_cname.Checked == false && chb_sear_buss_cc_lic.Checked == true)
                        strsql = strsql + "where lic ='" + cb_sear_cc_lic.Text + "'";
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    DataSet myset = new DataSet();//建立数据集对象
                    SqlDataAdapter da = new SqlDataAdapter(strsql, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                    SqlDataReader dr = dbquery.ExecuteReader();
                    while (dr.Read())
                    {
                        ListViewItem lt = new ListViewItem();
                        //将数据库数据转变成ListView类型的一行数据
                        lt.Text = dr["cname"].ToString();
                        lt.SubItems.Add(dr["lic"].ToString());
                        lv_sear_buss.Items.Add(lt);
                    }
                    dr.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                }
            }
            if (search_buss_mark == 3)
            {
                lv_sear_buss.Clear();
                lv_sear_buss.Columns.Add("学生姓名");
                lv_sear_buss.Columns.Add("学号");
                lv_sear_buss.Columns.Add("科目");
                lv_sear_buss.Columns.Add("成绩");
                try
                {
                    string strsql = "select *  from sname_grade where sname='" + cb_sear_exam_sname.Text + "' ";
                    if (cb_sear_exam_subject.SelectedIndex != -1)
                        strsql = strsql + " and sjname=" + (cb_sear_exam_subject.SelectedIndex + 1);
                    //根据查询条件构造sql语句
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    DataSet myset = new DataSet();//建立数据集对象
                    SqlDataAdapter da = new SqlDataAdapter(strsql, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                    SqlDataReader dr = dbquery.ExecuteReader();
                    while (dr.Read())
                    {
                        ListViewItem lt = new ListViewItem();
                        //将数据库数据转变成ListView类型的一行数据
                        lt.Text = dr["sname"].ToString();
                        lt.SubItems.Add(dr["sno"].ToString());
                        lt.SubItems.Add(dr["sjname"].ToString());
                        lt.SubItems.Add(dr["grade"].ToString());
                        lv_sear_buss.Items.Add(lt);
                    }
                    dr.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                }
            }
        }
        private void b_sear_cc_Click(object sender, EventArgs e)
        {
            l_sear_buss1.Text = "教练名";
            l_sear_buss2.Text = "车牌号";
            l_sear_buss1.Show();
            l_sear_buss2.Show();
            hide_sear_sc();
            show_sear_cc();
            hide_sear_exam();
            search_buss_mark = 2;
            lv_sear_buss.Clear();
            lv_sear_buss.Columns.Add("教练名");
            lv_sear_buss.Columns.Add("车牌号");
            try
            {
                string strsql = "select * from cname_lic ";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["cname"].ToString();
                    lt.SubItems.Add(dr["lic"].ToString());
                    lv_sear_buss.Items.Add(lt);
                }
                dr.Close();
                //生成教练姓名下拉单
                string str1 = "select distinct cname from cname_lic";
                DataSet myset1 = new DataSet();//建立数据集对象
                SqlDataAdapter da = new SqlDataAdapter(str1, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                da.Fill(myset1, "cname");//将查询数据填充到数据集的表中
                cb_sear_cc_cname.DataSource = myset1.Tables["cname"];
                cb_sear_cc_cname.DisplayMember = "cname";
                //生成车牌号下拉单
                string str2 = "select distinct lic from cname_lic";
                DataSet myset2 = new DataSet();//建立数据集对象
                SqlDataAdapter da1 = new SqlDataAdapter(str2, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                da1.Fill(myset2, "lic");//将查询数据填充到数据集的表中
                cb_sear_cc_lic.DataSource = myset2.Tables["lic"];//将combobox与数据源绑定
                cb_sear_cc_lic.DisplayMember = "lic";

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }

        }
        private void b_sear_exam_Click(object sender, EventArgs e)
        {
            l_sear_buss1.Text = "学员名";
            l_sear_buss2.Text = "科目";
            l_sear_buss1.Show();
            l_sear_buss2.Show();
            hide_sear_sc();
            hide_sear_cc();
            show_sear_exam();
            search_buss_mark = 3;
            lv_sear_buss.Clear();
            lv_sear_buss.Columns.Add("学生姓名");
            lv_sear_buss.Columns.Add("学号");
            lv_sear_buss.Columns.Add("科目");
            lv_sear_buss.Columns.Add("成绩");
            try
            {
                string strsql = "select * from sname_grade ";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["sname"].ToString();
                    lt.SubItems.Add(dr["sno"].ToString());
                    lt.SubItems.Add(dr["sjname"].ToString());
                    lt.SubItems.Add(dr["grade"].ToString());
                    lv_sear_buss.Items.Add(lt);
                }
                dr.Close();
                //生成教练姓名下拉单
                string str1 = "select distinct sname from sname_grade";
                DataSet myset1 = new DataSet();//建立数据集对象
                SqlDataAdapter da = new SqlDataAdapter(str1, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                da.Fill(myset1, "sname");//将查询数据填充到数据集的表中
                cb_sear_exam_sname.DataSource = myset1.Tables["sname"];
                cb_sear_exam_sname.DisplayMember = "sname";
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }

        }
        private void b_sear_car_accurate_Click(object sender, EventArgs e)
        {
            lv_sear_car.Items.Clear();
            try
            {
                string strsql = "select * from car ";
                int mark = 0;
                //根据查询条件构造sql语句
                if (chb_sear_car_lic.Checked == true)
                {
                    mark = 1;
                    strsql = strsql + " where lic ='" + cb_sear_car_lic.Text + "'";
                }
                if (chb_sear_car_color.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and color='" + cb_sear_car_color.Text + "'";
                    else strsql = strsql + " where color='" + cb_sear_car_color.Text + "'";
                    mark = 1;
                }
                if (chb_sear_car_brand.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and brand='" + cb_sear_car_brand.Text + "'";
                    else strsql = strsql + " where brand='" + cb_sear_car_brand.Text + "'";
                    mark = 1;
                }
                if (chb_sear_car_buytime.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and buytime between cast('" + dtp_sear_car_buytime_start.Value.ToString() + "' as datetime) and cast('" + dtp_sear_car_buytime_end.Value.ToString() + "' as datetime)";
                    else strsql = strsql + " where buytime between cast('" + dtp_sear_car_buytime_start.Value.ToString() + "' as datetime) and cast('" + dtp_sear_car_buytime_end.Value.ToString() + "' as datetime)";
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["lic"].ToString();
                    lt.SubItems.Add(dr["color"].ToString());
                    lt.SubItems.Add(dr["mile"].ToString());
                    lt.SubItems.Add(dr["brand"].ToString());
                    lt.SubItems.Add(dr["buytime"].ToString());
                    lv_sear_car.Items.Add(lt);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        private void search_tabcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (search_tabcontrol.SelectedTab.Name == "车辆信息查询_tabpage")
            {
                try
                {
                    //生成车牌号下拉单
                    string str1 = "select distinct lic from car";
                    DataSet myset1 = new DataSet();//建立数据集对象
                    SqlDataAdapter da = new SqlDataAdapter(str1, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da.Fill(myset1, "lic");//将查询数据填充到数据集的表中
                    cb_sear_car_lic.DataSource = myset1.Tables["lic"];
                    cb_sear_car_lic.DisplayMember = "lic";
                    //生成颜色姓名下拉单
                    string str2 = "select distinct color from car";
                    DataSet myset2 = new DataSet();//建立数据集对象
                    SqlDataAdapter da1 = new SqlDataAdapter(str2, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da1.Fill(myset2, "color");//将查询数据填充到数据集的表中
                    cb_sear_car_color.DataSource = myset2.Tables["color"];//将combobox与数据源绑定
                    cb_sear_car_color.DisplayMember = "color";
                    //生成厂商姓名下拉单
                    string str3 = "select distinct brand from car";
                    DataSet myset3 = new DataSet();//建立数据集对象
                    SqlDataAdapter da2 = new SqlDataAdapter(str3, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da2.Fill(myset3, "brand");//将查询数据填充到数据集的表中
                    cb_sear_car_brand.DataSource = myset3.Tables["brand"];//将combobox与数据源绑定
                    cb_sear_car_brand.DisplayMember = "brand";
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                }
            }
            if (search_tabcontrol.SelectedTab.Name == "学员信息_tabpage")
            {
                try
                {
                    //生成学员姓名下拉单
                    string str1 = "select distinct sname from student";
                    DataSet myset1 = new DataSet();//建立数据集对象
                    SqlDataAdapter da = new SqlDataAdapter(str1, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da.Fill(myset1, "sname");//将查询数据填充到数据集的表中
                    cb_sear_stu_sname.DataSource = myset1.Tables["sname"];
                    cb_sear_stu_sname.DisplayMember = "sname";
                    //生成学号下拉单
                    string str2 = "select distinct sno from student";
                    DataSet myset2 = new DataSet();//建立数据集对象
                    SqlDataAdapter da1 = new SqlDataAdapter(str2, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da1.Fill(myset2, "sno");//将查询数据填充到数据集的表中
                    cb_sear_stu_sno.DataSource = myset2.Tables["sno"];//将combobox与数据源绑定
                    cb_sear_stu_sno.DisplayMember = "sno";
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                }
            }
            if (search_tabcontrol.SelectedTab.Name == "教练信息_tabpage")
            {
                try
                {
                    //生成教练姓名下拉单
                    string str1 = "select distinct cname from coach";
                    DataSet myset1 = new DataSet();//建立数据集对象
                    SqlDataAdapter da = new SqlDataAdapter(str1, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da.Fill(myset1, "cname");//将查询数据填充到数据集的表中
                    cb_sear_coach_cname.DataSource = myset1.Tables["cname"];
                    cb_sear_coach_cname.DisplayMember = "cname";
                    //生成教练编号下拉单
                    string str2 = "select distinct cno from coach";
                    DataSet myset2 = new DataSet();//建立数据集对象
                    SqlDataAdapter da1 = new SqlDataAdapter(str2, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da1.Fill(myset2, "cno");//将查询数据填充到数据集的表中
                    cb_sear_coach_cno.DataSource = myset2.Tables["cno"];//将combobox与数据源绑定
                    cb_sear_coach_cno.DisplayMember = "cno";
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                }
            }
            if (search_tabcontrol.SelectedTab.Name == "收支信息_tabpage")
            {
                try
                {
                    //生成账单编号下拉单
                    string str1 = "select distinct bno from ename_exp";
                    DataSet myset1 = new DataSet();//建立数据集对象
                    SqlDataAdapter da = new SqlDataAdapter(str1, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da.Fill(myset1, "bno");//将查询数据填充到数据集的表中
                    cb_sear_acc_bno.DataSource = myset1.Tables["bno"];
                    cb_sear_acc_bno.DisplayMember = "bno";
                    //生成类型下拉单
                    string str2 = "select distinct ename from ename_exp";
                    DataSet myset2 = new DataSet();//建立数据集对象
                    SqlDataAdapter da1 = new SqlDataAdapter(str2, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da1.Fill(myset2, "ename");//将查询数据填充到数据集的表中
                    cb_sear_acc_ekno.DataSource = myset2.Tables["ename"];//将combobox与数据源绑定
                    cb_sear_acc_ekno.DisplayMember = "ename";
                    //生成车牌号下拉单
                    string str3 = "select distinct lic from ename_exp";
                    DataSet myset3 = new DataSet();//建立数据集对象
                    SqlDataAdapter da2 = new SqlDataAdapter(str3, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da2.Fill(myset3, "lic");//将查询数据填充到数据集的表中
                    cb_sear_acc_lic.DataSource = myset3.Tables["lic"];//将combobox与数据源绑定
                    cb_sear_acc_lic.DisplayMember = "lic";
                    //生成操作员下拉单
                    string str4 = "select distinct operator from ename_exp";
                    DataSet myset4 = new DataSet();//建立数据集对象
                    SqlDataAdapter da3 = new SqlDataAdapter(str4, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da3.Fill(myset4, "operator");//将查询数据填充到数据集的表中
                    cb_sear_acc_oper1.DataSource = myset4.Tables["operator"];//将combobox与数据源绑定
                    cb_sear_acc_oper1.DisplayMember = "operator";
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                }
                try
                {
                    //生成账单编号下拉单
                    string str1 = "select distinct ino from iname_sname_in";
                    DataSet myset1 = new DataSet();//建立数据集对象
                    SqlDataAdapter da = new SqlDataAdapter(str1, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da.Fill(myset1, "ino");//将查询数据填充到数据集的表中
                    cb_sear_acc_ino.DataSource = myset1.Tables["ino"];
                    cb_sear_acc_ino.DisplayMember = "ino";
                    //生成类型下拉单
                    string str2 = "select distinct iname from iname_sname_in";
                    DataSet myset2 = new DataSet();//建立数据集对象
                    SqlDataAdapter da1 = new SqlDataAdapter(str2, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da1.Fill(myset2, "iname");//将查询数据填充到数据集的表中
                    cb_sear_acc_ikno.DataSource = myset2.Tables["iname"];//将combobox与数据源绑定
                    cb_sear_acc_ikno.DisplayMember = "iname";
                    //生成学员姓名下拉单
                    string str3 = "select distinct sname from iname_sname_in";
                    DataSet myset3 = new DataSet();//建立数据集对象
                    SqlDataAdapter da2 = new SqlDataAdapter(str3, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da2.Fill(myset3, "sname");//将查询数据填充到数据集的表中
                    cb_sear_acc_sno.DataSource = myset3.Tables["sname"];//将combobox与数据源绑定
                    cb_sear_acc_sno.DisplayMember = "sname";
                    //生成操作员下拉单
                    string str4 = "select distinct operator from iname_sname_in";
                    DataSet myset4 = new DataSet();//建立数据集对象
                    SqlDataAdapter da3 = new SqlDataAdapter(str4, conn);//建立适配器对象,并初始化，执行sql语句，得到查询数据
                    da3.Fill(myset4, "operator");//将查询数据填充到数据集的表中
                    cb_sear_acc_oper2.DataSource = myset4.Tables["operator"];//将combobox与数据源绑定
                    cb_sear_acc_oper2.DisplayMember = "operator";
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                }
            }
        }
        private void b_sear_car_like_Click(object sender, EventArgs e)
        {
            lv_sear_car.Items.Clear();
            try
            {
                string strsql = "select * from car ";
                int mark = 0;
                //根据查询条件构造sql语句
                if (tb_sear_car_lic.Text.Trim() != "")
                {
                    mark = 1;
                    strsql = strsql + " where lic like '%" + tb_sear_car_lic.Text + "%'";
                }
                if (tb_sear_car_color.Text.Trim() != "")
                {
                    if (mark == 1)
                        strsql = strsql + " and color like '%" + tb_sear_car_color.Text + "%'";
                    else strsql = strsql + " where color like '%" + tb_sear_car_color.Text + "%'";
                    mark = 1;
                }
                if (tb_sear_car_brand.Text.Trim() != "")
                {
                    if (mark == 1)
                        strsql = strsql + " and brand like '%" + tb_sear_car_brand.Text + "%'";
                    else strsql = strsql + " where brand like '%" + tb_sear_car_brand.Text + "%'";
                    mark = 1;
                }
                if (chb_sear_car_buytime.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and buytime between cast('" + dtp_sear_car_buytime_start.Value.ToString() + "' as datetime) and cast('" + dtp_sear_car_buytime_end.Value.ToString() + "' as datetime)";
                    else strsql = strsql + " where buytime between cast('" + dtp_sear_car_buytime_start.Value.ToString() + "' as datetime) and cast('" + dtp_sear_car_buytime_end.Value.ToString() + "' as datetime)";
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["lic"].ToString();
                    lt.SubItems.Add(dr["color"].ToString());
                    lt.SubItems.Add(dr["mile"].ToString());
                    lt.SubItems.Add(dr["brand"].ToString());
                    lt.SubItems.Add(dr["buytime"].ToString());
                    lv_sear_car.Items.Add(lt);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        private void b_sear_stu_accurate_Click(object sender, EventArgs e)
        {
            lv_sear_stu.Items.Clear();
            try
            {
                string strsql = "select * from student ";
                int mark = 0;
                //根据查询条件构造sql语句
                if (chb_sear_stu_sname.Checked == true)
                {
                    mark = 1;
                    strsql = strsql + " where sname ='" + cb_sear_stu_sname.Text + "'";
                }
                if (chb_sear_stu_sno.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and sno='" + cb_sear_stu_sno.Text + "'";
                    else strsql = strsql + " where sno='" + cb_sear_stu_sno.Text + "'";
                    mark = 1;
                }
                if (cb_sear_stu_sex.SelectedIndex > 0)
                {
                    if (mark == 1)
                        strsql = strsql + " and sex='";
                    else strsql = strsql + " where sex='";

                    if (cb_sear_stu_sex.SelectedIndex == 1)
                        strsql = strsql + "男'";
                    if (cb_sear_stu_sex.SelectedIndex == 2)
                        strsql = strsql + "女'";
                    mark = 1;
                }
                if (cb_sear_stu_pay.SelectedIndex > 0)
                {
                    if (mark == 1)
                        strsql = strsql + " and pay=";
                    else strsql = strsql + " where pay=";

                    if (cb_sear_stu_pay.SelectedIndex == 1)
                        strsql = strsql + "1";
                    if (cb_sear_stu_pay.SelectedIndex == 2)
                        strsql = strsql + "0";
                    mark = 1;
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["sname"].ToString();
                    lt.SubItems.Add(dr["sno"].ToString());
                    lt.SubItems.Add(dr["phone"].ToString());
                    lt.SubItems.Add(dr["pay"].ToString());
                    lt.SubItems.Add(dr["sex"].ToString());
                    lt.SubItems.Add(dr["progress"].ToString());
                    lv_sear_stu.Items.Add(lt);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        private void b_sear_stu_like_Click(object sender, EventArgs e)
        {
            lv_sear_stu.Items.Clear();
            try
            {
                string strsql = "select * from student ";
                int mark = 0;
                //根据查询条件构造sql语句
                if (tb_sear_stu_sname.Text.Trim() != "")
                {
                    mark = 1;
                    strsql = strsql + " where sname like'%" + tb_sear_stu_sname.Text + "%'";
                }
                if (tb_sear_stu_sno.Text.Trim() != "")
                {
                    if (mark == 1)
                        strsql = strsql + " and sno like'%" + tb_sear_stu_sno.Text + "%'";
                    else strsql = strsql + " where sno like'%" + tb_sear_stu_sno.Text + "%'";
                    mark = 1;
                }
                if (cb_sear_stu_sex.SelectedIndex > 0)
                {
                    if (mark == 1)
                        strsql = strsql + " and sex='";
                    else strsql = strsql + " where sex='";

                    if (cb_sear_stu_sex.SelectedIndex == 1)
                        strsql = strsql + "男'";
                    if (cb_sear_stu_sex.SelectedIndex == 2)
                        strsql = strsql + "女'";
                    mark = 1;
                }
                if (cb_sear_stu_pay.SelectedIndex > 0)
                {
                    if (mark == 1)
                        strsql = strsql + " and pay=";
                    else strsql = strsql + " where pay=";

                    if (cb_sear_stu_pay.SelectedIndex == 1)
                        strsql = strsql + "1";
                    if (cb_sear_stu_pay.SelectedIndex == 2)
                        strsql = strsql + "0";
                    mark = 1;
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["sname"].ToString();
                    lt.SubItems.Add(dr["sno"].ToString());
                    lt.SubItems.Add(dr["phone"].ToString());
                    lt.SubItems.Add(dr["pay"].ToString());
                    lt.SubItems.Add(dr["sex"].ToString());
                    lt.SubItems.Add(dr["progress"].ToString());
                    lv_sear_stu.Items.Add(lt);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        private void b_sear_coach_accurate_Click(object sender, EventArgs e)
        {
            lv_sear_coach.Items.Clear();
            try
            {
                string strsql = "select * from coach ";
                int mark = 0;
                //根据查询条件构造sql语句
                if (chb_sear_coach_cname.Checked == true)
                {
                    mark = 1;
                    strsql = strsql + " where cname ='" + cb_sear_coach_cname.Text + "'";
                }
                if (chb_sear_coach_cno.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and cno='" + cb_sear_coach_cno.Text + "'";
                    else strsql = strsql + " where cno='" + cb_sear_coach_cno.Text + "'";
                    mark = 1;
                }
                if (cb_sear_coach_sex.SelectedIndex > 0)
                {
                    if (mark == 1)
                        strsql = strsql + " and sex='";
                    else strsql = strsql + " where sex='";

                    if (cb_sear_coach_sex.SelectedIndex == 1)
                        strsql = strsql + "男'";
                    if (cb_sear_coach_sex.SelectedIndex == 2)
                        strsql = strsql + "女'";
                    mark = 1;
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["cname"].ToString();
                    lt.SubItems.Add(dr["cno"].ToString());
                    lt.SubItems.Add(dr["phone"].ToString());
                    lt.SubItems.Add(dr["sex"].ToString());
                    lv_sear_coach.Items.Add(lt);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        private void b_sear_coach_like_Click(object sender, EventArgs e)
        {
            lv_sear_coach.Items.Clear();
            try
            {
                string strsql = "select * from coach ";
                int mark = 0;
                //根据查询条件构造sql语句
                if (tb_sear_coach_cname.Text.Trim() != "")
                {
                    mark = 1;
                    strsql = strsql + " where cname like'%" + tb_sear_coach_cname.Text + "%'";
                }
                if (tb_sear_coach_cno.Text.Trim() != "")
                {
                    if (mark == 1)
                        strsql = strsql + " and cno like'%" + tb_sear_coach_cno.Text + "%'";
                    else strsql = strsql + " where cno like'%" + tb_sear_coach_cno.Text + "%'";
                    mark = 1;
                }
                if (cb_sear_coach_sex.SelectedIndex > 0)
                {
                    if (mark == 1)
                        strsql = strsql + " and sex='";
                    else strsql = strsql + " where sex='";

                    if (cb_sear_coach_sex.SelectedIndex == 1)
                        strsql = strsql + "男'";
                    if (cb_sear_coach_sex.SelectedIndex == 2)
                        strsql = strsql + "女'";
                    mark = 1;
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["cname"].ToString();
                    lt.SubItems.Add(dr["cno"].ToString());
                    lt.SubItems.Add(dr["phone"].ToString());
                    lt.SubItems.Add(dr["sex"].ToString());
                    lv_sear_coach.Items.Add(lt);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        private void b_sear_acc_exp_Click(object sender, EventArgs e)
        {
            ch_sear_acc_object.Text = "车牌号";
            lv_sear_acc.Items.Clear();
            try
            {
                string strsql = "select * from ename_exp ";
                int mark = 0;
                //根据查询条件构造sql语句
                if (chb_sear_acc_bno.Checked == true)
                {
                    mark = 1;
                    strsql = strsql + " where bno ='" + cb_sear_acc_bno.Text + "'";
                }
                if (chb_sear_acc_ekno.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and ename='" + cb_sear_acc_ekno.Text + "'";
                    else strsql = strsql + " where ename='" + cb_sear_acc_ekno.Text + "'";
                    mark = 1;
                }
                if (chb_sear_acc_lic.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and lic='" + cb_sear_acc_lic.Text + "'";
                    else strsql = strsql + " where lic='" + cb_sear_acc_lic.Text + "'";
                    mark = 1;
                }
                if (chb_sear_acc_oper1.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and operator='" + cb_sear_acc_oper1.Text + "'";
                    else strsql = strsql + " where operator='" + cb_sear_acc_oper1.Text + "'";
                    mark = 1;
                }
                if (chb_sear_acc_edate.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and edate between cast('" + dtp_sear_acc_edate_start.Value.ToString() + "' as datetime) and cast('" + dtp_sear_acc_edate_end.Value.ToString() + "' as datetime)";
                    else strsql = strsql + " where edate between cast('" + dtp_sear_acc_edate_start.Value.ToString() + "' as datetime) and cast('" + dtp_sear_acc_edate_end.Value.ToString() + "' as datetime)";
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["bno"].ToString();
                    lt.SubItems.Add(dr["ename"].ToString());
                    lt.SubItems.Add(dr["lic"].ToString());
                    lt.SubItems.Add(dr["cash"].ToString());
                    lt.SubItems.Add(dr["edate"].ToString());
                    lt.SubItems.Add(dr["operator"].ToString());
                    lv_sear_acc.Items.Add(lt);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        private void b_sear_acc_in_Click(object sender, EventArgs e)
        {
            ch_sear_acc_object.Text = "学员名";
            lv_sear_acc.Items.Clear();
            try
            {
                string strsql = "select * from iname_sname_in ";
                int mark = 0;
                //根据查询条件构造sql语句
                if (chb_sear_acc_ino.Checked == true)
                {
                    mark = 1;
                    strsql = strsql + " where ino ='" + cb_sear_acc_ino.Text + "'";
                }
                if (chb_sear_acc_ikno.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and iname='" + cb_sear_acc_ikno.Text + "'";
                    else strsql = strsql + " where iname='" + cb_sear_acc_ikno.Text + "'";
                    mark = 1;
                }
                if (chb_sear_acc_sno.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and sname='" + cb_sear_acc_sno.Text + "'";
                    else strsql = strsql + " where sname='" + cb_sear_acc_sno.Text + "'";
                    mark = 1;
                }
                if (chb_sear_acc_oper2.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and operator='" + cb_sear_acc_oper2.Text + "'";
                    else strsql = strsql + " where operator='" + cb_sear_acc_oper1.Text + "'";
                    mark = 1;
                }
                if (chb_sear_acc_edate.Checked == true)
                {
                    if (mark == 1)
                        strsql = strsql + " and edate between cast('" + dtp_sear_acc_edate_start.Value.ToString() + "' as datetime) and cast('" + dtp_sear_acc_edate_end.Value.ToString() + "' as datetime)";
                    else strsql = strsql + " where edate between cast('" + dtp_sear_acc_edate_start.Value.ToString() + "' as datetime) and cast('" + dtp_sear_acc_edate_end.Value.ToString() + "' as datetime)";
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand dbquery = new SqlCommand(strsql, conn);            //构造sql命令
                SqlDataReader dr = dbquery.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    //将数据库数据转变成ListView类型的一行数据
                    lt.Text = dr["ino"].ToString();
                    lt.SubItems.Add(dr["iname"].ToString());
                    lt.SubItems.Add(dr["sname"].ToString());
                    lt.SubItems.Add(dr["cash"].ToString());
                    lt.SubItems.Add(dr["edate"].ToString());
                    lt.SubItems.Add(dr["operator"].ToString());
                    lv_sear_acc.Items.Add(lt);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }
        #endregion


    #region 业务
        /**
         * 编写人：唐胜洋
         * 时间：2018-1-1  20:28
         * 功能：下面这两个函数实现输入姓名自动找到学号、输入学号找到姓名
         */
        private void b_inc_sno_TextChanged(object sender, EventArgs e)
        {
            if (b_inc_sno.Text.Length == 8)
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
         * 编写人 唐胜洋
         * 时间2018-1-9
         * 功能：把datagridview的头变成中文
         * */
        private string Translate(string a) {
            string b = "";
            switch (a)
            {
                case "SNO": b = "学号";break;
                case "SNAME": b = "姓名"; break;
                case "PHONE": b = "电话"; break;
                case "PROGRESS": b = "进度"; break;
                case "SEX": b = "性别"; break;
                case "PAY": b = "缴费状态"; break;
                case "CNO": b = "工号"; break;
                case "CNAME": b = "姓名"; break;
                case "LIC": b = "车牌号"; break;
                case "COLOR": b = "颜色"; break;
                case "MILE": b = "里程"; break;
                case "BRAND": b = "品牌"; break;
                case "BUYTIME": b = "购买时间"; break;
                case "SJNAME": b = "科目"; break;
                case "GRADE": b = "成绩"; break;
                case "EDATE": b = "时间"; break;
                case "PLACE": b = "地点"; break;
                case "BNO": b = "账单号"; break;
                case "EKNO": b = "类型编号"; break;
                case "CASH": b = "金额"; break;
                case "OPERATOR": b = "操作者"; break;
                case "ENAME": b = "支出名"; break;
                case "EINTRO": b = "介绍"; break;
                case "INO": b = "账单号"; break;
                case "IKNO": b = "类型编号"; break;
                case "INAME": b = "收入名"; break;
                case "IINTRO": b = "介绍"; break;
                case "SINTRO": b = "介绍"; break;
            }

            return b;
        }

         private void etoc()
        {
            int len = b_dataview.Columns.Count;
            for(int i = 0; i < len; i++)
            {
                b_dataview.Columns[i].HeaderText = Translate(b_dataview.Columns[i].HeaderText);
            }
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
                b_dataview.DataSource = bing;etoc();
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
                b_dataview.DataSource = bing;etoc();
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
                b_dataview.DataSource = bing;etoc();
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
                b_dataview.DataSource = bing;etoc();
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
                b_dataview.DataSource = bing;etoc();
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
                        sql += "and SNO = dbo.SNA_SNO(@cna) ";
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
                    MessageBox.Show(em.Message);
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
                    MessageBox.Show(em.Message);
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
                    MessageBox.Show(em.Message);
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
            else if (sta_inc_c2.Text.Length > 0 && !Regex.IsMatch(sta_inc_c2.Text, @"[1-9]\d*"))
            {
                MessageBox.Show("金额输入不正确.");
                return;
            }
            else if ((sta_inc_c2.Text.Length ^ sta_inc_c1.Text.Length) == 1)
            {
                MessageBox.Show("请输入分数完整范围.");
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
                    MessageBox.Show(em.Message);
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
            else if (sta_exp_c2.Text.Length > 0 && !Regex.IsMatch(sta_exp_c2.Text, @"[1-9]\d*"))
            {
                MessageBox.Show("金额输入不正确.");
                return;
            }
            else if ((sta_exp_c2.Text.Length ^ sta_exp_c1.Text.Length) == 1)
            {
                MessageBox.Show("请输入分数完整范围.");
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
                    MessageBox.Show(em.Message);
                }
            }
        }


        #endregion

    #region 生成报表
        private void b_table_student_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");

            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.student out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";
                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");

        }

        private void b_table_coach_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.coach out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }

        private void b_table_car_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.car out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }

        private void b_table_sc_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.sc out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }

        private void b_table_cc_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.cc out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }

        private void b_table_exp_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.expenditure out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }

        private void b_table_ekind_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.e_kind out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }

        private void b_table_income_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.income out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }

        private void b_table_ikind_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.i_kind out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }

        private void b_table_grade_Click(object sender, EventArgs e)
        {
            if (tb_table_path.Text.Trim() == "") MessageBox.Show("请输入路径");
            else if (System.IO.File.Exists(tb_table_path.Text.Trim()))
            {
                string strsql = "EXEC master..xp_cmdshell 'bcp DriverSchool.dbo.sname_grade out " + tb_table_path.Text.Trim() + " -c -q -S\"(local)\" -U\"sa\" -P\"111\"' ";

                try
                {
                    int count = 0;
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(strsql, conn);  //构造sql命令
                    count = (int)cmd.ExecuteNonQuery();//执行该命令，插入一行数据
                    conn.Close();
                    MessageBox.Show("生成成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + "打开数据库失败");

                }
            }
            else MessageBox.Show("不存在该文件");
        }
        #endregion
        
        
    }
}

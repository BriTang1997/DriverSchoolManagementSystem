namespace test
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.L_mana = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.信息更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.信息查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.联系管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.L_madein = new System.Windows.Forms.Label();
            this.业务panel = new System.Windows.Forms.Panel();
            this.b_dataview = new System.Windows.Forms.DataGridView();
            this.bussiness_table = new System.Windows.Forms.TabControl();
            this.page_income = new System.Windows.Forms.TabPage();
            this.l_inc_ino_note = new System.Windows.Forms.Label();
            this.L_b_inc_ino = new System.Windows.Forms.Label();
            this.b_inc_ino = new System.Windows.Forms.TextBox();
            this.b_inc_add = new System.Windows.Forms.Button();
            this.b_inc_del = new System.Windows.Forms.Button();
            this.b_inc_sub = new System.Windows.Forms.Button();
            this.b_inc_note = new System.Windows.Forms.RichTextBox();
            this.L_b_inc_note = new System.Windows.Forms.Label();
            this.b_inc_kind = new System.Windows.Forms.ComboBox();
            this.L_b_inc_kind = new System.Windows.Forms.Label();
            this.L_b_inc_sna = new System.Windows.Forms.Label();
            this.b_inc_sna = new System.Windows.Forms.TextBox();
            this.L_b_inc_money = new System.Windows.Forms.Label();
            this.b_inc_money = new System.Windows.Forms.TextBox();
            this.L_b_inc_d = new System.Windows.Forms.Label();
            this.L_b_inc_m = new System.Windows.Forms.Label();
            this.L_b_inc_y = new System.Windows.Forms.Label();
            this.b_inc_day = new System.Windows.Forms.ComboBox();
            this.b_inc_mon = new System.Windows.Forms.ComboBox();
            this.b_inc_year = new System.Windows.Forms.ComboBox();
            this.L_b_inc_sno = new System.Windows.Forms.Label();
            this.b_inc_sno = new System.Windows.Forms.TextBox();
            this.page_expend = new System.Windows.Forms.TabPage();
            this.b_exp_add = new System.Windows.Forms.Button();
            this.b_exp_del = new System.Windows.Forms.Button();
            this.b_exp_sub = new System.Windows.Forms.Button();
            this.b_exp_note = new System.Windows.Forms.RichTextBox();
            this.L_b_exp_note = new System.Windows.Forms.Label();
            this.b_exp_kind = new System.Windows.Forms.ComboBox();
            this.L_b_exp_kind = new System.Windows.Forms.Label();
            this.L_b_exp_money = new System.Windows.Forms.Label();
            this.b_exp_money = new System.Windows.Forms.TextBox();
            this.L_b_exp_day = new System.Windows.Forms.Label();
            this.L_b_exp_mon = new System.Windows.Forms.Label();
            this.L_b_exp_year = new System.Windows.Forms.Label();
            this.b_exp_day = new System.Windows.Forms.ComboBox();
            this.b_exp_mon = new System.Windows.Forms.ComboBox();
            this.b_exp_year = new System.Windows.Forms.ComboBox();
            this.L_b_exp_lic = new System.Windows.Forms.Label();
            this.b_exp_lic = new System.Windows.Forms.TextBox();
            this.page_exam = new System.Windows.Forms.TabPage();
            this.page_sc = new System.Windows.Forms.TabPage();
            this.page_cc = new System.Windows.Forms.TabPage();
            this.L_b_exp_bno_not = new System.Windows.Forms.Label();
            this.L_b_exp_bno = new System.Windows.Forms.Label();
            this.b_exp_bno = new System.Windows.Forms.TextBox();
            this.menu.SuspendLayout();
            this.业务panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.b_dataview)).BeginInit();
            this.bussiness_table.SuspendLayout();
            this.page_income.SuspendLayout();
            this.page_expend.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "管理员：";
            // 
            // L_mana
            // 
            this.L_mana.AutoSize = true;
            this.L_mana.Location = new System.Drawing.Point(71, 420);
            this.L_mana.Name = "L_mana";
            this.L_mana.Size = new System.Drawing.Size(41, 12);
            this.L_mana.TabIndex = 1;
            this.L_mana.Text = "label2";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始ToolStripMenuItem,
            this.信息更新ToolStripMenuItem,
            this.信息查询ToolStripMenuItem,
            this.联系管理ToolStripMenuItem,
            this.打印报表ToolStripMenuItem,
            this.统计ToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(637, 25);
            this.menu.TabIndex = 2;
            this.menu.Text = "menuStrip1";
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.开始ToolStripMenuItem.Text = "开始";
            // 
            // 信息更新ToolStripMenuItem
            // 
            this.信息更新ToolStripMenuItem.Name = "信息更新ToolStripMenuItem";
            this.信息更新ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.信息更新ToolStripMenuItem.Text = "信息更新";
            // 
            // 信息查询ToolStripMenuItem
            // 
            this.信息查询ToolStripMenuItem.Name = "信息查询ToolStripMenuItem";
            this.信息查询ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.信息查询ToolStripMenuItem.Text = "信息查询";
            // 
            // 联系管理ToolStripMenuItem
            // 
            this.联系管理ToolStripMenuItem.Name = "联系管理ToolStripMenuItem";
            this.联系管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.联系管理ToolStripMenuItem.Text = "业务管理";
            this.联系管理ToolStripMenuItem.Click += new System.EventHandler(this.联系管理ToolStripMenuItem_Click);
            // 
            // 打印报表ToolStripMenuItem
            // 
            this.打印报表ToolStripMenuItem.Name = "打印报表ToolStripMenuItem";
            this.打印报表ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.打印报表ToolStripMenuItem.Text = "打印报表";
            // 
            // 统计ToolStripMenuItem
            // 
            this.统计ToolStripMenuItem.Name = "统计ToolStripMenuItem";
            this.统计ToolStripMenuItem.RightToLeftAutoMirrorImage = true;
            this.统计ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.统计ToolStripMenuItem.Text = "统计";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "时间";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // L_madein
            // 
            this.L_madein.AutoSize = true;
            this.L_madein.Location = new System.Drawing.Point(233, 420);
            this.L_madein.Name = "L_madein";
            this.L_madein.Size = new System.Drawing.Size(83, 12);
            this.L_madein.TabIndex = 8;
            this.L_madein.Text = "Made in China";
            // 
            // 业务panel
            // 
            this.业务panel.Controls.Add(this.b_dataview);
            this.业务panel.Controls.Add(this.bussiness_table);
            this.业务panel.Location = new System.Drawing.Point(12, 28);
            this.业务panel.Name = "业务panel";
            this.业务panel.Size = new System.Drawing.Size(613, 389);
            this.业务panel.TabIndex = 9;
            // 
            // b_dataview
            // 
            this.b_dataview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.b_dataview.Location = new System.Drawing.Point(3, 161);
            this.b_dataview.Name = "b_dataview";
            this.b_dataview.RowTemplate.Height = 23;
            this.b_dataview.Size = new System.Drawing.Size(586, 225);
            this.b_dataview.TabIndex = 1;
            // 
            // bussiness_table
            // 
            this.bussiness_table.Controls.Add(this.page_income);
            this.bussiness_table.Controls.Add(this.page_expend);
            this.bussiness_table.Controls.Add(this.page_exam);
            this.bussiness_table.Controls.Add(this.page_sc);
            this.bussiness_table.Controls.Add(this.page_cc);
            this.bussiness_table.Location = new System.Drawing.Point(3, 3);
            this.bussiness_table.Name = "bussiness_table";
            this.bussiness_table.SelectedIndex = 0;
            this.bussiness_table.Size = new System.Drawing.Size(590, 152);
            this.bussiness_table.TabIndex = 0;
            // 
            // page_income
            // 
            this.page_income.Controls.Add(this.l_inc_ino_note);
            this.page_income.Controls.Add(this.L_b_inc_ino);
            this.page_income.Controls.Add(this.b_inc_ino);
            this.page_income.Controls.Add(this.b_inc_add);
            this.page_income.Controls.Add(this.b_inc_del);
            this.page_income.Controls.Add(this.b_inc_sub);
            this.page_income.Controls.Add(this.b_inc_note);
            this.page_income.Controls.Add(this.L_b_inc_note);
            this.page_income.Controls.Add(this.b_inc_kind);
            this.page_income.Controls.Add(this.L_b_inc_kind);
            this.page_income.Controls.Add(this.L_b_inc_sna);
            this.page_income.Controls.Add(this.b_inc_sna);
            this.page_income.Controls.Add(this.L_b_inc_money);
            this.page_income.Controls.Add(this.b_inc_money);
            this.page_income.Controls.Add(this.L_b_inc_d);
            this.page_income.Controls.Add(this.L_b_inc_m);
            this.page_income.Controls.Add(this.L_b_inc_y);
            this.page_income.Controls.Add(this.b_inc_day);
            this.page_income.Controls.Add(this.b_inc_mon);
            this.page_income.Controls.Add(this.b_inc_year);
            this.page_income.Controls.Add(this.L_b_inc_sno);
            this.page_income.Controls.Add(this.b_inc_sno);
            this.page_income.Location = new System.Drawing.Point(4, 22);
            this.page_income.Name = "page_income";
            this.page_income.Padding = new System.Windows.Forms.Padding(3);
            this.page_income.Size = new System.Drawing.Size(582, 126);
            this.page_income.TabIndex = 0;
            this.page_income.Text = "入账";
            this.page_income.UseVisualStyleBackColor = true;
            this.page_income.Click += new System.EventHandler(this.page_income_Click);
            // 
            // l_inc_ino_note
            // 
            this.l_inc_ino_note.AutoSize = true;
            this.l_inc_ino_note.Location = new System.Drawing.Point(150, 69);
            this.l_inc_ino_note.Name = "l_inc_ino_note";
            this.l_inc_ino_note.Size = new System.Drawing.Size(83, 12);
            this.l_inc_ino_note.TabIndex = 22;
            this.l_inc_ino_note.Text = "如:IN00000001";
            // 
            // L_b_inc_ino
            // 
            this.L_b_inc_ino.AutoSize = true;
            this.L_b_inc_ino.Location = new System.Drawing.Point(6, 69);
            this.L_b_inc_ino.Name = "L_b_inc_ino";
            this.L_b_inc_ino.Size = new System.Drawing.Size(41, 12);
            this.L_b_inc_ino.TabIndex = 21;
            this.L_b_inc_ino.Text = "表单号";
            // 
            // b_inc_ino
            // 
            this.b_inc_ino.Location = new System.Drawing.Point(53, 66);
            this.b_inc_ino.Name = "b_inc_ino";
            this.b_inc_ino.Size = new System.Drawing.Size(88, 21);
            this.b_inc_ino.TabIndex = 20;
            // 
            // b_inc_add
            // 
            this.b_inc_add.Location = new System.Drawing.Point(8, 91);
            this.b_inc_add.Name = "b_inc_add";
            this.b_inc_add.Size = new System.Drawing.Size(85, 29);
            this.b_inc_add.TabIndex = 19;
            this.b_inc_add.Text = "增加进数据库";
            this.b_inc_add.UseVisualStyleBackColor = true;
            this.b_inc_add.Click += new System.EventHandler(this.b_inc_add_Click);
            // 
            // b_inc_del
            // 
            this.b_inc_del.Location = new System.Drawing.Point(155, 91);
            this.b_inc_del.Name = "b_inc_del";
            this.b_inc_del.Size = new System.Drawing.Size(71, 29);
            this.b_inc_del.TabIndex = 17;
            this.b_inc_del.Text = "删除";
            this.b_inc_del.UseVisualStyleBackColor = true;
            this.b_inc_del.Click += new System.EventHandler(this.b_inc_del_Click);
            // 
            // b_inc_sub
            // 
            this.b_inc_sub.Location = new System.Drawing.Point(233, 91);
            this.b_inc_sub.Name = "b_inc_sub";
            this.b_inc_sub.Size = new System.Drawing.Size(73, 29);
            this.b_inc_sub.TabIndex = 16;
            this.b_inc_sub.Text = "提交删除";
            this.b_inc_sub.UseVisualStyleBackColor = true;
            this.b_inc_sub.Click += new System.EventHandler(this.b_inc_sub_Click);
            // 
            // b_inc_note
            // 
            this.b_inc_note.Location = new System.Drawing.Point(314, 39);
            this.b_inc_note.Name = "b_inc_note";
            this.b_inc_note.Size = new System.Drawing.Size(265, 81);
            this.b_inc_note.TabIndex = 15;
            this.b_inc_note.Text = "";
            // 
            // L_b_inc_note
            // 
            this.L_b_inc_note.AutoSize = true;
            this.L_b_inc_note.Location = new System.Drawing.Point(519, 24);
            this.L_b_inc_note.Name = "L_b_inc_note";
            this.L_b_inc_note.Size = new System.Drawing.Size(53, 12);
            this.L_b_inc_note.TabIndex = 14;
            this.L_b_inc_note.Text = "类型说明";
            // 
            // b_inc_kind
            // 
            this.b_inc_kind.FormattingEnabled = true;
            this.b_inc_kind.Location = new System.Drawing.Point(185, 42);
            this.b_inc_kind.Name = "b_inc_kind";
            this.b_inc_kind.Size = new System.Drawing.Size(121, 20);
            this.b_inc_kind.TabIndex = 13;
            this.b_inc_kind.SelectedIndexChanged += new System.EventHandler(this.b_inc_kind_SelectedIndexChanged);
            // 
            // L_b_inc_kind
            // 
            this.L_b_inc_kind.AutoSize = true;
            this.L_b_inc_kind.Location = new System.Drawing.Point(150, 42);
            this.L_b_inc_kind.Name = "L_b_inc_kind";
            this.L_b_inc_kind.Size = new System.Drawing.Size(29, 12);
            this.L_b_inc_kind.TabIndex = 12;
            this.L_b_inc_kind.Text = "类型";
            // 
            // L_b_inc_sna
            // 
            this.L_b_inc_sna.AutoSize = true;
            this.L_b_inc_sna.Location = new System.Drawing.Point(6, 42);
            this.L_b_inc_sna.Name = "L_b_inc_sna";
            this.L_b_inc_sna.Size = new System.Drawing.Size(29, 12);
            this.L_b_inc_sna.TabIndex = 11;
            this.L_b_inc_sna.Text = "姓名";
            // 
            // b_inc_sna
            // 
            this.b_inc_sna.Location = new System.Drawing.Point(41, 39);
            this.b_inc_sna.Name = "b_inc_sna";
            this.b_inc_sna.Size = new System.Drawing.Size(100, 21);
            this.b_inc_sna.TabIndex = 10;
            this.b_inc_sna.TextChanged += new System.EventHandler(this.b_inc_sna_TextChanged);
            // 
            // L_b_inc_money
            // 
            this.L_b_inc_money.AutoSize = true;
            this.L_b_inc_money.Location = new System.Drawing.Point(153, 9);
            this.L_b_inc_money.Name = "L_b_inc_money";
            this.L_b_inc_money.Size = new System.Drawing.Size(29, 12);
            this.L_b_inc_money.TabIndex = 9;
            this.L_b_inc_money.Text = "金额";
            // 
            // b_inc_money
            // 
            this.b_inc_money.Location = new System.Drawing.Point(188, 6);
            this.b_inc_money.Name = "b_inc_money";
            this.b_inc_money.Size = new System.Drawing.Size(100, 21);
            this.b_inc_money.TabIndex = 8;
            // 
            // L_b_inc_d
            // 
            this.L_b_inc_d.AutoSize = true;
            this.L_b_inc_d.Location = new System.Drawing.Point(496, 12);
            this.L_b_inc_d.Name = "L_b_inc_d";
            this.L_b_inc_d.Size = new System.Drawing.Size(17, 12);
            this.L_b_inc_d.TabIndex = 7;
            this.L_b_inc_d.Text = "日";
            // 
            // L_b_inc_m
            // 
            this.L_b_inc_m.AutoSize = true;
            this.L_b_inc_m.Location = new System.Drawing.Point(422, 12);
            this.L_b_inc_m.Name = "L_b_inc_m";
            this.L_b_inc_m.Size = new System.Drawing.Size(17, 12);
            this.L_b_inc_m.TabIndex = 6;
            this.L_b_inc_m.Text = "月";
            // 
            // L_b_inc_y
            // 
            this.L_b_inc_y.AutoSize = true;
            this.L_b_inc_y.Location = new System.Drawing.Point(351, 12);
            this.L_b_inc_y.Name = "L_b_inc_y";
            this.L_b_inc_y.Size = new System.Drawing.Size(17, 12);
            this.L_b_inc_y.TabIndex = 5;
            this.L_b_inc_y.Text = "年";
            // 
            // b_inc_day
            // 
            this.b_inc_day.FormattingEnabled = true;
            this.b_inc_day.Location = new System.Drawing.Point(445, 9);
            this.b_inc_day.Name = "b_inc_day";
            this.b_inc_day.Size = new System.Drawing.Size(45, 20);
            this.b_inc_day.TabIndex = 4;
            // 
            // b_inc_mon
            // 
            this.b_inc_mon.FormattingEnabled = true;
            this.b_inc_mon.Location = new System.Drawing.Point(371, 9);
            this.b_inc_mon.Name = "b_inc_mon";
            this.b_inc_mon.Size = new System.Drawing.Size(45, 20);
            this.b_inc_mon.TabIndex = 3;
            // 
            // b_inc_year
            // 
            this.b_inc_year.FormattingEnabled = true;
            this.b_inc_year.Location = new System.Drawing.Point(300, 9);
            this.b_inc_year.Name = "b_inc_year";
            this.b_inc_year.Size = new System.Drawing.Size(45, 20);
            this.b_inc_year.TabIndex = 2;
            // 
            // L_b_inc_sno
            // 
            this.L_b_inc_sno.AutoSize = true;
            this.L_b_inc_sno.Location = new System.Drawing.Point(6, 9);
            this.L_b_inc_sno.Name = "L_b_inc_sno";
            this.L_b_inc_sno.Size = new System.Drawing.Size(29, 12);
            this.L_b_inc_sno.TabIndex = 1;
            this.L_b_inc_sno.Text = "学号";
            // 
            // b_inc_sno
            // 
            this.b_inc_sno.Location = new System.Drawing.Point(41, 6);
            this.b_inc_sno.Name = "b_inc_sno";
            this.b_inc_sno.Size = new System.Drawing.Size(100, 21);
            this.b_inc_sno.TabIndex = 0;
            this.b_inc_sno.TextChanged += new System.EventHandler(this.b_inc_sno_TextChanged);
            // 
            // page_expend
            // 
            this.page_expend.Controls.Add(this.L_b_exp_bno_not);
            this.page_expend.Controls.Add(this.L_b_exp_bno);
            this.page_expend.Controls.Add(this.b_exp_bno);
            this.page_expend.Controls.Add(this.b_exp_add);
            this.page_expend.Controls.Add(this.b_exp_del);
            this.page_expend.Controls.Add(this.b_exp_sub);
            this.page_expend.Controls.Add(this.b_exp_note);
            this.page_expend.Controls.Add(this.L_b_exp_note);
            this.page_expend.Controls.Add(this.b_exp_kind);
            this.page_expend.Controls.Add(this.L_b_exp_kind);
            this.page_expend.Controls.Add(this.L_b_exp_money);
            this.page_expend.Controls.Add(this.b_exp_money);
            this.page_expend.Controls.Add(this.L_b_exp_day);
            this.page_expend.Controls.Add(this.L_b_exp_mon);
            this.page_expend.Controls.Add(this.L_b_exp_year);
            this.page_expend.Controls.Add(this.b_exp_day);
            this.page_expend.Controls.Add(this.b_exp_mon);
            this.page_expend.Controls.Add(this.b_exp_year);
            this.page_expend.Controls.Add(this.L_b_exp_lic);
            this.page_expend.Controls.Add(this.b_exp_lic);
            this.page_expend.Location = new System.Drawing.Point(4, 22);
            this.page_expend.Name = "page_expend";
            this.page_expend.Padding = new System.Windows.Forms.Padding(3);
            this.page_expend.Size = new System.Drawing.Size(582, 126);
            this.page_expend.TabIndex = 1;
            this.page_expend.Text = "出账";
            this.page_expend.UseVisualStyleBackColor = true;
            this.page_expend.Click += new System.EventHandler(this.page_expend_Click);
            // 
            // b_exp_add
            // 
            this.b_exp_add.Location = new System.Drawing.Point(6, 91);
            this.b_exp_add.Name = "b_exp_add";
            this.b_exp_add.Size = new System.Drawing.Size(87, 29);
            this.b_exp_add.TabIndex = 39;
            this.b_exp_add.Text = "增加进数据库";
            this.b_exp_add.UseVisualStyleBackColor = true;
            this.b_exp_add.Click += new System.EventHandler(this.b_exp_add_Click);
            // 
            // b_exp_del
            // 
            this.b_exp_del.Location = new System.Drawing.Point(154, 91);
            this.b_exp_del.Name = "b_exp_del";
            this.b_exp_del.Size = new System.Drawing.Size(71, 29);
            this.b_exp_del.TabIndex = 37;
            this.b_exp_del.Text = "删除";
            this.b_exp_del.UseVisualStyleBackColor = true;
            this.b_exp_del.Click += new System.EventHandler(this.b_exp_del_Click);
            // 
            // b_exp_sub
            // 
            this.b_exp_sub.Location = new System.Drawing.Point(234, 91);
            this.b_exp_sub.Name = "b_exp_sub";
            this.b_exp_sub.Size = new System.Drawing.Size(73, 29);
            this.b_exp_sub.TabIndex = 36;
            this.b_exp_sub.Text = "提交删除";
            this.b_exp_sub.UseVisualStyleBackColor = true;
            this.b_exp_sub.Click += new System.EventHandler(this.b_exp_sub_Click);
            // 
            // b_exp_note
            // 
            this.b_exp_note.Location = new System.Drawing.Point(313, 40);
            this.b_exp_note.Name = "b_exp_note";
            this.b_exp_note.Size = new System.Drawing.Size(265, 80);
            this.b_exp_note.TabIndex = 35;
            this.b_exp_note.Text = "";
            // 
            // L_b_exp_note
            // 
            this.L_b_exp_note.AutoSize = true;
            this.L_b_exp_note.Location = new System.Drawing.Point(518, 25);
            this.L_b_exp_note.Name = "L_b_exp_note";
            this.L_b_exp_note.Size = new System.Drawing.Size(53, 12);
            this.L_b_exp_note.TabIndex = 34;
            this.L_b_exp_note.Text = "类型说明";
            // 
            // b_exp_kind
            // 
            this.b_exp_kind.FormattingEnabled = true;
            this.b_exp_kind.Location = new System.Drawing.Point(184, 43);
            this.b_exp_kind.Name = "b_exp_kind";
            this.b_exp_kind.Size = new System.Drawing.Size(121, 20);
            this.b_exp_kind.TabIndex = 33;
            this.b_exp_kind.SelectedIndexChanged += new System.EventHandler(this.b_exp_kind_SelectedIndexChanged);
            // 
            // L_b_exp_kind
            // 
            this.L_b_exp_kind.AutoSize = true;
            this.L_b_exp_kind.Location = new System.Drawing.Point(149, 43);
            this.L_b_exp_kind.Name = "L_b_exp_kind";
            this.L_b_exp_kind.Size = new System.Drawing.Size(29, 12);
            this.L_b_exp_kind.TabIndex = 32;
            this.L_b_exp_kind.Text = "类型";
            // 
            // L_b_exp_money
            // 
            this.L_b_exp_money.AutoSize = true;
            this.L_b_exp_money.Location = new System.Drawing.Point(152, 10);
            this.L_b_exp_money.Name = "L_b_exp_money";
            this.L_b_exp_money.Size = new System.Drawing.Size(29, 12);
            this.L_b_exp_money.TabIndex = 29;
            this.L_b_exp_money.Text = "金额";
            // 
            // b_exp_money
            // 
            this.b_exp_money.Location = new System.Drawing.Point(187, 7);
            this.b_exp_money.Name = "b_exp_money";
            this.b_exp_money.Size = new System.Drawing.Size(100, 21);
            this.b_exp_money.TabIndex = 28;
            // 
            // L_b_exp_day
            // 
            this.L_b_exp_day.AutoSize = true;
            this.L_b_exp_day.Location = new System.Drawing.Point(495, 13);
            this.L_b_exp_day.Name = "L_b_exp_day";
            this.L_b_exp_day.Size = new System.Drawing.Size(17, 12);
            this.L_b_exp_day.TabIndex = 27;
            this.L_b_exp_day.Text = "日";
            // 
            // L_b_exp_mon
            // 
            this.L_b_exp_mon.AutoSize = true;
            this.L_b_exp_mon.Location = new System.Drawing.Point(421, 13);
            this.L_b_exp_mon.Name = "L_b_exp_mon";
            this.L_b_exp_mon.Size = new System.Drawing.Size(17, 12);
            this.L_b_exp_mon.TabIndex = 26;
            this.L_b_exp_mon.Text = "月";
            // 
            // L_b_exp_year
            // 
            this.L_b_exp_year.AutoSize = true;
            this.L_b_exp_year.Location = new System.Drawing.Point(350, 13);
            this.L_b_exp_year.Name = "L_b_exp_year";
            this.L_b_exp_year.Size = new System.Drawing.Size(17, 12);
            this.L_b_exp_year.TabIndex = 25;
            this.L_b_exp_year.Text = "年";
            // 
            // b_exp_day
            // 
            this.b_exp_day.FormattingEnabled = true;
            this.b_exp_day.Location = new System.Drawing.Point(444, 10);
            this.b_exp_day.Name = "b_exp_day";
            this.b_exp_day.Size = new System.Drawing.Size(45, 20);
            this.b_exp_day.TabIndex = 24;
            // 
            // b_exp_mon
            // 
            this.b_exp_mon.FormattingEnabled = true;
            this.b_exp_mon.Location = new System.Drawing.Point(370, 10);
            this.b_exp_mon.Name = "b_exp_mon";
            this.b_exp_mon.Size = new System.Drawing.Size(45, 20);
            this.b_exp_mon.TabIndex = 23;
            // 
            // b_exp_year
            // 
            this.b_exp_year.FormattingEnabled = true;
            this.b_exp_year.Location = new System.Drawing.Point(299, 10);
            this.b_exp_year.Name = "b_exp_year";
            this.b_exp_year.Size = new System.Drawing.Size(45, 20);
            this.b_exp_year.TabIndex = 22;
            // 
            // L_b_exp_lic
            // 
            this.L_b_exp_lic.AutoSize = true;
            this.L_b_exp_lic.Location = new System.Drawing.Point(3, 10);
            this.L_b_exp_lic.Name = "L_b_exp_lic";
            this.L_b_exp_lic.Size = new System.Drawing.Size(41, 12);
            this.L_b_exp_lic.TabIndex = 21;
            this.L_b_exp_lic.Text = "车牌号";
            // 
            // b_exp_lic
            // 
            this.b_exp_lic.Location = new System.Drawing.Point(46, 7);
            this.b_exp_lic.Name = "b_exp_lic";
            this.b_exp_lic.Size = new System.Drawing.Size(100, 21);
            this.b_exp_lic.TabIndex = 20;
            // 
            // page_exam
            // 
            this.page_exam.Location = new System.Drawing.Point(4, 22);
            this.page_exam.Name = "page_exam";
            this.page_exam.Padding = new System.Windows.Forms.Padding(3);
            this.page_exam.Size = new System.Drawing.Size(582, 126);
            this.page_exam.TabIndex = 2;
            this.page_exam.Text = "考试";
            this.page_exam.UseVisualStyleBackColor = true;
            this.page_exam.Click += new System.EventHandler(this.page_exam_Click);
            // 
            // page_sc
            // 
            this.page_sc.Location = new System.Drawing.Point(4, 22);
            this.page_sc.Name = "page_sc";
            this.page_sc.Padding = new System.Windows.Forms.Padding(3);
            this.page_sc.Size = new System.Drawing.Size(582, 126);
            this.page_sc.TabIndex = 3;
            this.page_sc.Text = "学员教练匹配";
            this.page_sc.UseVisualStyleBackColor = true;
            this.page_sc.Click += new System.EventHandler(this.page_sc_Click);
            // 
            // page_cc
            // 
            this.page_cc.Location = new System.Drawing.Point(4, 22);
            this.page_cc.Name = "page_cc";
            this.page_cc.Padding = new System.Windows.Forms.Padding(3);
            this.page_cc.Size = new System.Drawing.Size(582, 126);
            this.page_cc.TabIndex = 4;
            this.page_cc.Text = "教练车辆分配";
            this.page_cc.UseVisualStyleBackColor = true;
            this.page_cc.Click += new System.EventHandler(this.page_cc_Click);
            // 
            // L_b_exp_bno_not
            // 
            this.L_b_exp_bno_not.AutoSize = true;
            this.L_b_exp_bno_not.Location = new System.Drawing.Point(52, 64);
            this.L_b_exp_bno_not.Name = "L_b_exp_bno_not";
            this.L_b_exp_bno_not.Size = new System.Drawing.Size(83, 12);
            this.L_b_exp_bno_not.TabIndex = 42;
            this.L_b_exp_bno_not.Text = "如:EX00000003";
            // 
            // L_b_exp_bno
            // 
            this.L_b_exp_bno.AutoSize = true;
            this.L_b_exp_bno.Location = new System.Drawing.Point(3, 43);
            this.L_b_exp_bno.Name = "L_b_exp_bno";
            this.L_b_exp_bno.Size = new System.Drawing.Size(41, 12);
            this.L_b_exp_bno.TabIndex = 41;
            this.L_b_exp_bno.Text = "账单号";
            // 
            // b_exp_bno
            // 
            this.b_exp_bno.Location = new System.Drawing.Point(46, 40);
            this.b_exp_bno.Name = "b_exp_bno";
            this.b_exp_bno.Size = new System.Drawing.Size(100, 21);
            this.b_exp_bno.TabIndex = 40;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 441);
            this.Controls.Add(this.业务panel);
            this.Controls.Add(this.L_madein);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.L_mana);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "驾校信息管理系统";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.业务panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.b_dataview)).EndInit();
            this.bussiness_table.ResumeLayout(false);
            this.page_income.ResumeLayout(false);
            this.page_income.PerformLayout();
            this.page_expend.ResumeLayout(false);
            this.page_expend.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label L_mana;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 信息更新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 信息查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 联系管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 统计ToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label L_madein;
        private System.Windows.Forms.Panel 业务panel;
        private System.Windows.Forms.TabControl bussiness_table;
        private System.Windows.Forms.TabPage page_income;
        private System.Windows.Forms.Label L_b_inc_sno;
        private System.Windows.Forms.TextBox b_inc_sno;
        private System.Windows.Forms.TabPage page_expend;
        private System.Windows.Forms.TabPage page_exam;
        private System.Windows.Forms.TabPage page_sc;
        private System.Windows.Forms.TabPage page_cc;
        private System.Windows.Forms.ComboBox b_inc_day;
        private System.Windows.Forms.ComboBox b_inc_mon;
        private System.Windows.Forms.ComboBox b_inc_year;
        private System.Windows.Forms.Label L_b_inc_money;
        private System.Windows.Forms.TextBox b_inc_money;
        private System.Windows.Forms.Label L_b_inc_d;
        private System.Windows.Forms.Label L_b_inc_m;
        private System.Windows.Forms.Label L_b_inc_y;
        private System.Windows.Forms.Label L_b_inc_sna;
        private System.Windows.Forms.TextBox b_inc_sna;
        private System.Windows.Forms.Button b_inc_sub;
        private System.Windows.Forms.RichTextBox b_inc_note;
        private System.Windows.Forms.Label L_b_inc_note;
        private System.Windows.Forms.ComboBox b_inc_kind;
        private System.Windows.Forms.Label L_b_inc_kind;
        private System.Windows.Forms.DataGridView b_dataview;
        private System.Windows.Forms.Button b_inc_add;
        private System.Windows.Forms.Button b_inc_del;
        private System.Windows.Forms.Button b_exp_add;
        private System.Windows.Forms.Button b_exp_del;
        private System.Windows.Forms.Button b_exp_sub;
        private System.Windows.Forms.RichTextBox b_exp_note;
        private System.Windows.Forms.Label L_b_exp_note;
        private System.Windows.Forms.ComboBox b_exp_kind;
        private System.Windows.Forms.Label L_b_exp_kind;
        private System.Windows.Forms.Label L_b_exp_money;
        private System.Windows.Forms.TextBox b_exp_money;
        private System.Windows.Forms.Label L_b_exp_day;
        private System.Windows.Forms.Label L_b_exp_mon;
        private System.Windows.Forms.Label L_b_exp_year;
        private System.Windows.Forms.ComboBox b_exp_day;
        private System.Windows.Forms.ComboBox b_exp_mon;
        private System.Windows.Forms.ComboBox b_exp_year;
        private System.Windows.Forms.Label L_b_exp_lic;
        private System.Windows.Forms.TextBox b_exp_lic;
        private System.Windows.Forms.Label l_inc_ino_note;
        private System.Windows.Forms.Label L_b_inc_ino;
        private System.Windows.Forms.TextBox b_inc_ino;
        private System.Windows.Forms.Label L_b_exp_bno_not;
        private System.Windows.Forms.Label L_b_exp_bno;
        private System.Windows.Forms.TextBox b_exp_bno;
    }
}
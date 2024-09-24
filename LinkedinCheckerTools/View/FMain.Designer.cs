namespace LinkedinCheckerTools.View
{
    partial class FMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.dtgrvdata = new System.Windows.Forms.DataGridView();
            this.clstt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clemail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clproxy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cllinkedObj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnimportproxy = new System.Windows.Forms.Button();
            this.btnimportdata = new System.Windows.Forms.Button();
            this.btnresultFolder = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.btnstart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdchecklkobj = new System.Windows.Forms.RadioButton();
            this.rdcheckdie = new System.Windows.Forms.RadioButton();
            this.rdchecklinked = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbbprxtype = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkuseproxy = new System.Windows.Forms.CheckBox();
            this.numthreads = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lbdataC = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lbprxC = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.lblinkedC = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.lbdieC = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.lbnotlkC = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel11 = new System.Windows.Forms.ToolStripLabel();
            this.lbsuccessC = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.lbretriesC = new System.Windows.Forms.ToolStripLabel();
            this.tmupdatecount = new System.Windows.Forms.Timer(this.components);
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.lbfailedC = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvdata)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numthreads)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgrvdata
            // 
            this.dtgrvdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrvdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clstt,
            this.clemail,
            this.clproxy,
            this.cllinkedObj,
            this.clstatus});
            this.dtgrvdata.Location = new System.Drawing.Point(14, 12);
            this.dtgrvdata.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.dtgrvdata.Name = "dtgrvdata";
            this.dtgrvdata.RowHeadersVisible = false;
            this.dtgrvdata.Size = new System.Drawing.Size(639, 314);
            this.dtgrvdata.TabIndex = 1;
            // 
            // clstt
            // 
            this.clstt.HeaderText = "#";
            this.clstt.Name = "clstt";
            this.clstt.Width = 60;
            // 
            // clemail
            // 
            this.clemail.HeaderText = "Email";
            this.clemail.Name = "clemail";
            // 
            // clproxy
            // 
            this.clproxy.HeaderText = "Proxy";
            this.clproxy.Name = "clproxy";
            // 
            // cllinkedObj
            // 
            this.cllinkedObj.HeaderText = "Linked Obj";
            this.cllinkedObj.Name = "cllinkedObj";
            this.cllinkedObj.Width = 120;
            // 
            // clstatus
            // 
            this.clstatus.HeaderText = "Status";
            this.clstatus.Name = "clstatus";
            this.clstatus.Width = 300;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnimportproxy);
            this.groupBox1.Controls.Add(this.btnimportdata);
            this.groupBox1.Controls.Add(this.btnresultFolder);
            this.groupBox1.Controls.Add(this.btnstop);
            this.groupBox1.Controls.Add(this.btnstart);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.numthreads);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 332);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(639, 139);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // btnimportproxy
            // 
            this.btnimportproxy.Location = new System.Drawing.Point(517, 50);
            this.btnimportproxy.Name = "btnimportproxy";
            this.btnimportproxy.Size = new System.Drawing.Size(110, 34);
            this.btnimportproxy.TabIndex = 30;
            this.btnimportproxy.Text = "Import Proxy";
            this.btnimportproxy.UseVisualStyleBackColor = true;
            this.btnimportproxy.Click += new System.EventHandler(this.btnimportproxy_Click);
            // 
            // btnimportdata
            // 
            this.btnimportdata.Location = new System.Drawing.Point(517, 12);
            this.btnimportdata.Name = "btnimportdata";
            this.btnimportdata.Size = new System.Drawing.Size(110, 34);
            this.btnimportdata.TabIndex = 29;
            this.btnimportdata.Text = "Import Data";
            this.btnimportdata.UseVisualStyleBackColor = true;
            this.btnimportdata.Click += new System.EventHandler(this.btnimportdata_Click);
            // 
            // btnresultFolder
            // 
            this.btnresultFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnresultFolder.Image")));
            this.btnresultFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnresultFolder.Location = new System.Drawing.Point(378, 87);
            this.btnresultFolder.Name = "btnresultFolder";
            this.btnresultFolder.Size = new System.Drawing.Size(131, 34);
            this.btnresultFolder.TabIndex = 28;
            this.btnresultFolder.Text = "Result Folder";
            this.btnresultFolder.UseVisualStyleBackColor = true;
            this.btnresultFolder.Click += new System.EventHandler(this.btnresultFolder_Click);
            // 
            // btnstop
            // 
            this.btnstop.Image = ((System.Drawing.Image)(resources.GetObject("btnstop.Image")));
            this.btnstop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnstop.Location = new System.Drawing.Point(378, 50);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(131, 34);
            this.btnstop.TabIndex = 27;
            this.btnstop.Text = "Stop";
            this.btnstop.UseVisualStyleBackColor = true;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // btnstart
            // 
            this.btnstart.Image = ((System.Drawing.Image)(resources.GetObject("btnstart.Image")));
            this.btnstart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnstart.Location = new System.Drawing.Point(378, 12);
            this.btnstart.Name = "btnstart";
            this.btnstart.Size = new System.Drawing.Size(131, 34);
            this.btnstart.TabIndex = 26;
            this.btnstart.Text = "Start";
            this.btnstart.UseVisualStyleBackColor = true;
            this.btnstart.Click += new System.EventHandler(this.btnstart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdchecklkobj);
            this.groupBox2.Controls.Add(this.rdcheckdie);
            this.groupBox2.Controls.Add(this.rdchecklinked);
            this.groupBox2.Location = new System.Drawing.Point(9, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 86);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tasks";
            // 
            // rdchecklkobj
            // 
            this.rdchecklkobj.AutoSize = true;
            this.rdchecklkobj.Location = new System.Drawing.Point(6, 55);
            this.rdchecklkobj.Name = "rdchecklkobj";
            this.rdchecklkobj.Size = new System.Drawing.Size(117, 19);
            this.rdchecklkobj.TabIndex = 2;
            this.rdchecklkobj.Text = "Check Linked Obj";
            this.rdchecklkobj.UseVisualStyleBackColor = true;
            this.rdchecklkobj.CheckedChanged += new System.EventHandler(this.rdchecklkobj_CheckedChanged);
            // 
            // rdcheckdie
            // 
            this.rdcheckdie.AutoSize = true;
            this.rdcheckdie.Location = new System.Drawing.Point(6, 37);
            this.rdcheckdie.Name = "rdcheckdie";
            this.rdcheckdie.Size = new System.Drawing.Size(78, 19);
            this.rdcheckdie.TabIndex = 1;
            this.rdcheckdie.Text = "Check Die";
            this.rdcheckdie.UseVisualStyleBackColor = true;
            this.rdcheckdie.CheckedChanged += new System.EventHandler(this.rdcheckdie_CheckedChanged);
            // 
            // rdchecklinked
            // 
            this.rdchecklinked.AutoSize = true;
            this.rdchecklinked.Checked = true;
            this.rdchecklinked.Location = new System.Drawing.Point(6, 18);
            this.rdchecklinked.Name = "rdchecklinked";
            this.rdchecklinked.Size = new System.Drawing.Size(95, 19);
            this.rdchecklinked.TabIndex = 0;
            this.rdchecklinked.TabStop = true;
            this.rdchecklinked.Text = "Check Linked";
            this.rdchecklinked.UseVisualStyleBackColor = true;
            this.rdchecklinked.CheckedChanged += new System.EventHandler(this.rdchecklinked_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbbprxtype);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.chkuseproxy);
            this.groupBox3.Location = new System.Drawing.Point(149, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(223, 113);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proxy Settings";
            // 
            // cbbprxtype
            // 
            this.cbbprxtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbprxtype.FormattingEnabled = true;
            this.cbbprxtype.Items.AddRange(new object[] {
            "Http",
            "Socks4",
            "Socks5"});
            this.cbbprxtype.Location = new System.Drawing.Point(86, 39);
            this.cbbprxtype.Name = "cbbprxtype";
            this.cbbprxtype.Size = new System.Drawing.Size(87, 23);
            this.cbbprxtype.TabIndex = 5;
            this.cbbprxtype.SelectedIndexChanged += new System.EventHandler(this.cbbprxtype_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Proxy Type";
            // 
            // chkuseproxy
            // 
            this.chkuseproxy.AutoSize = true;
            this.chkuseproxy.Location = new System.Drawing.Point(6, 20);
            this.chkuseproxy.Name = "chkuseproxy";
            this.chkuseproxy.Size = new System.Drawing.Size(78, 19);
            this.chkuseproxy.TabIndex = 0;
            this.chkuseproxy.Text = "Use Proxy";
            this.chkuseproxy.UseVisualStyleBackColor = true;
            this.chkuseproxy.CheckedChanged += new System.EventHandler(this.chkuseproxy_CheckedChanged);
            // 
            // numthreads
            // 
            this.numthreads.Location = new System.Drawing.Point(61, 20);
            this.numthreads.Name = "numthreads";
            this.numthreads.Size = new System.Drawing.Size(53, 23);
            this.numthreads.TabIndex = 3;
            this.numthreads.ValueChanged += new System.EventHandler(this.numthreads_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Threads";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.lbdataC,
            this.toolStripSeparator1,
            this.toolStripLabel3,
            this.lbprxC,
            this.toolStripSeparator2,
            this.toolStripLabel5,
            this.lblinkedC,
            this.toolStripSeparator3,
            this.toolStripLabel7,
            this.lbdieC,
            this.toolStripSeparator4,
            this.toolStripLabel9,
            this.lbnotlkC,
            this.toolStripSeparator5,
            this.toolStripLabel11,
            this.lbsuccessC,
            this.toolStripSeparator6,
            this.toolStripLabel4,
            this.lbfailedC,
            this.toolStripSeparator7,
            this.toolStripLabel2,
            this.lbretriesC});
            this.toolStrip1.Location = new System.Drawing.Point(0, 480);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(667, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "Data";
            // 
            // lbdataC
            // 
            this.lbdataC.Name = "lbdataC";
            this.lbdataC.Size = new System.Drawing.Size(14, 22);
            this.lbdataC.Text = "0";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel3.Text = "Proxies";
            // 
            // lbprxC
            // 
            this.lbprxC.Name = "lbprxC";
            this.lbprxC.Size = new System.Drawing.Size(14, 22);
            this.lbprxC.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.ForeColor = System.Drawing.Color.Green;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel5.Text = "Linked";
            // 
            // lblinkedC
            // 
            this.lblinkedC.ForeColor = System.Drawing.Color.Green;
            this.lblinkedC.Name = "lblinkedC";
            this.lblinkedC.Size = new System.Drawing.Size(14, 22);
            this.lblinkedC.Text = "0";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.ForeColor = System.Drawing.Color.Maroon;
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(25, 22);
            this.toolStripLabel7.Text = "Die";
            // 
            // lbdieC
            // 
            this.lbdieC.ForeColor = System.Drawing.Color.Maroon;
            this.lbdieC.Name = "lbdieC";
            this.lbdieC.Size = new System.Drawing.Size(14, 22);
            this.lbdieC.Text = "0";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel9.Text = "Not Linked";
            // 
            // lbnotlkC
            // 
            this.lbnotlkC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbnotlkC.Name = "lbnotlkC";
            this.lbnotlkC.Size = new System.Drawing.Size(14, 22);
            this.lbnotlkC.Text = "0";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel11
            // 
            this.toolStripLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.toolStripLabel11.Name = "toolStripLabel11";
            this.toolStripLabel11.Size = new System.Drawing.Size(49, 22);
            this.toolStripLabel11.Text = "Success";
            // 
            // lbsuccessC
            // 
            this.lbsuccessC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbsuccessC.Name = "lbsuccessC";
            this.lbsuccessC.Size = new System.Drawing.Size(14, 22);
            this.lbsuccessC.Text = "0";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel2.Text = "Retries";
            // 
            // lbretriesC
            // 
            this.lbretriesC.Name = "lbretriesC";
            this.lbretriesC.Size = new System.Drawing.Size(14, 22);
            this.lbretriesC.Text = "0";
            // 
            // tmupdatecount
            // 
            this.tmupdatecount.Interval = 1000;
            this.tmupdatecount.Tick += new System.EventHandler(this.tmupdatecount_Tick);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabel4.Text = "Failed";
            // 
            // lbfailedC
            // 
            this.lbfailedC.Name = "lbfailedC";
            this.lbfailedC.Size = new System.Drawing.Size(14, 22);
            this.lbfailedC.Text = "0";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 505);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtgrvdata);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FMain";
            this.Load += new System.EventHandler(this.FMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvdata)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numthreads)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgrvdata;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numthreads;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbbprxtype;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkuseproxy;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lbdataC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel lbprxC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel lblinkedC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripLabel lbdieC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel9;
        private System.Windows.Forms.ToolStripLabel lbnotlkC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel11;
        private System.Windows.Forms.ToolStripLabel lbsuccessC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdchecklkobj;
        private System.Windows.Forms.RadioButton rdcheckdie;
        private System.Windows.Forms.RadioButton rdchecklinked;
        private System.Windows.Forms.Button btnresultFolder;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.Button btnstart;
        private System.Windows.Forms.Button btnimportproxy;
        private System.Windows.Forms.Button btnimportdata;
        private System.Windows.Forms.DataGridViewTextBoxColumn clstt;
        private System.Windows.Forms.DataGridViewTextBoxColumn clemail;
        private System.Windows.Forms.DataGridViewTextBoxColumn clproxy;
        private System.Windows.Forms.DataGridViewTextBoxColumn cllinkedObj;
        private System.Windows.Forms.DataGridViewTextBoxColumn clstatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel lbretriesC;
        private System.Windows.Forms.Timer tmupdatecount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel lbfailedC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}
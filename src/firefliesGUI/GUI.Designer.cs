namespace firefliesGUI
{
    partial class GUI
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
            this.mainTable1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainTable2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainTable4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.getAdminsAccessID = new System.Windows.Forms.Button();
            this.getStudentsAccessID = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.startTask = new System.Windows.Forms.Button();
            this.dateRange = new System.Windows.Forms.Label();
            this.txtDateRange = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.mainTable1.SuspendLayout();
            this.mainTable2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.mainTable4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTable1
            // 
            this.mainTable1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.mainTable1.ColumnCount = 2;
            this.mainTable1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTable1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTable1.Controls.Add(this.mainTable2, 0, 0);
            this.mainTable1.Controls.Add(this.richTextBox1, 1, 0);
            this.mainTable1.Location = new System.Drawing.Point(9, 9);
            this.mainTable1.Margin = new System.Windows.Forms.Padding(0);
            this.mainTable1.Name = "mainTable1";
            this.mainTable1.RowCount = 1;
            this.mainTable1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 543F));
            this.mainTable1.Size = new System.Drawing.Size(966, 543);
            this.mainTable1.TabIndex = 0;
            // 
            // mainTable2
            // 
            this.mainTable2.ColumnCount = 1;
            this.mainTable2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.mainTable2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.mainTable2.Location = new System.Drawing.Point(4, 4);
            this.mainTable2.Name = "mainTable2";
            this.mainTable2.RowCount = 2;
            this.mainTable2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.0298F));
            this.mainTable2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.97021F));
            this.mainTable2.Size = new System.Drawing.Size(475, 535);
            this.mainTable2.TabIndex = 2;
            this.mainTable2.Paint += new System.Windows.Forms.PaintEventHandler(this.mainTable2_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.mainTable4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.36364F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.63636F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(469, 165);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // mainTable4
            // 
            this.mainTable4.ColumnCount = 1;
            this.mainTable4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable4.Controls.Add(this.txtPassword, 0, 3);
            this.mainTable4.Controls.Add(this.Password, 0, 2);
            this.mainTable4.Controls.Add(this.Email, 0, 0);
            this.mainTable4.Controls.Add(this.txtEmail, 0, 1);
            this.mainTable4.Location = new System.Drawing.Point(4, 4);
            this.mainTable4.Name = "mainTable4";
            this.mainTable4.RowCount = 4;
            this.mainTable4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTable4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTable4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTable4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTable4.Size = new System.Drawing.Size(461, 117);
            this.mainTable4.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(3, 90);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(455, 30);
            this.txtPassword.TabIndex = 3;
            // 
            // Password
            // 
            this.Password.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Password.AutoSize = true;
            this.Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.Location = new System.Drawing.Point(3, 62);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(98, 25);
            this.Password.TabIndex = 2;
            this.Password.Text = "Password";
            // 
            // Email
            // 
            this.Email.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Email.AutoSize = true;
            this.Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Email.Location = new System.Drawing.Point(3, 4);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(60, 25);
            this.Email.TabIndex = 0;
            this.Email.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(3, 32);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(455, 30);
            this.txtEmail.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.getAdminsAccessID, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.getStudentsAccessID, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 128);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(461, 31);
            this.tableLayoutPanel2.TabIndex = 4;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // getAdminsAccessID
            // 
            this.getAdminsAccessID.Location = new System.Drawing.Point(233, 3);
            this.getAdminsAccessID.Name = "getAdminsAccessID";
            this.getAdminsAccessID.Size = new System.Drawing.Size(225, 25);
            this.getAdminsAccessID.TabIndex = 1;
            this.getAdminsAccessID.Text = "Get Admins AccessID";
            this.getAdminsAccessID.UseVisualStyleBackColor = true;
            // 
            // getStudentsAccessID
            // 
            this.getStudentsAccessID.Location = new System.Drawing.Point(3, 3);
            this.getStudentsAccessID.Name = "getStudentsAccessID";
            this.getStudentsAccessID.Size = new System.Drawing.Size(224, 25);
            this.getStudentsAccessID.TabIndex = 0;
            this.getStudentsAccessID.Text = "Get Students AccessID";
            this.getStudentsAccessID.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.startTask, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.dateRange, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtDateRange, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 174);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.077994F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.922F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(469, 358);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // startTask
            // 
            this.startTask.Location = new System.Drawing.Point(238, 4);
            this.startTask.Name = "startTask";
            this.startTask.Size = new System.Drawing.Size(227, 22);
            this.startTask.TabIndex = 5;
            this.startTask.Text = "Start Task";
            this.startTask.UseVisualStyleBackColor = true;
            // 
            // dateRange
            // 
            this.dateRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateRange.AutoSize = true;
            this.dateRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRange.Location = new System.Drawing.Point(4, 4);
            this.dateRange.Name = "dateRange";
            this.dateRange.Size = new System.Drawing.Size(115, 25);
            this.dateRange.TabIndex = 3;
            this.dateRange.Text = "Date Range";
            // 
            // txtDateRange
            // 
            this.txtDateRange.Location = new System.Drawing.Point(4, 33);
            this.txtDateRange.Name = "txtDateRange";
            this.txtDateRange.Size = new System.Drawing.Size(227, 321);
            this.txtDateRange.TabIndex = 4;
            this.txtDateRange.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.richTextBox1.Font = new System.Drawing.Font("Google Sans Code", 11.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Location = new System.Drawing.Point(486, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(476, 535);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.mainTable1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GUI";
            this.ShowIcon = false;
            this.Text = "firefliesGUI";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.mainTable1.ResumeLayout(false);
            this.mainTable2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.mainTable4.ResumeLayout(false);
            this.mainTable4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTable1;
        private System.Windows.Forms.TableLayoutPanel mainTable2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel mainTable4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button getStudentsAccessID;
        private System.Windows.Forms.Button getAdminsAccessID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button startTask;
        private System.Windows.Forms.Label dateRange;
        private System.Windows.Forms.RichTextBox txtDateRange;
    }
}


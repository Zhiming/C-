namespace _20120821_1_CodeGenerator
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.clbList = new System.Windows.Forms.CheckedListBox();
            this.cbModel = new System.Windows.Forms.CheckBox();
            this.cbDAL = new System.Windows.Forms.CheckBox();
            this.cbBLL = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(168, 12);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(194, 21);
            this.txtDatabaseName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "DataBase Name:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(203, 177);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(109, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generator Code";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // clbList
            // 
            this.clbList.FormattingEnabled = true;
            this.clbList.Location = new System.Drawing.Point(48, 57);
            this.clbList.Name = "clbList";
            this.clbList.Size = new System.Drawing.Size(124, 340);
            this.clbList.TabIndex = 3;
            // 
            // cbModel
            // 
            this.cbModel.AutoSize = true;
            this.cbModel.Checked = true;
            this.cbModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbModel.Location = new System.Drawing.Point(203, 57);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(54, 16);
            this.cbModel.TabIndex = 4;
            this.cbModel.Text = "Model";
            this.cbModel.UseVisualStyleBackColor = true;
            // 
            // cbDAL
            // 
            this.cbDAL.AutoSize = true;
            this.cbDAL.Checked = true;
            this.cbDAL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDAL.Location = new System.Drawing.Point(320, 57);
            this.cbDAL.Name = "cbDAL";
            this.cbDAL.Size = new System.Drawing.Size(42, 16);
            this.cbDAL.TabIndex = 5;
            this.cbDAL.Text = "DAL";
            this.cbDAL.UseVisualStyleBackColor = true;
            // 
            // cbBLL
            // 
            this.cbBLL.AutoSize = true;
            this.cbBLL.Checked = true;
            this.cbBLL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBLL.Location = new System.Drawing.Point(422, 57);
            this.cbBLL.Name = "cbBLL";
            this.cbBLL.Size = new System.Drawing.Size(42, 16);
            this.cbBLL.TabIndex = 6;
            this.cbBLL.Text = "BLL";
            this.cbBLL.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(389, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(203, 222);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(261, 175);
            this.txtStatus.TabIndex = 8;
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(278, 91);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(186, 21);
            this.txtNamespace.TabIndex = 9;
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(278, 134);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(186, 21);
            this.txtOutputPath.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "Namespace:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "Output Path:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 423);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cbBLL);
            this.Controls.Add(this.cbDAL);
            this.Controls.Add(this.cbModel);
            this.Controls.Add(this.clbList);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDatabaseName);
            this.Name = "Form1";
            this.Text = "Code Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.CheckedListBox clbList;
        private System.Windows.Forms.CheckBox cbModel;
        private System.Windows.Forms.CheckBox cbDAL;
        private System.Windows.Forms.CheckBox cbBLL;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}


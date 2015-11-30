using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _20120821_1_CodeGenerator.library;
using _20120821_1_CodeGenerator.BLL;

namespace _20120821_1_CodeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtDatabaseName.Text == "")
            {
                MessageBox.Show("Please enter the name of a database");
                return;
            }
            clbList.Items.Clear();
            if (txtDatabaseName.Text == null) {
                MessageBox.Show("Please enter database name");
            }
            GlobalParameters.DatabaseName = txtDatabaseName.Text;
            DataTable dt = SqlHelperController.ShowTable();
            if (dt == null) {
                MessageBox.Show("No table in the database");
            }
            foreach (DataRow dr in dt.Rows) {
                clbList.Items.Add(dr["table_name"].ToString());
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtNamespace.Text == "")
            {
                MessageBox.Show("Please Enter Namespace");
                return;
            }
            string nameSpace = txtNamespace.Text;

            if (txtOutputPath.Text == "")
            {
                MessageBox.Show("Please Enter Output Path");
                return;
            }
            string outputPath = txtOutputPath.Text;

            if (clbList.CheckedItems.Count == 0) {
                MessageBox.Show("Please select at least one table");
                return;
            }

            foreach (string tableName in clbList.CheckedItems)
            {
                if (cbModel.Checked) {
                    SqlHelperController.CreateModel(tableName, nameSpace, outputPath);
                    txtStatus.AppendText("Model generating...");
                }
                if (cbBLL.Checked) {
                    SqlHelperController.CreateBLL(tableName, nameSpace, outputPath);
                    txtStatus.AppendText("\r\nBLL generating...");
                }
                if (cbDAL.Checked) {
                    SqlHelperController.CreateDAL(tableName, nameSpace, outputPath);
                    txtStatus.AppendText("\r\nDAL generating...");
                }  
            }
            txtStatus.AppendText("\r\nComplete...");
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectRename
{
    public partial class Form1 : Form
    {
        Project currentProject;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                try
                {
                    currentProject = new Project(textBox1.Text);
                    dataGridView1.DataSource = new BindingSource() { DataSource = currentProject.UnnamedFiles };
                }
                catch (DirectoryNotFoundException)
                {
                    dataGridView1.DataSource = null;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentProject.RenameUnnamedFiles();
            textBox1.Text = "";
            dataGridView1.DataSource = null;
            currentProject = null;
        }
    }
}

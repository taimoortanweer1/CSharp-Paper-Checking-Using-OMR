using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Page
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "case" && textBox2.Text == "case")
            {
                MessageBox.Show("Login Successful");
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
        }

        

       
    }
}

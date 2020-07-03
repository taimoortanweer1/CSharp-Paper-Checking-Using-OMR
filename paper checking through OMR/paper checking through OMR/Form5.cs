using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paper_checking_through_OMR
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "case" && textBox2.Text == "case")
            {
                MessageBox.Show("Login Successful");
                Form1 p = new Form1(this);
                p.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
        }
    }
}

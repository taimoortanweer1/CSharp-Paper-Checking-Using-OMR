using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hassan
{
    public partial class Form2 : Form
    {
        Form1 p;
        public Form2(Form1 f)
        {
            InitializeComponent();
            p = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.Show();
            this.Hide();
        }
    }
}

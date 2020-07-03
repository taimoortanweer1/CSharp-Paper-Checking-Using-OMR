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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("10,20");
            listBox1.Items.Add("15,22");
            listBox1.Items.Add("11,25");
            listBox1.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a0, a1, a2;
            string[] piece;
            a0 = listBox1.SelectedItem.ToString();
            piece=a0.Split(',');

            MessageBox.Show(piece[0].ToString());
            MessageBox.Show(piece[1].ToString());


            listBox2.Items.Add(piece[0].ToString());
            listBox3.Items.Add(piece[1].ToString());


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 p = new Form2(this);
            p.Show();
            this.Hide();
        }
    }
}

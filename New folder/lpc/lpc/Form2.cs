using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace lpc
{
    public partial class Form2 : Form
    {
        string s1;
        string s2;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s2 = textBox2.Text;
          //  textBox3.Text = textBox1.Text;
            if (s1 == s2)
            {
            //    textBox4.Text = "true";
                MessageBox.Show("Login Successfull");
                Form1 p = new Form1(this);
                p.Show();
                this.Hide();

            }
            else
            {
             //   textBox4.Text = "false";
                MessageBox.Show("Invalid Password");
            
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=lpc.accdb";

            OleDbConnection conn = new OleDbConnection(constring);

            string sql = "SELECT * FROM Table1 where roll = '" + textBox1.Text + "'";

            OleDbCommand cmd = new OleDbCommand(sql, conn);

            conn.Open();

            OleDbDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                this.textBox1.Text = reader["roll"].ToString();
                s1 = reader["pass_Word"].ToString();
                break;
            }
            reader.Close();
            conn.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace paper_checking_through_OMR
{
    public partial class Form3 : Form
    {
        Form2 p;
        string s1;
        string s2;
        public Form3(Form2 f)
        {
            InitializeComponent();
            p = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form4 p = new Form4(this);
            //p.Show();
            //this.Hide();
            s2 = textBox2.Text;
            //  textBox3.Text = textBox1.Text;
            if (s1 == s2)
            {
                //    textBox4.Text = "true";
                MessageBox.Show("Login Successfull");
                Form4 p = new Form4(this);
                p.Show();
                this.Hide();

            }
            else
            {
                //   textBox4.Text = "false";
                MessageBox.Show("Invalid Password");

            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb";

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication17
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

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database88.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();
            stb.Append("INSERT into Table1 (std_name,roll) " + " VALUES ('" + textBox1.Text + "','" + textBox2.Text + "')");

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Insert Query Run Success Fully");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database88.accdb";

            OleDbConnection conn = new OleDbConnection(constring);

            string sql = "SELECT * FROM Table1 where roll = '" + textBox1.Text + "'";

            OleDbCommand cmd = new OleDbCommand(sql, conn);

            conn.Open();

            OleDbDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                this.textBox2.Text = reader["std_name"].ToString();
                this.textBox3.Text = reader["f_name"].ToString();
                this.textBox4.Text = reader["phone_num"].ToString();
                this.textBox5.Text = reader["password"].ToString();

                break;
            }

            reader.Close();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database88.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();
            stb.Append("UPDATE Table1 SET std_name = '" + textBox1.Text + "' WHERE roll = '" + textBox2.Text + "'");

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Edit Query Run Success Fully");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database88.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();
            stb.Append("Delete from Table1 where roll = '" + this.textBox1.Text + "' ");
            MessageBox.Show(stb.ToString());
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Delete Query Run Success Fully");
        }

        
    }
}

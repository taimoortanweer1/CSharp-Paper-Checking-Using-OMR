using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;

namespace WindowsFormsApplication256
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

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database256.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();
            stb.Append("INSERT into Table1 (std_name,roll,phone_num) " + " VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')");

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Insert Query Run Success Fully");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database256.accdb";

            OleDbConnection conn = new OleDbConnection(constring);

            string sql = "SELECT * FROM Table1 where std_name = '" + textBox1.Text + "'";

            OleDbCommand cmd = new OleDbCommand(sql, conn);

            conn.Open();

            OleDbDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                this.textBox2.Text = reader["roll"].ToString();
                this.textBox3.Text = reader["phone_num"].ToString();
                break;
            }

            reader.Close();
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database256.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();
            stb.Append("UPDATE Table1 SET std_name = '" + textBox1.Text + "' WHERE std_name = '" + textBox2.Text + "'");
            stb.Append("UPDATE Table1 SET std_name = '" + textBox1.Text + "' WHERE phone_num = '" + textBox3.Text + "'");
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Edit Query Run Success Fully");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database256.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();
            stb.Append("Delete from Table1 where std_name = '" + this.textBox1.Text + "' ");
            stb.Append("Delete from Table1 where roll = '" + this.textBox2.Text + "' ");
            stb.Append("Delete from Table1 where phone_num = '" + this.textBox3.Text + "' ");
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

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
namespace lpc
{
    public partial class Form1 : Form
    {
        string[,] no = new string[100, 4];
        Form2 p;
        string s1, s2,s3,e2;
        public Form1(Form2 f)
        {

            InitializeComponent();
            p = f;

        }

        private void insert_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=lpc.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();
            stb.Append("INSERT into Table1 (std_name,roll,f_name,phone_num,pass_Word,obt_marks) " + " VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')");

            MessageBox.Show(stb.ToString());
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Insert Query Run Success Fully");
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
                string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=lpc.accdb";

                OleDbConnection conn = new OleDbConnection(constring);

                string sql = "SELECT * FROM Table1 where roll = '" + textBox2.Text + "'";

                OleDbCommand cmd = new OleDbCommand(sql, conn);

                conn.Open();

                OleDbDataReader reader;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    this.textBox1.Text = reader["std_name"].ToString();
                    this.textBox3.Text = reader["f_name"].ToString();
                    this.textBox4.Text = reader["phone_num"].ToString();
                    this.textBox5.Text = reader["pass_Word"].ToString();
                    this.textBox6.Text = reader["obt_marks"].ToString();
                    break;
                }

                reader.Close();
                conn.Close();
            
            

        }
   

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=lpc.accdb";
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

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=lpc.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();
            stb.Append("Delete from Table1 where std_name = '" + this.textBox1.Text + "' ");
            
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Delete Query Run Success Fully");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            p.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void recievesms(string roll)
        {
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=lpc.accdb";

            OleDbConnection conn = new OleDbConnection(constring);
            s1 = roll;
            string sql = "SELECT * FROM Table1 where s1 = '" + textBox2.Text + "'";
            
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            conn.Open();

            OleDbDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                
                this.textBox1.Text = reader["std_name"].ToString();
                this.textBox3.Text = reader["f_name"].ToString();
                this.textBox4.Text = reader["phone_num"].ToString();
                this.textBox5.Text = reader["pass_Word"].ToString();
                this.textBox6.Text = reader["obt_marks"].ToString();
                break;
            }

            reader.Close();
            conn.Close();
                    }

        void assigning_to_datagrid()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;data source=lpc.accdb";
            DataTable results = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM Table1", conn);
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(results);
            }
            dataGridView1.DataSource = results;
        }
        
        public void sms(int m)
        {


            if (!serialPort1.IsOpen == true)
            {
                serialPort1.Open();
                serialPort1.Write("AT+CMGF=1\r\n");
                string message;
                message = no[m, 2] + no[m, 6] + no[m, 1];
                string cellno = no[m, 4];
                this.serialPort1.Write("AT+CMGS=\"" + cellno + "\"\r\n");
                this.serialPort1.Write(message + "\u001a" + "\r\n");//message text
                MessageBox.Show("\nMessage Sent\n", "Information");
            }
            
            //serialPort1.Open();
            //    serialPort1.Write("AT+CMGF=1\r\n");
            //    string message;
            //    message = no[m,2] + no[m,6] + no[m,1];
            //    this.serialPort1.Write("AT+CMGS=\"" + no[m,4] + "\"\r\n");
            //    this.serialPort1.Write(message + "\u001a" + "\r\n");//message text
            //    MessageBox.Show("\nMessage Sent\n", "Information");
            //    Thread.Sleep(1000);
            // //   serialPort1.Close();
                
               
               
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //serialPort1.Open();
            //serialPort1.Write("AT+CMGF=1\r\n");
            //string message;

            //message = textBox2.Text + textBox6.Text + textBox1.Text;
            //this.serialPort1.Write("AT+CMGS=\"" + textBox4.Text + "\"\r\n");
            //this.serialPort1.Write(message + "\u001a" + "\r\n");//message text
            //MessageBox.Show("\nMessage Sent\n", "Information");
            //Thread.Sleep(1000);
            //serialPort1.Close();

            assigning_to_datagrid();
            
            int l=0;
            try
            {

                while (true)
                {
                    no[l, 0] = dataGridView1[1, l].Value.ToString();
                    no[l, 1] = dataGridView1[2, l].Value.ToString();
                    no[l, 2] = dataGridView1[4, l].Value.ToString();
                    no[l, 3] = dataGridView1[6, l].Value.ToString();

                    sms(l);
                    MessageBox.Show(no[l, 0].ToString() + "," + no[l, 1].ToString() + "," + no[l, 2].ToString() + "," + no[l, 3].ToString());
                    
                    l++;
                }
            }
            catch
            {
                MessageBox.Show("phone nos set");

            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                this.serialPort1.Open();
                this.serialPort1.Write("AT+CMGF=1\r\n");
                this.serialPort1.Write("AT+CMGL=\"ALL\"\r\n");
                Thread.Sleep(5000);
                s3 = serialPort1.ReadExisting();
                if (s3 == " ")
                {
                    MessageBox.Show("NO message");
                }
                else
                {
                    richTextBox1.Text = s3;
                }

                string[] s2 = s3.Split(':');

               // for (int p = 1; p <= 20; p++)
                //{
                    string[] e1 = s2[1].Split('+');
                    string[] e2 = e1[1].Split('"');// num
                    richTextBox1.Text = e2[0];
                    string[] e3 = e2[1].Split('+');// msg
                    MessageBox.Show(e3[0]);
                    richTextBox1.Text = e3[0]+e2[0];
                    textBox7.Text = e2[0];
                    //recievesms(e3[0]);

                //}
            }
            catch
            {
                MessageBox.Show("Message Read");
            }
                    serialPort1.Close();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.serialPort1.IsOpen)
                {
                    serialPort1.PortName = "COM4";
                    serialPort1.BaudRate = 9600;
                    serialPort1.DataBits = 8;
                    serialPort1.StopBits = System.IO.Ports.StopBits.One;
                    serialPort1.Parity = System.IO.Ports.Parity.None;
                    MessageBox.Show("\nCONNECTED SUCESSFULLY\n", "Information");
                }
                else
                {
                    MessageBox.Show("\n ALREADY CONNECTED\n");
                }
            }
            catch
            {
                MessageBox.Show("\ncommport is not connected", "information");

            }

            
        }

        

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            assigning_to_datagrid();
        }


    
}
}
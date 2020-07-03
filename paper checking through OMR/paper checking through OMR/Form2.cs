using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace paper_checking_through_OMR
{
    public partial class Form2 : Form
    {
        
        int correct_answers = 0;
        int[,] centers_answers = new int[200, 2];
        int no_of_circles = 0;
        double rad = 14;
        int centers_found=0;
        Bitmap bit3;
        int[,] centers = new int[9000, 2];
        int[,] centers_final = new int[20000, 2];
        Form1 p;
        public Form2(Form1 f)
        {
            InitializeComponent();
            p = f;
        }

        public Boolean isblack(int x, int y)
        {
            //  Image im3 = pictureBox1.Image;
            ////  im3 = resize(im3);
            //  bit3 = new Bitmap(im3);

            //  Color col2 = bit3.GetPixel(x, y);

            //  if ( col2.R < 15 && col2.B <= 15 && col2.G <= 15 )
            //  {
            //      return true;
            //  }
            //  else
            //      return false;
            return true;
        }

        public int finding_filled_circles(int x, int y, int center_avg)
        {
            double radius;
            // int j = 1;
            float v = 0, b = 0;
            //rad = float.Parse(textBox2.Text);
            for (double t = 0; t < rad; t += 1)
            {
                radius = t;
                for (int i = 0; i < 360; i += 1)
                {
                    double angle = i * System.Math.PI / 180;
                    int p = (int)(x + radius * System.Math.Cos(angle));
                    int q = (int)(y + radius * System.Math.Sin(angle));
                    if (p <= 0 || q <= 0 || q >= bit3.Height || p >= bit3.Width)
                    {
                        continue;
                    }
                    Color col2 = bit3.GetPixel(p, q);
                    //try
                    {
                        if (col2.R < 60 && col2.B < 60 && col2.G < 60)          // v shows black points 
                            v += 1;
                    }
                    // catch { }                // b covers all points
                    b += 1;
                }
            }

            float n;
            n = (v / b) * 100;
            if (n >= center_avg)
                return 1;
            else
                return 0;


            //MessageBox.Show(v.ToString() + "," + b.ToString());
        }

        private void button2_Click(object sender, EventArgs e)      //autual centers
        {
            int meanx, meany;
            int count;
            int x, y;
            int a = 0;
            int m, n;
            int x1, y1;
            int xa, ya;
            for (m = 0; m < centers_found; m++)
            {
                count = 0;
                x = centers[m, 0];
                y = centers[m, 1];
                xa = x;
                ya = y;

                for (n = 1; n < centers_found; n++)
                {
                    x1 = centers[n, 0];
                    y1 = centers[n, 1];
                    if (((x > x1 && (x1 + rad) > x) || (x > (x1 - rad)) && (x1 + rad) > x) && ((y > y1 && (y1 + rad) > y) || (y > (y1 - rad)) && (y1 + rad) > y))   // = is removed
                    {
                        count++;
                        xa = xa + x1;
                        ya = ya + y1;

                        centers[n, 0] = 0;
                        centers[n, 1] = 0;
                    }

                }
                meanx = xa / (count + 1);
                meany = ya / (count + 1);
                if (meanx != 0 && meany != 0)
                {
                    centers_final[m, 0] = meanx;
                    centers_final[m, 1] = meany;
                    listBox2.Items.Add(centers_final[m, 0] + "," + centers_final[m, 1]);
                    no_of_circles++;
                }
            }
            MessageBox.Show("Number Of Circles = " + no_of_circles.ToString());

            int p;
            string a0;
            string[] piece;
            for (p = 0; p < no_of_circles; p++)
            {
                listBox2.SelectedIndex = p;
                a0 = listBox2.SelectedItem.ToString();
                piece = a0.Split(',');
                //MessageBox.Show(piece[0].ToString());
                //MessageBox.Show(piece[1].ToString());
                centers_final[p, 0] = int.Parse(piece[0]);
                centers_final[p, 1] = int.Parse(piece[1]);
                //listBox2.Items.Add(piece[0].ToString());

            }

        }

        void save(int m)        // saving centers
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=centers.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();

            stb.Append("INSERT into Table1 (x_point,y_point) " + " VALUES ('" + centers_final[m, 0] + "','" + centers_final[m, 1] + "')");

            //MessageBox.Show(stb.ToString());
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }


        private void button8_Click(object sender, EventArgs e)
        {
            p.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int x, y;
            var mouseArgs = (MouseEventArgs)e;

            x = mouseArgs.X;
            y = mouseArgs.Y;

            richTextBox1.Text = x.ToString() + "," + y.ToString() + "\n";
        }

        private void button1_Click(object sender, EventArgs e)      // finding all circles
        {
            int p;
            centers_found = 0;
            int a = 0, b = 0;
            Image im3 = pictureBox1.Image;
            bit3 = new Bitmap(im3);
            int x, y, m = 91*2, n = 181*2;  //m=60 n=150
            for (b = 0; b < 5; b++)
            {

                for (y = 430*2; y < 720*2; y++)        // y=469 y<769
                {
                    for (x = m; x < n; x++)
                    {
                        Color col4 = bit3.GetPixel(x, y);
                        if (col4.R < 40 && col4.B < 40 && col4.G < 40)
                        {
                            p = finding_filled_circles(x, y, 88);
                            if (p == 1)
                            {
                                centers_found++;
                                centers[a, 0] = x;
                                centers[a, 1] = y;
                                //listBox1.Items.Add(x.ToString() + "," + y.ToString());
                                listBox1.Items.Add(centers[a, 0] + "," + centers[a, 1]);
                                a++;
                            }
                        }
                    }
                }
                m = n + 12*2;
                n = n + 95*2;

            }

            MessageBox.Show(centers_found.ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.AutoScroll = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void button3_Click(object sender, EventArgs e)   // saving centers of completely filled circles
        {
            int m;
            for (m = 0; m < no_of_circles; m++)
            {
                save(m);
            }

            MessageBox.Show("Insert Query Run Success Fully");

        }

        void assigning_centers_to_datagrid()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;data source=centers.accdb";
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

        private void button5_Click(object sender, EventArgs e)      // assigning autual centers
        {
            assigning_centers_to_datagrid();
            int l = 0;
            try
            {
                while (true)
                {
                    centers_final[l, 0] = int.Parse(dataGridView1[1, l].Value.ToString());
                    centers_final[l, 1] = int.Parse(dataGridView1[2, l].Value.ToString());
                    //MessageBox.Show(centers_final[l, 0].ToString()+","+centers_final[l,1].ToString());
                    l++;
                }
            }
            catch       // when datagrid view ends its comes out of while
            {
                no_of_circles = l;
                MessageBox.Show("assigned to 2d array");
                MessageBox.Show("no of circles = " + no_of_circles.ToString());
            }

        }

        int circles_twice_filled(int x_start, int y_start)
        {
            int count = 0;
            int xf = x_start, yf = y_start, z;
            for (z = 0; z < 4; z++)
            {
                if (finding_filled_circles(xf, yf, 45) == 1)        //50
                {
                    count++;
                }
                xf = xf + 29;
            }
            if (count == 1)
            {
                return 1;
            }
            else if (count == 0)
                return 0;
            else
                return -1;

        }

        private void button7_Click(object sender, EventArgs e)      // checking answers
        {
            int xp, xn, yp, yn;
            int circles_filled_twice = 0 , circles_unfilled=0;
            int count = 0;
            int c , q=0;
            int x, y, m, n, p = 0, x_start = 0, y_start = 0;
            Image im3 = pictureBox1.Image;
            bit3 = new Bitmap(im3);
            correct_answers = 0;
            for (m = 0; m < no_of_circles; m++)
            {
                if (count == 4)
                {
                    count = 0;
                    p++;
                }
                if (count == 0)
                {
                    x_start = centers_final[m, 0];
                    y_start = centers_final[m, 1];
                }
                x = centers_final[m, 0];
                y = centers_final[m, 1];
                count++;
                //xp = x - 5;
                //xn = x + 5;
                //yp = y - 5;
                //yn = y + 5;

                if (centers_answers[p, 0] == x && centers_answers[p, 1] == y)
                //if ((centers_answers[p, 0] >= xp) && (centers_answers[p, 0] <= xn) && (centers_answers[p, 1] >= yp) && (centers_answers[p, 1] <= yn))
                {
                    q = finding_filled_circles(x, y, 55);
                    
                    c = circles_twice_filled(x_start, y_start);
                    if (c == 1 && q == 1)
                    {
                        correct_answers++;
                    }
                    else if (c == -1)
                        circles_filled_twice++;
                    else if (c == 0)
                        circles_unfilled++;
                }
            }

            MessageBox.Show(correct_answers.ToString());
            MessageBox.Show("circles that are filled twice = "+circles_filled_twice);


        }

        void save_ans(int m)        // saving answers
        {
            OleDbConnection con = new OleDbConnection();
            string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=answers.accdb";
            con.ConnectionString = constring;
            con.Open();
            StringBuilder stb = new StringBuilder();

            stb.Append("INSERT into Table1 (x_point,y_point) " + " VALUES ('" + centers_answers[m, 0] + "','" + centers_answers[m, 1] + "')");

            //MessageBox.Show(stb.ToString());
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = stb.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }

        private void button4_Click(object sender, EventArgs e)      //saving answers to access
        {
            Image im3 = pictureBox1.Image;
            bit3 = new Bitmap(im3);
            int x, y, m, p = 0;
            for (m = 0; m < no_of_circles; m++)
            {
                x = centers_final[m, 0];
                y = centers_final[m, 1];
                if (finding_filled_circles(x, y, 80) == 1)
                {
                    centers_answers[p, 0] = x;
                    centers_answers[p, 1] = y;
                    save_ans(p);
                    p++;
                }

            }
            MessageBox.Show("no of answers saved ="+p);

        }

        void assigning_answers_to_datagrid()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;data source=answers.accdb";
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

        private void button6_Click(object sender, EventArgs e)      //assigning answers to centers answers
        {
            assigning_answers_to_datagrid();
            int l = 0;
            try
            {
                while (true)
                {
                    centers_answers[l, 0] = int.Parse(dataGridView1[1, l].Value.ToString());
                    centers_answers[l, 1] = int.Parse(dataGridView1[2, l].Value.ToString());
                    //MessageBox.Show(centers_final[l, 0].ToString()+","+centers_final[l,1].ToString());
                    l++;
                }
            }
            catch       // when datagrid view ends its comes out of while
            {
                no_of_circles = l * 4;
                MessageBox.Show("assigned answers to 2d array");
                MessageBox.Show("no of circles = " + no_of_circles.ToString());
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(open.FileName);
                }
            }
            catch (System.Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form3 p = new Form3(this);
            p.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }



    }
}

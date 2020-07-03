using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkcirclepixel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int length = 0;
            int width = 0;
            int centerx = 0; centerx = Convert.ToInt32(textBox1.Text);
            int centery = 0; centery = Convert.ToInt32(textBox2.Text);
            int radius = 0; radius = Convert.ToInt32(textBox3.Text);
            float count = 0;
            float total = 0;
            float percentage = 0;

            length = centery + radius;
            width = centerx + radius;

            int zeroy = (length - (2 * radius));  // find 0,0 of rectangle around circle
            int zerox = (width - (2 * radius));

            Bitmap image = new Bitmap(pictureBox1.Image);
            for (int i = zeroy; i < length; i++)
            {
                for (int j = zerox; j < width; j++)
                {
                    if ((i - centery) * (i - centery) + (j - centerx) * (j - centerx) < (radius * radius))
                    {
                        int red, blue, green;
                        int blackness;
                        Color colour;
                        colour = image.GetPixel(j, i);

                        red = colour.R;
                        blue = colour.B;
                        green = colour.G;

                        blackness = ((red + blue + green) / 3);

                        if (blackness < 100)
                        {
                            count++;
                        }
                        total++;

                        percentage = (count / total) * 100;

                    }

                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Threading; 

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int xa, xb;
        int ya, yb;
        float xf, yf;
        float angle;
        public Form1()
        {
            InitializeComponent();
        }

        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            // gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now dRotateImageByAngle(Image oldBitmap, float angle)raw our new image onto the graphics object
            gfx.DrawImage(img, new System.Drawing.Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

       
    
       public static Bitmap RotateImg(Bitmap bmp, float angle, Color bkColor)
        {
            angle = angle % 360;
            if (angle > 180)
                angle -= 360;

            System.Drawing.Imaging.PixelFormat pf = default(System.Drawing.Imaging.PixelFormat);
            if (bkColor == Color.Transparent)
            {
                pf = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            }
            else
            {
                pf = bmp.PixelFormat;
            }

            float sin = (float)Math.Abs(Math.Sin(angle * Math.PI / 180.0)); // this function takes radians
            float cos = (float)Math.Abs(Math.Cos(angle * Math.PI / 180.0)); // this one too
            float newImgWidth = sin * bmp.Height + cos * bmp.Width;
            float newImgHeight = sin * bmp.Width + cos * bmp.Height;
            float originX = 0f;
            float originY = 0f;

            if (angle > 0)
            {
                if (angle <= 90)
                    originX = sin * bmp.Height;
                else
                {
                    originX = newImgWidth;
                    originY = newImgHeight - sin * bmp.Width;
                }
            }
            else
            {
                if (angle >= -90)
                originY = sin * bmp.Width;
                else
                {
                    originX = newImgWidth - sin * bmp.Height;
                    originY = newImgHeight;
                }
            }

            Bitmap newImg = new Bitmap((int)newImgWidth, (int)newImgHeight, pf);
            Graphics g = Graphics.FromImage(newImg);
            g.Clear(bkColor);
            g.TranslateTransform(originX, originY); // offset the origin to our calculated values
            g.RotateTransform(angle); // set up rotate
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(bmp, 0, 0); // draw the image at 0, 0
            g.Dispose();

            return newImg;
        }

       void calangle()
       {
           double tempangle;
           xf = xb - xa;
           yf = yb - ya;
           if (xf < 0)
           {
               xf = xf * -1;
           }
           if (yf < 0)
           {
               yf = yf * -1;
           }
           tempangle = Math.Atan((yf/xf))*(180/Math.PI);
           angle = Convert.ToInt32(tempangle);
       }

       void setimage()
       {
           Image im;
           Bitmap bit;
           im = pictureBox1.Image;
           bit = new Bitmap(im);
           if (ya < yb)
           {
               angle = angle * -1;
           }
           pictureBox1.Image = RotateImage(bit, angle);
           Image im2;
           Bitmap bit2;
           im2 = pictureBox1.Image;
           bit2 = new Bitmap(im2);
           pictureBox1.Image = RotateImg(bit2, 0, Color.White);
       }

       float percent(int centerx, int centery)
       {
           int height = 0;
           int width = 0;
           int radius = 10;
           float count = 0;
           float total = 0;
           float percentage = 0;
           
           height = centery + radius;
           width = centerx + radius;

           int zeroy = (height - (2 * radius));
           int zerox = (width - (2 * radius));
           Bitmap image = new Bitmap(pictureBox1.Image);
           for (int i =zeroy ; i < height; i++)
           {
               for (int j = centerx; j < width; j++)
               {
                   if (((i - centery) * (i - centery) + (j - centerx) * (j - centerx)) < (radius * radius))
                   {    
                       Color colour;
                       colour = image.GetPixel(j, i);

                       if (colour.R < 10 && colour.G < 10 && colour.B < 10)
                       {
                           count++;
                       }
                       total++;
                   }
               }
           }

           percentage = (count / total) * 100;

           image.Dispose();
           return percentage;

       }

       void findxypoints()
       {
           float per1=0, per2=0;
           int count = 0;
           Image im;
           Bitmap bit;
           im = pictureBox1.Image;
           bit = new Bitmap(im);
           im = bit;
           Color col;
           for (int x = 10; x < 125; x++)
           {
               if (count == 1)
               {
                   break;
               }
               for (int y = 10; y < 100; y++)
               {
                   col = bit.GetPixel(x, y);
                   if (col.R < 10 && col.B < 10 && col.G < 10)
                   {
                       xa = x;
                       ya = y;
                       per1 = percent(xa, ya);
                       if (per1 > 30)
                       {
                           richTextBox1.Text = xa.ToString() + "," + ya.ToString();
                           textBox1.Text = per1.ToString();
                           count++;
                           break;
                       }
                   }
               }
           }

           count = 0;

           for (int x = 510; x < 615; x++)
           {
               if (count == 1)
               {
                   break;
               }
               for (int y = 10; y < 100; y++)
               {
                   col = bit.GetPixel(x, y);
                   if (col.R < 10 && col.B < 10 && col.G < 10)
                   {
                       xb = x;
                       yb = y;
                       per2 = percent(xb, yb);
                       if (per2 > 30)
                       {
                           richTextBox2.Text = xb.ToString() + "," + yb.ToString();
                           textBox2.Text = per2.ToString();
                           count++;
                           break;
                       }
                   }
               }
           }
       }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                findxypoints();
            }
            catch (System.Exception excep)
            {
                MessageBox.Show(excep.Message);
            }   
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(open.FileName);
                      
                }
            }
            catch(System.Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int x, y;
            var mouseArgs = (MouseEventArgs)e;

            x = mouseArgs.X;
            y = mouseArgs.Y;

            richTextBox2.Text = x.ToString() + "," + y.ToString();
        }

        private static Bitmap _resize(Image image, int width, int height)
        {
            Bitmap newImage = new Bitmap(width, height);
            //this is what allows the quality to stay the same when reducing image dimensions
            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(image, new Rectangle(0, 0, width, height));
            }
            return newImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image im;
            im = pictureBox1.Image;
            pictureBox1.Image = _resize(im, 621, 878);
            im = pictureBox1.Image;
            try
            {
                while (true)
                {
                    findxypoints();
                    calangle();
                    if (yf < 4)
                    {
                        break;
                    }
                    setimage();
                }
            }
            catch (System.Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
            try
            {
                pictureBox1.Image.Save(@"D:\Abu bakar.Jpeg", ImageFormat.Jpeg);
            }
            catch (System.Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.AutoScroll = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

    }
}

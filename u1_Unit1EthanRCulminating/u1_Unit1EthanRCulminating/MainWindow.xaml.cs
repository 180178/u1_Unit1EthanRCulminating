//Ethan Ross
//3/6/2018
//u1_Unit1EthanRCulminating
//"Supposed to" Draw a rectangle around a power cube or yellow square on image
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;//for bitmap.getpixel
namespace u1_Unit1EthanRCulminating
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double locX = 1.0;
        double locY = 1.0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            int PixelBottom = 0;
            int PixelTop = 0;
            int PixelRight = 0;
            int PixelLeft = 0;
            lbl.Content = canvas.Children.Count;
            SolidColorBrush color = new SolidColorBrush();
            Random rnd = new Random();
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.ShowDialog();
            BitmapImage bi = new BitmapImage(new Uri(openFileDialog.FileName));
            double ScaleX = 1;
            double ScaleY = 1;
            if (bi.PixelWidth != 0 && bi.PixelHeight != 0)
            {
                locX = 640 / bi.PixelWidth;
                locY = 480 / bi.PixelHeight;
                ScaleX = 640 / bi.PixelWidth;
                ScaleY = 480 / bi.PixelHeight;
            }
            // The following transformed bitmap should be scaled down to the size of the canvas, Duting testing it made any image larger than the canvas 1x1, also causes errors
            //TransformedBitmap bit = new TransformedBitmap(bi, new ScaleTransform(640 / ScaleX, 480 / ScaleY));
            //Console.WriteLine(bit.PixelHeight);
            //Console.WriteLine(bit.PixelWidth);
            System.Windows.Media.ImageBrush ib = new ImageBrush(bi);
            canvas.Background = ib;
            Console.WriteLine(bi.PixelHeight);
            Console.WriteLine(bi.PixelWidth);
            for (int x = 0; x < bi.PixelWidth; x++)
            {
                PixelBottom = 0;
                PixelTop = 0;
                PixelRight = 0;
                PixelLeft = 0;
                int y = rnd.Next(0, bi.PixelHeight);
                int stride = bi.PixelWidth * 4;
                int size = bi.PixelHeight * stride;
                byte[] pixels = new byte[size];
                bi.CopyPixels(pixels, stride, 0);
                int index = y * stride + 4 * x;
                byte blue = pixels[index];
                byte green = pixels[index + 1];
                byte red = pixels[index + 2];
                byte alpha = pixels[index + 3];
                //Debug Code the Check each instance of this loop, uncomment to debug
                //Console.WriteLine("Stuff" + " " + x + " " + " " + y + " " + red + " " + green + " " + blue);
                lbl.Content = canvas.Children.Count;
                if (red <= 255 && red >= 100 && blue >= 0 && blue <= 200 && green <= 255 && green >= 100)
                {
                    for (int i = y; i < bi.PixelHeight; i++)
                    {
                        index = i * stride + 4 * x;
                        blue = pixels[index];
                        green = pixels[index + 1];
                        red = pixels[index + 2];
                        alpha = pixels[index + 3];
                        if (red <= 255 && red >= 100 && blue >= 0 && blue <= 200 && green <= 255 && green >= 100)
                        {

                        }
                        else
                        {
                            PixelBottom = i;
                            break;
                        }

                    }


                    for (int i = y; i > 0; i--)
                    {
                        index = i * stride + 4 * x;
                        blue = pixels[index];
                        green = pixels[index + 1];
                        red = pixels[index + 2];
                        alpha = pixels[index + 3];
                        if (red <= 255 && red >= 100 && blue >= 0 && blue <= 200 && green <= 255 && green >= 100)
                        {
                            continue;
                        }
                        else
                        {
                            PixelTop = i;
                            break;
                        }

                    }
                    for (int i = x; i > 0; i--)
                    {
                        index = y * stride + 4 * i;
                        blue = pixels[index];
                        green = pixels[index + 1];
                        red = pixels[index + 2];
                        alpha = pixels[index + 3];
                        if (red <= 255 && red >= 100 && blue >= 0 && blue <= 200 && green <= 255 && green >= 100)
                        {
                            continue;
                        }
                        else
                        {
                            PixelLeft = i;
                            break;
                        }

                    }
                    for (int i = x; i < bi.PixelWidth; i++)
                    {
                        index = y * stride + 4 * i;
                        blue = pixels[index];
                        green = pixels[index + 1];
                        red = pixels[index + 2];
                        alpha = pixels[index + 3];
                        if (red <= 255 && red >= 100 && blue >= 0 && blue <= 200 && green <= 255 && green >= 100)
                        {
                            continue;
                        }
                        else
                        {
                            PixelRight = i;
                            break;
                        }

                    }
                }
                //Defines the Rectangle if the width or height isnt going to be 0
                if ((PixelRight - PixelLeft != 0)&&(PixelBottom-PixelTop!=0))
                {
                    color.Color = Color.FromArgb(255, 192, 41, 224);
                    Rectangle rect = new Rectangle();
                    //Has a tendancy to crash if the image is larger than the canvas
                    rect.Width = PixelRight - PixelLeft;
                    rect.Height = PixelBottom - PixelTop;
                    rect.Stroke = color;
                    rect.StrokeThickness = 10;
                    canvas.Children.Add(rect);
                    //Console.WriteLine(locY);
                    //Console.WriteLine(locX);
                    Canvas.SetTop(rect, (PixelTop));
                    Canvas.SetLeft(rect, (PixelLeft));
                    lbl.Content = (canvas.Children.Count);
                    //Only outputs to console when it finds the yellow range
                    if (PixelTop != 0 || PixelBottom != 0 || PixelLeft != 0 || PixelRight != 0)
                    {
                        Console.WriteLine("Top" + PixelTop.ToString() + "\n" + "Bottom" + PixelBottom.ToString() + "\n" + "Left" + PixelLeft.ToString() + "\n" + "Right" + PixelRight.ToString() + "\n" + "Height " + (PixelBottom - PixelTop).ToString() + "\n" + "Width " + (PixelRight - PixelLeft).ToString() + "\n\n");
                    }
                }
                

            }
            Console.WriteLine("Done");
        }
        //This Button clears off the children rectangles of the canvas to allow the canvas to be cleared without removing the buttons or labels
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            lbl.Content = (canvas.Children.Count);
            if (canvas.Children.Count > 4)
            {
                for (int k = canvas.Children.Count - 1; k > 3; k--)
                {
                   canvas.Children.RemoveAt(k);
                }
            }
            lbl.Content = (canvas.Children.Count);
       }
    }
}

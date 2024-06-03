using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace WindowsFormsApp1
{
    public partial class Comparison : Form
    {
        public Comparison()
        {
            InitializeComponent();
        }

        private Image ChooseImage()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedImagePath = openFileDialog.FileName;
                    return Image.FromFile(selectedImagePath);
                }
            }
            return null;
        }

        private void SelectImage1(object sender, EventArgs e)
        {
            image1.Image = ChooseImage();
        }
        private void SelectImage2(object sender, EventArgs e)
        {
            image2.Image = ChooseImage();
        }

        private Bitmap ConvertToGrayScale(Bitmap image)
        {
            Bitmap grayscaleImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    int grayValue = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    grayscaleImage.SetPixel(x, y, grayColor);
                }
            }

            return grayscaleImage;
        }

        private void Compare(object sender, EventArgs e)
        {
            if (image2.Image == null || image1.Image == null)
            {
                SetResultLabel("Choose images first!", Color.Red);
                return;
            }

            var image1GrayScale = ResizeImage(ConvertToGrayScale(new Bitmap(image1.Image)), 1000, 1000);
            var image2GrayScale = ResizeImage(ConvertToGrayScale(new Bitmap(image2.Image)), 1000, 1000);
            var sum = GetSumDifferences(image1GrayScale, image2GrayScale);

            if (sum == 0)
            {
                SetResultLabel("Patient's status is the same.", Color.Gray);
            }
            if (sum < 0)
            {
                SetResultLabel("Patient's status is getting worse!", Color.Red);
            }
            if (sum > 0)
            {
                SetResultLabel("Patient's status is getting better!", Color.Green);
            }
        }

        private void SetResultLabel(string message, Color color)
        {
            result.Text = message;
            result.ForeColor = color;
        }

        private double GetSumDifferences(Bitmap image1, Bitmap image2)
        {
            var sum = 0.0;
            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    Color pixelColor1 = image1.GetPixel(x, y);
                    Color pixelColor2 = image2.GetPixel(x, y);
                    sum += (pixelColor1.R - pixelColor2.R);
                }
            }
            return sum;
        }
        private Bitmap ResizeImage(Bitmap image, int Height, int Width)
        {
            var resizedImage = new Bitmap(Width, Height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, Width, Height);
            }
            return resizedImage;
        }

    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;


namespace WindowsFormsApp1
{
    public partial class Fourier : Form
    {
        System.Drawing.Image originalImage;
        public Fourier()
        {
            InitializeComponent();
        }

        private void SelectImage(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedImagePath = openFileDialog.FileName;
                    image.Image = System.Drawing.Image.FromFile(selectedImagePath);
                    originalImage = image.Image;
                }
            }
        }


        private ComplexImage ConvertToFFT(Bitmap image)
        {

            var grayScaleImage = ResizeToPowerOfTwo(image);

            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage = grayscaleFilter.Apply(grayScaleImage);

            ComplexImage complexImage = ComplexImage.FromBitmap(grayImage);
            complexImage.ForwardFourierTransform();

            return complexImage;
        }

        private void SetErrorText()
        {
            result.Text = "Choose an image first!";
            result.ForeColor = Color.Red;
        }

        private void ResetErrorText()
        {
            result.Text = "";
            result.ForeColor = Color.Black;
        }

        public Bitmap ResizeToPowerOfTwo(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;

            int newWidth = GetNearestPowerOfTwo(width);
            int newHeight = GetNearestPowerOfTwo(height);

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);

            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return resizedImage;
        }

        private int GetNearestPowerOfTwo(int value)
        {
            int powerOfTwo = (int)Math.Pow(2, (int)Math.Ceiling(Math.Log(value, 2)));
            return powerOfTwo;
        }

        private void ApplyFFT(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                SetErrorText();
                return;
            }

            ResetErrorText();
            image.Image = ConvertToFFT(new Bitmap(originalImage)).ToBitmap();
        }

        private void ApplyHPF(object sender, EventArgs e)
        {

            if (originalImage == null)
            {
                SetErrorText();
                return;
            }

            ResetErrorText();

            var complexImage = ConvertToFFT(new Bitmap(originalImage));
            int width = complexImage.Width;
            int height = complexImage.Height;
            double radius = 0.01;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double distance = Math.Sqrt((x - width / 2) * (x - width / 2) + (y - height / 2) * (y - height / 2)) / (Math.Sqrt(width * width + height * height) / 2);
                    if (distance < radius)
                    {
                        complexImage.Data[y, x] = new AForge.Math.Complex(0, 0);
                    }
                }
            }

            complexImage.BackwardFourierTransform();
            image.Image = complexImage.ToBitmap();
        }

        private void ApplyLPF(object sender, EventArgs e)
        {

            if (originalImage == null)
            {
                SetErrorText();
                return;
            }

            ResetErrorText();

            var complexImage = ConvertToFFT(new Bitmap(originalImage));
            int width = complexImage.Width;
            int height = complexImage.Height;
            double radius = 0.1;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double distance = Math.Sqrt((x - width / 2) * (x - width / 2) + (y - height / 2) * (y - height / 2)) / (Math.Sqrt(width * width + height * height) / 2);
                    if (distance > radius)
                    {
                        complexImage.Data[y, x] = new AForge.Math.Complex(0, 0);
                    }
                }
            }

            complexImage.BackwardFourierTransform();
            image.Image = complexImage.ToBitmap();
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Diagnosis : Form
    {
        Point start;
        Point end;
        Rectangle rectangle;
        bool isPressed = false;
        public Diagnosis()
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
                    image.Image = Image.FromFile(selectedImagePath);
                    start.X = 0; start.Y = 0;
                    end.X = 0; end.Y = 0;
                    SetResultLabel("", Color.Black);
                }
            }
        }

        private void PicInputMouseDown(object sender, MouseEventArgs e)
        {
            if (!InRange(e.Location.X, 0, image.Size.Width) || !InRange(e.Location.Y, 0, image.Size.Height))
            {
                start = new Point(0, 0);
                return;
            }
            isPressed = true;
            start = e.Location;
        }


        private void PicInputMouseUp(object sender, MouseEventArgs e)
        {
            if (!InRange(e.Location.X, 0, image.Size.Width) || !InRange(e.Location.Y, 0, image.Size.Height))
            {
                end = new Point(image.Size);
                isPressed = false;
                return;
            }
            isPressed = false;
            end = e.Location;
        }

        private void PicInputMouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed == true)
            {
                if (!InRange(e.Location.X, 0, image.Size.Width) || !InRange(e.Location.Y, 0, image.Size.Height))
                {
                    end = new Point(image.Size);
                    isPressed = false;
                    return;
                }
                end = e.Location;
                Refresh();
            }
        }

        private void PickInputPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Yellow, GetRect());
        }

        private Rectangle GetRect()
        {
            rectangle = new Rectangle
            {
                X = Math.Min(start.X, end.X),
                Y = Math.Min(start.Y, end.Y),
                Width = Math.Abs(start.X - end.X),
                Height = Math.Abs(start.Y - end.Y)
            };
            return rectangle;
        }

        private void Diagnose(object sender, EventArgs e)
        {
            if (image.Image == null)
            {
                SetResultLabel("Choose an image first!", Color.Red);
                return;
            }

            if(start.X == 0 || end.Y == 0)
            {
                SetResultLabel("Select a region first!", Color.Red);
                return;
            }

            var imageGrayScale = ResizeImage(ConvertToGrayScale(new Bitmap(image.Image)), image.Size.Height, image.Size.Width);
            
            var sum = 0.0;
            for (int y = start.Y; y < end.Y; y++)
            {
                for (int x = start.X; x < end.X; x++)
                {
                    Color pixelColor = imageGrayScale.GetPixel(x, y);
                    sum += pixelColor.R;
                }
            }
            var width = end.X - start.X;
            var height = end.Y - start.Y;
            var ratio = sum / (width * height * 255) * 100;
            
            if (ratio >= 70)
            {
                SetResultLabel("Patient's status is dangerous!", Color.Red);
            }
            else if (ratio >= 60)
            {
                SetResultLabel("Patient's status is in the middle.", Color.Gray);
            }
            else
            {
                SetResultLabel("Patient's status is simple.", Color.Green);
            }
        }
        private void SetResultLabel(string message, Color color)
        {
            result.Text = message;
            result.ForeColor = color;
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

        private Bitmap ResizeImage(Bitmap image, int Height, int Width)
        {
            var resizedImage = new Bitmap(Width, Height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, Width, Height);
            }
            return resizedImage;
        }

        private bool InRange(int value, int start, int end)
        {
            return (value >= start && value <= end);
        }
    }
}

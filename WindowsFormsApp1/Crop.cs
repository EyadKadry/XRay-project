using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Crop : Form
    {
        public Crop()
        {
            InitializeComponent();
        }

        Point start;
        Point end;
        Rectangle rectangle;
        Image originalImage;
        Image croppedImage;
        string selectedImagePath;
        bool isPressed = false;
        private void SelectImage(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    image.Image = Image.FromFile(selectedImagePath);
                    start.X = 0; start.Y = 0;
                    end.X = 0; end.Y = 0;
                    SetResultLabel("", Color.Black);
                    croppedImageBox.Image = null;
                    originalImage = image.Image;
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

        private void CropImage(object sender, EventArgs e)
        {
            if (image.Image == null)
            {
                SetResultLabel("Choose an image first!", Color.Red);
                return;
            }

            if (start.X == 0 || end.Y == 0)
            {
                SetResultLabel("Select a region first!", Color.Red);
                return;
            }

            SetResultLabel("", Color.Black);


            var width = end.X - start.X;
            var height = end.Y - start.Y;
            var resizedImage = ResizeImage(new Bitmap(image.Image), image.Size.Height, image.Size.Width);
            croppedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(resizedImage, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height),
                            rectangle,
                            GraphicsUnit.Pixel);
            }

            croppedImageBox.Image = croppedImage;
            start.X = 0; start.Y = 0;
            end.X = 0; end.Y = 0;
            image.Image = originalImage;
        }
        private void SetResultLabel(string message, Color color)
        {
            result.Text = message;
            result.ForeColor = color;
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

        private void Save(object sender, EventArgs e)
        {
            if(croppedImageBox.Image == null)
            {
                SetResultLabel("crop an image first!", Color.Red);
                return;
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png";
                saveFileDialog.Title = "Save an Image File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                        croppedImage.Save(saveFileDialog.FileName);
                }
            }
        }
    }
}

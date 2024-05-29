using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
namespace ImageEditor
{
    public partial class Form1 : Form
    {
        private WaveOutEvent waveOut;
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private string outputFilePath;
        private Rectangle _drawnShapeRect;
        private enum DrawMode { None, Rectangle, Ellipse, Line, Curve, Crop, Text }
        private DrawMode _drawMode = DrawMode.None;

        private Point _startPoint;
        private Point _startPoint2;
        private Point _endPoint;
        private List<Point> _curvePoints;
        private bool _isDrawing = false;
        private bool _isSelecting1 = false;

        private bool _isSelecting = false;
        private Rectangle _selectedRectangle;
        private Rectangle _cropRect;
        private string _textToDraw = string.Empty;
        private Point _textPosition;
        private IWaveProvider audioFileReader;
        private WaveStream waveStream;
        private Button btnCompressAndSave;


        public Form1()
        {
            InitializeComponent();
           
        }
       

        private void btnCompressAndSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JPEG Image|*.jpg";
                    saveFileDialog.Title = "Save Compressed Image";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        SaveCompressedImage((Bitmap)pictureBox1.Image, saveFileDialog.FileName, 50L);
                    }
                }
            }
        }

        private void SaveCompressedImage(Bitmap bmp, string filePath, long quality)
        {
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            if (jpegCodec == null)
            {
                MessageBox.Show("JPEG codec not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EncoderParameters encoderParams = new EncoderParameters(1);
            EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            bmp.Save(filePath, jpegCodec, encoderParams);
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == mimeType)
                {
                    return codec;
                }
            }
            return null;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);

            btnDrawRectangle.Click += new EventHandler(btnDrawRectangle_Click);
            btnDrawEllipse.Click += new EventHandler(btnDrawEllipse_Click);
            btnDrawLine.Click += new EventHandler(btnDrawLine_Click);
            btnDrawCurve.Click += new EventHandler(btnDrawCurve_Click);
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void btnSelectArea_Click(object sender, EventArgs e)
        {
            _isSelecting = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (_drawMode == DrawMode.Text)
                {
                    _textPosition = e.Location;
                    DrawTextOnImage(_textToDraw, _textPosition);
                    pictureBox1.Invalidate();
                }
                else
                {
                    _startPoint2 = e.Location;
                    _isSelecting1 = true;
                }
            }
            if (pictureBox1.Image != null && !_isSelecting && _drawMode != DrawMode.Text)
            {
                _startPoint = e.Location;
                _isDrawing = true;

                if (_drawMode == DrawMode.Curve)
                {
                    _curvePoints = new List<Point> { _startPoint };
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelecting1)
            {
                Point tempEndPoint = e.Location;
                _cropRect = new Rectangle(
                    Math.Min(_startPoint2.X, tempEndPoint.X),
                    Math.Min(_startPoint2.Y, tempEndPoint.Y),
                    Math.Abs(_startPoint2.X - tempEndPoint.X),
                    Math.Abs(_startPoint2.Y - tempEndPoint.Y));

                pictureBox1.Invalidate();
            }
            if (_isDrawing)
            {
                _endPoint = e.Location;

                if (_drawMode == DrawMode.Crop)
                {
                    _cropRect = new Rectangle(
                        Math.Min(_startPoint.X, _endPoint.X),
                        Math.Min(_startPoint.Y, _endPoint.Y),
                        Math.Abs(_startPoint.X - _endPoint.X),
                        Math.Abs(_startPoint.Y - _endPoint.Y));
                    pictureBox1.Invalidate();
                }
                pictureBox1.Invalidate();
            }

            if (_isDrawing && _drawMode == DrawMode.Curve)
            {
                _curvePoints.Add(e.Location);
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _isSelecting1 = false;
            _isDrawing = false;

            if (_drawMode == DrawMode.Text && !string.IsNullOrEmpty(_textToDraw))
            {
                _textPosition = e.Location;
                DrawTextOnImage(_textToDraw, _textPosition);
                pictureBox1.Invalidate();
            }

            pictureBox1.Invalidate();
        }


        private void btnGenerateReport_Click(object sender, EventArgs e)
            {
            // Prompt user to enter patient name and report title
            string reportTitle = Prompt.ShowDialog("Enter the report title:", "Report Title");
            string patientName = Prompt.ShowDialog("Enter the patient's name:", "Patient Name");

            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = reportTitle;

            // Create an empty page
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont titleFont = new XFont("Arial", 20, XFontStyleEx.Bold);
            XFont textFont = new XFont("Arial", 14, XFontStyleEx.Regular);
            XFont pageNumberFont = new XFont("Arial", 12, XFontStyleEx.Regular);

            PdfPage page2 = document.AddPage();


            // Draw the report title
            gfx.DrawString(reportTitle, titleFont, XBrushes.Black, new XRect(0, 0, page.Width, 50), XStringFormats.Center);

            // Draw the patient name
            gfx.DrawString("Patient Name: " + patientName, textFont, XBrushes.Black, new XRect(40, 60, page.Width, 50), XStringFormats.TopLeft);

            // Draw the date of report
            gfx.DrawString("Date : " + DateAndTime.DateString, textFont, XBrushes.Black, new XRect(40, 80, page.Width, 50), XStringFormat.TopLeft);
            // Add image
            if (pictureBox1.Image != null)
            {
                // Save the PictureBox image to a temporary file
                string tempFilePath = System.IO.Path.GetTempFileName() + ".png";
                pictureBox1.Image.Save(tempFilePath, System.Drawing.Imaging.ImageFormat.Png);

                // Load the image from the file into the PDF
                XImage xImage = XImage.FromFile(tempFilePath);


                // Calculate the scaling to maintain aspect ratio
                double aspectRatio = (double)xImage.PixelWidth / xImage.PixelHeight;
                double imageWidth = page.Width - 80; // Leave some margin
                double imageHeight = imageWidth / aspectRatio;
                if (imageHeight > page.Height - 160) // Adjust to fit within page height if necessary
                {
                    imageHeight = page.Height - 160;
                    imageWidth = imageHeight * aspectRatio;
                }

                // Center the image horizontally on the page
                double xPosition = (page.Width - imageWidth) / 2;

                // Draw Image on page
                gfx.DrawImage(xImage, xPosition, 100, imageWidth, imageHeight);



                // Delete the temporary file
                System.IO.File.Delete(tempFilePath);
            }

            // Dispose the initial XGraphics object
            gfx.Dispose();

            // Add page numbers to each page
            for (int i = 0; i < document.PageCount; i++)
            {
                PdfPage currentPage = document.Pages[i];
                XGraphics pageGfx = XGraphics.FromPdfPage(currentPage);
                pageGfx.DrawString($"Page {i + 1} of {document.PageCount}", pageNumberFont, XBrushes.Black, new XRect(0, currentPage.Height - 50, currentPage.Width, 50), XStringFormats.Center);
                pageGfx.Dispose();
            }

            // Save the document
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Document|*.pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    document.Save(saveFileDialog.FileName);
                    MessageBox.Show("Report saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }





        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_isSelecting && pictureBox1.Image != null && _cropRect.Width > 0 && _cropRect.Height > 0)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, _cropRect);
                }
            }
            if (pictureBox1.Image != null && _isDrawing)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    switch (_drawMode)
                    {
                        case DrawMode.Rectangle:
                            e.Graphics.DrawRectangle(pen, GetRectangle(_startPoint, _endPoint));
                            break;
                        case DrawMode.Ellipse:
                            e.Graphics.DrawEllipse(pen, GetRectangle(_startPoint, _endPoint));
                            break;
                        case DrawMode.Line:
                            e.Graphics.DrawLine(pen, _startPoint, _endPoint);
                            break;
                        case DrawMode.Curve:
                            if (_curvePoints.Count > 1)
                            {
                                e.Graphics.DrawCurve(pen, _curvePoints.ToArray());
                            }
                            break;
                        case DrawMode.Crop:
                            e.Graphics.DrawRectangle(pen, _cropRect);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(_textToDraw) && _drawMode == DrawMode.Text)
            {
                using (Font font = new Font("Arial", 16))
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    e.Graphics.DrawString(_textToDraw, font, brush, _textPosition);
                }
            }
        }

        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }

        private void btnDrawRectangle_Click(object sender, EventArgs e)
        {
            _drawMode = DrawMode.Rectangle;
            _isSelecting = false;
        }

        private void btnDrawEllipse_Click(object sender, EventArgs e)
        {
            _drawMode = DrawMode.Ellipse;
            _isSelecting = false;
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            _drawMode = DrawMode.Line;
            _isSelecting = false;
        }

        private void btnDrawCurve_Click(object sender, EventArgs e)
        {
            _drawMode = DrawMode.Curve;
            _isSelecting = false;
        }

        private void btnAddText_Click(object sender, EventArgs e)
        {
            _drawMode = DrawMode.Text;
            _isSelecting = false;

            using (Form inputForm = new Form())
            {
                inputForm.Text = "ÅÏÎÇá äÕ";
                TextBox textBox = new TextBox { Dock = DockStyle.Fill };
                inputForm.Controls.Add(textBox);
                textBox.KeyDown += (s, evt) =>
                {
                    if (evt.KeyCode == Keys.Enter)
                    {
                        _textToDraw = textBox.Text;
                        inputForm.DialogResult = DialogResult.OK;
                        inputForm.Close();
                    }
                };
                inputForm.ShowDialog();
            }
        }

        private void DrawTextOnImage(string text, Point position)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (Font font = new Font("Arial", 16))
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        g.DrawString(text, font, brush, position);
                    }
                }
                pictureBox1.Image = bmp;
            }
        }
        private Bitmap DrawShapeOnImage(Bitmap originalImage)
        {
            Bitmap bmp = new Bitmap(originalImage);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    switch (_drawMode)
                    {
                        case DrawMode.Rectangle:
                            g.DrawRectangle(pen, GetRectangle(_startPoint, _endPoint));
                            break;
                        case DrawMode.Ellipse:
                            g.DrawEllipse(pen, GetRectangle(_startPoint, _endPoint));
                            break;
                        case DrawMode.Line:
                            g.DrawLine(pen, _startPoint, _endPoint);
                            break;
                        case DrawMode.Curve:
                            if (_curvePoints.Count > 1)
                            {
                                g.DrawCurve(pen, _curvePoints.ToArray());
                            }
                            break;
                    }
                }
            }

            return bmp;
        }

        private void btnCropAndSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // äÓÎ ÇáÕæÑÉ ÇáÃÕáíÉ
                Bitmap bmp = new Bitmap(pictureBox1.Image);

                // ÑÓã ÇáÔßá ÇáãÍÏÏ Úáì ÇáÕæÑÉ
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        switch (_drawMode)
                        {
                            case DrawMode.Rectangle:
                                g.DrawRectangle(pen, GetRectangle(_startPoint, _endPoint));
                                break;
                            case DrawMode.Ellipse:
                                g.DrawEllipse(pen, GetRectangle(_startPoint, _endPoint));
                                break;
                            case DrawMode.Line:
                                g.DrawLine(pen, _startPoint, _endPoint);
                                break;
                            case DrawMode.Curve:
                                if (_curvePoints.Count > 1)
                                {
                                    g.DrawCurve(pen, _curvePoints.ToArray());
                                }
                                break;
                        }
                    }
                }

                // ÍÝÙ ÇáÌÒÁ ÇáãÞØæÚ ÅÐÇ ßÇä ãæÌæÏðÇ
                if (_cropRect.Width > 0 && _cropRect.Height > 0)
                {
                    float ratioX = (float)pictureBox1.Image.Width / pictureBox1.ClientSize.Width;
                    float ratioY = (float)pictureBox1.Image.Height / pictureBox1.ClientSize.Height;

                    Rectangle cropArea = new Rectangle(
                        (int)(_cropRect.X * ratioX),
                        (int)(_cropRect.Y * ratioY),
                        (int)(_cropRect.Width * ratioX),
                        (int)(_cropRect.Height * ratioY));

                    bmp = bmp.Clone(cropArea, bmp.PixelFormat);
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png";
                    saveFileDialog.Title = "Save an Image File";
                    saveFileDialog.ShowDialog();

                    if (saveFileDialog.FileName != "")
                    {
                        using (System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile())
                        {
                            switch (saveFileDialog.FilterIndex)
                            {
                                case 1:
                                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    break;
                                case 2:
                                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                                    break;
                                case 3:
                                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                                    break;
                            }
                        }
                    }
                }
            }
        }



        private void btnRecordAudio_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Wave File|*.wav";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFilePath = saveFileDialog.FileName;
                    StartRecording();
                }
            }
        }

        private void StartRecording()
        {
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.RecordingStopped += WaveIn_RecordingStopped;
            waveIn.StartRecording();
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
        }

        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            writer.Dispose();
            writer = null;

            // ÇáÊÍÞÞ ãä ÚÏã ÞíãÉ null ÞÈá ÇÓÊÏÚÇÁ Dispose()
            if (waveIn != null)
            {
                waveIn.Dispose();
                waveIn = null;
            }

            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;

                if (waveStream != null)
                {
                    waveStream.Close();
                    waveStream.Dispose();
                    waveStream = null;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
            }
            if (writer != null)
            {
                writer.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void btnPlayRecordedAudio_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(outputFilePath) && System.IO.File.Exists(outputFilePath))
            {
                waveOut = new WaveOutEvent();
                using (var waveStream = new WaveFileReader(outputFilePath))
                {
                    waveOut.Init(waveStream);
                    waveOut.Play();
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            else
            {
                MessageBox.Show("No audio file found. Please record audio first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // äÓÎ ÇáÕæÑÉ ÇáÃÕáíÉ
                Bitmap bmp = new Bitmap(pictureBox1.Image);

                // ÑÓã ÇáÔßá ÇáãÍÏÏ Úáì ÇáÕæÑÉ
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        switch (_drawMode)
                        {
                            case DrawMode.Rectangle:
                                g.DrawRectangle(pen, GetRectangle(_startPoint, _endPoint));
                                break;
                            case DrawMode.Ellipse:
                                g.DrawEllipse(pen, GetRectangle(_startPoint, _endPoint));
                                break;
                            case DrawMode.Line:
                                g.DrawLine(pen, _startPoint, _endPoint);
                                break;
                            case DrawMode.Curve:
                                if (_curvePoints.Count > 1)
                                {
                                    g.DrawCurve(pen, _curvePoints.ToArray());
                                }
                                break;
                        }
                    }
                }



                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png";
                    saveFileDialog.Title = "Save an Image File";
                    saveFileDialog.ShowDialog();

                    if (saveFileDialog.FileName != "")
                    {
                        using (System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile())
                        {
                            switch (saveFileDialog.FilterIndex)
                            {
                                case 1:
                                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    break;
                                case 2:
                                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                                    break;
                                case 3:
                                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                                    break;
                            }
                        }
                    }
                }
            }
        }
        private void btnCompressFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourceFile = openFileDialog.FileName;
                    string compressedFile = Path.ChangeExtension(sourceFile, ".zip");

                    CompressFile(sourceFile, compressedFile);
                    MessageBox.Show($"File {sourceFile} has been compressed to {compressedFile}.");
                }
            }
        }

        private void btnCompressFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourceFolder = folderBrowserDialog.SelectedPath;
                    string zipFilePath = Path.Combine(Path.GetDirectoryName(sourceFolder), Path.GetFileName(sourceFolder) + ".zip");

                    ZipFile.CreateFromDirectory(sourceFolder, zipFilePath);
                    MessageBox.Show($"Folder {sourceFolder} has been compressed to {zipFilePath}.");
                }
            }
        }

        private void CompressFile(string sourceFile, string destinationFile)
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (GZipStream compressionStream = new GZipStream(destinationStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                    }
                }
            }
        }
        private void btnShareImageViaEmail_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // حفظ الصورة في MemoryStream بتنسيق JPEG
                    pictureBox1.Image.Save(stream, ImageFormat.Jpeg);

                    // إنشاء رسالة بريد إلكتروني
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("your-email@example.com");
                    mail.To.Add("recipient-email@example.com");
                    mail.Subject = "Subject of the Email";
                    mail.Body = "Body of the Email";

                    // إضافة الصورة كمرفق إلى البريد الإلكتروني
                    stream.Position = 0;
                    mail.Attachments.Add(new Attachment(stream, "image.jpg", "image/jpeg"));

                    // إرسال البريد الإلكتروني
                    SmtpClient smtp = new SmtpClient("smtp.example.com");
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-password");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    // إظهار رسالة تأكيد الإرسال
                    MessageBox.Show("Email sent successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, AutoSize = true };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }
    }
}

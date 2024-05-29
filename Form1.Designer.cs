namespace ImageEditor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnCropAndSave;
        private System.Windows.Forms.Button btnDrawRectangle;
        private System.Windows.Forms.Button btnDrawEllipse;
        private System.Windows.Forms.Button btnDrawLine;
        private System.Windows.Forms.Button btnDrawCurve;
        private System.Windows.Forms.Button btnCompressFile;
        private System.Windows.Forms.Button btnCompressFolder;
        private System.Windows.Forms.Button btnShareImage;
        //private System.Windows.Forms.Button btnCompressAndSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            btnLoadImage = new Button();
            btnCropAndSave = new Button();
            btnDrawRectangle = new Button();
            btnDrawEllipse = new Button();
            btnDrawLine = new Button();
            btnDrawCurve = new Button();
            btnShareImage = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            btnCompressFile = new Button();
            btnCompressFolder = new Button();
            btnCompressAndSave = new Button();
            btnExport = new Button();

            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(634, 561);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(870, 23);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(130, 30);
            btnLoadImage.TabIndex = 1;
            btnLoadImage.Text = "Load Image";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += btnLoadImage_Click;
            // 
            // btnCropAndSave
            // 
            btnCropAndSave.Location = new Point(870, 59);
            btnCropAndSave.Name = "btnCropAndSave";
            btnCropAndSave.Size = new Size(130, 30);
            btnCropAndSave.TabIndex = 2;
            btnCropAndSave.Text = "Crop and Save";
            btnCropAndSave.UseVisualStyleBackColor = true;
            btnCropAndSave.Click += btnCropAndSave_Click;
            // 
            // btnDrawRectangle
            // 
            btnDrawRectangle.Location = new Point(870, 95);
            btnDrawRectangle.Name = "btnDrawRectangle";
            btnDrawRectangle.Size = new Size(130, 30);
            btnDrawRectangle.TabIndex = 3;
            btnDrawRectangle.Text = "Draw Rectangle";
            btnDrawRectangle.UseVisualStyleBackColor = true;
            btnDrawRectangle.Click += btnDrawRectangle_Click;
            // 
            // btnDrawEllipse
            // 
            btnDrawEllipse.Location = new Point(870, 131);
            btnDrawEllipse.Name = "btnDrawEllipse";
            btnDrawEllipse.Size = new Size(130, 30);
            btnDrawEllipse.TabIndex = 4;
            btnDrawEllipse.Text = "Draw Ellipse";
            btnDrawEllipse.UseVisualStyleBackColor = true;
            btnDrawEllipse.Click += btnDrawEllipse_Click;
            // 
            // btnDrawLine
            // 
            btnDrawLine.Location = new Point(870, 167);
            btnDrawLine.Name = "btnDrawLine";
            btnDrawLine.Size = new Size(130, 30);
            btnDrawLine.TabIndex = 5;
            btnDrawLine.Text = "Draw Line";
            btnDrawLine.UseVisualStyleBackColor = true;
            btnDrawLine.Click += btnDrawLine_Click;
            // 
            // btnDrawCurve
            // 
            btnDrawCurve.Location = new Point(870, 203);
            btnDrawCurve.Name = "btnDrawCurve";
            btnDrawCurve.Size = new Size(130, 30);
            btnDrawCurve.TabIndex = 6;
            btnDrawCurve.Text = "Draw Curve";
            btnDrawCurve.UseVisualStyleBackColor = true;
            btnDrawCurve.Click += btnDrawCurve_Click;
            // 
            // button1
            // 
            button1.Location = new Point(870, 239);
            button1.Name = "button1";
            button1.Size = new Size(130, 30);
            button1.TabIndex = 7;
            button1.Text = "Select Area";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSelectArea_Click;
            // 
            // button2
            // 
            button2.Location = new Point(870, 275);
            button2.Name = "button2";
            button2.Size = new Size(130, 30);
            button2.TabIndex = 8;
            button2.Text = "Add Text";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnAddText_Click;
            // 
            // button3
            // 
            button3.Location = new Point(870, 311);
            button3.Name = "button3";
            button3.Size = new Size(130, 30);
            button3.TabIndex = 9;
            button3.Text = "Add Record";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnRecordAudio_Click;
            // 
            // button4
            // 
            button4.Location = new Point(870, 347);
            button4.Name = "button4";
            button4.Size = new Size(130, 30);
            button4.TabIndex = 10;
            button4.Text = "Play Record";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnPlayRecordedAudio_Click;
            // 
            // button5
            // 
            button5.Location = new Point(870, 383);
            button5.Name = "button5";
            button5.Size = new Size(130, 30);
            button5.TabIndex = 11;
            button5.Text = "Stop Record";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnStopRecording_Click;
            // 
            // button6
            // 
            button6.Location = new Point(870, 419);
            button6.Name = "button6";
            button6.Size = new Size(130, 30);
            button6.TabIndex = 12;
            button6.Text = "Save";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // btnCompressFile
            // 
            btnCompressFile.Location = new Point(870, 455);
            btnCompressFile.Name = "btnCompressFile";
            btnCompressFile.Size = new Size(130, 30);
            btnCompressFile.TabIndex = 13;
            btnCompressFile.Text = "Compress File";
            btnCompressFile.UseVisualStyleBackColor = true;
            btnCompressFile.Click += btnCompressFile_Click;
            // 
            // btnCompressFolder
            // 
            btnCompressFolder.Location = new Point(870, 491);
            btnCompressFolder.Name = "btnCompressFolder";
            btnCompressFolder.Size = new Size(130, 30);
            btnCompressFolder.TabIndex = 14;
            btnCompressFolder.Text = "Compress Folder";
            btnCompressFolder.UseVisualStyleBackColor = true;
            btnCompressFolder.Click += btnCompressFolder_Click;
            // 
            // btnCompressAndSave
            // 
            btnCompressAndSave.Location = new Point(870, 530);
            btnCompressAndSave.Name = "btnCompressAndSave";
            btnCompressAndSave.Size = new Size(150, 30);
            btnCompressAndSave.TabIndex = 15;
            btnCompressAndSave.Text = "Compress and Save Image";
            btnCompressAndSave.UseVisualStyleBackColor = true;
            btnCompressAndSave.Click += btnCompressAndSave_Click;
            
            //
            // btnShareImag
            //
            
            btnShareImage.Location = new Point(870, 560);
            btnShareImage.Name = "btnShareImage";
            btnShareImage.TabIndex = 16;
            btnShareImage.Size = new Size(120, 30);
            btnShareImage.Text = "Share Image";
            btnShareImage.UseVisualStyleBackColor = true;
            btnShareImage.Click += btnShareImageViaEmail_Click;

            // 
            // btnExport
            // 
            btnExport.Location = new Point(870, 590);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(100, 30);
            btnExport.TabIndex = 17;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnGenerateReport_Click;

            //
            // Form1
            // 
            ClientSize = new Size(1000, 800);
            Controls.Add(btnCompressAndSave);
            Controls.Add(btnCompressFolder);
            Controls.Add(btnCompressFile);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnDrawCurve);
            Controls.Add(btnDrawLine);
            Controls.Add(btnDrawEllipse);
            Controls.Add(btnDrawRectangle);
            Controls.Add(btnCropAndSave);
            Controls.Add(btnLoadImage);
            Controls.Add(btnShareImage);
            Controls.Add(btnExport);
            Controls.Add(pictureBox1);
            
            Name = "Form1";
            Text = "Image Editor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
        private Button btnExport;
    }
}

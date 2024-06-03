namespace WindowsFormsApp1
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.fourier = new System.Windows.Forms.Button();
            this.search = new System.Windows.Forms.Button();
            this.diagnosis = new System.Windows.Forms.Button();
            this.comparison = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.crop = new System.Windows.Forms.Button();
            this.draw = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.draw);
            this.panel1.Controls.Add(this.crop);
            this.panel1.Controls.Add(this.fourier);
            this.panel1.Controls.Add(this.search);
            this.panel1.Controls.Add(this.diagnosis);
            this.panel1.Controls.Add(this.comparison);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(114, 502);
            this.panel1.TabIndex = 1;
            // 
            // fourier
            // 
            this.fourier.FlatAppearance.BorderSize = 0;
            this.fourier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fourier.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fourier.Location = new System.Drawing.Point(0, 162);
            this.fourier.Name = "fourier";
            this.fourier.Size = new System.Drawing.Size(114, 48);
            this.fourier.TabIndex = 4;
            this.fourier.Text = "Fourier";
            this.fourier.UseVisualStyleBackColor = true;
            this.fourier.Click += new System.EventHandler(this.GoToFourier);
            // 
            // search
            // 
            this.search.FlatAppearance.BorderSize = 0;
            this.search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.search.Location = new System.Drawing.Point(0, 108);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(114, 48);
            this.search.TabIndex = 3;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.GoToSearch);
            // 
            // diagnosis
            // 
            this.diagnosis.FlatAppearance.BorderSize = 0;
            this.diagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.diagnosis.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.diagnosis.Location = new System.Drawing.Point(0, 54);
            this.diagnosis.Name = "diagnosis";
            this.diagnosis.Size = new System.Drawing.Size(114, 48);
            this.diagnosis.TabIndex = 2;
            this.diagnosis.Text = "Diagnosis";
            this.diagnosis.UseVisualStyleBackColor = true;
            this.diagnosis.Click += new System.EventHandler(this.GoToDiagnosis);
            // 
            // comparison
            // 
            this.comparison.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.comparison.FlatAppearance.BorderSize = 0;
            this.comparison.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comparison.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.comparison.Location = new System.Drawing.Point(-3, 0);
            this.comparison.Name = "comparison";
            this.comparison.Size = new System.Drawing.Size(117, 48);
            this.comparison.TabIndex = 1;
            this.comparison.Text = "Comparison";
            this.comparison.UseVisualStyleBackColor = false;
            this.comparison.Click += new System.EventHandler(this.GoToComparison);
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(114, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(864, 502);
            this.panelMain.TabIndex = 2;
            this.panelMain.Click += new System.EventHandler(this.GoToDiagnosis);
            // 
            // crop
            // 
            this.crop.FlatAppearance.BorderSize = 0;
            this.crop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.crop.Location = new System.Drawing.Point(0, 216);
            this.crop.Name = "crop";
            this.crop.Size = new System.Drawing.Size(114, 48);
            this.crop.TabIndex = 5;
            this.crop.Text = "Crop";
            this.crop.UseVisualStyleBackColor = true;
            this.crop.Click += new System.EventHandler(this.GoToCrop);
            // 
            // draw
            // 
            this.draw.FlatAppearance.BorderSize = 0;
            this.draw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.draw.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.draw.Location = new System.Drawing.Point(0, 270);
            this.draw.Name = "draw";
            this.draw.Size = new System.Drawing.Size(114, 48);
            this.draw.TabIndex = 6;
            this.draw.Text = "Draw Shapes";
            this.draw.UseVisualStyleBackColor = true;
            this.draw.Click += new System.EventHandler(this.GoToDraw);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 502);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Main";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button diagnosis;
        private System.Windows.Forms.Button comparison;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Button fourier;
        private System.Windows.Forms.Button crop;
        private System.Windows.Forms.Button draw;
    }
}
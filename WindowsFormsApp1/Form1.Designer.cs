namespace WindowsFormsApp1
{
    partial class Comparison
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.image1 = new System.Windows.Forms.PictureBox();
            this.image2 = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.image1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image2)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(163, 412);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(167, 35);
            this.button3.TabIndex = 1;
            this.button3.Text = "Select an image";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.SelectImage1);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button4.Location = new System.Drawing.Point(684, 412);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(167, 35);
            this.button4.TabIndex = 2;
            this.button4.Text = "Select an image";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SelectImage2);
            // 
            // image1
            // 
            this.image1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image1.Location = new System.Drawing.Point(107, 29);
            this.image1.Name = "image1";
            this.image1.Size = new System.Drawing.Size(279, 377);
            this.image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image1.TabIndex = 3;
            this.image1.TabStop = false;
            // 
            // image2
            // 
            this.image2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.image2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image2.Location = new System.Drawing.Point(619, 29);
            this.image2.Name = "image2";
            this.image2.Size = new System.Drawing.Size(279, 377);
            this.image2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image2.TabIndex = 4;
            this.image2.TabStop = false;
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button5.Location = new System.Drawing.Point(426, 412);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(167, 35);
            this.button5.TabIndex = 5;
            this.button5.Text = "Compare";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Compare);
            // 
            // result
            // 
            this.result.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.result.AutoSize = true;
            this.result.Location = new System.Drawing.Point(423, 180);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(0, 13);
            this.result.TabIndex = 6;
            // 
            // Comparison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 478);
            this.Controls.Add(this.result);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.image2);
            this.Controls.Add(this.image1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Comparison";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.image1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox image1;
        private System.Windows.Forms.PictureBox image2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label result;
    }
}


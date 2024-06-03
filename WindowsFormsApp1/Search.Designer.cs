namespace WindowsFormsApp1
{
    partial class Search
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
            this.button5 = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Label();
            this.filesTable = new System.Windows.Forms.DataGridView();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.last_modified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.sizeInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.warning = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.filesTable)).BeginInit();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button5.Location = new System.Drawing.Point(661, 71);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(167, 35);
            this.button5.TabIndex = 7;
            this.button5.Text = "Choose a Folder";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ChooseFolder);
            // 
            // result
            // 
            this.result.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.result.AutoSize = true;
            this.result.Location = new System.Drawing.Point(242, 82);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(91, 13);
            this.result.TabIndex = 8;
            this.result.Text = "Selected folder is:";
            // 
            // filesTable
            // 
            this.filesTable.AllowUserToAddRows = false;
            this.filesTable.AllowUserToDeleteRows = false;
            this.filesTable.AllowUserToResizeColumns = false;
            this.filesTable.AllowUserToResizeRows = false;
            this.filesTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.filesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.path,
            this.last_modified,
            this.size});
            this.filesTable.Location = new System.Drawing.Point(245, 231);
            this.filesTable.Name = "filesTable";
            this.filesTable.ReadOnly = true;
            this.filesTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.filesTable.Size = new System.Drawing.Size(583, 246);
            this.filesTable.TabIndex = 9;
            // 
            // path
            // 
            this.path.HeaderText = "File name";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.path.Width = 250;
            // 
            // last_modified
            // 
            this.last_modified.HeaderText = "Last modified";
            this.last_modified.Name = "last_modified";
            this.last_modified.ReadOnly = true;
            this.last_modified.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.last_modified.Width = 150;
            // 
            // size
            // 
            this.size.HeaderText = "Size (KB)";
            this.size.Name = "size";
            this.size.ReadOnly = true;
            this.size.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.size.Width = 140;
            // 
            // datePicker
            // 
            this.datePicker.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datePicker.CustomFormat = "";
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePicker.Location = new System.Drawing.Point(245, 129);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(200, 20);
            this.datePicker.TabIndex = 10;
            this.datePicker.Value = new System.DateTime(2024, 6, 3, 14, 13, 24, 0);
            // 
            // sizeInput
            // 
            this.sizeInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sizeInput.Location = new System.Drawing.Point(245, 160);
            this.sizeInput.Name = "sizeInput";
            this.sizeInput.Size = new System.Drawing.Size(200, 20);
            this.sizeInput.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(661, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 25);
            this.button1.TabIndex = 12;
            this.button1.Text = "Filter by date";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.FilterByDate);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Location = new System.Drawing.Point(661, 155);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 25);
            this.button2.TabIndex = 13;
            this.button2.Text = "Filter by size";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.FilterBySize);
            // 
            // warning
            // 
            this.warning.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.warning.AutoSize = true;
            this.warning.ForeColor = System.Drawing.Color.Red;
            this.warning.Location = new System.Drawing.Point(462, 149);
            this.warning.Name = "warning";
            this.warning.Size = new System.Drawing.Size(0, 13);
            this.warning.TabIndex = 14;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(661, 186);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(167, 25);
            this.button3.TabIndex = 15;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ResetTable);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 540);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.warning);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sizeInput);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.filesTable);
            this.Controls.Add(this.result);
            this.Controls.Add(this.button5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Search";
            this.Text = "Search";
            ((System.ComponentModel.ISupportInitialize)(this.filesTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.DataGridView filesTable;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.TextBox sizeInput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label warning;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.DataGridViewTextBoxColumn last_modified;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
    }
}
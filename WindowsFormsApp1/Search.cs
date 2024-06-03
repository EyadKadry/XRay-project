using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Search : Form
    {
        private List<string> imageFiles = new List<string>();
        private List<DataGridViewRow> rows = new List<DataGridViewRow>();
        private string selectedFolder = "";
        public Search()
        {
            InitializeComponent();
        }

        private void ChooseFolder(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            DialogResult folderDialogResult = folderDialog.ShowDialog();

            if (folderDialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                string folderPath = folderDialog.SelectedPath;
                result.Text = "Selected Folder is: " + folderPath;
                selectedFolder = folderPath;

                var patterns = new string[] { "*.jpg", "*.jpeg", "*.png" };
                imageFiles.Clear();
                foreach (var pattern in patterns)
                {
                    var temp = Directory.GetFiles(folderPath, pattern);
                    imageFiles.AddRange(temp);
                }

                filesTable.Rows.Clear();

                foreach (string filePath in imageFiles)
                {
                    string fileName = Path.GetFileName(filePath);
                    DateTime lastModified = File.GetLastWriteTime(filePath);
                    long fileSizeBytes = new FileInfo(filePath).Length;
                    double fileSizeKB = fileSizeBytes / (1024.0);

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(filesTable, fileName, lastModified, fileSizeKB.ToString("0.##"));
                    rows.Add(row);
                    filesTable.Rows.Add(row);
                }
            }
        }

        private void FilterBySize(object sender, EventArgs e)
        {
            warning.ForeColor = Color.Red;
            var enteredSize = sizeInput.Text;
            if (selectedFolder == "")
            {
                warning.Text = "Choose a folder first";
                return;
            }
            if (enteredSize == "")
            {
                warning.Text = "Enter a size first!";
                return;
            }

            if (!Double.TryParse(enteredSize, out Double inputSize))
            {
                warning.Text = "The size must be a nubmer!";
                return;
            }

            var parsedEnteredSize = Double.Parse(enteredSize);

            if (parsedEnteredSize <= 0)
            {
                warning.Text = "size must be larger than 0!";
                return;
            }

            warning.Text = "";
            filesTable.Rows.Clear();

            foreach (string filePath in imageFiles)
            {
                string fileName = Path.GetFileName(filePath);
                DateTime lastModified = File.GetLastWriteTime(filePath);
                long fileSizeBytes = new FileInfo(filePath).Length;
                double fileSizeKB = Math.Round(fileSizeBytes / (1024.0), 2);

                if (fileSizeKB == parsedEnteredSize)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(filesTable, fileName, lastModified, fileSizeKB);
                    filesTable.Rows.Add(row);
                }
            }

            if (filesTable.Rows.Count == 0)
            {
                warning.Text = "There is no result in this folder";
                warning.ForeColor = Color.Gray;
            }

        }

        private void FilterByDate(object sender, EventArgs e)
        {
            warning.ForeColor = Color.Red;
            var selectedDate = datePicker.Value;
            if (selectedFolder == "")
            {
                warning.Text = "Choose a folder first";
                return;
            }

            warning.Text = "";
            filesTable.Rows.Clear();

            foreach (string filePath in imageFiles)
            {
                string fileName = Path.GetFileName(filePath);
                DateTime lastModified = File.GetLastWriteTime(filePath);
                long fileSizeBytes = new FileInfo(filePath).Length;
                double fileSizeKB = Math.Round(fileSizeBytes / (1024.0), 2);
                if (lastModified.Date == selectedDate.Date)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(filesTable, fileName, lastModified, fileSizeKB);
                    filesTable.Rows.Add(row);
                }
            }

            if (filesTable.Rows.Count == 0)
            {
                warning.Text = "There is no result in this folder";
                warning.ForeColor = Color.Gray;
            }

        }

        private void ResetTable(object sender, EventArgs e)
        {
            if (selectedFolder == "")
            {
                warning.ForeColor = Color.Red;
                warning.Text = "Choose a folder first";
                return;
            }

            warning.Text = "";

            sizeInput.Text = "";
            datePicker.Value = DateTime.Now;
            filesTable.Rows.Clear();
            filesTable.Rows.AddRange(rows.ToArray());
        }
    }
}

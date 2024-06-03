using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        Button selectedButton = new Button();
        private Form currentChildForm;
        public Main()
        {
            InitializeComponent();
            OpenChildForm(new Comparison());
            HighlightButton(comparison);
        }

        private void HighlightButton(Button button)
        {
            selectedButton.BackColor = SystemColors.ActiveCaptionText;
            selectedButton = button;
            selectedButton.BackColor = SystemColors.ControlDarkDark;
        }

        private void GoToComparison(object sender, EventArgs e)
        {
            OpenChildForm(new Comparison());
            HighlightButton(comparison);
        }

        private void GoToDiagnosis(object sender, EventArgs e)
        {
            OpenChildForm(new Diagnosis());
            HighlightButton(diagnosis);
        }

        private void GoToSearch(object sender, EventArgs e)
        {
            OpenChildForm(new Search());
            HighlightButton(search);
        }

        private void GoToFourier(object sender, EventArgs e)
        {
            OpenChildForm(new Fourier());
            HighlightButton(fourier);
        }

        private void GoToCrop(object sender, EventArgs e)
        {
            OpenChildForm(new Crop());
            HighlightButton(crop);
        }

        private void GoToDraw(object sender, EventArgs e)
        {
            OpenChildForm(new Draw());
            HighlightButton(draw);
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.Show();
        }
    }
}

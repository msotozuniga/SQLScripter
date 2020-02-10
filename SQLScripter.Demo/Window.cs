using System;
using System.IO;
using System.Windows.Forms;
using SQLScripter;

namespace SQLScripter.Demo

{
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileNameLabel.Text = Path.GetFileName(fileDialog.FileName);
            }

        }

        private void pathButton_Click(object sender, EventArgs e)
        {
            if(pathDialog.ShowDialog() == DialogResult.OK)
            {
                zipPathLabel.Text = pathDialog.SelectedPath;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if(zipPathLabel.Text.Equals("Sin selección") || fileNameLabel.Text.Equals("Sin selección"))

            {
                MessageBox.Show("Falta seleccionar ubicación y/o archivo");
                return;
            }
            SQLScripter.StandardProcess.Process(fileDialog.FileName,pathDialog.SelectedPath,srvBox.Text,userBox.Text,pswBox.Text,alterBox.Checked);
            MessageBox.Show("Terminado");
            srvBox.ResetText();
            userBox.ResetText();
            pswBox.ResetText();
            fileNameLabel.Text = "Sin selección";
            zipPathLabel.Text = "Sin selección";
            fileDialog.Reset();
            pathDialog.Reset();
        }

    }
}

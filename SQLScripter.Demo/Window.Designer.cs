using System;

namespace SQLScripter.Demo
{
    partial class Window
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileButton = new System.Windows.Forms.Button();
            this.srvBox = new System.Windows.Forms.TextBox();
            this.userBox = new System.Windows.Forms.TextBox();
            this.pswBox = new System.Windows.Forms.TextBox();
            this.srvLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.pswLabel = new System.Windows.Forms.Label();
            this.fileLabel = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.pathButton = new System.Windows.Forms.Button();
            this.pathLabel = new System.Windows.Forms.Label();
            this.zipPathLabel = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.startButton = new System.Windows.Forms.Button();
            this.alterBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            //
            // alterBox
            //
            this.alterBox.Location = new System.Drawing.Point(32, 208);
            this.alterBox.Name = "alterBox";
            this.alterBox.Size = new System.Drawing.Size(75, 23);
            this.alterBox.Text = "Alter";

            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(160, 137);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(75, 23);
            this.fileButton.TabIndex = 0;
            this.fileButton.Text = "Examinar";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.fileButton_Click);
            // 
            // srvBox
            // 
            this.srvBox.Location = new System.Drawing.Point(158, 24);
            this.srvBox.Name = "srvBox";
            this.srvBox.Size = new System.Drawing.Size(318, 20);
            this.srvBox.TabIndex = 1;
            // 
            // userBox
            // 
            this.userBox.Location = new System.Drawing.Point(158, 61);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(318, 20);
            this.userBox.TabIndex = 2;
            // 
            // pswBox
            // 
            this.pswBox.Location = new System.Drawing.Point(158, 98);
            this.pswBox.Name = "pswBox";
            this.pswBox.Size = new System.Drawing.Size(318, 20);
            this.pswBox.TabIndex = 3;
            // 
            // srvLabel
            // 
            this.srvLabel.AutoSize = true;
            this.srvLabel.Location = new System.Drawing.Point(32, 27);
            this.srvLabel.Name = "srvLabel";
            this.srvLabel.Size = new System.Drawing.Size(100, 13);
            this.srvLabel.TabIndex = 4;
            this.srvLabel.Text = "Servidor e instancia";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(35, 61);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(43, 13);
            this.userLabel.TabIndex = 5;
            this.userLabel.Text = "Usuario";
            // 
            // pswLabel
            // 
            this.pswLabel.AutoSize = true;
            this.pswLabel.Location = new System.Drawing.Point(32, 101);
            this.pswLabel.Name = "pswLabel";
            this.pswLabel.Size = new System.Drawing.Size(61, 13);
            this.pswLabel.TabIndex = 6;
            this.pswLabel.Text = "Contraseña";
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(32, 142);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(122, 13);
            this.fileLabel.TabIndex = 7;
            this.fileLabel.Text = "Archivo con entregables";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(254, 142);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(70, 13);
            this.fileNameLabel.TabIndex = 8;
            this.fileNameLabel.Text = "Sin selección";
            // 
            // pathButton
            // 
            this.pathButton.Location = new System.Drawing.Point(160, 172);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(75, 23);
            this.pathButton.TabIndex = 9;
            this.pathButton.Text = "Examinar";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(32, 177);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(107, 13);
            this.pathLabel.TabIndex = 10;
            this.pathLabel.Text = "Ubicación de destino";
            // 
            // zipPathLabel
            // 
            this.zipPathLabel.AutoSize = true;
            this.zipPathLabel.Location = new System.Drawing.Point(254, 177);
            this.zipPathLabel.Name = "zipPathLabel";
            this.zipPathLabel.Size = new System.Drawing.Size(70, 13);
            this.zipPathLabel.TabIndex = 11;
            this.zipPathLabel.Text = "Sin selección";
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "fileDialog";
            this.fileDialog.Filter = "Excel (*.xlsx,*.xls)|";
            this.fileDialog.InitialDirectory = " @\"C:/\"";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(401, 208);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Comenzar";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // TempScripter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 252);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.zipPathLabel);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.pathButton);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.pswLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.srvLabel);
            this.Controls.Add(this.pswBox);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.srvBox);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.alterBox);
            this.Name = "TempScripter";
            this.Text = "TempScripter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.TextBox srvBox;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.TextBox pswBox;
        private System.Windows.Forms.Label srvLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label pswLabel;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label zipPathLabel;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.FolderBrowserDialog pathDialog;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox alterBox;
    }
}

